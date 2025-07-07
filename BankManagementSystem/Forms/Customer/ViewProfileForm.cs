using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BankManagementSystem.Classes;
using BankManagementSystem.Forms.Employee;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BankManagementSystem.Forms.Customer
{
    public partial class ViewProfileForm : Form
    {
        public ViewProfileForm(int userId)
        {
            InitializeComponent();
            LoadProfileInfo(userId);
        }

        private void ViewProfileForm_Load(object sender, EventArgs e)
        {
            // show user details

  


        }

        public void LoadProfileInfo(int userId)
        {
            string sql = @"SELECT u.FirstName, u.LastName, u.Email, u.PhoneNumber, u.Address, 
                          a.AccountNumber, a.Balance, a.DateOpened 
                   FROM Users u
                   INNER JOIN Accounts a ON u.UserID = a.UserID
                   WHERE u.UserID = @UserID";
            SqlParameter[] parameters = { new SqlParameter("@UserID", userId) };

            DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtFirstName.Text = row["FirstName"].ToString();
                txtLastName.Text = row["LastName"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtPhone.Text = row["PhoneNumber"].ToString();
                txtAddress.Text = row["Address"].ToString();
                txtAccountNumber.Text = row["AccountNumber"].ToString();
                txtBalance.Text = row["Balance"].ToString();
                txtDateOpened.Text = Convert.ToDateTime(row["DateOpened"]).ToString("yyyy-MM-dd");
            }
            else
            {
                MessageBox.Show("Profile information not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewProfileForm_Load_1(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Navigate back to the previous form
            this.Hide();
            CustomerDashboardForm dashboardForm = new CustomerDashboardForm();
            dashboardForm.Show();

        }

        private void lblBalance_Click(object sender, EventArgs e)
        {

        }

        private void lblAccountNumber_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditCustomerForm editForm = new EditCustomerForm(SessionManager.UserID); // Assuming SessionManager.UserID holds the current user's ID
            editForm.ShowDialog();
            // After editing, reload the profile info
            LoadProfileInfo(SessionManager.UserID); // Assuming SessionManager.UserID holds the current user's ID

            this.Show(); // Show the form again after editing

        }

        private void btnPrintTransaction_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT TransactionType, Amount, Timestamp, Description 
                          FROM Transactions 
                          WHERE AccountID = (SELECT AccountID FROM Accounts WHERE UserID = @UserID) 
                          ORDER BY Timestamp DESC";
            SqlParameter[] parameters = { new SqlParameter("@UserID", SessionManager.UserID) };

            DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

            if (dt.Rows.Count > 0)
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    saveFileDialog.Title = "Save Transaction History";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;

                        using (var document = new iTextSharp.text.Document())
                        {
                            iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                            document.Open();

                            var titleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD);
                            var headerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD);
                            var bodyFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);

                            document.Add(new iTextSharp.text.Paragraph("Transaction History", titleFont));
                            document.Add(new iTextSharp.text.Paragraph("\n"));

                            var table = new iTextSharp.text.pdf.PdfPTable(4);
                            table.AddCell(new iTextSharp.text.Phrase("Type", headerFont));
                            table.AddCell(new iTextSharp.text.Phrase("Amount", headerFont));
                            table.AddCell(new iTextSharp.text.Phrase("Timestamp", headerFont));
                            table.AddCell(new iTextSharp.text.Phrase("Description", headerFont));

                            foreach (DataRow row in dt.Rows)
                            {
                                table.AddCell(new iTextSharp.text.Phrase(row["TransactionType"].ToString(), bodyFont));
                                table.AddCell(new iTextSharp.text.Phrase(row["Amount"].ToString(), bodyFont));
                                table.AddCell(new iTextSharp.text.Phrase(row["Timestamp"].ToString(), bodyFont));
                                table.AddCell(new iTextSharp.text.Phrase(row["Description"].ToString(), bodyFont));
                            }

                            document.Add(table);
                            document.Close();
                        }

                        MessageBox.Show("Transaction history saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("No transactions found.", "Transaction History", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
