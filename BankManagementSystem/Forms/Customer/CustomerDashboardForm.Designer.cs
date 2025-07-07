
namespace BankManagementSystem.Forms.Customer
{
    partial class CustomerDashboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.gbAccountDetails = new System.Windows.Forms.GroupBox();
            this.btnViewProfile = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblBalance = new System.Windows.Forms.Label();
            this.btnMakeTxn = new System.Windows.Forms.Button();
            this.lblBalanceTitle = new System.Windows.Forms.Label();
            this.lblAccNum = new System.Windows.Forms.Label();
            this.lblAccNumTitle = new System.Windows.Forms.Label();
            this.gbTransactions = new System.Windows.Forms.GroupBox();
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.buttonClose = new System.Windows.Forms.Button();
            this.gbAccountDetails.SuspendLayout();
            this.gbTransactions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblWelcome.Location = new System.Drawing.Point(86, 90);
            this.lblWelcome.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(260, 25);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome, [Customer Name]";
            // 
            // gbAccountDetails
            // 
            this.gbAccountDetails.Controls.Add(this.btnViewProfile);
            this.gbAccountDetails.Controls.Add(this.btnLogout);
            this.gbAccountDetails.Controls.Add(this.lblBalance);
            this.gbAccountDetails.Controls.Add(this.btnMakeTxn);
            this.gbAccountDetails.Controls.Add(this.lblBalanceTitle);
            this.gbAccountDetails.Controls.Add(this.lblAccNum);
            this.gbAccountDetails.Controls.Add(this.lblAccNumTitle);
            this.gbAccountDetails.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAccountDetails.Location = new System.Drawing.Point(88, 132);
            this.gbAccountDetails.Margin = new System.Windows.Forms.Padding(2);
            this.gbAccountDetails.Name = "gbAccountDetails";
            this.gbAccountDetails.Padding = new System.Windows.Forms.Padding(2);
            this.gbAccountDetails.Size = new System.Drawing.Size(293, 368);
            this.gbAccountDetails.TabIndex = 1;
            this.gbAccountDetails.TabStop = false;
            this.gbAccountDetails.Text = "Account Details";
            // 
            // btnViewProfile
            // 
            this.btnViewProfile.BackColor = System.Drawing.Color.Teal;
            this.btnViewProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewProfile.FlatAppearance.BorderSize = 0;
            this.btnViewProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewProfile.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewProfile.ForeColor = System.Drawing.Color.White;
            this.btnViewProfile.Location = new System.Drawing.Point(20, 244);
            this.btnViewProfile.Margin = new System.Windows.Forms.Padding(2);
            this.btnViewProfile.Name = "btnViewProfile";
            this.btnViewProfile.Size = new System.Drawing.Size(167, 37);
            this.btnViewProfile.TabIndex = 5;
            this.btnViewProfile.Text = "View Profile";
            this.btnViewProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewProfile.UseVisualStyleBackColor = false;
            this.btnViewProfile.Click += new System.EventHandler(this.btnViewProfile_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(20, 312);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(167, 37);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.lblBalance.Location = new System.Drawing.Point(15, 122);
            this.lblBalance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(77, 32);
            this.lblBalance.TabIndex = 3;
            this.lblBalance.Text = "$0.00";
            this.lblBalance.Click += new System.EventHandler(this.lblBalance_Click);
            // 
            // btnMakeTxn
            // 
            this.btnMakeTxn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnMakeTxn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMakeTxn.FlatAppearance.BorderSize = 0;
            this.btnMakeTxn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMakeTxn.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMakeTxn.ForeColor = System.Drawing.Color.White;
            this.btnMakeTxn.Location = new System.Drawing.Point(20, 185);
            this.btnMakeTxn.Margin = new System.Windows.Forms.Padding(2);
            this.btnMakeTxn.Name = "btnMakeTxn";
            this.btnMakeTxn.Size = new System.Drawing.Size(167, 37);
            this.btnMakeTxn.TabIndex = 3;
            this.btnMakeTxn.Text = "Make a Transaction ➜";
            this.btnMakeTxn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMakeTxn.UseVisualStyleBackColor = false;
            this.btnMakeTxn.Click += new System.EventHandler(this.btnMakeTxn_Click);
            // 
            // lblBalanceTitle
            // 
            this.lblBalanceTitle.AutoSize = true;
            this.lblBalanceTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblBalanceTitle.Location = new System.Drawing.Point(16, 102);
            this.lblBalanceTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBalanceTitle.Name = "lblBalanceTitle";
            this.lblBalanceTitle.Size = new System.Drawing.Size(111, 19);
            this.lblBalanceTitle.TabIndex = 2;
            this.lblBalanceTitle.Text = "Current Balance:";
            // 
            // lblAccNum
            // 
            this.lblAccNum.AutoSize = true;
            this.lblAccNum.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.lblAccNum.Location = new System.Drawing.Point(15, 61);
            this.lblAccNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAccNum.Name = "lblAccNum";
            this.lblAccNum.Size = new System.Drawing.Size(103, 21);
            this.lblAccNum.TabIndex = 1;
            this.lblAccNum.Text = "000-000-000";
            // 
            // lblAccNumTitle
            // 
            this.lblAccNumTitle.AutoSize = true;
            this.lblAccNumTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccNumTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblAccNumTitle.Location = new System.Drawing.Point(16, 41);
            this.lblAccNumTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAccNumTitle.Name = "lblAccNumTitle";
            this.lblAccNumTitle.Size = new System.Drawing.Size(120, 19);
            this.lblAccNumTitle.TabIndex = 0;
            this.lblAccNumTitle.Text = "Account Number:";
            // 
            // gbTransactions
            // 
            this.gbTransactions.Controls.Add(this.dgvTransactions);
            this.gbTransactions.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTransactions.Location = new System.Drawing.Point(403, 132);
            this.gbTransactions.Margin = new System.Windows.Forms.Padding(2);
            this.gbTransactions.Name = "gbTransactions";
            this.gbTransactions.Padding = new System.Windows.Forms.Padding(8);
            this.gbTransactions.Size = new System.Drawing.Size(546, 368);
            this.gbTransactions.TabIndex = 2;
            this.gbTransactions.TabStop = false;
            this.gbTransactions.Text = "Recent Transactions";
            // 
            // dgvTransactions
            // 
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AllowUserToDeleteRows = false;
            this.dgvTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransactions.BackgroundColor = System.Drawing.Color.White;
            this.dgvTransactions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTransactions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTransactions.ColumnHeadersHeight = 40;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTransactions.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTransactions.EnableHeadersVisualStyles = false;
            this.dgvTransactions.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvTransactions.Location = new System.Drawing.Point(8, 27);
            this.dgvTransactions.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.RowHeadersVisible = false;
            this.dgvTransactions.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.dgvTransactions.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTransactions.RowTemplate.Height = 35;
            this.dgvTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransactions.Size = new System.Drawing.Size(530, 333);
            this.dgvTransactions.TabIndex = 0;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackColor = System.Drawing.Color.Transparent;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.buttonClose.ForeColor = System.Drawing.SystemColors.InfoText;
            this.buttonClose.Location = new System.Drawing.Point(985, 12);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(27, 30);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "X";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // CustomerDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 600);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.gbTransactions);
            this.Controls.Add(this.gbAccountDetails);
            this.Controls.Add(this.lblWelcome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "CustomerDashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Dashboard";
            this.Load += new System.EventHandler(this.CustomerDashboardForm_Load);
            this.gbAccountDetails.ResumeLayout(false);
            this.gbAccountDetails.PerformLayout();
            this.gbTransactions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.GroupBox gbAccountDetails;
        private System.Windows.Forms.Label lblAccNumTitle;
        private System.Windows.Forms.Label lblAccNum;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblBalanceTitle;
        private System.Windows.Forms.GroupBox gbTransactions;
        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.Button btnMakeTxn;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button btnViewProfile;
    }
}