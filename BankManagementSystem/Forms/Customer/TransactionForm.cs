using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankManagementSystem.Classes;

namespace BankManagementSystem.Forms.Customer
{
    public partial class TransactionForm : Form
    {
        public TransactionForm()
        {
            InitializeComponent();
        }

        private void cmbAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cmbAction.SelectedItem.ToString();
            bool isTransfer = selected == "Transfer";

            lblTarget.Visible = isTransfer;
            txtTarget.Visible = isTransfer;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid positive amount.");
                return;
            }

            string type = cmbAction.SelectedItem?.ToString();
            string targetAcc = txtTarget.Text.Trim();
            string desc = "";

            // Step 1: Get current user account
            string accSQL = "SELECT * FROM Accounts WHERE UserID = @UID";
            SqlParameter[] accParam = { new SqlParameter("@UID", SessionManager.UserID) };
            DataTable accData = DatabaseHelper.ExecuteQuery(accSQL, accParam);

            if (accData.Rows.Count == 0)
            {
                MessageBox.Show("Account not found.");
                return;
            }

            int accountId = Convert.ToInt32(accData.Rows[0]["AccountID"]);
            decimal balance = Convert.ToDecimal(accData.Rows[0]["Balance"]);

         
         using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    SqlCommand cmdUpdateOwn = new SqlCommand("", conn, transaction);
                    SqlCommand cmdInsertTxn = new SqlCommand("", conn, transaction);
                    SqlCommand cmdUpdateTarget = null;
                    SqlCommand cmdInsertTxnTarget = null;

                    if (type == "Deposit")
                    {
                        decimal newBal = balance + amount;

                        cmdUpdateOwn.CommandText = "UPDATE Accounts SET Balance = @Bal WHERE AccountID = @ID";
                        cmdUpdateOwn.Parameters.AddWithValue("@Bal", newBal);
                        cmdUpdateOwn.Parameters.AddWithValue("@ID", accountId);

                        desc = "Self deposit";
                    }
                    else if (type == "Withdraw")
                    {
                        if (balance < amount)
                        {
                            MessageBox.Show("Insufficient balance.");
                            transaction.Rollback(); return;
                        }

                        decimal newBal = balance - amount;

                        cmdUpdateOwn.CommandText = "UPDATE Accounts SET Balance = @Bal WHERE AccountID = @ID";
                        cmdUpdateOwn.Parameters.AddWithValue("@Bal", newBal);
                        cmdUpdateOwn.Parameters.AddWithValue("@ID", accountId);

                        desc = "Self withdrawal";
                    }
                    else if (type == "Transfer")
                    {
                        if (targetAcc == "")
                        {
                            MessageBox.Show("Enter target Account#.");
                            transaction.Rollback(); return;
                        }

                        // Get receiver's account ID
                        string targetSQL = "SELECT * FROM Accounts WHERE AccountNumber = @AccNum";
                        SqlCommand cmdCheck = new SqlCommand(targetSQL, conn, transaction);
                        cmdCheck.Parameters.AddWithValue("@AccNum", targetAcc);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmdCheck);
                        DataTable targetAccData = new DataTable();
                        adapter.Fill(targetAccData);

                        if (targetAccData.Rows.Count == 0)
                        {
                            MessageBox.Show("Target account not found.");
                            transaction.Rollback(); return;
                        }

                        int targetID = Convert.ToInt32(targetAccData.Rows[0]["AccountID"]);
                        decimal targetBal = Convert.ToDecimal(targetAccData.Rows[0]["Balance"]);

                        if (balance < amount)
                        {
                            MessageBox.Show("Insufficient balance.");
                            transaction.Rollback(); return;
                        }

                        // Deduct from sender
                        cmdUpdateOwn.CommandText = "UPDATE Accounts SET Balance = @Bal WHERE AccountID = @ID";
                        cmdUpdateOwn.Parameters.AddWithValue("@Bal", balance - amount);
                        cmdUpdateOwn.Parameters.AddWithValue("@ID", accountId);

                        // Add to receiver
                        cmdUpdateTarget = new SqlCommand("UPDATE Accounts SET Balance = @Bal WHERE AccountID = @ID", conn, transaction);
                        cmdUpdateTarget.Parameters.AddWithValue("@Bal", targetBal + amount);
                        cmdUpdateTarget.Parameters.AddWithValue("@ID", targetID);

                        // Log for both
                        cmdInsertTxnTarget = new SqlCommand(@"INSERT INTO Transactions (AccountID, TransactionType, Amount, Description)
                                                      VALUES (@ID, 'Deposit', @Amt, @Desc)", conn, transaction);
                        cmdInsertTxnTarget.Parameters.AddWithValue("@ID", targetID);
                        cmdInsertTxnTarget.Parameters.AddWithValue("@Amt", amount);
                        cmdInsertTxnTarget.Parameters.AddWithValue("@Desc", $"Received from {SessionManager.Name}");

                        desc = $"Transfer to Account #{targetAcc}";
                    }

                    // Run update for sender's account
                    cmdUpdateOwn.ExecuteNonQuery();

                    // Log transaction (for sender)
                    cmdInsertTxn.CommandText = @"INSERT INTO Transactions 
                (AccountID, TransactionType, Amount, Description) 
                 VALUES (@ID, @Type, @Amt, @Desc)";
                    cmdInsertTxn.Parameters.AddWithValue("@ID", accountId);
                    cmdInsertTxn.Parameters.AddWithValue("@Type", type);
                    cmdInsertTxn.Parameters.AddWithValue("@Amt", amount);
                    cmdInsertTxn.Parameters.AddWithValue("@Desc", desc);
                    cmdInsertTxn.ExecuteNonQuery();

                    // If transfer, update receiver and log it too
                    cmdUpdateTarget?.ExecuteNonQuery();
                    cmdInsertTxnTarget?.ExecuteNonQuery();

                    transaction.Commit();

                    MessageBox.Show(type + " completed successfully.");

                    // go back to dashboard
                    
                    this.Close();
                    Forms.Customer.CustomerDashboardForm dashboard = new Forms.Customer.CustomerDashboardForm();
                    dashboard.Show();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Forms.Customer.CustomerDashboardForm dashboard = new Forms.Customer.CustomerDashboardForm();
            dashboard.Show();
        }

        private void TransactionForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
