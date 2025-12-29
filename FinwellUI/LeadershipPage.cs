using FinwellLibrary;
using FinwellLibrary.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FinwellUI
{
    public partial class LeadershipPage : Form
    {
        private List<LeaderboardEntryModel> _leaderboardData;
        private List<scenarioModel> _scenarios;
        private bool _isOverallMode = true;

        public LeadershipPage()
        {
            InitializeComponent();
        }

        private void LeadershipPage_Load(object sender, EventArgs e)
        {
            try
            {
                // Check if GlobalConfig is initialized
                if (GlobalConfig.Connection == null)
                {
                    MessageBox.Show("Database connection not initialized. Returning to main menu.",
                        "Configuration Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Set up radio buttons
                overalRadioButton.Checked = true;
                scenarioRadioButton.Checked = false;

                // Load scenarios for dropdown
                LoadScenarios();

                // Load initial leaderboard
                LoadOverallLeaderboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing leaderboard: {ex.Message}\n\nStack Trace: {ex.StackTrace}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadScenarios()
        {
            try
            {
                _scenarios = GlobalConfig.Connection.GetAllScenarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading scenarios: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                _scenarios = new List<scenarioModel>();
            }
        }

        private void LoadOverallLeaderboard()
        {
            try
            {
                _leaderboardData = GlobalConfig.Connection.GetOverallLeaderboard(100);
                _isOverallMode = true;

                if (_leaderboardData == null || _leaderboardData.Count == 0)
                {
                    MessageBox.Show("No leaderboard data available yet.\nComplete some simulations to see rankings!",
                        "No Data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    _leaderboardData = new List<LeaderboardEntryModel>();
                }

                DisplayLeaderboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading leaderboard: {ex.Message}\n\nStack Trace: {ex.StackTrace}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadScenarioLeaderboard(int scenarioId)
        {
            try
            {
                _leaderboardData = GlobalConfig.Connection.GetScenarioLeaderboard(scenarioId, 100);
                _isOverallMode = false;

                if (_leaderboardData == null || _leaderboardData.Count == 0)
                {
                    MessageBox.Show("No data for this scenario yet.",
                        "No Data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    _leaderboardData = new List<LeaderboardEntryModel>();
                }

                DisplayLeaderboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading scenario leaderboard: {ex.Message}\n\nStack Trace: {ex.StackTrace}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DisplayLeaderboard()
        {
            try
            {
                // Clear existing data
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                if (_leaderboardData == null || _leaderboardData.Count == 0)
                {
                    // Show empty message in grid
                    dataGridView1.Columns.Add("Message", "Message");
                    dataGridView1.Rows.Add("No leaderboard data available. Complete simulations to see rankings!");
                    return;
                }

                // Configure DataGridView
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Add columns
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Rank",
                    HeaderText = "Rank",
                    DataPropertyName = "Rank",
                    Width = 60,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "UserName",
                    HeaderText = "Player",
                    DataPropertyName = "UserName",
                    Width = 120
                });

                if (_isOverallMode)
                {
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "ScenarioName",
                        HeaderText = "Scenario",
                        DataPropertyName = "ScenarioName",
                        Width = 150
                    });
                }

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "EfficiencyScore",
                    HeaderText = "Score",
                    Width = 80,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "TotalMonths",
                    HeaderText = "Months",
                    DataPropertyName = "TotalMonths",
                    Width = 70,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "FinalNetWorth",
                    HeaderText = "Net Worth",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "TotalDebtPaid",
                    HeaderText = "Debt Paid",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Rating",
                    HeaderText = "Rating",
                    DataPropertyName = "EfficiencyRating",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                // Populate rows
                foreach (var entry in _leaderboardData)
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    DataGridViewRow row = dataGridView1.Rows[rowIndex];

                    row.Cells["Rank"].Value = entry.Rank;
                    row.Cells["UserName"].Value = entry.UserName ?? "Unknown";

                    if (_isOverallMode)
                    {
                        row.Cells["ScenarioName"].Value = entry.ScenarioName ?? "N/A";
                    }

                    row.Cells["EfficiencyScore"].Value = $"{entry.EfficiencyScore:P0}";
                    row.Cells["TotalMonths"].Value = entry.TotalMonths;
                    row.Cells["FinalNetWorth"].Value = $"${entry.FinalNetWorth:N2}";
                    row.Cells["TotalDebtPaid"].Value = $"${entry.TotalDebtPaid:N2}";
                    row.Cells["Rating"].Value = entry.EfficiencyRating;

                    // Color code top 3
                    if (entry.Rank == 1)
                    {
                        row.DefaultCellStyle.BackColor = Color.Gold;
                        row.DefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                    }
                    else if (entry.Rank == 2)
                    {
                        row.DefaultCellStyle.BackColor = Color.Silver;
                    }
                    else if (entry.Rank == 3)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(205, 127, 50); // Bronze
                    }

                    // Highlight current user
                    if (GlobalConfig.CurrentUser != null && entry.UserId == GlobalConfig.CurrentUser.UserId)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightBlue;
                        row.DefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying leaderboard: {ex.Message}\n\nStack Trace: {ex.StackTrace}",
                    "Display Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void overallRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (overalRadioButton.Checked)
            {
                LoadOverallLeaderboard();
            }
        }

        private void scenarioRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (scenarioRadioButton.Checked)
            {
                ShowScenarioSelectionDialog();
            }
        }

        private void ShowScenarioSelectionDialog()
        {
            if (_scenarios == null || _scenarios.Count == 0)
            {
                MessageBox.Show("No scenarios available.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                overalRadioButton.Checked = true;
                return;
            }

            Form selectionForm = new Form
            {
                Text = "Select Scenario",
                Width = 400,
                Height = 300,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            ListBox scenarioListBox = new ListBox
            {
                DataSource = _scenarios,
                DisplayMember = "ScenarioName",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };

            Button selectButton = new Button
            {
                Text = "Select",
                Dock = DockStyle.Bottom,
                Height = 50,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };

            selectButton.Click += (s, ev) =>
            {
                if (scenarioListBox.SelectedItem != null)
                {
                    var selectedScenario = (scenarioModel)scenarioListBox.SelectedItem;
                    LoadScenarioLeaderboard(selectedScenario.ScenarioId);
                    selectionForm.Close();
                }
                else
                {
                    MessageBox.Show("Please select a scenario.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            selectionForm.Controls.Add(scenarioListBox);
            selectionForm.Controls.Add(selectButton);
            selectionForm.ShowDialog();
        }

        private void refreshButton_Click_1(object sender, EventArgs e)
        {
            if (_isOverallMode)
            {
                LoadOverallLeaderboard();
            }
            else
            {
                if (_leaderboardData != null && _leaderboardData.Count > 0)
                {
                    LoadScenarioLeaderboard(_leaderboardData[0].ScenarioId);
                }
                else
                {
                    LoadOverallLeaderboard();
                }
            }
        }
        private void backToScenarioButton_Click(object sender, EventArgs e)
        {
            scenarioPage scenarioForm = new scenarioPage();
            scenarioForm.Show();
            this.Close();

        }
    }
}