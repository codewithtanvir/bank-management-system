using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankManagementSystem.Classes;

namespace BankManagementSystem.Classes
{
    /// <summary>
    /// Represents a Bank Account with comprehensive business logic and validation
    /// Demonstrates proper OOP principles: Encapsulation, Abstraction, and Data Validation
    /// </summary>
    public class Account
    {
        #region Private Fields
        private string _accountNumber;
        private decimal _balance;
        private AccountType _accountType;
        private DateTime _dateOpened;
        private bool _isActive;
        private List<Transaction> _transactions;
        #endregion

        #region Properties with Validation
        public int AccountID { get; set; }
        public int UserID { get; set; }

        public string AccountNumber
        {
            get { return _accountNumber; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Account number cannot be empty");
                if (!IsValidAccountNumber(value))
                    throw new ArgumentException("Invalid account number format");
                _accountNumber = value;
            }
        }

        public decimal Balance
        {
            get { return _balance; }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Account balance cannot be negative");
                _balance = value;
            }
        }

        public AccountType AccountType
        {
            get { return _accountType; }
            set { _accountType = value; }
        }

        public DateTime DateOpened
        {
            get { return _dateOpened; }
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Account opening date cannot be in the future");
                _dateOpened = value;
            }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public List<Transaction> Transactions
        {
            get { return _transactions ?? (_transactions = new List<Transaction>()); }
            private set { _transactions = value; }
        }

        // Calculated Properties
        public decimal AvailableBalance => IsActive ? Balance : 0;
        public bool IsEligibleForLoan => Balance >= 10000 && IsActive;
        public int AccountAge => (DateTime.Now - DateOpened).Days;
        public string FormattedBalance => Balance.ToString("C");
        #endregion

        #region Constructors
        public Account()
        {
            IsActive = true;
            DateOpened = DateTime.Now;
            Balance = 0;
            AccountType = AccountType.Savings;
            Transactions = new List<Transaction>();
        }

        public Account(int userID, string accountNumber, AccountType accountType, decimal initialBalance = 0)
        {
            UserID = userID;
            AccountNumber = accountNumber;
            AccountType = accountType;
            Balance = initialBalance;
            DateOpened = DateTime.Now;
            IsActive = true;
            Transactions = new List<Transaction>();
        }
        #endregion

        #region Business Logic Methods
        
        /// <summary>
        /// Deposits money into the account with validation
        /// </summary>
        /// <param name="amount">Amount to deposit</param>
        /// <param name="description">Transaction description</param>
        /// <returns>Success status</returns>
        public bool Deposit(decimal amount, string description = "Deposit")
        {
            // Validation
            if (!IsActive)
                throw new InvalidOperationException("Cannot deposit to inactive account");
            
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive");

            if (amount > 1000000) // Business rule: Max single deposit
                throw new ArgumentException("Single deposit cannot exceed $1,000,000");

            // Process deposit
            Balance += amount;
            
            // Record transaction
            var transaction = new Transaction
            {
                AccountID = AccountID,
                TransactionType = TransactionType.Deposit,
                Amount = amount,
                Description = description,
                Timestamp = DateTime.Now,
                BalanceAfter = Balance
            };
            
            Transactions.Add(transaction);
            return true;
        }

        /// <summary>
        /// Withdraws money from the account with validation
        /// </summary>
        /// <param name="amount">Amount to withdraw</param>
        /// <param name="description">Transaction description</param>
        /// <returns>Success status</returns>
        public bool Withdraw(decimal amount, string description = "Withdrawal")
        {
            // Validation
            if (!IsActive)
                throw new InvalidOperationException("Cannot withdraw from inactive account");

            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive");

            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds");

            // Business rules
            if (amount > GetDailyWithdrawalLimit())
                throw new InvalidOperationException($"Daily withdrawal limit of {GetDailyWithdrawalLimit():C} exceeded");

            // Process withdrawal
            Balance -= amount;
            
            // Record transaction
            var transaction = new Transaction
            {
                AccountID = AccountID,
                TransactionType = TransactionType.Withdrawal,
                Amount = amount,
                Description = description,
                Timestamp = DateTime.Now,
                BalanceAfter = Balance
            };
            
            Transactions.Add(transaction);
            return true;
        }

        /// <summary>
        /// Transfers money to another account
        /// </summary>
        /// <param name="targetAccount">Destination account</param>
        /// <param name="amount">Amount to transfer</param>
        /// <param name="description">Transfer description</param>
        /// <returns>Success status</returns>
        public bool TransferTo(Account targetAccount, decimal amount, string description = "Transfer")
        {
            if (targetAccount == null)
                throw new ArgumentNullException(nameof(targetAccount));

            if (!targetAccount.IsActive)
                throw new InvalidOperationException("Cannot transfer to inactive account");

            if (AccountNumber == targetAccount.AccountNumber)
                throw new InvalidOperationException("Cannot transfer to the same account");

            // Use existing withdrawal logic
            this.Withdraw(amount, $"Transfer to {targetAccount.AccountNumber}: {description}");
            
            // Use existing deposit logic
            targetAccount.Deposit(amount, $"Transfer from {this.AccountNumber}: {description}");

            return true;
        }

        /// <summary>
        /// Gets daily withdrawal limit based on account type
        /// </summary>
        /// <returns>Daily limit amount</returns>
        public decimal GetDailyWithdrawalLimit()
        {
            switch (AccountType)
            {
                case AccountType.Savings:
                    return 5000;
                case AccountType.Checking:
                    return 10000;
                default:
                    return 1000;
            }
        }

        /// <summary>
        /// Gets monthly transaction limit based on account type
        /// </summary>
        /// <returns>Monthly transaction limit</returns>
        public int GetMonthlyTransactionLimit()
        {
            switch (AccountType)
            {
                case AccountType.Savings:
                    return 50;
                case AccountType.Checking:
                    return 200;
                default:
                    return 10;
            }
        }

        /// <summary>
        /// Checks if account can perform a transaction
        /// </summary>
        /// <param name="amount">Transaction amount</param>
        /// <returns>Validation result</returns>
        public AccountValidationResult ValidateTransaction(decimal amount)
        {
            var result = new AccountValidationResult();

            if (!IsActive)
                result.AddError("Account is not active");

            if (amount <= 0)
                result.AddError("Transaction amount must be positive");

            var todayTransactions = Transactions.Where(t => t.Timestamp.Date == DateTime.Today).Count();
            if (todayTransactions >= GetMonthlyTransactionLimit())
                result.AddError("Daily transaction limit exceeded");

            return result;
        }

        /// <summary>
        /// Gets transaction history for a specific period
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>List of transactions</returns>
        public List<Transaction> GetTransactionHistory(DateTime startDate, DateTime endDate)
        {
            return Transactions.Where(t => t.Timestamp >= startDate && t.Timestamp <= endDate)
                              .OrderByDescending(t => t.Timestamp)
                              .ToList();
        }

        /// <summary>
        /// Calculates interest for savings account
        /// </summary>
        /// <returns>Interest amount</returns>
        public decimal CalculateInterest()
        {
            if (AccountType == AccountType.Savings && Balance > 0)
            {
                decimal interestRate = 0.03m; // 3% annual interest
                return Balance * interestRate / 12; // Monthly interest
            }
            return 0;
        }

        /// <summary>
        /// Applies monthly interest to savings accounts
        /// </summary>
        public void ApplyInterest()
        {
            if (AccountType == AccountType.Savings)
            {
                decimal interest = CalculateInterest();
                if (interest > 0)
                {
                    Deposit(interest, "Monthly Interest");
                }
            }
        }

        /// <summary>
        /// Deactivates the account
        /// </summary>
        public void DeactivateAccount()
        {
            IsActive = false;
        }

        /// <summary>
        /// Public method to set balance (used for data loading from database)
        /// </summary>
        /// <param name="balance">The balance to set</param>
        public void SetBalance(decimal balance)
        {
            if (balance < 0)
                throw new ArgumentException("Account balance cannot be negative");
            _balance = balance;
        }

        /// <summary>
        /// Activates the account
        /// </summary>
        public void ActivateAccount()
        {
            IsActive = true;
        }
        #endregion

        #region Validation Methods
        private bool IsValidAccountNumber(string accountNumber)
        {
            // Account number should be 10-12 digits
            return System.Text.RegularExpressions.Regex.IsMatch(accountNumber, @"^\d{10,12}$");
        }

        public bool ValidateAccount(out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrWhiteSpace(AccountNumber))
                errors.Add("Account number is required");

            if (UserID <= 0)
                errors.Add("Valid User ID is required");

            if (Balance < 0)
                errors.Add("Account balance cannot be negative");

            return errors.Count == 0;
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return $"Account: {AccountNumber} | Type: {AccountType} | Balance: {FormattedBalance}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Account other)
                return AccountNumber == other.AccountNumber;
            return false;
        }

        public override int GetHashCode()
        {
            return AccountNumber?.GetHashCode() ?? 0;
        }
        #endregion
    }

    /// <summary>
    /// Enumeration for Account Types
    /// </summary>
    public enum AccountType
    {
        Savings,
        Checking,
        Business,
        Student
    }

    /// <summary>
    /// Represents a bank transaction
    /// </summary>
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int AccountID { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal BalanceAfter { get; set; }
        public string Reference { get; set; }

        public string FormattedAmount => Amount.ToString("C");
        public string FormattedTimestamp => Timestamp.ToString("yyyy-MM-dd HH:mm:ss");

        public override string ToString()
        {
            return $"{TransactionType}: {FormattedAmount} at {FormattedTimestamp}";
        }
    }

    /// <summary>
    /// Enumeration for Transaction Types
    /// </summary>
    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        Transfer,
        Interest,
        Fee
    }

    /// <summary>
    /// Validation result for account operations
    /// </summary>
    public class AccountValidationResult
    {
        public List<string> Errors { get; private set; }
        public bool IsValid => Errors.Count == 0;

        public AccountValidationResult()
        {
            Errors = new List<string>();
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public string GetErrorMessage()
        {
            return string.Join(Environment.NewLine, Errors);
        }
    }
}
