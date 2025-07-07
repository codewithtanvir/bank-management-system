using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BankManagementSystem.Forms.Employee;
using BankManagementSystem.Forms.Customer;
using BankManagementSystem.Forms.Admin;
using BankManagementSystem.Classes;

namespace BankManagementSystem.Forms.Admin
{
    public partial class AdminDashboardForm : Form
    {
        public AdminDashboardForm()
        {
            InitializeComponent();
        }

        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            string sql = "SELECT UserID, FirstName, LastName, Email, Role, PhoneNumber, IsActive FROM Users WHERE Role IN ('Employee', 'Admin')";
            DataTable dt = DatabaseHelper.ExecuteQuery(sql);

            dgvEmployees.DataSource = dt;
            dgvEmployees.Columns["IsActive"].Visible = true; // Optional: hide IsActive
            dgvEmployees.ReadOnly = true;
            dgvEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ManageEmployeeForm addForm = new ManageEmployeeForm(); // Add Mode
            addForm.ShowDialog();
            LoadEmployeeData(); // Refresh grid
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 1)
            {
                int selectedUserId = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells["UserID"].Value);

                ManageEmployeeForm editForm = new ManageEmployeeForm(selectedUserId); // Edit Mode
                editForm.ShowDialog();
                LoadEmployeeData(); // Refresh
            }
            else
            {
                MessageBox.Show("Please select a user to edit.");
            }
        }

        private void btnDeactive_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 1)
            {
                int selectedUserId = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells["UserID"].Value);

                DialogResult result = MessageBox.Show(
                    "Are you sure you want to disable this user?",
                    "Confirm Deactivation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    string sql = "UPDATE Users SET IsActive = 0 WHERE UserID = @UID";
                    SqlParameter[] param = { new SqlParameter("@UID", selectedUserId) };

                    DatabaseHelper.ExecuteNonQuery(sql, param);

                    // Log the action
                    DatabaseHelper.LogAction(SessionManager.UserID, $"Deactivated UserID: {selectedUserId}");

                    LoadEmployeeData(); // Refresh
                    MessageBox.Show("User has been deactivated.");
                }
            }
            else
            {
                MessageBox.Show("Please select a user to deactivate.");
            }
        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            AuditLogForm logForm = new AuditLogForm();
            logForm.ShowDialog();
        }

        private void dgvEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // You can optionally add double-click to edit logic here if needed
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();

        }

        private void btnReActive_Click(object sender, EventArgs e)
        {
            // Re-activate a user
            if (dgvEmployees.SelectedRows.Count == 1)
            {
                int userId = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells["UserID"].Value);
                DialogResult confirm = MessageBox.Show(
                    "Are you sure you want to reactivate this user?",
                    "Confirm Reactivation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (confirm == DialogResult.Yes)
                {
                    string sql = "UPDATE Users SET IsActive = 1 WHERE UserID = @UserID";
                    SqlParameter[] parameters = { new SqlParameter("@UserID", userId) };
                    DatabaseHelper.ExecuteNonQuery(sql, parameters);
                    DatabaseHelper.LogAction(SessionManager.UserID, $"Reactivated user ID: {userId}");
                    LoadEmployeeData();
                    MessageBox.Show("User has been reactivated.");
                }
            }
            else
            {
                MessageBox.Show("Please select one user to reactivate.");



            }
    }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 1)
            {
                int userId = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells["UserID"].Value);
                DialogResult confirm = MessageBox.Show(
                    "Are you sure you want to delete this user? This action cannot be undone.",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        // Delete dependent records in AuditLogs
                        string deleteAuditLogsSql = "DELETE FROM AuditLogs WHERE UserID = @UserID";
                        SqlParameter[] auditLogParams = { new SqlParameter("@UserID", userId) };
                        DatabaseHelper.ExecuteNonQuery(deleteAuditLogsSql, auditLogParams);

                        // Delete dependent records in Accounts
                        string deleteAccountsSql = "DELETE FROM Accounts WHERE UserID = @UserID";
                        SqlParameter[] accountParams = { new SqlParameter("@UserID", userId) };
                        DatabaseHelper.ExecuteNonQuery(deleteAccountsSql, accountParams);

                        // Delete user record
                        string deleteUserSql = "DELETE FROM Users WHERE UserID = @UserID";
                        SqlParameter[] userParams = { new SqlParameter("@UserID", userId) };
                        DatabaseHelper.ExecuteNonQuery(deleteUserSql, userParams);

                        DatabaseHelper.LogAction(SessionManager.UserID, $"Deleted user ID: {userId}");
                        LoadEmployeeData();
                        MessageBox.Show("User has been deleted.");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"An error occurred while deleting the user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select one user to delete.");
            }


        }
    }
 
}
