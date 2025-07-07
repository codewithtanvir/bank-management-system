Table Users {
  UserID int [pk]
  FirstName nvarchar
  LastName nvarchar
  Email nvarchar [unique]
  HashedPassword nvarchar
  PhoneNumber nvarchar
  Address nvarchar
  DateOfBirth date
  Role nvarchar
  IsActive bit
}

Table Accounts {
  AccountID int [pk]
  UserID int [ref: > Users.UserID]
  AccountNumber nvarchar
  AccountType nvarchar
  Balance decimal
  DateOpened datetime
}

Table Transactions {
  TransactionID int [pk]
  AccountID int [ref: > Accounts.AccountID]
  TransactionType nvarchar
  Amount decimal
  Description nvarchar
  Timestamp datetime
}

Table AuditLogs {
  LogID int [pk]
  UserID int [ref: > Users.UserID]
  Action nvarchar
  Timestamp datetime
}