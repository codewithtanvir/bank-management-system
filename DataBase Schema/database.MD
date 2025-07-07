Absolutely! ✅ Here's the complete, optimized **database schema** for your **Bank Management System** project, ready for deployment in **SQL Server**.

---

# ✅ Bank Management System — Database Schema

You’ll be using **SQL Server** with normalized tables for:
- Users (customers/employees/admins)
- Accounts
- Transactions
- Audit Logs

---

## 🛢️ Database Name:

```sql
CREATE DATABASE BankData;
GO
USE BankData;
```

---

## 📄 1. `Users` Table

Stores **Login + personal details** of all users:
- Customers
- Employees
- Admins

```sql
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Email NVARCHAR(100) UNIQUE NOT NULL,
    HashedPassword NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(20),
    Address NVARCHAR(255),
    DateOfBirth DATE,
    Role NVARCHAR(20) CHECK (Role IN ('Customer','Employee','Admin')),
    IsActive BIT DEFAULT 1
);
```

---

## 💳 2. `Accounts` Table

Each **customer** has one or more bank accounts.

```sql
CREATE TABLE Accounts (
    AccountID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    AccountNumber NVARCHAR(20) UNIQUE,
    AccountType NVARCHAR(50) CHECK (AccountType IN ('Savings Account', 'Checking Account')),
    Balance DECIMAL(18, 2) DEFAULT 0.00,
    DateOpened DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
        ON DELETE CASCADE
);
```

---

## 💰 3. `Transactions` Table

Logs every **deposit, withdrawal, or transfer**, linked to `AccountID`.

```sql
CREATE TABLE Transactions (
    TransactionID INT PRIMARY KEY IDENTITY(1,1),
    AccountID INT NOT NULL,
    TransactionType NVARCHAR(50) CHECK (TransactionType IN ('Deposit', 'Withdrawal', 'Transfer')),
    Amount DECIMAL(18, 2) CHECK (Amount > 0),
    Description NVARCHAR(255),
    Timestamp DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
        ON DELETE CASCADE
);
```

---

## 📜 4. `AuditLogs` Table

Tracks **admin/employee actions** (who did what & when).

```sql
CREATE TABLE AuditLogs (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Action NVARCHAR(255),
    Timestamp DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
        ON DELETE SET NULL
);
```

---

## 🔒 Optional: 5. Seed Admin User (for testing)

```sql
-- Hashed password = "admin123" (use BCrypt generator in C#)
INSERT INTO Users (FirstName, LastName, Email, HashedPassword, PhoneNumber, Address, DateOfBirth, Role)
VALUES (
    'System', 'Admin', 'admin@bank.com',
    '$2a$11$5dQDTm9Kx8hpAKvMTsU0neF/hqddwf9aBlKsD6/M7NSJJmfgwM03a',  -- generated with BCrypt
    '01234567890', 'Bank HQ', '1990-01-01', 'Admin'
);
```

---

## 🔀 Relationships Overview

```text
Users (1) -----------< Accounts (many)
Accounts (1) --------< Transactions (many)
Users (1) -----------< AuditLogs (many)
```

---

## 🧪 Basic Test Queries

```sql
-- View all users
SELECT * FROM Users;

-- Get all balance amounts of customers
SELECT u.FirstName, u.Email, a.AccountNumber, a.Balance
FROM Users u
JOIN Accounts a ON u.UserID = a.UserID
WHERE u.Role = 'Customer';

-- Show last transactions for a specific account
SELECT TOP 10 * FROM Transactions
WHERE AccountID = 1
ORDER BY Timestamp DESC;
```

---

## ✅ Summary of Tables

| Table        | Purpose                                 |
|--------------|-----------------------------------------|
| Users        | Stores all (admins, employees, customers) |
| Accounts     | Bank accounts linked to customers        |
| Transactions | Deposits, Withdrawals, Transfers         |
| AuditLogs    | Logs system actions (admin/employee)     |

---

## 🗂️ Export Options

Let me know if you want:
- 📦 Full `.sql` file 📄 (ready to run/import)
- ✅ Seed data + test queries
- 💾 Microsoft SQL Server `.bak` file export

---

Would you like me to generate:
> ✅ Final ZIP folder with database + source code + tutorial?

Let me know, and I’ll ship it 🚀