
namespace FinwellUI
{
    partial class LeadershipPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            headerLabel = new Label();
            dataGridView1 = new DataGridView();
            refreshButton = new Button();
            overalRadioButton = new RadioButton();
            backToScenarioButton = new Button();
            scenarioRadioButton = new RadioButton();
            colName = new DataGridViewTextBoxColumn();
            colScore = new DataGridViewTextBoxColumn();
            colRank = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // headerLabel
            // 
            headerLabel.AutoSize = true;
            headerLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            headerLabel.Location = new Point(41, 24);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(333, 48);
            headerLabel.TabIndex = 1;
            headerLabel.Text = "Leaderboard Table";
            headerLabel.Click += headerLabel_Click;
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle2.ForeColor = Color.Silver;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.BackgroundColor = Color.Linen;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colName, colScore, colRank });
            dataGridView1.Location = new Point(26, 166);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1195, 298);
            dataGridView1.TabIndex = 2;
            // 
            // refreshButton
            // 
            refreshButton.BackColor = Color.Gray;
            refreshButton.Location = new Point(269, 535);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(244, 74);
            refreshButton.TabIndex = 3;
            refreshButton.Text = "Refresh";
            refreshButton.UseVisualStyleBackColor = false;
            refreshButton.Click += refreshButton_Click_1;
            // 
            // overalRadioButton
            // 
            overalRadioButton.AutoSize = true;
            overalRadioButton.Location = new Point(256, 100);
            overalRadioButton.Name = "overalRadioButton";
            overalRadioButton.Size = new Size(187, 29);
            overalRadioButton.TabIndex = 4;
            overalRadioButton.TabStop = true;
            overalRadioButton.Text = "Overal Leaderboad";
            overalRadioButton.UseVisualStyleBackColor = true;
            // 
            // backToScenarioButton
            // 
            backToScenarioButton.BackColor = Color.Gray;
            backToScenarioButton.Location = new Point(829, 535);
            backToScenarioButton.Name = "backToScenarioButton";
            backToScenarioButton.Size = new Size(244, 74);
            backToScenarioButton.TabIndex = 6;
            backToScenarioButton.Text = "Back to Scenario Page";
            backToScenarioButton.UseVisualStyleBackColor = false;
            backToScenarioButton.Click += backToScenarioButton_Click;
            // 
            // scenarioRadioButton
            // 
            scenarioRadioButton.AutoSize = true;
            scenarioRadioButton.Location = new Point(558, 100);
            scenarioRadioButton.Name = "scenarioRadioButton";
            scenarioRadioButton.Size = new Size(169, 29);
            scenarioRadioButton.TabIndex = 5;
            scenarioRadioButton.TabStop = true;
            scenarioRadioButton.Text = "Scenario Specific";
            scenarioRadioButton.UseVisualStyleBackColor = true;
            // 
            // colName
            // 
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colName.HeaderText = "Name";
            colName.MinimumWidth = 8;
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colScore
            // 
            colScore.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colScore.HeaderText = "Score";
            colScore.MinimumWidth = 8;
            colScore.Name = "colScore";
            colScore.ReadOnly = true;
            // 
            // colRank
            // 
            colRank.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colRank.HeaderText = "Rank";
            colRank.MinimumWidth = 8;
            colRank.Name = "colRank";
            colRank.ReadOnly = true;
            // 
            // LeadershipPage
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            ClientSize = new Size(1233, 665);
            Controls.Add(dataGridView1);
            Controls.Add(backToScenarioButton);
            Controls.Add(scenarioRadioButton);
            Controls.Add(overalRadioButton);
            Controls.Add(refreshButton);
            Controls.Add(headerLabel);
            Name = "LeadershipPage";
            Text = "LeadershipPage";
            Load += LeadershipPage_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void headerLabel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Label headerLabel;
        private DataGridView dataGridView1;
        private Button refreshButton;
        private RadioButton overalRadioButton;
        private Button backToScenarioButton;
        private RadioButton scenarioRadioButton;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colScore;
        private DataGridViewTextBoxColumn colRank;
    }
}