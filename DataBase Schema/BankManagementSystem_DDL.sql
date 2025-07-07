-- Bank Management System Database Creation Script
-- This script creates the complete database schema with proper constraints and indexes

USE master;
GO

-- Create database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'BankData')
BEGIN
    CREATE DATABASE BankData;
END
GO

USE BankData;
GO

-- Drop tables if they exist (for clean installation)
IF OBJECT_ID('AuditLogs', 'U') IS NOT NULL DROP TABLE AuditLogs;
IF OBJECT_ID('Transactions', 'U') IS NOT NULL DROP TABLE Transactions;
IF OBJECT_ID('Accounts', 'U') IS NOT NULL DROP TABLE Accounts;
IF OBJECT_ID('Users', 'U') IS NOT NULL DROP TABLE Users;
GO

-- Create Users table
CREATE TABLE Users (
    UserID int IDENTITY(1,1) PRIMARY KEY,
    FirstName nvarchar(50) NOT NULL,
    LastName nvarchar(50) NOT NULL,
    Email nvarchar(100) NOT NULL UNIQUE,
    HashedPassword nvarchar(255) NOT NULL,
    PhoneNumber nvarchar(20),
    Address nvarchar(200),
    DateOfBirth date,
    Role nvarchar(20) NOT NULL DEFAULT 'Customer',
    IsActive bit NOT NULL DEFAULT 1,
    CreatedDate datetime NOT NULL DEFAULT GETDATE(),
    ModifiedDate datetime NOT NULL DEFAULT GETDATE()
);
GO

-- Create Accounts table
CREATE TABLE Accounts (
    AccountID int IDENTITY(1,1) PRIMARY KEY,
    UserID int NOT NULL,
    AccountNumber nvarchar(20) NOT NULL UNIQUE,
    AccountType nvarchar(20) NOT NULL DEFAULT 'Savings',
    Balance decimal(18,2) NOT NULL DEFAULT 0.00,
    DateOpened datetime NOT NULL DEFAULT GETDATE(),
    IsActive bit NOT NULL DEFAULT 1,
    CONSTRAINT FK_Accounts_Users FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT CK_Accounts_Balance CHECK (Balance >= 0),
    CONSTRAINT CK_Accounts_AccountType CHECK (AccountType IN ('Savings', 'Checking', 'Business', 'Student'))
);
GO

-- Create Transactions table
CREATE TABLE Transactions (
    TransactionID int IDENTITY(1,1) PRIMARY KEY,
    AccountID int NOT NULL,
    TransactionType nvarchar(20) NOT NULL,
    Amount decimal(18,2) NOT NULL,
    Description nvarchar(200),
    Timestamp datetime NOT NULL DEFAULT GETDATE(),
    BalanceAfter decimal(18,2),
    Reference nvarchar(50),
    CONSTRAINT FK_Transactions_Accounts FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID),
    CONSTRAINT CK_Transactions_Amount CHECK (Amount > 0),
    CONSTRAINT CK_Transactions_TransactionType CHECK (TransactionType IN ('Deposit', 'Withdrawal', 'Transfer', 'Interest', 'Fee'))
);
GO

-- Create AuditLogs table with larger Action column
CREATE TABLE AuditLogs (
    LogID int IDENTITY(1,1) PRIMARY KEY,
    UserID int NULL, -- Allow NULL for system operations
    Action nvarchar(MAX) NOT NULL, -- Changed to MAX to handle long error messages
    Timestamp datetime NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_AuditLogs_Users FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

-- Create indexes for better performance
CREATE INDEX IX_Users_Email ON Users(Email);
CREATE INDEX IX_Users_Role ON Users(Role);
CREATE INDEX IX_Accounts_UserID ON Accounts(UserID);
CREATE INDEX IX_Accounts_AccountNumber ON Accounts(AccountNumber);
CREATE INDEX IX_Transactions_AccountID ON Transactions(AccountID);
CREATE INDEX IX_Transactions_Timestamp ON Transactions(Timestamp);
CREATE INDEX IX_AuditLogs_UserID ON AuditLogs(UserID);
CREATE INDEX IX_AuditLogs_Timestamp ON AuditLogs(Timestamp);
GO

-- Insert sample admin user for testing
INSERT INTO Users (FirstName, LastName, Email, HashedPassword, Role, IsActive)
VALUES 
    ('System', 'Administrator', 'admin@bank.com', '$2a$11$rQzB3ZjJ8/H.c4LH.Q5a8eO8zBx0p5v2bA7WOGy3LkJ5H.QzB3ZjJ8', 'Admin', 1),
    ('Bank', 'Employee', 'employee@bank.com', '$2a$11$rQzB3ZjJ8/H.c4LH.Q5a8eO8zBx0p5v2bA7WOGy3LkJ5H.QzB3ZjJ8', 'Employee', 1),
    ('Demo', 'Customer', 'customer@bank.com', '$2a$11$rQzB3ZjJ8/H.c4LH.Q5a8eO8zBx0p5v2bA7WOGy3LkJ5H.QzB3ZjJ8', 'Customer', 1);
GO

-- Insert sample accounts for demo users
INSERT INTO Accounts (UserID, AccountNumber, AccountType, Balance)
VALUES 
    (1, '1000000001', 'Checking', 10000.00),
    (2, '1000000002', 'Savings', 5000.00),
    (3, '1000000003', 'Savings', 1500.00);
GO

-- Insert sample transactions
INSERT INTO Transactions (AccountID, TransactionType, Amount, Description, BalanceAfter)
VALUES 
    (1, 'Deposit', 10000.00, 'Initial Deposit', 10000.00),
    (2, 'Deposit', 5000.00, 'Initial Deposit', 5000.00),
    (3, 'Deposit', 1500.00, 'Initial Deposit', 1500.00);
GO

PRINT 'Bank Management System database created successfully!';
PRINT 'Default login credentials:';
PRINT 'Admin: admin@bank.com / Password123!';
PRINT 'Employee: employee@bank.com / Password123!';
PRINT 'Customer: customer@bank.com / Password123!';
GO
