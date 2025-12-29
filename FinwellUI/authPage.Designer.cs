
namespace FinwellUI
{
    partial class authPage
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
            userNameLabel = new Label();
            userNameValueHolder = new TextBox();
            passwordValueHolder = new TextBox();
            passwordLabel = new Label();
            newPasswordValueHolder = new TextBox();
            newPasswordLabel = new Label();
            textBox3 = new TextBox();
            newUserNameLabel = new Label();
            loginButton = new Button();
            createLabel = new Label();
            registrationButton = new Button();
            SuspendLayout();
            // 
            // headerLabel
            // 
            headerLabel.AutoSize = true;
            headerLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            headerLabel.Location = new Point(46, 35);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(210, 38);
            headerLabel.TabIndex = 0;
            headerLabel.Text = "Login / Signup";
            // 
            // userNameLabel
            // 
            userNameLabel.AutoSize = true;
            userNameLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            userNameLabel.Location = new Point(97, 125);
            userNameLabel.Name = "userNameLabel";
            userNameLabel.Size = new Size(128, 32);
            userNameLabel.TabIndex = 1;
            userNameLabel.Text = "UserName";
            // 
            // userNameValueHolder
            // 
            userNameValueHolder.Location = new Point(278, 128);
            userNameValueHolder.Name = "userNameValueHolder";
            userNameValueHolder.Size = new Size(252, 31);
            userNameValueHolder.TabIndex = 2;
            // 
            // passwordValueHolder
            // 
            passwordValueHolder.Location = new Point(278, 192);
            passwordValueHolder.Name = "passwordValueHolder";
            passwordValueHolder.Size = new Size(252, 31);
            passwordValueHolder.TabIndex = 4;
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            passwordLabel.Location = new Point(97, 189);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(115, 32);
            passwordLabel.TabIndex = 3;
            passwordLabel.Text = "Password";
            // 
            // newPasswordValueHolder
            // 
            newPasswordValueHolder.Location = new Point(975, 347);
            newPasswordValueHolder.Name = "newPasswordValueHolder";
            newPasswordValueHolder.Size = new Size(252, 31);
            newPasswordValueHolder.TabIndex = 8;
            // 
            // newPasswordLabel
            // 
            newPasswordLabel.AutoSize = true;
            newPasswordLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            newPasswordLabel.Location = new Point(794, 344);
            newPasswordLabel.Name = "newPasswordLabel";
            newPasswordLabel.Size = new Size(171, 32);
            newPasswordLabel.TabIndex = 7;
            newPasswordLabel.Text = "New Password";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(975, 283);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(252, 31);
            textBox3.TabIndex = 6;
            // 
            // newUserNameLabel
            // 
            newUserNameLabel.AutoSize = true;
            newUserNameLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            newUserNameLabel.Location = new Point(794, 280);
            newUserNameLabel.Name = "newUserNameLabel";
            newUserNameLabel.Size = new Size(184, 32);
            newUserNameLabel.TabIndex = 5;
            newUserNameLabel.Text = "New UserName";
            // 
            // loginButton
            // 
            loginButton.BackColor = Color.LightGray;
            loginButton.Location = new Point(156, 308);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(341, 68);
            loginButton.TabIndex = 34;
            loginButton.Text = "Login / SignUp";
            loginButton.UseVisualStyleBackColor = false;
            loginButton.Click += loginButton_Click;
            // 
            // createLabel
            // 
            createLabel.AutoSize = true;
            createLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            createLabel.Location = new Point(682, 189);
            createLabel.Name = "createLabel";
            createLabel.Size = new Size(517, 38);
            createLabel.TabIndex = 35;
            createLabel.Text = "Create New User Name and Password";
            // 
            // registrationButton
            // 
            registrationButton.BackColor = Color.LightGray;
            registrationButton.Location = new Point(835, 436);
            registrationButton.Name = "registrationButton";
            registrationButton.Size = new Size(341, 68);
            registrationButton.TabIndex = 36;
            registrationButton.Text = "Register";
            registrationButton.UseVisualStyleBackColor = false;
            registrationButton.Click += registrationButton_Click;
            // 
            // authPage
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            ClientSize = new Size(1338, 592);
            Controls.Add(registrationButton);
            Controls.Add(createLabel);
            Controls.Add(loginButton);
            Controls.Add(newPasswordValueHolder);
            Controls.Add(newPasswordLabel);
            Controls.Add(textBox3);
            Controls.Add(newUserNameLabel);
            Controls.Add(passwordValueHolder);
            Controls.Add(passwordLabel);
            Controls.Add(userNameValueHolder);
            Controls.Add(userNameLabel);
            Controls.Add(headerLabel);
            Name = "authPage";
            Text = "authPage";
            Load += authPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        //private void registrationButton_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        private Label headerLabel;
        private Label userNameLabel;
        private TextBox userNameValueHolder;
        private TextBox passwordValueHolder;
        private Label passwordLabel;
        private TextBox newPasswordValueHolder;
        private Label newPasswordLabel;
        private TextBox textBox3;
        private Label newUserNameLabel;
        private Button loginButton;
        private Label createLabel;
        private Button registrationButton;
    }
}