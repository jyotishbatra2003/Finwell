using FinwellLibrary;
using FinwellLibrary.Models;
using System;
using System.Windows.Forms;

namespace FinwellUI
{
    public partial class ResultPage : Form
    {
        private int _simulationId;
        private simulationResultModel _result;

        public ResultPage(int simulationId)
        {
            InitializeComponent();
            _simulationId = simulationId;
        }

        private void ResultPage_Load(object sender, EventArgs e)
        {
            LoadResults();
        }

        private void LoadResults()
        {
            try
            {
                // Get the simulation result
                _result = GlobalConfig.Connection.GetSimulationResult(_simulationId);

                if (_result == null)
                {
                    MessageBox.Show("No results found for this simulation.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Populate the form fields
                resultMonthValueHolder.Text = _result.TotalMonths.ToString();
                resultNetWorthValueHolder.Text = $"${_result.FinalNetWorth:N2}";
                textBox2.Text = $"{_result.EfficiencyRating} ({_result.EfficiencyScore:P0})";
                totalInterestValueHolder.Text = $"${_result.TotalInterestPaid:N2}";
                totalInestmentGainsValueHolder.Text = $"${_result.TotalInvestmentGains:N2}";

                // Optional: Add if you have these controls
                // totalDebtPaidValue.Text = $"${_result.TotalDebtPaid:N2}";
                // performanceSummaryLabel.Text = _result.PerformanceSummary;

                // Set efficiency color based on score
                if (_result.EfficiencyScore >= 0.75m)
                {
                    textBox2.ForeColor = System.Drawing.Color.Green;
                }
                else if (_result.EfficiencyScore >= 0.6m)
                {
                    textBox2.ForeColor = System.Drawing.Color.Orange;
                }
                else
                {
                    textBox2.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading results: {ex.Message}\n\nStack Trace: {ex.StackTrace}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void backToMenuButton_Click(object sender, EventArgs e)
        {
            // Return to scenario selection
            scenarioPage scenarioForm = new scenarioPage();
            scenarioForm.Show();
            this.Close();
        }

        private void viewLeaderboardButton_Click(object sender, EventArgs e)
        {
            LeadershipPage leadershipPage = new LeadershipPage();
            leadershipPage.Show();
            this.Close();
           
        }
    }
}