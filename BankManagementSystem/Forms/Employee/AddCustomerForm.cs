using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using BankManagementSystem.Classes;

namespace BankManagementSystem.Forms.Employee
{
    public partial class AddCustomerForm : Form
    {
        public AddCustomerForm()
        {
            InitializeComponent();
            cmbRole.SelectedIndex = 0; // Set default role to 'Customer'
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string fname = txtFirstName.Text.Trim();
            string lname = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            DateTime dob = dtpDOB.Value;
            string role = cmbRole.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(fname) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("First name, email and password are required.");
                return;
            }

            string hashed = PasswordHasher.HashPassword(password);

            // Insert into Users table
            string insertSQL = @"
                INSERT INTO Users 
                (FirstName, LastName, Email, HashedPassword, PhoneNumber, Address, DateOfBirth, Role)
                VALUES 
                (@FName, @LName, @Email, @Hashed, @Phone, @Address, @DOB, @Role)";

            SqlParameter[] param = {
                new SqlParameter("@FName", fname),
                new SqlParameter("@LName", lname),
                new SqlParameter("@Email", email),
                new SqlParameter("@Hashed", hashed),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@Address", address),
                new SqlParameter("@DOB", dob),
                new SqlParameter("@Role", role)
            };

            DatabaseHelper.ExecuteNonQuery(insertSQL, param);

            // Get new user ID
            object userIdObj = DatabaseHelper.ExecuteScalar("SELECT MAX(UserID) FROM Users");
            int userId = Convert.ToInt32(userIdObj);

            // Create Account
            string accNum = "AC" + new Random().Next(100000, 999999);
            string accSQL = @"INSERT INTO Accounts (UserID, AccountNumber, AccountType)
                              VALUES (@UserId, @AccNum, 'Savings Account')";
            SqlParameter[] accParams = {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@AccNum", accNum)
            };
            DatabaseHelper.ExecuteNonQuery(accSQL, accParams);

            DatabaseHelper.LogAction(SessionManager.UserID, $"Added new customer: {email}");

            MessageBox.Show("Customer created successfully!");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddCustomerForm_Load(object sender, EventArgs e)
        {

        }
        public static class PasswordHasher
        {
            public static string HashPassword(string password)
            {
                return BCrypt.Net.BCrypt.HashPassword(password);
            }

            public static bool VerifyPassword(string enteredPassword, string hashedPassword)
            {
                return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}