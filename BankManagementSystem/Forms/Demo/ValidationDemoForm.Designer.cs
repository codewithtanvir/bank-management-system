namespace BankManagementSystem.Forms.Demo
{
    partial class ValidationDemoForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCustomerRegistration = new System.Windows.Forms.TabPage();
            this.btnDemoRegistration = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabTransactionValidation = new System.Windows.Forms.TabPage();
            this.btnDemoTransaction = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbTransactionType = new System.Windows.Forms.ComboBox();
            this.txtTransactionAmount = new System.Windows.Forms.TextBox();
            this.txtAccountNumber = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabDataFlow = new System.Windows.Forms.TabPage();
            this.btnDemoDataFlow = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnPopulateSample = new System.Windows.Forms.Button();
            this.txtValidationLog = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabCustomerRegistration.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabTransactionValidation.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabDataFlow.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCustomerRegistration);
            this.tabControl1.Controls.Add(this.tabTransactionValidation);
            this.tabControl1.Controls.Add(this.tabDataFlow);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1024, 300);
            this.tabControl1.TabIndex = 0;
            // 
            // tabCustomerRegistration
            // 
            this.tabCustomerRegistration.Controls.Add(this.btnDemoRegistration);
            this.tabCustomerRegistration.Controls.Add(this.groupBox1);
            this.tabCustomerRegistration.Location = new System.Drawing.Point(4, 22);
            this.tabCustomerRegistration.Name = "tabCustomerRegistration";
            this.tabCustomerRegistration.Padding = new System.Windows.Forms.Padding(3);
            this.tabCustomerRegistration.Size = new System.Drawing.Size(1016, 274);
            this.tabCustomerRegistration.TabIndex = 0;
            this.tabCustomerRegistration.Text = "Customer Registration Validation";
            this.tabCustomerRegistration.UseVisualStyleBackColor = true;
            // 
            // btnDemoRegistration
            // 
            this.btnDemoRegistration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnDemoRegistration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDemoRegistration.ForeColor = System.Drawing.Color.White;
            this.btnDemoRegistration.Location = new System.Drawing.Point(550, 200);
            this.btnDemoRegistration.Name = "btnDemoRegistration";
            this.btnDemoRegistration.Size = new System.Drawing.Size(200, 40);
            this.btnDemoRegistration.TabIndex = 8;
            this.btnDemoRegistration.Text = "Demo Registration Validation";
            this.btnDemoRegistration.UseVisualStyleBackColor = false;
            this.btnDemoRegistration.Click += new System.EventHandler(this.btnDemoRegistration_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpDateOfBirth);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.txtPhone);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.txtLastName);
            this.groupBox1.Controls.Add(this.txtFirstName);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 250);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Customer Information";
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateOfBirth.Location = new System.Drawing.Point(120, 210);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.Size = new System.Drawing.Size(350, 20);
            this.dtpDateOfBirth.TabIndex = 7;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(120, 150);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(350, 50);
            this.txtAddress.TabIndex = 6;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(120, 120);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(350, 20);
            this.txtPhone.TabIndex = 5;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(120, 90);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(350, 20);
            this.txtPassword.TabIndex = 4;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(120, 60);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(350, 20);
            this.txtEmail.TabIndex = 3;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(320, 30);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(150, 20);
            this.txtLastName.TabIndex = 2;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(120, 30);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(150, 20);
            this.txtFirstName.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 210);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Date of Birth:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Address:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Phone Number:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Email:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(276, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Last Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Name:";
            // 
            // tabTransactionValidation
            // 
            this.tabTransactionValidation.Controls.Add(this.btnDemoTransaction);
            this.tabTransactionValidation.Controls.Add(this.groupBox2);
            this.tabTransactionValidation.Location = new System.Drawing.Point(4, 22);
            this.tabTransactionValidation.Name = "tabTransactionValidation";
            this.tabTransactionValidation.Padding = new System.Windows.Forms.Padding(3);
            this.tabTransactionValidation.Size = new System.Drawing.Size(1016, 274);
            this.tabTransactionValidation.TabIndex = 1;
            this.tabTransactionValidation.Text = "Transaction Validation";
            this.tabTransactionValidation.UseVisualStyleBackColor = true;
            // 
            // btnDemoTransaction
            // 
            this.btnDemoTransaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnDemoTransaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDemoTransaction.ForeColor = System.Drawing.Color.White;
            this.btnDemoTransaction.Location = new System.Drawing.Point(550, 50);
            this.btnDemoTransaction.Name = "btnDemoTransaction";
            this.btnDemoTransaction.Size = new System.Drawing.Size(200, 40);
            this.btnDemoTransaction.TabIndex = 4;
            this.btnDemoTransaction.Text = "Demo Transaction Validation";
            this.btnDemoTransaction.UseVisualStyleBackColor = false;
            this.btnDemoTransaction.Click += new System.EventHandler(this.btnDemoTransaction_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbTransactionType);
            this.groupBox2.Controls.Add(this.txtTransactionAmount);
            this.groupBox2.Controls.Add(this.txtAccountNumber);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 120);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transaction Information";
            // 
            // cmbTransactionType
            // 
            this.cmbTransactionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransactionType.FormattingEnabled = true;
            this.cmbTransactionType.Location = new System.Drawing.Point(120, 80);
            this.cmbTransactionType.Name = "cmbTransactionType";
            this.cmbTransactionType.Size = new System.Drawing.Size(350, 21);
            this.cmbTransactionType.TabIndex = 3;
            // 
            // txtTransactionAmount
            // 
            this.txtTransactionAmount.Location = new System.Drawing.Point(120, 50);
            this.txtTransactionAmount.Name = "txtTransactionAmount";
            this.txtTransactionAmount.Size = new System.Drawing.Size(350, 20);
            this.txtTransactionAmount.TabIndex = 2;
            // 
            // txtAccountNumber
            // 
            this.txtAccountNumber.Location = new System.Drawing.Point(120, 20);
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.Size = new System.Drawing.Size(350, 20);
            this.txtAccountNumber.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Transaction Type:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Amount:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Account Number:";
            // 
            // tabDataFlow
            // 
            this.tabDataFlow.Controls.Add(this.btnDemoDataFlow);
            this.tabDataFlow.Controls.Add(this.label11);
            this.tabDataFlow.Location = new System.Drawing.Point(4, 22);
            this.tabDataFlow.Name = "tabDataFlow";
            this.tabDataFlow.Size = new System.Drawing.Size(1016, 274);
            this.tabDataFlow.TabIndex = 2;
            this.tabDataFlow.Text = "Data Flow Demonstration";
            this.tabDataFlow.UseVisualStyleBackColor = true;
            // 
            // btnDemoDataFlow
            // 
            this.btnDemoDataFlow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDemoDataFlow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDemoDataFlow.ForeColor = System.Drawing.Color.White;
            this.btnDemoDataFlow.Location = new System.Drawing.Point(20, 100);
            this.btnDemoDataFlow.Name = "btnDemoDataFlow";
            this.btnDemoDataFlow.Size = new System.Drawing.Size(200, 40);
            this.btnDemoDataFlow.TabIndex = 1;
            this.btnDemoDataFlow.Text = "Demo Complete Data Flow";
            this.btnDemoDataFlow.UseVisualStyleBackColor = false;
            this.btnDemoDataFlow.Click += new System.EventHandler(this.btnDemoDataFlow_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(20, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(800, 64);
            this.label11.TabIndex = 0;
            this.label11.Text = "This demonstration shows the complete data flow process from user input to datab" +
    "ase storage.\r\nIt includes all validation layers, business logic processing, and " +
    "error handling.\r\n\r\nClick the button below to see the step-by-step process in ac" +
    "tion.";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClearLog);
            this.panel1.Controls.Add(this.btnPopulateSample);
            this.panel1.Controls.Add(this.txtValidationLog);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 300);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 368);
            this.panel1.TabIndex = 1;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(100, 30);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(80, 23);
            this.btnClearLog.TabIndex = 3;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnPopulateSample
            // 
            this.btnPopulateSample.Location = new System.Drawing.Point(200, 30);
            this.btnPopulateSample.Name = "btnPopulateSample";
            this.btnPopulateSample.Size = new System.Drawing.Size(120, 23);
            this.btnPopulateSample.TabIndex = 2;
            this.btnPopulateSample.Text = "Populate Sample Data";
            this.btnPopulateSample.UseVisualStyleBackColor = true;
            this.btnPopulateSample.Click += new System.EventHandler(this.btnPopulateSample_Click);
            // 
            // txtValidationLog
            // 
            this.txtValidationLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValidationLog.BackColor = System.Drawing.Color.Black;
            this.txtValidationLog.ForeColor = System.Drawing.Color.Lime;
            this.txtValidationLog.Location = new System.Drawing.Point(12, 60);
            this.txtValidationLog.Multiline = true;
            this.txtValidationLog.Name = "txtValidationLog";
            this.txtValidationLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtValidationLog.Size = new System.Drawing.Size(1000, 296);
            this.txtValidationLog.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(12, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(201, 16);
            this.label12.TabIndex = 0;
            this.label12.Text = "Validation Results and Logs:";
            // 
            // ValidationDemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 668);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ValidationDemoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bank Management System - Validation & Data Flow Demonstration";
            this.tabControl1.ResumeLayout(false);
            this.tabCustomerRegistration.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabTransactionValidation.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabDataFlow.ResumeLayout(false);
            this.tabDataFlow.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCustomerRegistration;
        private System.Windows.Forms.TabPage tabTransactionValidation;
        private System.Windows.Forms.TabPage tabDataFlow;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtValidationLog;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDemoRegistration;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbTransactionType;
        private System.Windows.Forms.TextBox txtTransactionAmount;
        private System.Windows.Forms.TextBox txtAccountNumber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnDemoTransaction;
        private System.Windows.Forms.Button btnDemoDataFlow;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnPopulateSample;
    }
}
