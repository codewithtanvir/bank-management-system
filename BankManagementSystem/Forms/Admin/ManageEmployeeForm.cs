using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BankManagementSystem.Classes;

namespace BankManagementSystem.Forms.Admin
{
    public partial class ManageEmployeeForm : Form
    {
        private int? _userId = null; // null = Add, otherwise Edit mode

        public ManageEmployeeForm()
        {
            InitializeComponent();
            cmbRole.Items.AddRange(new string[] { "Admin", "Employee" });
            cmbRole.SelectedIndex = 0;
        }

        public ManageEmployeeForm(int userId) // Edit mode
        {
            InitializeComponent();
            _userId = userId;
            cmbRole.Items.AddRange(new string[] { "Admin", "Employee" });
            LoadUserData();
        }

        private void LoadUserData()
        {
            string sql = "SELECT * FROM Users WHERE UserID = @UID";
            SqlParameter[] param = { new SqlParameter("@UID", _userId) };

            DataTable dt = DatabaseHelper.ExecuteQuery(sql, param);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];
                txtFirstName.Text = row["FirstName"].ToString();
                txtLastName.Text = row["LastName"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtPhone.Text = row["PhoneNumber"].ToString();
                txtAddress.Text = row["Address"].ToString();
                dtpDOB.Value = Convert.ToDateTime(row["DateOfBirth"]);
                cmbRole.SelectedItem = row["Role"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string fname = txtFirstName.Text.Trim();
            string lname = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            DateTime dob = dtpDOB.Value;
            string role = cmbRole.SelectedItem.ToString();
            string password = txtPassword.Text;

            if (_userId == null)  // ➕ INSERT
            {
                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Password is required for new users.");
                    return;
                }

                string hashed = PasswordHasher.HashPassword(password);

                string insertSQL = @"
                    INSERT INTO Users (FirstName, LastName, Email, HashedPassword, PhoneNumber, Address, DateOfBirth, Role)
                    VALUES (@FName, @LName, @Email, @Hashed, @Phone, @Address, @DOB, @Role);";

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
                DatabaseHelper.LogAction(SessionManager.UserID, $"Added {role}: {fname} {lname}");

                MessageBox.Show($"{role} added successfully!");
            }
            else // ✏️ UPDATE
            {
                string updateSQL = @"
                    UPDATE Users 
                    SET FirstName=@FName, LastName=@LName, Email=@Email,
                        PhoneNumber=@Phone, Address=@Address, DateOfBirth=@DOB, Role=@Role 
                    WHERE UserID=@UID";

                SqlParameter[] param = {
                    new SqlParameter("@FName", fname),
                    new SqlParameter("@LName", lname),
                    new SqlParameter("@Email", email),
                    new SqlParameter("@Phone", phone),
                    new SqlParameter("@Address", address),
                    new SqlParameter("@DOB", dob),
                    new SqlParameter("@Role", role),
                    new SqlParameter("@UID", _userId)
                };

                DatabaseHelper.ExecuteNonQuery(updateSQL, param);

                // Optional password change
                if (!string.IsNullOrWhiteSpace(password))
                {
                    string hashed = PasswordHasher.HashPassword(password);
                    string pwUpdateSQL = "UPDATE Users SET HashedPassword = @HPW WHERE UserID = @UID";

                    SqlParameter[] pwParam = {
                        new SqlParameter("@HPW", hashed),
                        new SqlParameter("@UID", _userId)
                    };

                    DatabaseHelper.ExecuteNonQuery(pwUpdateSQL, pwParam);
                }

                DatabaseHelper.LogAction(SessionManager.UserID, $"Edited user ID: {_userId}");
                MessageBox.Show("User updated successfully!");
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ManageEmployeeForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}