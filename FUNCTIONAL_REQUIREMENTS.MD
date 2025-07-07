# Bank Management System - Functional Requirements & Real-Life Scenarios

## Table of Contents

1. [System Overview](#system-overview)
2. [Real-Life Banking Scenarios](#real-life-banking-scenarios)
3. [Functional Requirements](#functional-requirements)
4. [Data Validation & Verification](#data-validation--verification)
5. [Object-Oriented Design Principles](#object-oriented-design-principles)
6. [Data Flow Management](#data-flow-management)
7. [Testing Scenarios](#testing-scenarios)
8. [Performance Requirements](#performance-requirements)

## System Overview

The Bank Management System is designed to simulate real-world banking operations with comprehensive business logic, data validation, and security features. The system demonstrates proper Object-Oriented Programming principles while handling actual banking scenarios.

### Key Stakeholders

- **Customers**: Individual account holders performing banking transactions
- **Employees**: Bank staff processing customer requests and managing accounts
- **Administrators**: System managers overseeing operations and security
- **Auditors**: Compliance officers monitoring transactions and system integrity

## Real-Life Banking Scenarios

### Scenario 1: New Customer Registration and Account Opening

**Business Process:**

1. Customer visits bank branch or applies online
2. Fills out application form with personal information
3. Provides identification and address verification
4. Bank employee verifies information
5. System validates all data according to banking regulations
6. Account is created with unique account number
7. Initial deposit is processed (if provided)
8. Welcome kit and debit card are issued
9. Transaction is logged for audit purposes

**Technical Implementation:**

```csharp
// Real-life validation process
var customerInfo = new CustomerRegistrationInfo
{
    FirstName = "John",
    LastName = "Doe",
    Email = "john.doe@email.com",
    Password = "SecurePass123!",
    PhoneNumber = "12345678901",
    Address = "123 Main Street, City, State",
    DateOfBirth = new DateTime(1990, 5, 15)
};

// System processes registration with comprehensive validation
var result = bankService.RegisterCustomerWithAccount(
    customerInfo,
    AccountType.Savings,
    1000.00m // Initial deposit
);
```

**Functional Requirements Covered:**

- FR-001: System shall validate customer age (minimum 18 years)
- FR-002: System shall verify email uniqueness
- FR-003: System shall enforce password complexity rules
- FR-004: System shall generate unique account numbers
- FR-005: System shall record all account creation activities

### Scenario 2: Daily Banking Transactions

**Business Process - Deposit:**

1. Customer visits branch or uses ATM
2. Provides account information and amount
3. System validates account status and deposit limits
4. Cash/check is processed and verified
5. Account balance is updated in real-time
6. Receipt is generated
7. Transaction is recorded for reconciliation

**Business Process - Withdrawal:**

1. Customer initiates withdrawal request
2. System checks account balance and daily limits
3. System validates withdrawal against business rules
4. If valid, balance is debited
5. Cash is dispensed (ATM) or provided (teller)
6. Transaction receipt is generated
7. Account activity is logged

**Technical Implementation:**

```csharp
// Deposit Transaction
var depositResult = bankService.ProcessBankingTransaction(
    "ACC123456789",      // Account number
    TransactionType.Deposit,
    500.00m,             // Amount
    "Cash deposit at branch"
);

// Withdrawal Transaction with validation
var withdrawalResult = bankService.ProcessBankingTransaction(
    "ACC123456789",
    TransactionType.Withdrawal,
    200.00m,
    "ATM withdrawal"
);

// Transfer Transaction
var transferResult = bankService.ProcessBankingTransaction(
    "ACC123456789",      // From account
    TransactionType.Transfer,
    150.00m,             // Amount
    "Rent payment",     // Description
    "ACC987654321"       // To account
);
```

**Functional Requirements Covered:**

- FR-006: System shall validate sufficient funds for withdrawals
- FR-007: System shall enforce daily withdrawal limits
- FR-008: System shall update balances in real-time
- FR-009: System shall maintain transaction history
- FR-010: System shall generate transaction receipts

### Scenario 3: Account Management and Monitoring

**Business Process:**

1. Bank monitors accounts for unusual activities
2. System analyzes transaction patterns
3. Suspicious activities trigger alerts
4. Risk assessment is performed automatically
5. High-risk accounts are flagged for review
6. Compliance reports are generated
7. Regulatory requirements are maintained

**Technical Implementation:**

```csharp
// Automated account monitoring
var monitoringResult = bankService.MonitorAccountActivity("ACC123456789");

if (monitoringResult.IsSuccess && monitoringResult.Data.RiskLevel == "High")
{
    // Alert compliance team
    // Freeze account if necessary
    // Generate investigation report
}
```

**Functional Requirements Covered:**

- FR-011: System shall detect unusual transaction patterns
- FR-012: System shall generate risk assessment reports
- FR-013: System shall maintain audit trails
- FR-014: System shall support compliance monitoring

## Functional Requirements

### Authentication and Authorization (FR-100 Series)

**FR-101: User Authentication**

- System shall authenticate users using email and password
- System shall implement password hashing using BCrypt
- System shall prevent unauthorized access attempts
- System shall maintain session management

**FR-102: Role-Based Access Control**

- System shall support three user roles: Customer, Employee, Admin
- System shall restrict functionality based on user roles
- System shall log all access attempts and role changes

**FR-103: Password Security**

- Passwords must be minimum 6 characters
- Passwords must contain uppercase, lowercase, digit, and special character
- System shall prevent password reuse
- System shall enforce password expiration policies

### Account Management (FR-200 Series)

**FR-201: Account Creation**

- System shall generate unique 10-12 digit account numbers
- System shall support multiple account types (Savings, Checking, Business)
- System shall validate customer eligibility
- System shall require minimum opening balance

**FR-202: Account Operations**

- System shall process deposits with validation
- System shall process withdrawals with balance verification
- System shall support inter-account transfers
- System shall maintain real-time balance updates

**FR-203: Account Limits and Controls**

- Savings accounts: $5,000 daily withdrawal limit
- Checking accounts: $10,000 daily withdrawal limit
- Maximum single deposit: $1,000,000
- System shall enforce business hour restrictions

### Transaction Processing (FR-300 Series)

**FR-301: Transaction Validation**

- System shall validate all transactions before processing
- System shall check account status and available balance
- System shall enforce daily and monthly transaction limits
- System shall prevent duplicate transactions

**FR-302: Transaction Recording**

- System shall record all transactions with timestamps
- System shall maintain transaction descriptions and references
- System shall track balance changes for each transaction
- System shall generate unique transaction IDs

**FR-303: Transaction History**

- System shall maintain complete transaction history
- System shall support date range queries
- System shall generate account statements
- System shall export transaction data for reporting

### Data Validation and Integrity (FR-400 Series)

**FR-401: Input Validation**

- System shall validate all user inputs before processing
- System shall prevent SQL injection and XSS attacks
- System shall enforce data type and format requirements
- System shall provide clear error messages for validation failures

**FR-402: Business Rule Validation**

- System shall enforce minimum age requirement (18 years)
- System shall validate email format and uniqueness
- System shall verify phone number format (11 digits)
- System shall ensure account number uniqueness

**FR-403: Data Integrity**

- System shall maintain referential integrity between tables
- System shall prevent orphaned records
- System shall ensure transaction atomicity
- System shall implement proper error handling and rollback

## Data Validation & Verification

### Input Validation Layers

**Layer 1: Client-Side Validation (Form Level)**

```csharp
// Example: Registration form validation
private bool ValidateRegistrationForm()
{
    var errors = new List<string>();

    if (string.IsNullOrWhiteSpace(txtFirstName.Text))
        errors.Add("First name is required");

    if (!IsValidEmail(txtEmail.Text))
        errors.Add("Invalid email format");

    if (errors.Any())
    {
        MessageBox.Show(string.Join("\n", errors));
        return false;
    }

    return true;
}
```

**Layer 2: Business Logic Validation (Class Level)**

```csharp
// Example: User class with validation
public class User
{
    private string _email;

    public string Email
    {
        get { return _email; }
        set
        {
            if (!IsValidEmail(value))
                throw new ArgumentException("Invalid email format");
            _email = value;
        }
    }
}
```

**Layer 3: Service Layer Validation (Business Rules)**

```csharp
// Example: Service layer validation
public ServiceResult ValidateTransaction(TransactionRequest request)
{
    var result = new ServiceResult();

    // Business rule validations
    if (request.Amount > GetDailyLimit(request.AccountType))
        result.AddError("Transaction exceeds daily limit");

    if (!IsAccountActive(request.AccountNumber))
        result.AddError("Account is not active");

    return result;
}
```

**Layer 4: Database Validation (Constraints)**

```sql
-- Database constraints for data integrity
ALTER TABLE Users
ADD CONSTRAINT CK_Users_Email CHECK (Email LIKE '%@%.%');

ALTER TABLE Accounts
ADD CONSTRAINT CK_Accounts_Balance CHECK (Balance >= 0);

ALTER TABLE Transactions
ADD CONSTRAINT CK_Transactions_Amount CHECK (Amount > 0);
```

### Validation Rules Matrix

| Data Field         | Format             | Business Rule         | Database Constraint |
| ------------------ | ------------------ | --------------------- | ------------------- |
| Email              | Valid email format | Must be unique        | UNIQUE constraint   |
| Phone              | 11 digits          | Numeric only          | VARCHAR(11) CHECK   |
| Password           | Min 6 chars        | Complex requirements  | Hashed storage      |
| Account Number     | 10-12 digits       | Auto-generated unique | UNIQUE constraint   |
| Transaction Amount | Decimal(18,2)      | Positive values       | CHECK > 0           |
| Date of Birth      | Valid date         | Min age 18 years      | CHECK constraint    |

## Object-Oriented Design Principles

### 1. Encapsulation

**Implementation Example:**

```csharp
public class Account
{
    private decimal _balance;  // Private field

    public decimal Balance     // Public property with validation
    {
        get { return _balance; }
        private set
        {
            if (value < 0)
                throw new ArgumentException("Balance cannot be negative");
            _balance = value;
        }
    }

    // Public methods for controlled access
    public bool Deposit(decimal amount)
    {
        if (amount <= 0) return false;
        Balance += amount;
        return true;
    }
}
```

**Benefits Demonstrated:**

- Data hiding and controlled access
- Validation at property level
- Consistent state maintenance
- Prevention of invalid operations

### 2. Inheritance

**Implementation Example:**

```csharp
public abstract class BankEmployee : User
{
    public string EmployeeID { get; set; }
    public DateTime HireDate { get; set; }

    public abstract bool ProcessTransaction(Transaction transaction);
}

public class Teller : BankEmployee
{
    public override bool ProcessTransaction(Transaction transaction)
    {
        // Teller-specific transaction processing
        return transaction.Amount <= 10000; // Teller limit
    }
}

public class Manager : BankEmployee
{
    public override bool ProcessTransaction(Transaction transaction)
    {
        // Manager can process any amount
        return true;
    }
}
```

### 3. Polymorphism

**Implementation Example:**

```csharp
public interface ITransactionProcessor
{
    ValidationResult ValidateTransaction(Transaction transaction);
    bool ProcessTransaction(Transaction transaction);
}

public class DepositProcessor : ITransactionProcessor
{
    public bool ProcessTransaction(Transaction transaction)
    {
        // Deposit-specific logic
        return true;
    }
}

public class WithdrawalProcessor : ITransactionProcessor
{
    public bool ProcessTransaction(Transaction transaction)
    {
        // Withdrawal-specific logic with balance check
        return CheckSufficientFunds(transaction);
    }
}
```

### 4. Abstraction

**Implementation Example:**

```csharp
public abstract class ReportGenerator
{
    public abstract void GenerateReport();

    protected void LogReportGeneration(string reportType)
    {
        DatabaseHelper.LogAction(SessionManager.UserID,
            $"Generated {reportType} report");
    }
}

public class AccountStatementGenerator : ReportGenerator
{
    public override void GenerateReport()
    {
        // Generate account statement
        LogReportGeneration("Account Statement");
    }
}
```

## Data Flow Management

### 1. Request Processing Flow

```
User Input → Form Validation → Business Logic → Data Access → Database
    ↓              ↓                ↓              ↓            ↓
Error Display ← Validation ← Service Result ← DAL Result ← DB Response
```

### 2. Transaction Flow Example

```csharp
// Step-by-step transaction processing
public ServiceResult<TransactionResult> ProcessDeposit(DepositRequest request)
{
    // Step 1: Input validation
    var inputValidation = ValidateDepositInput(request);
    if (!inputValidation.IsValid)
        return ServiceResult.Failure<TransactionResult>(inputValidation.Errors);

    // Step 2: Business rule validation
    var businessValidation = ValidateBusinessRules(request);
    if (!businessValidation.IsValid)
        return ServiceResult.Failure<TransactionResult>(businessValidation.Errors);

    // Step 3: Database transaction
    using (var transaction = BeginDatabaseTransaction())
    {
        try
        {
            // Step 4: Update account balance
            UpdateAccountBalance(request.AccountId, request.Amount);

            // Step 5: Record transaction
            var transactionId = RecordTransaction(request);

            // Step 6: Log audit trail
            LogAuditTrail(request);

            // Step 7: Commit transaction
            transaction.Commit();

            return ServiceResult.Success(new TransactionResult { Id = transactionId });
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return ServiceResult.Failure<TransactionResult>(ex.Message);
        }
    }
}
```

### 3. Error Handling Flow

```csharp
public class ErrorHandler
{
    public static void HandleException(Exception ex, string context)
    {
        // Log error details
        LogError(ex, context);

        // Determine error type and response
        switch (ex)
        {
            case ValidationException validationEx:
                ShowValidationError(validationEx.Message);
                break;

            case BusinessRuleException businessEx:
                ShowBusinessRuleError(businessEx.Message);
                break;

            case DatabaseException dbEx:
                ShowSystemError("System temporarily unavailable");
                break;

            default:
                ShowGenericError("An unexpected error occurred");
                break;
        }
    }
}
```

## Testing Scenarios

### Unit Testing Examples

```csharp
[TestClass]
public class AccountValidatorTests
{
    [TestMethod]
    public void ValidateDeposit_ValidAmount_ReturnsSuccess()
    {
        // Arrange
        var account = new Account { Balance = 1000, IsActive = true };
        decimal depositAmount = 500;

        // Act
        var result = AccountValidator.ValidateDeposit(account, depositAmount);

        // Assert
        Assert.IsTrue(result.IsValid);
        Assert.AreEqual(0, result.Errors.Count);
    }

    [TestMethod]
    public void ValidateWithdrawal_InsufficientFunds_ReturnsError()
    {
        // Arrange
        var account = new Account { Balance = 100, IsActive = true };
        decimal withdrawalAmount = 500;

        // Act
        var result = AccountValidator.ValidateWithdrawal(account, withdrawalAmount);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Contains("Insufficient funds"));
    }
}
```

### Integration Testing Scenarios

```csharp
[TestClass]
public class BankServiceIntegrationTests
{
    [TestMethod]
    public void RegisterCustomer_ValidData_CreatesAccountSuccessfully()
    {
        // Arrange
        var customerInfo = CreateValidCustomerInfo();
        var bankService = new BankService();

        // Act
        var result = bankService.RegisterCustomerWithAccount(
            customerInfo, AccountType.Savings, 1000);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.IsNotNull(result.Data);
        Assert.IsTrue(result.Data.AccountId > 0);

        // Verify database state
        var account = GetAccountFromDatabase(result.Data.AccountId);
        Assert.AreEqual(1000, account.Balance);
    }
}
```

## Performance Requirements

### Response Time Requirements

| Operation               | Maximum Response Time | Expected Volume         |
| ----------------------- | --------------------- | ----------------------- |
| User Login              | 2 seconds             | 1000 concurrent users   |
| Account Balance Inquiry | 1 second              | 5000 queries/minute     |
| Deposit Transaction     | 3 seconds             | 500 transactions/minute |
| Withdrawal Transaction  | 3 seconds             | 300 transactions/minute |
| Account Statement       | 5 seconds             | 100 requests/minute     |

### Database Performance

```sql
-- Index optimization for performance
CREATE INDEX IX_Accounts_UserID ON Accounts(UserID);
CREATE INDEX IX_Transactions_AccountID_Timestamp ON Transactions(AccountID, Timestamp);
CREATE INDEX IX_Users_Email ON Users(Email);
CREATE INDEX IX_AuditLogs_UserID_Timestamp ON AuditLogs(UserID, Timestamp);
```

### Caching Strategy

```csharp
public class CacheManager
{
    private static readonly MemoryCache _cache = new MemoryCache();

    public static T GetOrSet<T>(string key, Func<T> getItem, TimeSpan? expiry = null)
    {
        if (_cache.TryGetValue(key, out T item))
            return item;

        item = getItem();
        _cache.Set(key, item, expiry ?? TimeSpan.FromMinutes(15));
        return item;
    }
}

// Usage example
public Account GetAccount(string accountNumber)
{
    return CacheManager.GetOrSet(
        $"account_{accountNumber}",
        () => GetAccountFromDatabase(accountNumber),
        TimeSpan.FromMinutes(5)
    );
}
```

## Conclusion

This Bank Management System demonstrates comprehensive real-life banking scenarios with proper functional requirements, data validation, and Object-Oriented Programming principles. The system provides:

1. **Complete Business Logic**: Real banking operations with proper validation
2. **Robust Data Management**: Multi-layer validation and integrity checks
3. **Scalable Architecture**: Service-oriented design with clear separation of concerns
4. **Security Implementation**: Password hashing, session management, and audit trails
5. **Performance Optimization**: Caching, indexing, and efficient data access patterns

The implementation showcases how theoretical OOP concepts apply to practical business applications, providing students with a comprehensive understanding of enterprise software development practices.
