using FinwellLibrary;
using FinwellLibrary.BusinessLogic;
using FinwellLibrary.DataAccess;
using FinwellLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinwellUI
{
    public partial class montlyIteration : Form
    {
        private scenarioModel _selectedScenario;
        private MonthlySimulationManager _simulationManager;
        private int _currentSimulationId;
        private RandomEventModel _currentRandomEvent;

        public montlyIteration(scenarioModel selectedScenario, int simulationId)
        {
            InitializeComponent();

            _selectedScenario = selectedScenario;
            _currentSimulationId = simulationId;

            // Initialize the simulation manager
            _simulationManager = new MonthlySimulationManager(
                GlobalConfig.Connection,
                selectedScenario,
                simulationId
            );
        }

        private void montlyIteration_Load(object sender, EventArgs e)
        {
            LoadMonthlyState();
            CheckForRandomEvent();
        }

        /// <summary>
        /// Loads the current month's state and populates the UI
        /// </summary>
        private void LoadMonthlyState()
        {
            var state = _simulationManager.GetCurrentState();

            // Update Monthly Simulation Section (Left Side)
            incomeValue.Text = $"${state.Income:N2}";
            textBox1.Text = $"${state.InvestmentProfit:N2}";
            savingsValue.Text = $"${state.SavingsFromPrevious:N2}";
            totalValueHolder.Text = $"${state.TotalFunds:N2}";

            // Update Living Expenses (Fixed values)
            rentValueHolder.Text = $"${state.RentExpense:N2}";
            textBox5.Text = $"${state.InsuranceExpense:N2}";
            foodValueHolder.Text = $"${state.FoodExpense:N2}";
            utilitiesValueHolder.Text = $"${state.UtilitiesExpense:N2}";

            // Update Distribution Section (Right Side) - Show current balances
            studentDebtRemainingValueHolder.Text = $"${state.CurrentDebtBalance:N2}";
            creditDebtRemainingValueHolder.Text = $"${state.CurrentDebtBalance:N2}"; // Split this if tracking separately

            // Update month display
            monthLabel.Text = $"Month {state.MonthNumber}";

            // Clear user input fields for new month
            studentDebtValueHolder.Clear();
            creditDebtValueHolder.Clear();
            textBox4.Clear();
            savingsValueHolder2.Clear();

            // Set focus to first input
            studentDebtValueHolder.Focus();
        }

        /// <summary>
        /// Checks if a random event should occur this month
        /// </summary>
        private void CheckForRandomEvent()
        {
            // 25% chance of random event occurring
            _currentRandomEvent = RandomEventGenerator.GenerateEvent(0.25);

            if (_currentRandomEvent != null)
            {
                // Format the event description with the amount
                string amountDisplay = _currentRandomEvent.IsPositive
                    ? $"+${Math.Abs(_currentRandomEvent.ImpactAmount):N2}"
                    : $"-${Math.Abs(_currentRandomEvent.ImpactAmount):N2}";

                // Display random event WITH amount
                randomEventVariable.Text = $"{_currentRandomEvent.EventDescription} ({amountDisplay})";
                randomEventVariable.ForeColor = _currentRandomEvent.IsPositive ? Color.Green : Color.Red;
                randomEventVariable.Visible = true;

                // Update total funds to reflect the event
                var state = _simulationManager.GetCurrentState();
                decimal adjustedTotal = state.TotalFunds + _currentRandomEvent.ImpactAmount;
                totalValueHolder.Text = $"${adjustedTotal:N2}";

                // Show message box for dramatic effect
                string eventType = _currentRandomEvent.IsPositive ? "Good News!" : "Unexpected Expense!";
                string impact = _currentRandomEvent.IsPositive
                    ? $"+${Math.Abs(_currentRandomEvent.ImpactAmount):N2}"
                    : $"-${Math.Abs(_currentRandomEvent.ImpactAmount):N2}";

                MessageBox.Show(
                    $"{_currentRandomEvent.EventDescription}\n\nImpact: {impact}",
                    eventType,
                    MessageBoxButtons.OK,
                    _currentRandomEvent.IsPositive ? MessageBoxIcon.Information : MessageBoxIcon.Warning
                );
            }
            else
            {
                randomEventVariable.Visible = false;
            }
        }

        /// <summary>
        /// Handles the Next Month button click
        /// </summary>
        private void nextButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse user inputs
                if (!decimal.TryParse(studentDebtValueHolder.Text, out decimal studentDebtPayment))
                {
                    MessageBox.Show("Please enter a valid amount for Student Debt Payment.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    studentDebtValueHolder.Focus();
                    return;
                }

                if (!decimal.TryParse(creditDebtValueHolder.Text, out decimal creditDebtPayment))
                {
                    MessageBox.Show("Please enter a valid amount for Credit Debt Payment.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    creditDebtValueHolder.Focus();
                    return;
                }

                if (!decimal.TryParse(textBox4.Text, out decimal investmentAmount))
                {
                    MessageBox.Show("Please enter a valid amount for Investment.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox4.Focus();
                    return;
                }

                if (!decimal.TryParse(savingsValueHolder2.Text, out decimal savingsAmount))
                {
                    MessageBox.Show("Please enter a valid amount for Savings.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    savingsValueHolder2.Focus();
                    return;
                }

                // Validate non-negative values
                if (studentDebtPayment < 0 || creditDebtPayment < 0 || investmentAmount < 0 || savingsAmount < 0)
                {
                    MessageBox.Show("All amounts must be non-negative.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Process the monthly decision
                var result = _simulationManager.ProcessMonthlyDecision(
                    studentDebtPayment,
                    creditDebtPayment,
                    investmentAmount,
                    savingsAmount,
                    _currentRandomEvent
                );

                if (!result.IsValid)
                {
                    MessageBox.Show(result.ErrorMessage, "Allocation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // SAVE PROGRESS: Update simulation status after successful month
                GlobalConfig.Connection.UpdateSimulationStatus(
                    _currentSimulationId,
                    result.IsDebtFree ? "Completed" : "In Progress",
                    result.MonthsElapsed
                );

                // Check if debt-free
                if (result.IsDebtFree)
                {
                    // Calculate totals from all decisions
                    var allDecisions = GlobalConfig.Connection.GetAllDecisions(_currentSimulationId);

                    decimal totalDebtPaid = allDecisions.Sum(d => d.DebtPayment);
                    decimal totalInterestPaid = allDecisions.Sum(d => d.InterestAccrued);
                    decimal totalInvestmentContributions = allDecisions.Sum(d => d.InvestmentAmount);
                    decimal totalInvestmentGains = result.Decision.InvestmentValueAfter - totalInvestmentContributions;

                    // Calculate efficiency score
                    // Speed Efficiency (40%): How quickly compared to optimal time
                    decimal optimalMonths = _selectedScenario.InitialDebt /
                        Math.Max(1, _selectedScenario.MonthlyIncome - _selectedScenario.LivingExpenses);
                    decimal monthsEfficiency = Math.Max(0, 1 - ((result.MonthsElapsed - optimalMonths) / Math.Max(1, optimalMonths)));

                    // Interest Efficiency (30%): How little interest paid relative to debt
                    decimal interestEfficiency = Math.Max(0, 1 - (totalInterestPaid / Math.Max(1, _selectedScenario.InitialDebt)));

                    // Investment Efficiency (30%): Investment gains relative to potential (10% of income)
                    decimal expectedInvestmentBase = _selectedScenario.MonthlyIncome * result.MonthsElapsed * 0.1m;
                    decimal investmentEfficiency = Math.Min(1, totalInvestmentGains / Math.Max(1, expectedInvestmentBase));

                    // Final weighted score
                    decimal efficiencyScore = (monthsEfficiency * 0.4m) + (interestEfficiency * 0.3m) + (investmentEfficiency * 0.3m);
                    efficiencyScore = Math.Max(0, Math.Min(1, efficiencyScore)); // Clamp between 0 and 1

                    // Create result record
                    simulationResultModel simResult = new simulationResultModel
                    {
                        SimulationId = _currentSimulationId,
                        TotalMonths = result.MonthsElapsed,
                        FinalNetWorth = result.Decision.NetWorth,
                        TotalInterestPaid = totalInterestPaid,
                        TotalInvestmentGains = totalInvestmentGains,
                        TotalDebtPaid = totalDebtPaid,
                        EfficiencyScore = efficiencyScore,
                        UserId = GlobalConfig.CurrentUser?.UserId ?? 1
                    };

                    GlobalConfig.Connection.CreateSimulationResult(simResult);

                    MessageBox.Show(
                        $"🎉 Congratulations! You are DEBT FREE! 🎉\n\n" +
                        $"📅 Months to Freedom: {result.MonthsElapsed}\n" +
                        $"💰 Total Debt Paid: ${totalDebtPaid:N2}\n" +
                        $"💸 Total Interest Paid: ${totalInterestPaid:N2}\n" +
                        $"📈 Final Net Worth: ${result.Decision.NetWorth:N2}\n" +
                        $"📊 Investment Gains: ${totalInvestmentGains:N2}\n" +
                        $"⭐ Efficiency Score: {efficiencyScore:P0} ({simResult.EfficiencyRating})",
                        "Financial Freedom Achieved!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Navigate to results screen
                    ResultPage resultsForm = new ResultPage(_currentSimulationId);
                    resultsForm.Show();
                    this.Close();
                    return;
                }

                // Success - move to next month
                MessageBox.Show(
                    $"Month {result.MonthsElapsed} completed!\n\n" +
                    $"Remaining Debt: ${result.Decision.DebtBalanceAfter:N2}\n" +
                    $"Investment Value: ${result.Decision.InvestmentValueAfter:N2}\n" +
                    $"Net Worth: ${result.Decision.NetWorth:N2}\n\n" +
                    "✅ Progress saved!",
                    "Month Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Reload for next month
                LoadMonthlyState();
                CheckForRandomEvent();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Validates allocation in real-time (optional)
        /// </summary>
        private void ValidateAllocation()
        {
            decimal.TryParse(studentDebtValueHolder.Text, out decimal studentDebt);
            decimal.TryParse(creditDebtValueHolder.Text, out decimal creditDebt);
            decimal.TryParse(textBox4.Text, out decimal investment);
            decimal.TryParse(savingsValueHolder2.Text, out decimal savings);

            var state = _simulationManager.GetCurrentState();
            decimal totalAllocated = studentDebt + creditDebt + investment + savings + state.TotalLivingExpenses;
            decimal totalAvailable = state.TotalFunds + (_currentRandomEvent?.ImpactAmount ?? 0);
            decimal remaining = totalAvailable - totalAllocated;

            // Update a label showing remaining funds (optional)
            // remainingFundsLabel.Text = $"Remaining: ${remaining:N2}";
            // remainingFundsLabel.ForeColor = remaining >= 0 ? Color.Green : Color.Red;
        }

        // Optional: Add real-time validation as user types
        private void AllocationTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateAllocation();
        }

        // Event handlers that were auto-generated - you can remove these if not needed
        private void headerLabel_Click(object sender, EventArgs e) { }
        private void foodLabel_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e) { }
    }
}