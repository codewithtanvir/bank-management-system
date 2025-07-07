using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankManagementSystem.Forms.Admin
{
    public partial class AuditLogForm : Form
    {
        public AuditLogForm()
        {
            InitializeComponent();
        }

        private void dgvAuditLogs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AuditLogForm_Load(object sender, EventArgs e)
        {
            string sql = @"
        SELECT l.LogID, l.UserID, u.FirstName + ' ' + u.LastName AS PerformedBy, 
               l.Action, l.Timestamp
        FROM AuditLogs l
        INNER JOIN Users u ON l.UserID = u.UserID
        ORDER BY l.Timestamp DESC";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql);
            dgvAuditLogs.DataSource = dt;

            // Optional: Rename headers in code
            dgvAuditLogs.Columns["LogID"].HeaderText = "ID";
            dgvAuditLogs.Columns["UserID"].HeaderText = "User ID";
            dgvAuditLogs.Columns["PerformedBy"].HeaderText = "Performed By";
            dgvAuditLogs.Columns["Action"].HeaderText = "Action";
            dgvAuditLogs.Columns["Timestamp"].HeaderText = "Date / Time";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
