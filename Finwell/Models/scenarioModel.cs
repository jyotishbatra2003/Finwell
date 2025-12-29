using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinwellLibrary.Models
{
    public class scenarioModel
    {
        public int ScenarioId { get; set; }

        [Required]
        public string ScenarioName { get; set; }

        // Your actual database columns
        public decimal InitialDebt { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal LivingExpenses { get; set; }
        public string DeptType { get; set; }
        public decimal InterestRate { get; set; }

        // Monthly Expenses - if these are 0 or NULL, calculate from LivingExpenses
        public decimal RentExpense { get; set; }
        public decimal InsuranceExpense { get; set; }
        public decimal FoodExpense { get; set; }
        public decimal UtilitiesExpense { get; set; }

        // Interest Rates
        public decimal StudentLoanInterestRate { get; set; }
        public decimal CreditCardInterestRate { get; set; }
        public decimal MortgageInterestRate { get; set; }

        // Investment Parameters
        public decimal ExpectedInvestmentReturn { get; set; }

        // Game Settings
        public int MaxMonths { get; set; }
        public decimal TargetNetWorth { get; set; }

        // Calculated Properties
        [NotMapped]
        public decimal ActualRentExpense
        {
            get
            {
                if (RentExpense > 0) return RentExpense;
                // If individual expenses not set, split LivingExpenses proportionally
                return LivingExpenses * 0.60m; // 60% for rent
            }
        }

        [NotMapped]
        public decimal ActualInsuranceExpense
        {
            get
            {
                if (InsuranceExpense > 0) return InsuranceExpense;
                return LivingExpenses * 0.10m; // 10% for insurance
            }
        }

        [NotMapped]
        public decimal ActualFoodExpense
        {
            get
            {
                if (FoodExpense > 0) return FoodExpense;
                return LivingExpenses * 0.20m; // 20% for food
            }
        }

        [NotMapped]
        public decimal ActualUtilitiesExpense
        {
            get
            {
                if (UtilitiesExpense > 0) return UtilitiesExpense;
                return LivingExpenses * 0.10m; // 10% for utilities
            }
        }

        [NotMapped]
        public decimal StudentLoanBalance
        {
            get
            {
                if (DeptType == "Student Loan") return InitialDebt;
                if (DeptType == "Mixed Debt") return InitialDebt * 0.5m;
                return 0;
            }
        }

        [NotMapped]
        public decimal CreditCardBalance
        {
            get
            {
                if (DeptType == "Credit Card") return InitialDebt;
                if (DeptType == "Medical") return InitialDebt;
                if (DeptType == "Mixed Debt") return InitialDebt * 0.5m;
                return 0;
            }
        }

        [NotMapped]
        public decimal MortgageBalance
        {
            get
            {
                if (DeptType == "Mortgage") return InitialDebt;
                return 0;
            }
        }

        [NotMapped]
        public string Description => $"{ScenarioName} - {DeptType}";

        [NotMapped]
        public string DifficultyLevel
        {
            get
            {
                decimal debtToIncomeRatio = MonthlyIncome > 0 ? InitialDebt / (MonthlyIncome * 12) : 0;
                if (debtToIncomeRatio < 1) return "Easy";
                if (debtToIncomeRatio < 2) return "Medium";
                return "Hard";
            }
        }

        [NotMapped]
        public decimal TotalDebt => InitialDebt;

        [NotMapped]
        public decimal TotalMonthlyExpenses => ActualRentExpense + ActualInsuranceExpense + ActualFoodExpense + ActualUtilitiesExpense;

        [NotMapped]
        public decimal MonthlyDisposableIncome => MonthlyIncome - TotalMonthlyExpenses;
    }
}