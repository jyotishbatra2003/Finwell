
namespace FinwellUI
{
    partial class ResultPage
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
            resultMontLabel = new Label();
            resultMonthValueHolder = new TextBox();
            resultNetWorthValueHolder = new TextBox();
            netWorthResultLabel = new Label();
            textBox2 = new TextBox();
            efficiencyLabel = new Label();
            totalInterestValueHolder = new TextBox();
            totalInterestLabel = new Label();
            totalInestmentGainsValueHolder = new TextBox();
            totalInvestmentGainLabel = new Label();
            viewLeaderboardButton = new Button();
            SuspendLayout();
            // 
            // headerLabel
            // 
            headerLabel.AutoSize = true;
            headerLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            headerLabel.Location = new Point(43, 19);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(336, 48);
            headerLabel.TabIndex = 0;
            headerLabel.Text = "Simualtion Results";
            // 
            // resultMontLabel
            // 
            resultMontLabel.AutoSize = true;
            resultMontLabel.Location = new Point(50, 129);
            resultMontLabel.Name = "resultMontLabel";
            resultMontLabel.Size = new Size(260, 25);
            resultMontLabel.TabIndex = 1;
            resultMontLabel.Text = "Number of Months to freedom";
            resultMontLabel.Click += label1_Click;
            // 
            // resultMonthValueHolder
            // 
            resultMonthValueHolder.Location = new Point(420, 123);
            resultMonthValueHolder.Name = "resultMonthValueHolder";
            resultMonthValueHolder.Size = new Size(252, 31);
            resultMonthValueHolder.TabIndex = 3;
            // 
            // resultNetWorthValueHolder
            // 
            resultNetWorthValueHolder.Location = new Point(420, 196);
            resultNetWorthValueHolder.Name = "resultNetWorthValueHolder";
            resultNetWorthValueHolder.Size = new Size(252, 31);
            resultNetWorthValueHolder.TabIndex = 5;
            // 
            // netWorthResultLabel
            // 
            netWorthResultLabel.AutoSize = true;
            netWorthResultLabel.Location = new Point(50, 199);
            netWorthResultLabel.Name = "netWorthResultLabel";
            netWorthResultLabel.Size = new Size(327, 25);
            netWorthResultLabel.TabIndex = 4;
            netWorthResultLabel.Text = "Net Worth in the span of the Simualtion";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(420, 268);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(252, 31);
            textBox2.TabIndex = 7;
            // 
            // efficiencyLabel
            // 
            efficiencyLabel.AutoSize = true;
            efficiencyLabel.Location = new Point(50, 274);
            efficiencyLabel.Name = "efficiencyLabel";
            efficiencyLabel.Size = new Size(90, 25);
            efficiencyLabel.TabIndex = 6;
            efficiencyLabel.Text = "Efficiency ";
            // 
            // totalInterestValueHolder
            // 
            totalInterestValueHolder.Location = new Point(420, 339);
            totalInterestValueHolder.Name = "totalInterestValueHolder";
            totalInterestValueHolder.Size = new Size(252, 31);
            totalInterestValueHolder.TabIndex = 9;
            // 
            // totalInterestLabel
            // 
            totalInterestLabel.AutoSize = true;
            totalInterestLabel.Location = new Point(50, 345);
            totalInterestLabel.Name = "totalInterestLabel";
            totalInterestLabel.Size = new Size(151, 25);
            totalInterestLabel.TabIndex = 8;
            totalInterestLabel.Text = "Total Interest Paid";
            // 
            // totalInestmentGainsValueHolder
            // 
            totalInestmentGainsValueHolder.Location = new Point(420, 412);
            totalInestmentGainsValueHolder.Name = "totalInestmentGainsValueHolder";
            totalInestmentGainsValueHolder.Size = new Size(252, 31);
            totalInestmentGainsValueHolder.TabIndex = 11;
            // 
            // totalInvestmentGainLabel
            // 
            totalInvestmentGainLabel.AutoSize = true;
            totalInvestmentGainLabel.Location = new Point(50, 415);
            totalInvestmentGainLabel.Name = "totalInvestmentGainLabel";
            totalInvestmentGainLabel.Size = new Size(190, 25);
            totalInvestmentGainLabel.TabIndex = 10;
            totalInvestmentGainLabel.Text = "Total Investment Gains";
            // 
            // viewLeaderboardButton
            // 
            viewLeaderboardButton.BackColor = Color.LightGray;
            viewLeaderboardButton.Location = new Point(1171, 526);
            viewLeaderboardButton.Name = "viewLeaderboardButton";
            viewLeaderboardButton.Size = new Size(341, 68);
            viewLeaderboardButton.TabIndex = 34;
            viewLeaderboardButton.Text = "View LeaderBoard";
            viewLeaderboardButton.UseVisualStyleBackColor = false;
            viewLeaderboardButton.Click += viewLeaderboardButton_Click;
            // 
            // ResultPage
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            ClientSize = new Size(1621, 643);
            Controls.Add(viewLeaderboardButton);
            Controls.Add(totalInestmentGainsValueHolder);
            Controls.Add(totalInvestmentGainLabel);
            Controls.Add(totalInterestValueHolder);
            Controls.Add(totalInterestLabel);
            Controls.Add(textBox2);
            Controls.Add(efficiencyLabel);
            Controls.Add(resultNetWorthValueHolder);
            Controls.Add(netWorthResultLabel);
            Controls.Add(resultMonthValueHolder);
            Controls.Add(resultMontLabel);
            Controls.Add(headerLabel);
            Name = "ResultPage";
            Text = "ResultPage";
            ResumeLayout(false);
            PerformLayout();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Label headerLabel;
        private Label resultMontLabel;
        private TextBox resultMonthValueHolder;
        private TextBox resultNetWorthValueHolder;
        private Label netWorthResultLabel;
        private TextBox textBox2;
        private Label efficiencyLabel;
        private TextBox totalInterestValueHolder;
        private Label totalInterestLabel;
        private TextBox totalInestmentGainsValueHolder;
        private Label totalInvestmentGainLabel;
        private Button viewLeaderboardButton;
    }
}