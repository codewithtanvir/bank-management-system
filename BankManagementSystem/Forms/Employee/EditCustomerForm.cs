using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BankManagementSystem.Classes;

namespace BankManagementSystem.Forms.Employee
{
    public partial class EditCustomerForm : Form
    {
        private int userId;

        public EditCustomerForm(int customerId)
        {
            InitializeComponent();
            userId = customerId;
            // cmbRole.SelectedIndex = 0;
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            string sql = "SELECT * FROM Users WHERE UserID = @UID";
            SqlParameter[] p = { new SqlParameter("@UID", userId) };
            DataTable dt = DatabaseHelper.ExecuteQuery(sql, p);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];
                txtFirstName.Text = row["FirstName"].ToString();
                txtLastName.Text = row["LastName"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtPhone.Text = row["PhoneNumber"].ToString();
                txtAddress.Text = row["Address"].ToString();
                dtpDOB.Value = Convert.ToDateTime(row["DateOfBirth"]);
                //  cmbRole.SelectedItem = row["Role"].ToString();
                txtEmail.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string fname = txtFirstName.Text.Trim();
            string lname = txtLastName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            DateTime dob = dtpDOB.Value;
            // string role = cmbRole.SelectedItem.ToString();
            string password = txtPassword.Text;

            // Update user info
            string sql = @"
                UPDATE Users 
                SET FirstName=@FName, LastName=@LName, 
                    PhoneNumber=@Phone, Address=@Address, 
                    DateOfBirth=@DOB
                WHERE UserID=@UID";

            SqlParameter[] p = {
                new SqlParameter("@FName", fname),
                new SqlParameter("@LName", lname),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@Address", address),
                new SqlParameter("@DOB", dob),
              //  new SqlParameter("@Role", role),
                new SqlParameter("@UID", userId)
            };

            DatabaseHelper.ExecuteNonQuery(sql, p);

            if (!string.IsNullOrWhiteSpace(password))
            {
                string hashed = PasswordHasher.HashPassword(password);

                string pwSQL = "UPDATE Users SET HashedPassword = @HPW WHERE UserID = @UID";
                SqlParameter[] pwP = {
                    new SqlParameter("@HPW", hashed),
                    new SqlParameter("@UID", userId)
                };

                DatabaseHelper.ExecuteNonQuery(pwSQL, pwP);
            }

            DatabaseHelper.LogAction(SessionManager.UserID, $"Edited customer UserID: {userId}");

            MessageBox.Show("Customer updated successfully.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditCustomerForm_Load(object sender, EventArgs e)
        {

        }
    }
}