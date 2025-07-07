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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace BankManagementSystem.Forms.Employee
{
    public partial class ViewCustomerForm : Form
    {
        public ViewCustomerForm()
        {
            InitializeComponent();
        }

        private void ViewCustomerForm_Load(object sender, EventArgs e)
        {

            string userSQL = "SELECT * FROM Users WHERE UserID = @UID";
            SqlParameter[] userParam = { new SqlParameter("@UID", _userId) };
            DataTable userTable = DatabaseHelper.ExecuteQuery(userSQL, userParam);

            if (userTable.Rows.Count == 1)
            {
                var user = userTable.Rows[0];

               
            }

            // 2️⃣ Get Account Info
            string accSQL = "SELECT TOP 1 * FROM Accounts WHERE UserID = @UID";
            DataTable accTable = DatabaseHelper.ExecuteQuery(accSQL, userParam);

            if (accTable.Rows.Count > 0)
            {
                var acc = accTable.Rows[0];
                

                int accId = Convert.ToInt32(acc["AccountID"]);

                // 3️⃣ Load Transactions
                string txnSQL = @"SELECT TOP 10 TransactionType, Amount, Timestamp, Description
                          FROM Transactions 
                          WHERE AccountID = @AID 
                          ORDER BY Timestamp DESC";
                SqlParameter[] txnParam = { new SqlParameter("@AID", accId) };

                dgvTransactions.DataSource = DatabaseHelper.ExecuteQuery(txnSQL, txnParam);
            }
        }

        private int _userId;

        public ViewCustomerForm(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Close the form and return to the previous screen
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
