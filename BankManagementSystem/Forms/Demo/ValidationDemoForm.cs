using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankManagementSystem.Classes;

namespace BankManagementSystem.Forms.Demo
{
    /// <summary>
    /// Demonstration Form showcasing advanced validation and real-life banking scenarios
    /// This form demonstrates proper functional requirements implementation
    /// </summary>
    public partial class ValidationDemoForm : Form
    {
        private BankService _bankService;
        private ValidationResult _lastValidationResult;

        public ValidationDemoForm()
        {
            InitializeComponent();
            _bankService = new BankService();
            InitializeDemo();
        }

        private void InitializeDemo()
        {
            // Setup demo scenarios
            LoadDemoScenarios();
            SetupValidationDisplays();
            PopulateSampleData();
        }

        #region Demo Scenario 1: Customer Registration Validation

        private void btnDemoRegistration_Click(object sender, EventArgs e)
        {
            ClearValidationResults();
            
            try
            {
                // Create user from form data
                var user = new User
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    PhoneNumber = txtPhone.Text,
                    Address = txtAddress.Text,
                    DateOfBirth = dtpDateOfBirth.Value,
                    Role = UserRole.Customer
                };

                // Demonstrate multi-layer validation
                ShowValidationStep("Step 1: Input Format Validation");
                var inputValidation = ValidateInputFormats(user);
                DisplayValidationResults("Input Validation", inputValidation);

                if (inputValidation.IsValid)
                {
                    ShowValidationStep("Step 2: Business Rule Validation");
                    var businessValidation = AccountValidator.ValidateUserRegistration(user);
                    DisplayValidationResults("Business Rules", businessValidation);

                    if (businessValidation.IsValid)
                    {
                        ShowValidationStep("Step 3: Password Security Validation");
                        var passwordValidation = AccountValidator.ValidatePassword(txtPassword.Text);
                        DisplayValidationResults("Password Security", passwordValidation);

                        if (passwordValidation.IsValid)
                        {
                            ShowValidationStep("Step 4: Database Integrity Validation");
                            var dbValidation = ValidateDatabaseIntegrity(user);
                            DisplayValidationResults("Database Integrity", dbValidation);

                            if (dbValidation.IsValid)
                            {
                                ShowSuccessMessage("✅ All validations passed! Customer can be registered.");
                                LogValidationSuccess("Customer Registration", user.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Validation Error: {ex.Message}");
                LogValidationError("Customer Registration", ex.Message);
            }
        }

        #endregion

        #region Demo Scenario 2: Transaction Validation

        private void btnDemoTransaction_Click(object sender, EventArgs e)
        {
            ClearValidationResults();

            try
            {
                string accountNumber = txtAccountNumber.Text;
                decimal amount = decimal.Parse(txtTransactionAmount.Text);
                TransactionType transactionType = (TransactionType)cmbTransactionType.SelectedItem;

                ShowValidationStep("Step 1: Account Status Validation");
                var accountValidation = ValidateAccountStatus(accountNumber);
                DisplayValidationResults("Account Status", accountValidation);

                if (accountValidation.IsValid)
                {
                    ShowValidationStep("Step 2: Transaction Amount Validation");
                    var amountValidation = ValidateTransactionAmount(amount, transactionType);
                    DisplayValidationResults("Amount Validation", amountValidation);

                    if (amountValidation.IsValid)
                    {
                        ShowValidationStep("Step 3: Business Rule Compliance");
                        var businessValidation = ValidateTransactionBusinessRules(accountNumber, amount, transactionType);
                        DisplayValidationResults("Business Rules", businessValidation);

                        if (businessValidation.IsValid)
                        {
                            ShowValidationStep("Step 4: Risk Assessment");
                            var riskAssessment = PerformRiskAssessment(accountNumber, amount, transactionType);
                            DisplayRiskAssessment(riskAssessment);

                            ShowSuccessMessage("✅ Transaction validation completed successfully!");
                            LogValidationSuccess("Transaction", $"{transactionType} of {amount:C} on {accountNumber}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Transaction Validation Error: {ex.Message}");
                LogValidationError("Transaction", ex.Message);
            }
        }

        #endregion

        #region Demo Scenario 3: Data Flow Demonstration

        private void btnDemoDataFlow_Click(object sender, EventArgs e)
        {
            ClearValidationResults();
            
            ShowValidationStep("Demonstrating Complete Data Flow Process");

            try
            {
                // Step 1: User Input Collection
                ShowDataFlowStep("1. User Input Collection", "Collecting data from form controls");
                var customerInfo = CollectCustomerRegistrationData();

                // Step 2: Client-Side Validation
                ShowDataFlowStep("2. Client-Side Validation", "Validating input formats and required fields");
                var clientValidation = PerformClientSideValidation(customerInfo);
                
                if (!clientValidation.IsValid)
                {
                    ShowDataFlowError("Client-side validation failed", clientValidation.GetErrorMessage());
                    return;
                }

                // Step 3: Business Logic Processing
                ShowDataFlowStep("3. Business Logic Processing", "Applying business rules and constraints");
                var businessResult = ProcessBusinessLogic(customerInfo);
                
                if (!businessResult.IsSuccess)
                {
                    ShowDataFlowError("Business logic validation failed", businessResult.GetErrorMessage());
                    return;
                }

                // Step 4: Data Access Layer
                ShowDataFlowStep("4. Data Access Layer", "Preparing database operations");
                var dataAccessResult = PrepareDataAccess(customerInfo);
                
                if (!dataAccessResult.IsValid)
                {
                    ShowDataFlowError("Data access preparation failed", dataAccessResult.GetErrorMessage());
                    return;
                }

                // Step 5: Database Operations (Simulated)
                ShowDataFlowStep("5. Database Operations", "Executing database transactions");
                var dbResult = SimulateDatabaseOperations(customerInfo);
                
                if (!dbResult.IsSuccess)
                {
                    ShowDataFlowError("Database operations failed", dbResult.GetErrorMessage());
                    return;
                }

                // Step 6: Response Generation
                ShowDataFlowStep("6. Response Generation", "Generating response and logging");
                GenerateSuccessResponse(dbResult.Data);

                ShowSuccessMessage("✅ Complete data flow demonstration successful!");
            }
            catch (Exception ex)
            {
                ShowDataFlowError("System Error", ex.Message);
            }
        }

        #endregion

        #region Validation Helper Methods

        private ValidationResult ValidateInputFormats(User user)
        {
            var result = new ValidationResult();

            // Name format validation
            if (!System.Text.RegularExpressions.Regex.IsMatch(user.FirstName, @"^[a-zA-Z\s\-']+$"))
                result.AddError("First name contains invalid characters");

            if (!System.Text.RegularExpressions.Regex.IsMatch(user.LastName, @"^[a-zA-Z\s\-']+$"))
                result.AddError("Last name contains invalid characters");

            // Email format validation
            if (!System.Text.RegularExpressions.Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                result.AddError("Invalid email format");

            // Phone format validation
            if (!System.Text.RegularExpressions.Regex.IsMatch(user.PhoneNumber, @"^\d{11}$"))
                result.AddError("Phone number must be exactly 11 digits");

            return result;
        }

        private ValidationResult ValidateDatabaseIntegrity(User user)
        {
            var result = new ValidationResult();

            // Simulate database checks
            System.Threading.Thread.Sleep(500); // Simulate database query time

            // Check email uniqueness (simulated)
            if (user.Email.ToLower().Contains("existing"))
                result.AddError("Email already exists in database");

            // Check referential integrity
            if (string.IsNullOrEmpty(user.PhoneNumber))
                result.AddError("Phone number is required for database integrity");

            return result;
        }

        private ValidationResult ValidateAccountStatus(string accountNumber)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                result.AddError("Account number is required");
                return result;
            }

            // Simulate account lookup
            if (accountNumber.ToUpper().Contains("INACTIVE"))
                result.AddError("Account is inactive and cannot perform transactions");

            if (accountNumber.ToUpper().Contains("FROZEN"))
                result.AddError("Account is frozen due to suspicious activity");

            if (accountNumber.ToUpper().Contains("NOTFOUND"))
                result.AddError("Account not found in system");

            return result;
        }

        private ValidationResult ValidateTransactionAmount(decimal amount, TransactionType transactionType)
        {
            var result = new ValidationResult();

            if (amount <= 0)
                result.AddError("Transaction amount must be positive");

            switch (transactionType)
            {
                case TransactionType.Deposit:
                    if (amount > 1000000)
                        result.AddError("Single deposit cannot exceed $1,000,000");
                    if (amount > 50000)
                        result.AddWarning("Large deposit may require additional verification");
                    break;

                case TransactionType.Withdrawal:
                    if (amount > 10000)
                        result.AddError("Single withdrawal cannot exceed $10,000");
                    if (amount > 5000)
                        result.AddWarning("Large withdrawal may require manager approval");
                    break;

                case TransactionType.Transfer:
                    if (amount > 100000)
                        result.AddError("Single transfer cannot exceed $100,000");
                    if (amount > 25000)
                        result.AddWarning("Large transfer may require additional verification");
                    break;
            }

            return result;
        }

        private ValidationResult ValidateTransactionBusinessRules(string accountNumber, decimal amount, TransactionType transactionType)
        {
            var result = new ValidationResult();

            // Simulate business rule checks
            if (transactionType == TransactionType.Withdrawal)
            {
                // Simulate daily limit check
                decimal dailyWithdrawn = GetSimulatedDailyWithdrawn(accountNumber);
                if (dailyWithdrawn + amount > 5000)
                    result.AddError($"Daily withdrawal limit exceeded. Limit: $5,000, Already withdrawn: {dailyWithdrawn:C}");
            }

            // Business hours check
            var currentTime = DateTime.Now.TimeOfDay;
            if (currentTime < new TimeSpan(9, 0, 0) || currentTime > new TimeSpan(17, 0, 0))
            {
                if (amount > 1000)
                    result.AddWarning("Large transactions outside business hours may require approval");
            }

            return result;
        }

        private RiskAssessmentResult PerformRiskAssessment(string accountNumber, decimal amount, TransactionType transactionType)
        {
            var result = new RiskAssessmentResult
            {
                AccountNumber = accountNumber,
                TransactionType = transactionType,
                Amount = amount,
                RiskFactors = new List<string>(),
                RiskScore = 0
            };

            // Risk factor analysis
            if (amount > 10000)
            {
                result.RiskFactors.Add("High transaction amount");
                result.RiskScore += 3;
            }

            if (transactionType == TransactionType.Transfer)
            {
                result.RiskFactors.Add("Transfer transaction type");
                result.RiskScore += 1;
            }

            // Time-based risk
            if (DateTime.Now.Hour < 6 || DateTime.Now.Hour > 22)
            {
                result.RiskFactors.Add("Transaction outside normal hours");
                result.RiskScore += 2;
            }

            // Determine risk level
            if (result.RiskScore >= 5)
                result.RiskLevel = "High";
            else if (result.RiskScore >= 2)
                result.RiskLevel = "Medium";
            else
                result.RiskLevel = "Low";

            return result;
        }

        #endregion

        #region Data Flow Demonstration Methods

        private CustomerRegistrationInfo CollectCustomerRegistrationData()
        {
            return new CustomerRegistrationInfo
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text,
                PhoneNumber = txtPhone.Text,
                Address = txtAddress.Text,
                DateOfBirth = dtpDateOfBirth.Value
            };
        }

        private ValidationResult PerformClientSideValidation(CustomerRegistrationInfo info)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(info.FirstName))
                result.AddError("First name is required");

            if (string.IsNullOrWhiteSpace(info.Email))
                result.AddError("Email is required");

            if (string.IsNullOrWhiteSpace(info.Password))
                result.AddError("Password is required");

            return result;
        }

        private ServiceResult<string> ProcessBusinessLogic(CustomerRegistrationInfo info)
        {
            var result = new ServiceResult<string>();

            try
            {
                // Simulate business logic processing
                var user = new User(info.FirstName, info.LastName, info.Email, 
                                  info.PhoneNumber, info.Address, info.DateOfBirth);

                var validation = AccountValidator.ValidateUserRegistration(user);
                if (!validation.IsValid)
                {
                    result.AddErrors(validation.Errors);
                    return result;
                }

                result.IsSuccess = true;
                result.Data = "Business logic validation passed";
                result.Message = "User data meets all business requirements";
            }
            catch (Exception ex)
            {
                result.AddError($"Business logic error: {ex.Message}");
            }

            return result;
        }

        private ValidationResult PrepareDataAccess(CustomerRegistrationInfo info)
        {
            var result = new ValidationResult();

            // Simulate data access preparation
            if (string.IsNullOrEmpty(info.Email))
                result.AddError("Email is required for database operations");

            // Simulate connection check
            System.Threading.Thread.Sleep(200);

            return result;
        }

        private ServiceResult<AccountCreationResult> SimulateDatabaseOperations(CustomerRegistrationInfo info)
        {
            var result = new ServiceResult<AccountCreationResult>();

            try
            {
                // Simulate database operations
                System.Threading.Thread.Sleep(1000);

                result.Data = new AccountCreationResult
                {
                    UserId = new Random().Next(1000, 9999),
                    AccountId = new Random().Next(10000, 99999),
                    AccountNumber = "ACC" + new Random().Next(100000000, 999999999),
                    AccountType = AccountType.Savings,
                    InitialBalance = 0,
                    CustomerName = $"{info.FirstName} {info.LastName}"
                };

                result.IsSuccess = true;
                result.Message = "Account created successfully";
            }
            catch (Exception ex)
            {
                result.AddError($"Database operation failed: {ex.Message}");
            }

            return result;
        }

        private void GenerateSuccessResponse(AccountCreationResult accountResult)
        {
            var response = new StringBuilder();
            response.AppendLine("Account Creation Successful!");
            response.AppendLine($"Customer: {accountResult.CustomerName}");
            response.AppendLine($"Account Number: {accountResult.AccountNumber}");
            response.AppendLine($"Account Type: {accountResult.AccountType}");
            response.AppendLine($"User ID: {accountResult.UserId}");

            ShowDataFlowStep("Success Response", response.ToString());
        }

        #endregion

        #region UI Helper Methods

        private void DisplayValidationResults(string category, ValidationResult result)
        {
            var display = new StringBuilder();
            display.AppendLine($"{category} Results:");
            
            if (result.IsValid)
            {
                display.AppendLine("✅ All validations passed");
            }
            else
            {
                display.AppendLine("❌ Validation failed:");
                foreach (var error in result.Errors)
                {
                    display.AppendLine($"  • {error}");
                }
            }

            if (result.HasWarnings)
            {
                display.AppendLine("⚠️ Warnings:");
                foreach (var warning in result.Warnings)
                {
                    display.AppendLine($"  • {warning}");
                }
            }

            AppendToValidationLog(display.ToString());
        }

        private void DisplayRiskAssessment(RiskAssessmentResult riskResult)
        {
            var display = new StringBuilder();
            display.AppendLine($"Risk Assessment Results:");
            display.AppendLine($"Risk Level: {riskResult.RiskLevel}");
            display.AppendLine($"Risk Score: {riskResult.RiskScore}");
            
            if (riskResult.RiskFactors.Any())
            {
                display.AppendLine("Risk Factors:");
                foreach (var factor in riskResult.RiskFactors)
                {
                    display.AppendLine($"  • {factor}");
                }
            }

            AppendToValidationLog(display.ToString());
        }

        private void ShowValidationStep(string step)
        {
            AppendToValidationLog($"\n🔍 {step}");
            Application.DoEvents();
            System.Threading.Thread.Sleep(300);
        }

        private void ShowDataFlowStep(string step, string details)
        {
            AppendToValidationLog($"\n📊 {step}");
            if (!string.IsNullOrEmpty(details))
            {
                AppendToValidationLog($"   {details}");
            }
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);
        }

        private void ShowSuccessMessage(string message)
        {
            AppendToValidationLog($"\n{message}");
            MessageBox.Show(message, "Validation Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowErrorMessage(string message)
        {
            AppendToValidationLog($"\n❌ {message}");
            MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowDataFlowError(string step, string error)
        {
            AppendToValidationLog($"\n❌ {step}: {error}");
        }

        private void AppendToValidationLog(string text)
        {
            if (txtValidationLog.InvokeRequired)
            {
                txtValidationLog.Invoke(new Action(() => {
                    txtValidationLog.AppendText(text + Environment.NewLine);
                    txtValidationLog.ScrollToCaret();
                }));
            }
            else
            {
                txtValidationLog.AppendText(text + Environment.NewLine);
                txtValidationLog.ScrollToCaret();
            }
        }

        private void ClearValidationResults()
        {
            txtValidationLog.Clear();
        }

        private void LoadDemoScenarios()
        {
            cmbTransactionType.Items.Clear();
            cmbTransactionType.Items.AddRange(Enum.GetValues(typeof(TransactionType)).Cast<object>().ToArray());
            cmbTransactionType.SelectedIndex = 0;
        }

        private void SetupValidationDisplays()
        {
            txtValidationLog.ReadOnly = true;
            txtValidationLog.ScrollBars = ScrollBars.Both;
            txtValidationLog.Font = new Font("Consolas", 9);
        }

        private void PopulateSampleData()
        {
            // Populate with sample data for easy testing
            txtFirstName.Text = "John";
            txtLastName.Text = "Doe";
            txtEmail.Text = "john.doe@email.com";
            txtPassword.Text = "SecurePass123!";
            txtPhone.Text = "12345678901";
            txtAddress.Text = "123 Main Street, City, State";
            dtpDateOfBirth.Value = new DateTime(1990, 1, 1);
            
            txtAccountNumber.Text = "ACC123456789";
            txtTransactionAmount.Text = "100.00";
        }

        #endregion

        #region Utility Methods

        private decimal GetSimulatedDailyWithdrawn(string accountNumber)
        {
            // Simulate daily withdrawal tracking
            var hash = accountNumber.GetHashCode();
            return Math.Abs(hash % 3000); // Random amount between 0-3000
        }

        private void LogValidationSuccess(string operation, string details)
        {
            // Log successful validations for audit
            DatabaseHelper.LogAction(SessionManager.UserID != 0 ? SessionManager.UserID : 0, 
                $"Validation Demo - {operation} Success: {details}");
        }

        private void LogValidationError(string operation, string error)
        {
            // Log validation errors for troubleshooting
            DatabaseHelper.LogAction(SessionManager.UserID != 0 ? SessionManager.UserID : 0, 
                $"Validation Demo - {operation} Error: {error}");
        }

        #endregion

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            ClearValidationResults();
        }

        private void btnPopulateSample_Click(object sender, EventArgs e)
        {
            PopulateSampleData();
        }
    }

    #region Supporting Classes

    public class RiskAssessmentResult
    {
        public string AccountNumber { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public List<string> RiskFactors { get; set; }
        public int RiskScore { get; set; }
        public string RiskLevel { get; set; }

        public RiskAssessmentResult()
        {
            RiskFactors = new List<string>();
        }
    }

    #endregion
}
