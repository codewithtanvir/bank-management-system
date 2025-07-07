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
    public partial class CustomerDashboardForm : Form
    {
        public CustomerDashboardForm()
        {
            InitializeComponent();
        }

        private void btnMakeTxn_Click(object sender, EventArgs e)
        {
            this.Hide();
            TransactionForm txnForm = new TransactionForm();
            txnForm.Show();
        }

        private void CustomerDashboardForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Welcome, " + SessionManager.Name;

            string sqlAccount = "SELECT * FROM Accounts WHERE UserID = @UserID";
            SqlParameter[] param = { new SqlParameter("@UserID", SessionManager.UserID) };

            DataTable accountData = DatabaseHelper.ExecuteQuery(sqlAccount, param);

            if (accountData.Rows.Count > 0)
            {
                string accountNum = accountData.Rows[0]["AccountNumber"].ToString();
                decimal balance = Convert.ToDecimal(accountData.Rows[0]["Balance"]);

                // Display in labels
                lblAccNum.Text = accountNum;
                lblBalance.Text = balance.ToString("C");

                int accountId = Convert.ToInt32(accountData.Rows[0]["AccountID"]);

                // Load recent transactions
                LoadTransactions(accountId);
            }
            else
            {
                MessageBox.Show("No account found for this user.");
            }
        }

        private void LoadTransactions(int accountId)
        {
            string txnSQL = @"SELECT TOP 10 TransactionType, Amount, Timestamp, Description 
                      FROM Transactions 
                      WHERE AccountID = @AID 
                      ORDER BY Timestamp DESC";
            SqlParameter[] param = { new SqlParameter("@AID", accountId) };

            dgvTransactions.DataSource = DatabaseHelper.ExecuteQuery(txnSQL, param);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblBalance_Click(object sender, EventArgs e)
        {

        }

        private void btnViewProfile_Click(object sender, EventArgs e)
        {
            //this.Hide();
            ViewProfileForm profileForm = new ViewProfileForm(SessionManager.UserID);
            profileForm.Show();


        }
    }
}
