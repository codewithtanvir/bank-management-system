using BankManagementSystem.Forms;
using BankManagementSystem.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankManagementSystem.Forms.Admin;
using BankManagementSystem.Forms.Customer;
using BankManagementSystem.Forms.Employee;
using Microsoft.VisualBasic; // Added namespace


namespace BankManagementSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = "SELECT * FROM Users WHERE Email = @Email AND IsActive = 1";
                SqlParameter[] param = { DatabaseHelper.CreateParameter("@Email", email) };
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, param);

                if (dt.Rows.Count == 1)
                {
                    string hashed = dt.Rows[0]["HashedPassword"].ToString();

                    if (PasswordHasher.VerifyPassword(password, hashed))
                    {
                        // Set session info
                        SessionManager.UserID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                        SessionManager.Name = dt.Rows[0]["FirstName"] + " " + dt.Rows[0]["LastName"];
                        SessionManager.Role = dt.Rows[0]["Role"].ToString();

                        // Redirect to respective dashboard
                        this.Hide();
                        if (SessionManager.Role == "Admin")
                        {
                            new AdminDashboardForm().Show();
                        }
                        else if (SessionManager.Role == "Employee")
                        {
                            new EmployeeDashboardForm().Show();
                        }
                        else if (SessionManager.Role == "Customer")
                        {
                            new CustomerDashboardForm().Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect password.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("User not found or account disabled.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while processing your request: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistrationForm registerForm = new RegistrationForm();
            registerForm.Show();
            this.Hide();


        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        // Method to open Validation Demo Form for educational purposes
        private void btnDemo_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.Demo.ValidationDemoForm demoForm = new Forms.Demo.ValidationDemoForm();
                demoForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Demo form could not be loaded: " + ex.Message, "Demo Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showPass_CheckedChanged_1(object sender, EventArgs e)
        {
            if (showPass.Checked)
            {
                txtPassword.UseSystemPasswordChar = false; // Show password
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true; // Hide password
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkForgetPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Removed forget password functionality
            MessageBox.Show("Forget Password functionality is not implemented yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    // public static class PasswordHasher
    // {
    //     // Hashes a password using BCrypt
    //     public static string HashPassword(string password)
    //     {
    //         return BCrypt.Net.BCrypt.HashPassword(password);
    //     }

    //     // Verifies a given password against a stored hash
    //     public static bool VerifyPassword(string password, string hashedPassword)
    //     {
    //         return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    //     }

    //     // Placeholder for decryption logic (if needed)
    //     public static string DecryptPassword(string hashedPassword)
    //     {
    //         throw new NotImplementedException("Decryption is not supported for hashed passwords.");
    //     }
    }

