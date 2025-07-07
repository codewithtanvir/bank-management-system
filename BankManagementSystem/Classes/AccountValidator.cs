using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BankManagementSystem.Classes
{
    /// <summary>
    /// Comprehensive validation system for Bank Management System
    /// Demonstrates proper functional requirements and data validation
    /// </summary>
    public static class AccountValidator
    {
        #region Constants for Business Rules
        private const decimal MIN_DEPOSIT_AMOUNT = 1.0m;
        private const decimal MAX_SINGLE_DEPOSIT = 1000000.0m;
        private const decimal MIN_WITHDRAWAL_AMOUNT = 1.0m;
        private const decimal MAX_DAILY_WITHDRAWAL_SAVINGS = 5000.0m;
        private const decimal MAX_DAILY_WITHDRAWAL_CHECKING = 10000.0m;
        private const decimal MIN_ACCOUNT_BALANCE = 0.0m;
        private const int MIN_AGE_REQUIREMENT = 18;
        private const int MAX_AGE_REQUIREMENT = 120;
        private const int MIN_PASSWORD_LENGTH = 6;
        private const int MAX_PASSWORD_LENGTH = 50;
        #endregion

        #region User Validation Methods

        /// <summary>
        /// Validates user registration data
        /// </summary>
        /// <param name="user">User object to validate</param>
        /// <returns>Validation result with detailed error messages</returns>
        public static ValidationResult ValidateUserRegistration(User user)
        {
            var result = new ValidationResult();

            // Validate First Name
            if (string.IsNullOrWhiteSpace(user.FirstName))
                result.AddError("First name is required");
            else if (user.FirstName.Length < 2 || user.FirstName.Length > 50)
                result.AddError("First name must be between 2 and 50 characters");
            else if (!IsValidName(user.FirstName))
                result.AddError("First name contains invalid characters");

            // Validate Last Name
            if (string.IsNullOrWhiteSpace(user.LastName))
                result.AddError("Last name is required");
            else if (user.LastName.Length < 2 || user.LastName.Length > 50)
                result.AddError("Last name must be between 2 and 50 characters");
            else if (!IsValidName(user.LastName))
                result.AddError("Last name contains invalid characters");

            // Validate Email
            if (string.IsNullOrWhiteSpace(user.Email))
                result.AddError("Email is required");
            else if (!IsValidEmail(user.Email))
                result.AddError("Invalid email format");
            else if (IsEmailAlreadyExists(user.Email))
                result.AddError("Email already exists in the system");

            // Validate Phone Number
            if (string.IsNullOrWhiteSpace(user.PhoneNumber))
                result.AddError("Phone number is required");
            else if (!IsValidPhoneNumber(user.PhoneNumber))
                result.AddError("Phone number must be 11 digits");

            // Validate Address
            if (string.IsNullOrWhiteSpace(user.Address))
                result.AddError("Address is required");
            else if (user.Address.Length > 255)
                result.AddError("Address cannot exceed 255 characters");

            // Validate Date of Birth
            if (user.DateOfBirth == default(DateTime))
                result.AddError("Date of birth is required");
            else if (!IsValidAge(user.DateOfBirth))
                result.AddError($"User must be between {MIN_AGE_REQUIREMENT} and {MAX_AGE_REQUIREMENT} years old");

            return result;
        }

        /// <summary>
        /// Validates user login credentials
        /// </summary>
        /// <param name="email">Email address</param>
        /// <param name="password">Password</param>
        /// <returns>Validation result</returns>
        public static ValidationResult ValidateUserLogin(string email, string password)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(email))
                result.AddError("Email is required");
            else if (!IsValidEmail(email))
                result.AddError("Invalid email format");

            if (string.IsNullOrWhiteSpace(password))
                result.AddError("Password is required");

            return result;
        }

        /// <summary>
        /// Validates password strength
        /// </summary>
        /// <param name="password">Password to validate</param>
        /// <returns>Validation result</returns>
        public static ValidationResult ValidatePassword(string password)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(password))
            {
                result.AddError("Password is required");
                return result;
            }

            if (password.Length < MIN_PASSWORD_LENGTH)
                result.AddError($"Password must be at least {MIN_PASSWORD_LENGTH} characters long");

            if (password.Length > MAX_PASSWORD_LENGTH)
                result.AddError($"Password cannot exceed {MAX_PASSWORD_LENGTH} characters");

            if (!HasUppercase(password))
                result.AddError("Password must contain at least one uppercase letter");

            if (!HasLowercase(password))
                result.AddError("Password must contain at least one lowercase letter");

            if (!HasDigit(password))
                result.AddError("Password must contain at least one digit");

            if (!HasSpecialCharacter(password))
                result.AddError("Password must contain at least one special character");

            return result;
        }

        #endregion

        #region Account Validation Methods

        /// <summary>
        /// Validates account creation data
        /// </summary>
        /// <param name="account">Account to validate</param>
        /// <returns>Validation result</returns>
        public static ValidationResult ValidateAccountCreation(Account account)
        {
            var result = new ValidationResult();

            // Validate Account Number
            if (string.IsNullOrWhiteSpace(account.AccountNumber))
                result.AddError("Account number is required");
            else if (!IsValidAccountNumber(account.AccountNumber))
                result.AddError("Account number must be 10-12 digits");
            else if (IsAccountNumberExists(account.AccountNumber))
                result.AddError("Account number already exists");

            // Validate User ID
            if (account.UserID <= 0)
                result.AddError("Valid user ID is required");

            // Validate Initial Balance
            if (account.Balance < MIN_ACCOUNT_BALANCE)
                result.AddError($"Initial balance cannot be less than {MIN_ACCOUNT_BALANCE:C}");

            return result;
        }

        /// <summary>
        /// Validates deposit transaction
        /// </summary>
        /// <param name="account">Account for deposit</param>
        /// <param name="amount">Deposit amount</param>
        /// <returns>Validation result</returns>
        public static ValidationResult ValidateDeposit(Account account, decimal amount)
        {
            var result = new ValidationResult();

            // Validate account status
            if (!account.IsActive)
                result.AddError("Cannot deposit to inactive account");

            // Validate amount
            if (amount <= 0)
                result.AddError("Deposit amount must be positive");
            else if (amount < MIN_DEPOSIT_AMOUNT)
                result.AddError($"Minimum deposit amount is {MIN_DEPOSIT_AMOUNT:C}");
            else if (amount > MAX_SINGLE_DEPOSIT)
                result.AddError($"Maximum single deposit amount is {MAX_SINGLE_DEPOSIT:C}");

            // Business rule: Check for suspicious large deposits
            if (amount > 50000)
                result.AddWarning("Large deposit detected - may require additional verification");

            return result;
        }

        /// <summary>
        /// Validates withdrawal transaction
        /// </summary>
        /// <param name="account">Account for withdrawal</param>
        /// <param name="amount">Withdrawal amount</param>
        /// <returns>Validation result</returns>
        public static ValidationResult ValidateWithdrawal(Account account, decimal amount)
        {
            var result = new ValidationResult();

            // Validate account status
            if (!account.IsActive)
                result.AddError("Cannot withdraw from inactive account");

            // Validate amount
            if (amount <= 0)
                result.AddError("Withdrawal amount must be positive");
            else if (amount < MIN_WITHDRAWAL_AMOUNT)
                result.AddError($"Minimum withdrawal amount is {MIN_WITHDRAWAL_AMOUNT:C}");

            // Check sufficient funds
            if (amount > account.Balance)
                result.AddError("Insufficient funds");

            // Check daily withdrawal limits
            decimal dailyLimit = GetDailyWithdrawalLimit(account.AccountType);
            decimal todayWithdrawals = GetTodayWithdrawalAmount(account.AccountID);

            if (todayWithdrawals + amount > dailyLimit)
                result.AddError($"Daily withdrawal limit of {dailyLimit:C} exceeded");

            return result;
        }

        /// <summary>
        /// Validates transfer transaction
        /// </summary>
        /// <param name="fromAccount">Source account</param>
        /// <param name="toAccount">Destination account</param>
        /// <param name="amount">Transfer amount</param>
        /// <returns>Validation result</returns>
        public static ValidationResult ValidateTransfer(Account fromAccount, Account toAccount, decimal amount)
        {
            var result = new ValidationResult();

            // Validate source account
            if (!fromAccount.IsActive)
                result.AddError("Cannot transfer from inactive account");

            // Validate destination account
            if (toAccount == null)
                result.AddError("Destination account is required");
            else if (!toAccount.IsActive)
                result.AddError("Cannot transfer to inactive account");

            // Validate same account transfer
            if (fromAccount.AccountNumber == toAccount?.AccountNumber)
                result.AddError("Cannot transfer to the same account");

            // Validate amount
            if (amount <= 0)
                result.AddError("Transfer amount must be positive");

            // Check sufficient funds
            if (amount > fromAccount.Balance)
                result.AddError("Insufficient funds for transfer");

            // Business rule: Large transfer verification
            if (amount > 100000)
                result.AddWarning("Large transfer detected - may require additional verification");

            return result;
        }

        #endregion

        #region Helper Validation Methods

        /// <summary>
        /// Validates name format (letters, spaces, hyphens only)
        /// </summary>
        /// <param name="name">Name to validate</param>
        /// <returns>True if valid</returns>
        private static bool IsValidName(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z\s\-']+$");
        }

        /// <summary>
        /// Validates email format
        /// </summary>
        /// <param name="email">Email to validate</param>
        /// <returns>True if valid</returns>
        private static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        /// <summary>
        /// Validates phone number format (11 digits)
        /// </summary>
        /// <param name="phone">Phone number to validate</param>
        /// <returns>True if valid</returns>
        private static bool IsValidPhoneNumber(string phone)
        {
            return Regex.IsMatch(phone, @"^\d{11}$");
        }

        /// <summary>
        /// Validates account number format (10-12 digits)
        /// </summary>
        /// <param name="accountNumber">Account number to validate</param>
        /// <returns>True if valid</returns>
        private static bool IsValidAccountNumber(string accountNumber)
        {
            return Regex.IsMatch(accountNumber, @"^\d{10,12}$");
        }

        /// <summary>
        /// Validates age requirement
        /// </summary>
        /// <param name="dateOfBirth">Date of birth</param>
        /// <returns>True if valid age</returns>
        private static bool IsValidAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age--;

            return age >= MIN_AGE_REQUIREMENT && age <= MAX_AGE_REQUIREMENT;
        }

        /// <summary>
        /// Checks if password has uppercase letters
        /// </summary>
        /// <param name="password">Password to check</param>
        /// <returns>True if has uppercase</returns>
        private static bool HasUppercase(string password)
        {
            return Regex.IsMatch(password, @"[A-Z]");
        }

        /// <summary>
        /// Checks if password has lowercase letters
        /// </summary>
        /// <param name="password">Password to check</param>
        /// <returns>True if has lowercase</returns>
        private static bool HasLowercase(string password)
        {
            return Regex.IsMatch(password, @"[a-z]");
        }

        /// <summary>
        /// Checks if password has digits
        /// </summary>
        /// <param name="password">Password to check</param>
        /// <returns>True if has digits</returns>
        private static bool HasDigit(string password)
        {
            return Regex.IsMatch(password, @"\d");
        }

        /// <summary>
        /// Checks if password has special characters
        /// </summary>
        /// <param name="password">Password to check</param>
        /// <returns>True if has special characters</returns>
        private static bool HasSpecialCharacter(string password)
        {
            return Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]");
        }

        /// <summary>
        /// Checks if email already exists in database
        /// </summary>
        /// <param name="email">Email to check</param>
        /// <returns>True if exists</returns>
        private static bool IsEmailAlreadyExists(string email)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                SqlParameter[] parameters = { new SqlParameter("@Email", email) };
                object result = DatabaseHelper.ExecuteScalar(sql, parameters);
                return Convert.ToInt32(result) > 0;
            }
            catch
            {
                return false; // Assume doesn't exist if database error
            }
        }

        /// <summary>
        /// Checks if account number already exists in database
        /// </summary>
        /// <param name="accountNumber">Account number to check</param>
        /// <returns>True if exists</returns>
        private static bool IsAccountNumberExists(string accountNumber)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM Accounts WHERE AccountNumber = @AccountNumber";
                SqlParameter[] parameters = { new SqlParameter("@AccountNumber", accountNumber) };
                object result = DatabaseHelper.ExecuteScalar(sql, parameters);
                return Convert.ToInt32(result) > 0;
            }
            catch
            {
                return false; // Assume doesn't exist if database error
            }
        }

        /// <summary>
        /// Gets daily withdrawal limit based on account type
        /// </summary>
        /// <param name="accountType">Account type</param>
        /// <returns>Daily withdrawal limit</returns>
        private static decimal GetDailyWithdrawalLimit(AccountType accountType)
        {
            switch (accountType)
            {
                case AccountType.Savings:
                    return MAX_DAILY_WITHDRAWAL_SAVINGS;
                case AccountType.Checking:
                    return MAX_DAILY_WITHDRAWAL_CHECKING;
                default:
                    return 1000; // Default limit
            }
        }

        /// <summary>
        /// Gets today's withdrawal amount for an account
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <returns>Today's withdrawal amount</returns>
        private static decimal GetTodayWithdrawalAmount(int accountId)
        {
            try
            {
                string sql = @"SELECT ISNULL(SUM(Amount), 0) FROM Transactions 
                              WHERE AccountID = @AccountID 
                              AND TransactionType = 'Withdrawal' 
                              AND CAST(Timestamp AS DATE) = CAST(GETDATE() AS DATE)";
                SqlParameter[] parameters = { new SqlParameter("@AccountID", accountId) };
                object result = DatabaseHelper.ExecuteScalar(sql, parameters);
                return Convert.ToDecimal(result);
            }
            catch
            {
                return 0; // Return 0 if database error
            }
        }

        #endregion
    }

    /// <summary>
    /// Represents validation result with errors and warnings
    /// </summary>
    public class ValidationResult
    {
        public List<string> Errors { get; private set; }
        public List<string> Warnings { get; private set; }
        public bool IsValid => Errors.Count == 0;
        public bool HasWarnings => Warnings.Count > 0;

        public ValidationResult()
        {
            Errors = new List<string>();
            Warnings = new List<string>();
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public void AddWarning(string warning)
        {
            Warnings.Add(warning);
        }

        public string GetErrorMessage()
        {
            return string.Join(Environment.NewLine, Errors);
        }

        public string GetWarningMessage()
        {
            return string.Join(Environment.NewLine, Warnings);
        }

        public string GetAllMessages()
        {
            var messages = new List<string>();
            if (Errors.Count > 0)
            {
                messages.Add("Errors:");
                messages.AddRange(Errors.Select(e => "- " + e));
            }
            if (Warnings.Count > 0)
            {
                messages.Add("Warnings:");
                messages.AddRange(Warnings.Select(w => "- " + w));
            }
            return string.Join(Environment.NewLine, messages);
        }
    }
}
