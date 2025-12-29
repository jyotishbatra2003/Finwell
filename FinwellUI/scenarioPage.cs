using FinwellLibrary;
using FinwellLibrary.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FinwellUI
{
    public partial class scenarioPage : Form
    {
        private List<scenarioModel> scenarios = new List<scenarioModel>();
        private scenarioModel selectedScenario;

        public scenarioPage()
        {
            InitializeComponent();
        }

        private void scenarioPage_Load(object sender, EventArgs e)
        {
            LoadScenarios();
        }

        private void LoadScenarios()
        {
            try
            {
                scenarios = GlobalConfig.Connection.GetAllScenarios();
                if (scenarios == null || scenarios.Count == 0)
                {
                    MessageBox.Show("No scenarios found in database. Please add scenarios first.",
                        "No Data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                selectScenarioBox.DataSource = scenarios;
                selectScenarioBox.DisplayMember = "ScenarioName";
                selectScenarioBox.ValueMember = "ScenarioId";

                if (scenarios.Count > 0)
                {
                    selectScenarioBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading scenarios: {ex.Message}\n\nStack Trace: {ex.StackTrace}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void selectScenarioBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectScenarioBox.SelectedItem != null)
            {
                selectedScenario = (scenarioModel)selectScenarioBox.SelectedItem;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (selectedScenario == null)
            {
                MessageBox.Show("Please select a scenario first.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int userId = GlobalConfig.CurrentUser?.UserId ?? 1;

                // Check if there's an existing active simulation for this user + scenario
                simulationModel existingSimulation = GlobalConfig.Connection.GetActiveSimulation(userId, selectedScenario.ScenarioId);

                int simulationId;
                string message;

                if (existingSimulation != null)
                {
                    // Found saved game - ask if they want to resume or start fresh
                    DialogResult resumeResult = MessageBox.Show(
                        $"You have a saved game for {selectedScenario.ScenarioName}!\n\n" +
                        $"Progress: Month {existingSimulation.CurrentMonth}\n\n" +
                        "Would you like to RESUME your saved game?\n\n" +
                        "Click YES to continue where you left off\n" +
                        "Click NO to start a NEW challenge (current progress will be lost)",
                        "Saved Game Found",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (resumeResult == DialogResult.Cancel)
                    {
                        return; // User cancelled
                    }
                    else if (resumeResult == DialogResult.Yes)
                    {
                        // Resume existing game
                        simulationId = existingSimulation.SimulationId;
                        message = $"Resuming from Month {existingSimulation.CurrentMonth}...";
                    }
                    else // DialogResult.No
                    {
                        // Start new game - mark old one as abandoned
                        GlobalConfig.Connection.UpdateSimulationStatus(existingSimulation.SimulationId, "Abandoned", existingSimulation.CurrentMonth);

                        // Create new simulation
                        simulationModel newSimulation = new simulationModel
                        {
                            UserId = userId,
                            ScenarioId = selectedScenario.ScenarioId,
                            Status = "In Progress",
                            CurrentMonth = 1
                        };

                        simulationId = GlobalConfig.Connection.CreateSimulation(newSimulation);
                        message = "Starting fresh challenge...";
                    }
                }
                else
                {
                    // No existing game - show scenario details and create new
                    DialogResult result = MessageBox.Show(
                        $"Start Challenge: {selectedScenario.ScenarioName}\n\n" +
                        $"Initial Debt: ${selectedScenario.InitialDebt:N2}\n" +
                        $"Monthly Income: ${selectedScenario.MonthlyIncome:N2}\n" +
                        $"Living Expenses: ${selectedScenario.LivingExpenses:N2}\n" +
                        $"Debt Type: {selectedScenario.DeptType}\n" +
                        $"Interest Rate: {selectedScenario.InterestRate}%\n\n" +
                        $"Available for Debt: ${(selectedScenario.MonthlyIncome - selectedScenario.LivingExpenses):N2}\n\n" +
                        "Are you ready to begin?",
                        "Confirm Challenge",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result != DialogResult.Yes)
                    {
                        return; // User cancelled
                    }

                    // Create new simulation
                    simulationModel newSimulation = new simulationModel
                    {
                        UserId = userId,
                        ScenarioId = selectedScenario.ScenarioId,
                        Status = "In Progress",
                        CurrentMonth = 1
                    };

                    simulationId = GlobalConfig.Connection.CreateSimulation(newSimulation);
                    message = "Challenge starting!";
                }

                // Open monthly iteration form
                MessageBox.Show(message, "Let's Go!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                montlyIteration frm = new montlyIteration(selectedScenario, simulationId);
                frm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting simulation: {ex.Message}\n\nStack Trace: {ex.StackTrace}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void viewLearderBoardButton_Click(object sender, EventArgs e)
        {
            LeadershipPage leadershipPage = new LeadershipPage();
            leadershipPage.Show();
            this.Close();
        }
    }
}