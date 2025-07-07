using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BankManagementSystem.Classes
{
    /// <summary>
    /// Business Service Layer for Bank Operations
    /// Demonstrates real-life banking scenarios with proper functional requirements
    /// Implements comprehensive data flow management and validation
    /// </summary>
    public class BankService
    {
        #region Real-Life Banking Scenarios

        /// <summary>
        /// Scenario 1: Customer Registration with Account Opening
        /// Real-life process: New customer walks into bank, fills form, opens account
        /// </summary>
        /// <param name="userInfo">Customer information</param>
        /// <param name="accountType">Type of account to open</param>
        /// <param name="initialDeposit">Initial deposit amount</param>
        /// <returns>Service result with account details</returns>
        public ServiceResult<AccountCreationResult> RegisterCustomerWithAccount(
            CustomerRegistrationInfo userInfo, AccountType accountType, decimal initialDeposit)
        {
            var result = new ServiceResult<AccountCreationResult>();

            try
            {
                // Step 1: Validate customer information
                var user = new User
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    Email = userInfo.Email,
                    PhoneNumber = userInfo.PhoneNumber,
                    Address = userInfo.Address,
                    DateOfBirth = userInfo.DateOfBirth,
                    Role = UserRole.Customer
                };

                var userValidation = AccountValidator.ValidateUserRegistration(user);
                if (!userValidation.IsValid)
                {
                    result.AddErrors(userValidation.Errors);
                    return result;
                }

                // Step 2: Validate password
                var passwordValidation = AccountValidator.ValidatePassword(userInfo.Password);
                if (!passwordValidation.IsValid)
                {
                    result.AddErrors(passwordValidation.Errors);
                    return result;
                }

                // Step 3: Create user account in database
                int userId = CreateUserInDatabase(user, userInfo.Password);
                if (userId <= 0)
                {
                    result.AddError("Failed to create user account");
                    return result;
                }

                // Step 4: Generate unique account number
                string accountNumber = GenerateAccountNumber();

                // Step 5: Create bank account
                var account = new Account(userId, accountNumber, accountType, initialDeposit);
                var accountValidation = AccountValidator.ValidateAccountCreation(account);
                if (!accountValidation.IsValid)
                {
                    result.AddErrors(accountValidation.Errors);
                    return result;
                }

                // Step 6: Validate initial deposit
                if (initialDeposit > 0)
                {
                    var depositValidation = AccountValidator.ValidateDeposit(account, initialDeposit);
                    if (!depositValidation.IsValid)
                    {
                        result.AddErrors(depositValidation.Errors);
                        return result;
                    }
                }

                // Step 7: Save account to database
                int accountId = CreateAccountInDatabase(account);
                if (accountId <= 0)
                {
                    result.AddError("Failed to create bank account");
                    return result;
                }

                // Step 8: Record initial deposit if any
                if (initialDeposit > 0)
                {
                    RecordTransaction(accountId, TransactionType.Deposit, initialDeposit, "Initial Deposit");
                }

                // Step 9: Log audit trail
                DatabaseHelper.LogAction(userId, $"New customer registered with account {accountNumber}");

                // Step 10: Prepare result
                result.Data = new AccountCreationResult
                {
                    UserId = userId,
                    AccountId = accountId,
                    AccountNumber = accountNumber,
                    AccountType = accountType,
                    InitialBalance = initialDeposit,
                    CustomerName = $"{userInfo.FirstName} {userInfo.LastName}"
                };

                result.IsSuccess = true;
                result.Message = "Customer registration and account opening completed successfully";
            }
            catch (Exception ex)
            {
                result.AddError($"System error: {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Scenario 2: Daily Banking Operations - Deposits, Withdrawals, Transfers
        /// Real-life process: Customer visits bank/ATM for transactions
        /// </summary>
        /// <param name="accountNumber">Account number</param>
        /// <param name="transactionType">Type of transaction</param>
        /// <param name="amount">Transaction amount</param>
        /// <param name="description">Transaction description</param>
        /// <param name="targetAccountNumber">Target account for transfers</param>
        /// <returns>Service result with transaction details</returns>
        public ServiceResult<TransactionResult> ProcessBankingTransaction(
            string accountNumber, TransactionType transactionType, decimal amount, 
            string description, string targetAccountNumber = null)
        {
            var result = new ServiceResult<TransactionResult>();

            try
            {
                // Step 1: Retrieve and validate account
                var account = GetAccountByNumber(accountNumber);
                if (account == null)
                {
                    result.AddError("Account not found");
                    return result;
                }

                // Step 2: Process transaction based on type
                switch (transactionType)
                {
                    case TransactionType.Deposit:
                        result = ProcessDeposit(account, amount, description);
                        break;

                    case TransactionType.Withdrawal:
                        result = ProcessWithdrawal(account, amount, description);
                        break;

                    case TransactionType.Transfer:
                        if (string.IsNullOrEmpty(targetAccountNumber))
                        {
                            result.AddError("Target account number is required for transfers");
                            return result;
                        }
                        result = ProcessTransfer(account, targetAccountNumber, amount, description);
                        break;

                    default:
                        result.AddError("Invalid transaction type");
                        break;
                }

                // Step 3: Log audit trail if successful
                if (result.IsSuccess)
                {
                    DatabaseHelper.LogAction(SessionManager.UserID, 
                        $"{transactionType} of {amount:C} on account {accountNumber}");
                }
            }
            catch (Exception ex)
            {
                result.AddError($"Transaction failed: {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Scenario 3: Account Management - Balance Inquiry, Statement Generation
        /// Real-life process: Customer checks balance, requests statements
        /// </summary>
        /// <param name="accountNumber">Account number</param>
        /// <param name="startDate">Statement start date</param>
        /// <param name="endDate">Statement end date</param>
        /// <returns>Account statement</returns>
        public ServiceResult<AccountStatement> GenerateAccountStatement(
            string accountNumber, DateTime startDate, DateTime endDate)
        {
            var result = new ServiceResult<AccountStatement>();

            try
            {
                // Step 1: Validate account
                var account = GetAccountByNumber(accountNumber);
                if (account == null)
                {
                    result.AddError("Account not found");
                    return result;
                }

                // Step 2: Validate date range
                if (startDate > endDate)
                {
                    result.AddError("Start date cannot be later than end date");
                    return result;
                }

                if (endDate > DateTime.Now)
                {
                    result.AddError("End date cannot be in the future");
                    return result;
                }

                // Step 3: Get transaction history
                var transactions = GetTransactionHistory(account.AccountID, startDate, endDate);

                // Step 4: Calculate statement summary
                var statement = new AccountStatement
                {
                    AccountNumber = accountNumber,
                    AccountType = account.AccountType,
                    StatementPeriod = $"{startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}",
                    OpeningBalance = GetOpeningBalance(account.AccountID, startDate),
                    ClosingBalance = account.Balance,
                    Transactions = transactions,
                    TotalDeposits = transactions.Where(t => t.TransactionType == TransactionType.Deposit).Sum(t => t.Amount),
                    TotalWithdrawals = transactions.Where(t => t.TransactionType == TransactionType.Withdrawal).Sum(t => t.Amount),
                    TransactionCount = transactions.Count
                };

                result.Data = statement;
                result.IsSuccess = true;
                result.Message = "Account statement generated successfully";

                // Log audit trail
                DatabaseHelper.LogAction(SessionManager.UserID, 
                    $"Generated statement for account {accountNumber}");
            }
            catch (Exception ex)
            {
                result.AddError($"Failed to generate statement: {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Scenario 4: Account Monitoring - Suspicious Activity Detection
        /// Real-life process: Bank monitors accounts for unusual patterns
        /// </summary>
        /// <param name="accountNumber">Account to monitor</param>
        /// <returns>Monitoring result with alerts</returns>
        public ServiceResult<AccountMonitoringResult> MonitorAccountActivity(string accountNumber)
        {
            var result = new ServiceResult<AccountMonitoringResult>();

            try
            {
                var account = GetAccountByNumber(accountNumber);
                if (account == null)
                {
                    result.AddError("Account not found");
                    return result;
                }

                var monitoring = new AccountMonitoringResult
                {
                    AccountNumber = accountNumber,
                    MonitoringDate = DateTime.Now,
                    Alerts = new List<string>()
                };

                // Check for suspicious patterns
                var recentTransactions = GetRecentTransactions(account.AccountID, 30); // Last 30 days

                // Alert 1: Multiple large withdrawals
                var largeWithdrawals = recentTransactions
                    .Where(t => t.TransactionType == TransactionType.Withdrawal && t.Amount > 10000)
                    .Count();
                if (largeWithdrawals > 3)
                {
                    monitoring.Alerts.Add($"Multiple large withdrawals detected: {largeWithdrawals} transactions over $10,000");
                }

                // Alert 2: Unusual transaction volume
                var avgTransactionCount = GetAverageMonthlyTransactionCount(account.AccountID);
                var currentMonthCount = recentTransactions.Count(t => t.Timestamp.Month == DateTime.Now.Month);
                if (currentMonthCount > avgTransactionCount * 2)
                {
                    monitoring.Alerts.Add($"Unusual transaction volume: {currentMonthCount} transactions this month (average: {avgTransactionCount})");
                }

                // Alert 3: Large balance changes
                var balanceChanges = recentTransactions.Sum(t => 
                    t.TransactionType == TransactionType.Deposit ? t.Amount : -t.Amount);
                if (Math.Abs(balanceChanges) > account.Balance * 0.5m)
                {
                    monitoring.Alerts.Add($"Significant balance changes detected: {balanceChanges:C}");
                }

                monitoring.RiskLevel = monitoring.Alerts.Count > 2 ? "High" : 
                                      monitoring.Alerts.Count > 0 ? "Medium" : "Low";

                result.Data = monitoring;
                result.IsSuccess = true;
                result.Message = $"Account monitoring completed. Risk level: {monitoring.RiskLevel}";
            }
            catch (Exception ex)
            {
                result.AddError($"Monitoring failed: {ex.Message}");
            }

            return result;
        }

        #endregion

        #region Private Helper Methods

        private ServiceResult<TransactionResult> ProcessDeposit(Account account, decimal amount, string description)
        {
            var result = new ServiceResult<TransactionResult>();

            // Validate deposit
            var validation = AccountValidator.ValidateDeposit(account, amount);
            if (!validation.IsValid)
            {
                result.AddErrors(validation.Errors);
                return result;
            }

            // Process deposit
            account.Deposit(amount, description);

            // Update database
            UpdateAccountBalance(account);
            int transactionId = RecordTransaction(account.AccountID, TransactionType.Deposit, amount, description);

            result.Data = new TransactionResult
            {
                TransactionId = transactionId,
                AccountNumber = account.AccountNumber,
                TransactionType = TransactionType.Deposit,
                Amount = amount,
                NewBalance = account.Balance,
                Description = description,
                Timestamp = DateTime.Now
            };

            result.IsSuccess = true;
            result.Message = $"Deposit of {amount:C} successful";

            return result;
        }

        private ServiceResult<TransactionResult> ProcessWithdrawal(Account account, decimal amount, string description)
        {
            var result = new ServiceResult<TransactionResult>();

            // Validate withdrawal
            var validation = AccountValidator.ValidateWithdrawal(account, amount);
            if (!validation.IsValid)
            {
                result.AddErrors(validation.Errors);
                return result;
            }

            // Process withdrawal
            account.Withdraw(amount, description);

            // Update database
            UpdateAccountBalance(account);
            int transactionId = RecordTransaction(account.AccountID, TransactionType.Withdrawal, amount, description);

            result.Data = new TransactionResult
            {
                TransactionId = transactionId,
                AccountNumber = account.AccountNumber,
                TransactionType = TransactionType.Withdrawal,
                Amount = amount,
                NewBalance = account.Balance,
                Description = description,
                Timestamp = DateTime.Now
            };

            result.IsSuccess = true;
            result.Message = $"Withdrawal of {amount:C} successful";

            return result;
        }

        private ServiceResult<TransactionResult> ProcessTransfer(Account fromAccount, string toAccountNumber, decimal amount, string description)
        {
            var result = new ServiceResult<TransactionResult>();

            // Get target account
            var toAccount = GetAccountByNumber(toAccountNumber);
            if (toAccount == null)
            {
                result.AddError("Target account not found");
                return result;
            }

            // Validate transfer
            var validation = AccountValidator.ValidateTransfer(fromAccount, toAccount, amount);
            if (!validation.IsValid)
            {
                result.AddErrors(validation.Errors);
                return result;
            }

            // Process transfer
            fromAccount.TransferTo(toAccount, amount, description);

            // Update both accounts in database
            UpdateAccountBalance(fromAccount);
            UpdateAccountBalance(toAccount);

            // Record both transactions
            int fromTransactionId = RecordTransaction(fromAccount.AccountID, TransactionType.Transfer, -amount, 
                $"Transfer to {toAccountNumber}: {description}");
            int toTransactionId = RecordTransaction(toAccount.AccountID, TransactionType.Transfer, amount, 
                $"Transfer from {fromAccount.AccountNumber}: {description}");

            result.Data = new TransactionResult
            {
                TransactionId = fromTransactionId,
                AccountNumber = fromAccount.AccountNumber,
                TransactionType = TransactionType.Transfer,
                Amount = amount,
                NewBalance = fromAccount.Balance,
                Description = $"Transfer to {toAccountNumber}: {description}",
                Timestamp = DateTime.Now,
                TargetAccountNumber = toAccountNumber
            };

            result.IsSuccess = true;
            result.Message = $"Transfer of {amount:C} to {toAccountNumber} successful";

            return result;
        }

        private Account GetAccountByNumber(string accountNumber)
        {
            try
            {
                string sql = "SELECT * FROM Accounts WHERE AccountNumber = @AccountNumber AND IsActive = 1";
                SqlParameter[] parameters = { new SqlParameter("@AccountNumber", accountNumber) };
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                if (dt.Rows.Count == 1)
                {
                    var row = dt.Rows[0];
                    var account = new Account
                    {
                        AccountID = Convert.ToInt32(row["AccountID"]),
                        UserID = Convert.ToInt32(row["UserID"]),
                        AccountNumber = row["AccountNumber"].ToString(),
                        AccountType = (AccountType)Enum.Parse(typeof(AccountType), row["AccountType"].ToString()),
                        DateOpened = Convert.ToDateTime(row["DateOpened"]),
                        IsActive = true
                    };
                    account.SetBalance(Convert.ToDecimal(row["Balance"]));
                    return account;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        private int CreateUserInDatabase(User user, string password)
        {
            try
            {
                string hashedPassword = PasswordHasher.HashPassword(password);
                string sql = @"INSERT INTO Users (FirstName, LastName, Email, HashedPassword, PhoneNumber, Address, DateOfBirth, Role, IsActive) 
                              OUTPUT INSERTED.UserID
                              VALUES (@FirstName, @LastName, @Email, @HashedPassword, @PhoneNumber, @Address, @DateOfBirth, @Role, @IsActive)";

                SqlParameter[] parameters = {
                    new SqlParameter("@FirstName", user.FirstName),
                    new SqlParameter("@LastName", user.LastName),
                    new SqlParameter("@Email", user.Email),
                    new SqlParameter("@HashedPassword", hashedPassword),
                    new SqlParameter("@PhoneNumber", user.PhoneNumber),
                    new SqlParameter("@Address", user.Address),
                    new SqlParameter("@DateOfBirth", user.DateOfBirth),
                    new SqlParameter("@Role", user.Role.ToString()),
                    new SqlParameter("@IsActive", user.IsActive)
                };

                object result = DatabaseHelper.ExecuteScalar(sql, parameters);
                return Convert.ToInt32(result);
            }
            catch
            {
                return 0;
            }
        }

        private int CreateAccountInDatabase(Account account)
        {
            try
            {
                string sql = @"INSERT INTO Accounts (UserID, AccountNumber, AccountType, Balance, DateOpened) 
                              OUTPUT INSERTED.AccountID
                              VALUES (@UserID, @AccountNumber, @AccountType, @Balance, @DateOpened)";

                SqlParameter[] parameters = {
                    new SqlParameter("@UserID", account.UserID),
                    new SqlParameter("@AccountNumber", account.AccountNumber),
                    new SqlParameter("@AccountType", account.AccountType.ToString()),
                    new SqlParameter("@Balance", account.Balance),
                    new SqlParameter("@DateOpened", account.DateOpened)
                };

                object result = DatabaseHelper.ExecuteScalar(sql, parameters);
                return Convert.ToInt32(result);
            }
            catch
            {
                return 0;
            }
        }

        private void UpdateAccountBalance(Account account)
        {
            string sql = "UPDATE Accounts SET Balance = @Balance WHERE AccountID = @AccountID";
            SqlParameter[] parameters = {
                new SqlParameter("@Balance", account.Balance),
                new SqlParameter("@AccountID", account.AccountID)
            };
            DatabaseHelper.ExecuteNonQuery(sql, parameters);
        }

        private int RecordTransaction(int accountId, TransactionType transactionType, decimal amount, string description)
        {
            try
            {
                string sql = @"INSERT INTO Transactions (AccountID, TransactionType, Amount, Description, Timestamp) 
                              OUTPUT INSERTED.TransactionID
                              VALUES (@AccountID, @TransactionType, @Amount, @Description, @Timestamp)";

                SqlParameter[] parameters = {
                    new SqlParameter("@AccountID", accountId),
                    new SqlParameter("@TransactionType", transactionType.ToString()),
                    new SqlParameter("@Amount", Math.Abs(amount)),
                    new SqlParameter("@Description", description ?? ""),
                    new SqlParameter("@Timestamp", DateTime.Now)
                };

                object result = DatabaseHelper.ExecuteScalar(sql, parameters);
                return Convert.ToInt32(result);
            }
            catch
            {
                return 0;
            }
        }

        private string GenerateAccountNumber()
        {
            // Generate 12-digit account number
            Random random = new Random();
            string accountNumber;
            do
            {
                accountNumber = "ACC" + random.Next(100000000, 999999999).ToString();
            }
            while (IsAccountNumberExists(accountNumber));

            return accountNumber;
        }

        private bool IsAccountNumberExists(string accountNumber)
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
                return false;
            }
        }

        private List<Transaction> GetTransactionHistory(int accountId, DateTime startDate, DateTime endDate)
        {
            var transactions = new List<Transaction>();
            try
            {
                string sql = @"SELECT * FROM Transactions 
                              WHERE AccountID = @AccountID 
                              AND Timestamp >= @StartDate 
                              AND Timestamp <= @EndDate 
                              ORDER BY Timestamp DESC";

                SqlParameter[] parameters = {
                    new SqlParameter("@AccountID", accountId),
                    new SqlParameter("@StartDate", startDate),
                    new SqlParameter("@EndDate", endDate)
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                foreach (DataRow row in dt.Rows)
                {
                    transactions.Add(new Transaction
                    {
                        TransactionID = Convert.ToInt32(row["TransactionID"]),
                        AccountID = Convert.ToInt32(row["AccountID"]),
                        TransactionType = (TransactionType)Enum.Parse(typeof(TransactionType), row["TransactionType"].ToString()),
                        Amount = Convert.ToDecimal(row["Amount"]),
                        Description = row["Description"].ToString(),
                        Timestamp = Convert.ToDateTime(row["Timestamp"])
                    });
                }
            }
            catch { }

            return transactions;
        }

        private List<Transaction> GetRecentTransactions(int accountId, int days)
        {
            DateTime startDate = DateTime.Now.AddDays(-days);
            return GetTransactionHistory(accountId, startDate, DateTime.Now);
        }

        private decimal GetOpeningBalance(int accountId, DateTime startDate)
        {
            try
            {
                string sql = @"SELECT ISNULL(SUM(
                                CASE 
                                    WHEN TransactionType = 'Deposit' THEN Amount
                                    WHEN TransactionType = 'Withdrawal' THEN -Amount
                                    ELSE 0
                                END), 0)
                              FROM Transactions 
                              WHERE AccountID = @AccountID AND Timestamp < @StartDate";

                SqlParameter[] parameters = {
                    new SqlParameter("@AccountID", accountId),
                    new SqlParameter("@StartDate", startDate)
                };

                object result = DatabaseHelper.ExecuteScalar(sql, parameters);
                return Convert.ToDecimal(result);
            }
            catch
            {
                return 0;
            }
        }

        private int GetAverageMonthlyTransactionCount(int accountId)
        {
            try
            {
                string sql = @"SELECT AVG(MonthlyCount) FROM (
                                  SELECT COUNT(*) as MonthlyCount
                                  FROM Transactions 
                                  WHERE AccountID = @AccountID 
                                  AND Timestamp >= DATEADD(MONTH, -6, GETDATE())
                                  GROUP BY YEAR(Timestamp), MONTH(Timestamp)
                              ) as MonthlyTransactions";

                SqlParameter[] parameters = { new SqlParameter("@AccountID", accountId) };
                object result = DatabaseHelper.ExecuteScalar(sql, parameters);
                return result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
            catch
            {
                return 10; // Default average
            }
        }

        #endregion
    }

    #region Data Transfer Objects

    public class CustomerRegistrationInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class AccountCreationResult
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public decimal InitialBalance { get; set; }
        public string CustomerName { get; set; }
    }

    public class TransactionResult
    {
        public int TransactionId { get; set; }
        public string AccountNumber { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public decimal NewBalance { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public string TargetAccountNumber { get; set; }
    }

    public class AccountStatement
    {
        public string AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public string StatementPeriod { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public List<Transaction> Transactions { get; set; }
        public decimal TotalDeposits { get; set; }
        public decimal TotalWithdrawals { get; set; }
        public int TransactionCount { get; set; }
    }

    public class AccountMonitoringResult
    {
        public string AccountNumber { get; set; }
        public DateTime MonitoringDate { get; set; }
        public List<string> Alerts { get; set; }
        public string RiskLevel { get; set; }
    }

    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; private set; }

        public ServiceResult()
        {
            Errors = new List<string>();
        }

        public void AddError(string error)
        {
            Errors.Add(error);
            IsSuccess = false;
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            Errors.AddRange(errors);
            IsSuccess = false;
        }

        public string GetErrorMessage()
        {
            return string.Join(Environment.NewLine, Errors);
        }
    }

    #endregion
}
