using FinwellLibrary;
using FinwellLibrary.Models;
using System;
using System.Windows.Forms;

namespace FinwellUI
{
    public partial class authPage : Form
    {
        public authPage()
        {
            InitializeComponent();
        }

        private void authPage_Load(object sender, EventArgs e)
        {
            // Set password field to use password character
            passwordValueHolder.UseSystemPasswordChar = true;
        }

        // ===== LOGIN BUTTON =====
        private void loginButton_Click(object sender, EventArgs e)
        {
            if (ValidateLoginForm())
            {
                try
                {
                    userModel user = GlobalConfig.Connection.AuthenticateUser(
                        userNameValueHolder.Text,
                        passwordValueHolder.Text);

                    if (user != null)
                    {
                        // ✅ Store logged-in user globally
                        GlobalConfig.CurrentUser = user;

                        MessageBox.Show($"Welcome back, {user.UserName}!",
                            "Login Successful",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Clear fields
                        userNameValueHolder.Text = "";
                        passwordValueHolder.Text = "";

                        // Open Scenario Selection Form
                        scenarioPage frm = new scenarioPage();
                        frm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.\n\nIf you're a new user, please register first.",
                            "Login Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during login: {ex.Message}\n\nStack Trace: {ex.StackTrace}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        // ===== REGISTRATION BUTTON =====
        private void registrationButton_Click(object sender, EventArgs e)
        {
            if (ValidateRegistrationForm())
            {
                try
                {
                    // ✅ Create new user object using the same textboxes
                    userModel newUser = new userModel(
                        0,  // UserId will be set by database
                        textBox3.Text,  // ✅ FIXED: Use userNameValueHolder
                        newPasswordValueHolder.Text   // ✅ FIXED: Use passwordValueHolder
                    );

                    // ✅ Save to database
                    userModel createdUser = GlobalConfig.Connection.CreateUser(newUser);

                    if (createdUser != null && createdUser.UserId > 0)
                    {
                        MessageBox.Show($"Registration successful!\n\nUsername: {createdUser.UserName}\n\nYou can now log in with your credentials.",
                            "Welcome to FinWell!",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Clear fields after successful registration
                        userNameValueHolder.Text = "";
                        passwordValueHolder.Text = "";

                        // Focus on username field for login
                        userNameValueHolder.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Registration failed. Please try again.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Handle duplicate username error
                    if (ex.Message.Contains("UNIQUE") || ex.Message.Contains("duplicate") || ex.Message.Contains("Violation"))
                    {
                        MessageBox.Show("Username already exists. Please choose a different username.",
                            "Registration Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"Error during registration: {ex.Message}\n\nStack Trace: {ex.StackTrace}",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ===== VALIDATION METHODS =====
        private bool ValidateRegistrationForm()
        {
            if (string.IsNullOrWhiteSpace(userNameValueHolder.Text))
            {
                MessageBox.Show("Username is required",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                userNameValueHolder.Focus();
                return false;
            }

            if (userNameValueHolder.Text.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters long",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                userNameValueHolder.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(passwordValueHolder.Text))
            {
                MessageBox.Show("Password is required",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                passwordValueHolder.Focus();
                return false;
            }

            if (passwordValueHolder.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                passwordValueHolder.Focus();
                return false;
            }

            return true;
        }

        private bool ValidateLoginForm()
        {
            if (string.IsNullOrWhiteSpace(userNameValueHolder.Text))
            {
                MessageBox.Show("Username is required",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                userNameValueHolder.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(passwordValueHolder.Text))
            {
                MessageBox.Show("Password is required",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                passwordValueHolder.Focus();
                return false;
            }

            return true;
        }
    }
}