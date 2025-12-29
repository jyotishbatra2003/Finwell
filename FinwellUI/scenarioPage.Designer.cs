namespace FinwellUI
{
    partial class scenarioPage
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
            headerLabel = new Label();
            selectScenarioBox = new ComboBox();
            startButton = new Button();
            viewLearderBoardButton = new Button();
            SuspendLayout();
            // 
            // headerLabel
            // 
            headerLabel.AutoSize = true;
            headerLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            headerLabel.Location = new Point(32, 46);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(273, 48);
            headerLabel.TabIndex = 2;
            headerLabel.Text = "Select Scenario";
            // 
            // selectScenarioBox
            // 
            selectScenarioBox.FormattingEnabled = true;
            selectScenarioBox.Location = new Point(361, 58);
            selectScenarioBox.Name = "selectScenarioBox";
            selectScenarioBox.Size = new Size(519, 38);
            selectScenarioBox.TabIndex = 3;
            selectScenarioBox.SelectedIndexChanged += selectScenarioBox_SelectedIndexChanged;
            // 
            // startButton
            // 
            startButton.BackColor = Color.LightGray;
            startButton.Location = new Point(451, 163);
            startButton.Name = "startButton";
            startButton.Size = new Size(341, 68);
            startButton.TabIndex = 11;
            startButton.Text = "Start Game";
            startButton.UseVisualStyleBackColor = false;
            startButton.Click += startButton_Click;
            // 
            // viewLearderBoardButton
            // 
            viewLearderBoardButton.BackColor = Color.LightGray;
            viewLearderBoardButton.Location = new Point(810, 409);
            viewLearderBoardButton.Name = "viewLearderBoardButton";
            viewLearderBoardButton.Size = new Size(341, 68);
            viewLearderBoardButton.TabIndex = 12;
            viewLearderBoardButton.Text = "View LeaderBoard";
            viewLearderBoardButton.UseVisualStyleBackColor = false;
            viewLearderBoardButton.Click += viewLearderBoardButton_Click;
            // 
            // scenarioPage
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            ClientSize = new Size(1370, 656);
            Controls.Add(viewLearderBoardButton);
            Controls.Add(startButton);
            Controls.Add(selectScenarioBox);
            Controls.Add(headerLabel);
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "scenarioPage";
            Text = "scenarioPage";
            Load += scenarioPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label headerLabel;
        private ComboBox selectScenarioBox;
        private Button startButton;
        private Button viewLearderBoardButton;
    }
}