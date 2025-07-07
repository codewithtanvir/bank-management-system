using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using BankManagementSystem.Forms.Employee;
using BankManagementSystem.Classes;


namespace BankManagementSystem.Forms.Employee
{
    public partial class EmployeeDashboardForm : Form
    {
        public EmployeeDashboardForm()
        {
            InitializeComponent();
        }

        private void EmployeeDashboardForm_Load(object sender, EventArgs e)
        {
            cmbStatus.SelectedIndex = 0;
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            string sql = @"
                SELECT 
                    u.UserID, 
                    u.FirstName, 
                    u.LastName, 
                    u.Email, 
                    u.PhoneNumber, 
                    a.AccountNumber, 
                    a.Balance, 
                    u.IsActive
                FROM Users u
                INNER JOIN Accounts a ON u.UserID = a.UserID
                WHERE u.Role = 'Customer'";

            var parameters = new System.Collections.Generic.List<SqlParameter>();

            if (cmbStatus.SelectedItem != null && cmbStatus.SelectedItem.ToString() != "All")
            {
                sql += " AND u.IsActive = @status";
                int status = cmbStatus.SelectedItem.ToString() == "Active" ? 1 : 0;
                parameters.Add(new SqlParameter("@status", status));
            }

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                sql += " AND (u.FirstName LIKE @search OR u.LastName LIKE @search OR u.Email LIKE @search)";
                parameters.Add(new SqlParameter("@search", "%" + txtSearch.Text.Trim() + "%"));
            }

            DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters.ToArray());
            dgvCustomers.DataSource = dt;

            if (dgvCustomers.Columns["IsActive"] != null)
                dgvCustomers.Columns["IsActive"].Visible = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbStatus.SelectedIndex = 0;
            LoadCustomers();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "CSV (*.csv)|*.csv",
                    FileName = "CustomerList.csv"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        // Headers
                        for (int i = 0; i < dgvCustomers.Columns.Count; i++)
                        {
                            sw.Write(dgvCustomers.Columns[i].HeaderText);
                            if (i < dgvCustomers.Columns.Count - 1)
                                sw.Write(",");
                        }
                        sw.WriteLine();

                        // Rows
                        foreach (DataGridViewRow row in dgvCustomers.Rows)
                        {
                            for (int i = 0; i < row.Cells.Count; i++)
                            {
                                sw.Write(row.Cells[i].Value?.ToString());
                                if (i < row.Cells.Count - 1)
                                    sw.Write(",");
                            }
                            sw.WriteLine();
                        }
                    }

                    MessageBox.Show("Exported successfully.");
                }
            }
            else
            {
                MessageBox.Show("No data to export.");
            }
        }

        private void dgvCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int userId = Convert.ToInt32(dgvCustomers.Rows[e.RowIndex].Cells["UserID"].Value);

                // Ensure ViewCustomerForm displays transactions

                //viewForm.ShowDialog();
               
                ViewCustomerForm viewForm = new ViewCustomerForm(userId);
                viewForm.ShowDialog();
                // Optionally refresh the customer list after viewing

            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optional: Add logic to auto-filter
            LoadCustomers();
        }



        private void btnEdit_Click(object sender, EventArgs e)
        {
            LoadCustomers();
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["UserID"].Value);
                EditCustomerForm editCustomerForm = new EditCustomerForm(userId);
                if (editCustomerForm.ShowDialog() == DialogResult.OK)
                {
                    // Refresh the customer list after editing
                    LoadCustomers();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to edit.");
            }



        }

        private void btnDeactive_Click(object sender, EventArgs e)
        {
            LoadCustomers();
            if (dgvCustomers.SelectedRows.Count == 1)
            {
                int userId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["UserID"].Value);

                DialogResult confirm = MessageBox.Show(
                    "Are you sure you want to deactivate this customer?",
                    "Confirm Deactivation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirm == DialogResult.Yes)
                {
                    string sql = "UPDATE Users SET IsActive = 0 WHERE UserID = @UserID";
                    SqlParameter[] parameters = { new SqlParameter("@UserID", userId) };
                    DatabaseHelper.ExecuteNonQuery(sql, parameters);
                    DatabaseHelper.LogAction(SessionManager.UserID, $"Deactivated customer ID: {userId}");
                    LoadCustomers();
                    MessageBox.Show("Customer has been deactivated.");
                }
            }
            else
            {
                MessageBox.Show("Please select one customer to deactivate.");
            }


        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            AddCustomerForm addCustomerForm = new AddCustomerForm();
            addCustomerForm.ShowDialog();
            LoadCustomers(); // Refresh customer list
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 1)
            {
                int userId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["UserID"].Value);
                EditCustomerForm editCustomerForm = new EditCustomerForm(userId);
                editCustomerForm.ShowDialog();
                LoadCustomers(); // Refresh customer list
            }
            else
            {
                MessageBox.Show("Please select a customer to edit.");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            AddCustomerForm addCustomerForm = new AddCustomerForm();
            if (addCustomerForm.ShowDialog() == DialogResult.OK)
            {
                LoadCustomers(); // Refresh customer list after adding
            }
            else
            {
                MessageBox.Show("Customer addition cancelled.");

            }
        }

        private void btnReactiveAcc_Click(object sender, EventArgs e)
        {
            // Ensure the user has selected a customer to reactivate
            if (dgvCustomers.SelectedRows.Count == 1)
            {
                int userId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["UserID"].Value);
                DialogResult confirm = MessageBox.Show(
                    "Are you sure you want to reactivate this customer?",
                    "Confirm Reactivation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                if (confirm == DialogResult.Yes)
                {
                    string sql = "UPDATE Users SET IsActive = 1 WHERE UserID = @UserID";
                    SqlParameter[] parameters = { new SqlParameter("@UserID", userId) };
                    DatabaseHelper.ExecuteNonQuery(sql, parameters);
                    DatabaseHelper.LogAction(SessionManager.UserID, $"Reactivated customer ID: {userId}");
                    LoadCustomers();
                    MessageBox.Show("Customer has been reactivated.");
                }
            }
            else
            {
                MessageBox.Show("Please select one customer to reactivate.");
            }

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 1)
            {
                int userId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["UserID"].Value);
                DialogResult confirm = MessageBox.Show(
                    "Are you sure you want to delete this customer? This action cannot be undone.",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        // Delete dependent records in Transactions
                        string deleteTransactionsSql = "DELETE FROM Transactions WHERE AccountID IN (SELECT AccountID FROM Accounts WHERE UserID = @UserID)";
                        SqlParameter[] transactionParams = { new SqlParameter("@UserID", userId) };
                        DatabaseHelper.ExecuteNonQuery(deleteTransactionsSql, transactionParams);

                        // Delete dependent records in Accounts
                        string deleteAccountsSql = "DELETE FROM Accounts WHERE UserID = @UserID";
                        SqlParameter[] accountParams = { new SqlParameter("@UserID", userId) };
                        DatabaseHelper.ExecuteNonQuery(deleteAccountsSql, accountParams);

                        // Delete user record
                        string deleteUserSql = "DELETE FROM Users WHERE UserID = @UserID";
                        SqlParameter[] userParams = { new SqlParameter("@UserID", userId) };
                        DatabaseHelper.ExecuteNonQuery(deleteUserSql, userParams);

                        DatabaseHelper.LogAction(SessionManager.UserID, $"Deleted customer ID: {userId}");
                        LoadCustomers();
                        MessageBox.Show("Customer has been deleted.");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"An error occurred while deleting the customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select one customer to delete.");
            }


        }
    }
}
