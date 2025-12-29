using FinwellLibrary.DataAccess;
using FinwellLibrary.Models;
using System;

namespace FinwellLibrary.BusinessLogic
{
    public class MonthlySimulationManager
    {
        private readonly IDataConnection _dataConnection;
        private readonly scenarioModel _scenario;
        private int _currentSimulationId;

        public int CurrentMonth { get; private set; }
        public decimal CurrentDebtBalance { get; private set; }
        public decimal CurrentInvestmentValue { get; private set; }
        public decimal CurrentSavings { get; private set; }

        public MonthlySimulationManager(IDataConnection dataConnection, scenarioModel scenario, int simulationId)
        {
            _dataConnection = dataConnection;
            _scenario = scenario;
            _currentSimulationId = simulationId;

            // Initialize from scenario
            CurrentMonth = 1;
            CurrentDebtBalance = scenario.InitialDebt;
            CurrentInvestmentValue = 0;
            CurrentSavings = 0;

            // Check if there's existing progress
            var latestDecision = _dataConnection.GetLatestDecision(simulationId);
            if (latestDecision != null)
            {
                CurrentMonth = latestDecision.MonthNumber + 1;
                CurrentDebtBalance = latestDecision.DebtBalanceAfter;
                CurrentInvestmentValue = latestDecision.InvestmentValueAfter;
                CurrentSavings = latestDecision.RemainingBalance;
            }
        }

        public decimal CalculateInvestmentProfit()
        {
            if (CurrentMonth == 1 || CurrentInvestmentValue == 0)
            {
                return 0;
            }

            decimal annualReturn = _scenario.ExpectedInvestmentReturn > 0 ? _scenario.ExpectedInvestmentReturn : 0.07m;
            decimal monthlyReturnRate = annualReturn / 12;
            return CurrentInvestmentValue * monthlyReturnRate;
        }

        public decimal CalculateTotalFunds()
        {
            decimal income = _scenario.MonthlyIncome;
            decimal investmentProfit = CalculateInvestmentProfit();
            decimal savingsFromPrevious = CurrentSavings;

            return income + investmentProfit + savingsFromPrevious;
        }

        public decimal CalculateLivingExpenses()
        {
            return _scenario.ActualRentExpense +
                   _scenario.ActualInsuranceExpense +
                   _scenario.ActualFoodExpense +
                   _scenario.ActualUtilitiesExpense;
        }

        private decimal GetInterestRate()
        {
            if (_scenario.InterestRate > 0)
                return _scenario.InterestRate / 100;

            switch (_scenario.DeptType)
            {
                case "Student Loan":
                    return _scenario.StudentLoanInterestRate > 0 ? _scenario.StudentLoanInterestRate : 0.045m;
                case "Credit Card":
                case "Medical":
                    return _scenario.CreditCardInterestRate > 0 ? _scenario.CreditCardInterestRate : 0.18m;
                case "Mortgage":
                    return _scenario.MortgageInterestRate > 0 ? _scenario.MortgageInterestRate : 0.035m;
                case "Mixed Debt":
                    decimal studentRate = _scenario.StudentLoanInterestRate > 0 ? _scenario.StudentLoanInterestRate : 0.045m;
                    decimal creditRate = _scenario.CreditCardInterestRate > 0 ? _scenario.CreditCardInterestRate : 0.18m;
                    return (studentRate + creditRate) / 2;
                default:
                    return 0.08m;
            }
        }

        public MonthlyDecisionResult ProcessMonthlyDecision(
            decimal studentDebtPayment,
            decimal creditDebtPayment,
            decimal investmentAmount,
            decimal savingsAmount,
            RandomEventModel randomEvent = null)
        {
            var result = new MonthlyDecisionResult();

            decimal totalFunds = CalculateTotalFunds();
            decimal livingExpenses = CalculateLivingExpenses();
            decimal totalDebtPayment = studentDebtPayment + creditDebtPayment;
            decimal totalAllocated = livingExpenses + totalDebtPayment + investmentAmount + savingsAmount;

            if (randomEvent != null)
            {
                totalFunds += randomEvent.ImpactAmount;
                result.RandomEvent = randomEvent;
            }

            if (totalAllocated > totalFunds)
            {
                result.IsValid = false;
                result.ErrorMessage = $"Total allocation (${totalAllocated:N2}) exceeds available funds (${totalFunds:N2})";
                return result;
            }

            decimal annualRate = GetInterestRate();
            decimal monthlyRate = annualRate / 12;
            decimal interestAccrued = CurrentDebtBalance * monthlyRate;

            decimal newDebtBalance = Math.Max(0, CurrentDebtBalance + interestAccrued - totalDebtPayment);

            decimal investmentGrowth = CalculateInvestmentProfit();
            decimal newInvestmentValue = CurrentInvestmentValue + investmentAmount + investmentGrowth;

            decimal netWorth = newInvestmentValue + savingsAmount - newDebtBalance;

            var decision = new montlyDecisionModel
            {
                SimulationId = _currentSimulationId,
                MonthNumber = CurrentMonth,
                DebtPayment = totalDebtPayment,
                InvestmentAmount = investmentAmount,
                RemainingBalance = savingsAmount,
                LivingExpensesPaid = livingExpenses,
                InterestAccrued = interestAccrued,
                DebtBalanceAfter = newDebtBalance,
                InvestmentValueAfter = newInvestmentValue,
                NetWorth = netWorth
            };

            int decisionId = _dataConnection.CreateMonthlyDecision(decision);

            CurrentMonth++;
            CurrentDebtBalance = newDebtBalance;
            CurrentInvestmentValue = newInvestmentValue;
            CurrentSavings = savingsAmount;

            result.IsValid = true;
            result.Decision = decision;
            result.IsDebtFree = newDebtBalance <= 0;
            result.MonthsElapsed = CurrentMonth - 1;

            return result;
        }

        public MonthlyStateViewModel GetCurrentState()
        {
            return new MonthlyStateViewModel
            {
                MonthNumber = CurrentMonth,
                Income = _scenario.MonthlyIncome,
                InvestmentProfit = CalculateInvestmentProfit(),
                SavingsFromPrevious = CurrentSavings,
                TotalFunds = CalculateTotalFunds(),
                RentExpense = _scenario.ActualRentExpense,
                InsuranceExpense = _scenario.ActualInsuranceExpense,
                FoodExpense = _scenario.ActualFoodExpense,
                UtilitiesExpense = _scenario.ActualUtilitiesExpense,
                TotalLivingExpenses = CalculateLivingExpenses(),
                CurrentDebtBalance = CurrentDebtBalance,
                CurrentInvestmentValue = CurrentInvestmentValue,
                CurrentSavings = CurrentSavings
            };
        }
    }

    public class MonthlyDecisionResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        public montlyDecisionModel Decision { get; set; }
        public bool IsDebtFree { get; set; }
        public int MonthsElapsed { get; set; }
        public RandomEventModel RandomEvent { get; set; }
    }

    public class MonthlyStateViewModel
    {
        public int MonthNumber { get; set; }
        public decimal Income { get; set; }
        public decimal InvestmentProfit { get; set; }
        public decimal SavingsFromPrevious { get; set; }
        public decimal TotalFunds { get; set; }
        public decimal RentExpense { get; set; }
        public decimal InsuranceExpense { get; set; }
        public decimal FoodExpense { get; set; }
        public decimal UtilitiesExpense { get; set; }
        public decimal TotalLivingExpenses { get; set; }
        public decimal CurrentDebtBalance { get; set; }
        public decimal CurrentInvestmentValue { get; set; }
        public decimal CurrentSavings { get; set; }
    }

    public class RandomEventModel
    {
        public string EventDescription { get; set; }
        public decimal ImpactAmount { get; set; }
        public bool IsPositive => ImpactAmount > 0;
    }
}