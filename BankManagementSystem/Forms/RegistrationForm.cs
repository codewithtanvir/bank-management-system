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
using BankManagementSystem.Classes;
using System.Configuration; 

namespace BankManagementSystem.Forms
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Collect Data
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirm.Text;
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            DateTime dob = dtpDOB.Value;
            string role = "Customer"; //  Default role
            

            //  Validation
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Email and password fields are mandatory.");
                return;
            }

            // Add validation for required fields
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtConfirm.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text))
                
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            // Validate email format
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            // Validate phone number format
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text, @"^\d{11}$"))
            {
                MessageBox.Show("Phone number must be 11 digits.");
                return;
            }

            // Validate password length
            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.");
                return;
            }
            // birthdate validation 18 years or older
            if (dob > DateTime.Now.AddYears(-18))
            {
                MessageBox.Show("You must be at least 18 years old to register.");
                return;
            }
            // email already exists validation
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    string checkEmailSql = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    SqlCommand cmdCheckEmail = new SqlCommand(checkEmailSql, conn);
                    cmdCheckEmail.Parameters.AddWithValue("@Email", email);
                    int emailCount = (int)cmdCheckEmail.ExecuteScalar();
                    if (emailCount > 0)
                    {
                        MessageBox.Show("This email is already registered. Please use a different email.");
                        return;
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Database error: " + sqlEx.Message);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message);
                    return;
                }
            }


            string hashedPassword = PasswordHasher.HashPassword(password);
            string accNumber = "AC" + new Random().Next(100,999);
           

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Step 1: Insert into Users table
                        string insertUserSql = @"
                INSERT INTO Users (FirstName, LastName, Email, HashedPassword, PhoneNumber, Address, DateOfBirth, Role)
                VALUES (@FName, @LName, @Email, @Hashed, @Phone, @Address, @DOB, @Role);
                SELECT SCOPE_IDENTITY();";

                        SqlCommand cmdUser = new SqlCommand(insertUserSql, conn, transaction);
                        cmdUser.Parameters.AddWithValue("@FName", firstName);
                        cmdUser.Parameters.AddWithValue("@LName", lastName);
                        cmdUser.Parameters.AddWithValue("@Email", email);
                        cmdUser.Parameters.AddWithValue("@Hashed", hashedPassword);
                        cmdUser.Parameters.AddWithValue("@Phone", phone);
                        cmdUser.Parameters.AddWithValue("@Address", address);
                        cmdUser.Parameters.AddWithValue("@DOB", dob);
                        cmdUser.Parameters.AddWithValue("@Role", role);

                        object userIdObj = cmdUser.ExecuteScalar();
                        int newUserId = Convert.ToInt32(userIdObj);

                        // Step 2: Insert into Accounts table
                        string insertAccountSql = @"
                INSERT INTO Accounts (UserID, AccountNumber, AccountType)
                VALUES (@UID, @AccNum, @AccType)";

                        SqlCommand cmdAcc = new SqlCommand(insertAccountSql, conn, transaction);
                        cmdAcc.Parameters.AddWithValue("@UID", newUserId);
                        cmdAcc.Parameters.AddWithValue("@AccNum", accNumber);
                        cmdAcc.Parameters.AddWithValue("@AccType", "Savings Account");

                        cmdAcc.ExecuteNonQuery();

                        // ✅ Commit both inserts
                        transaction.Commit();
                                
                        MessageBox.Show("Registration Successful! Your Account Number is: " + accNumber);
                        this.Close();
                        LoginForm loginForm = new LoginForm();
                        loginForm.Show();
                        
                    }
                    catch (SqlException sqlEx)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Database error: " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("An unexpected error occurred: " + ex.Message);
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Failed to connect to the database: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred while connecting: " + ex.Message);
                }
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {

        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Close the application
        }

        private void showPass_CheckedChanged(object sender, EventArgs e)
        {
            if (showPass.Checked)
            {
                txtPassword.UseSystemPasswordChar = false; // Show password
                txtConfirm.UseSystemPasswordChar = false; // Show confirm password
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true; // Hide password
                txtConfirm.UseSystemPasswordChar = true; // Hide confirm password
            }
        }

       
    }
}
