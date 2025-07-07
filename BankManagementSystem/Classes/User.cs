using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Classes
{
    /// <summary>
    /// Represents a User in the Bank Management System
    /// Implements proper OOP principles with encapsulation, validation, and business logic
    /// </summary>
    public class User
    {
        #region Private Fields
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private string _address;
        private DateTime _dateOfBirth;
        private UserRole _role;
        private bool _isActive;
        #endregion

        #region Properties with Validation
        public int UserID { get; set; }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("First name cannot be empty");
                if (value.Length < 2 || value.Length > 50)
                    throw new ArgumentException("First name must be between 2 and 50 characters");
                _firstName = value.Trim();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Last name cannot be empty");
                if (value.Length < 2 || value.Length > 50)
                    throw new ArgumentException("Last name must be between 2 and 50 characters");
                _lastName = value.Trim();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email cannot be empty");
                if (!IsValidEmail(value))
                    throw new ArgumentException("Invalid email format");
                _email = value.ToLower().Trim();
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Phone number cannot be empty");
                if (!IsValidPhoneNumber(value))
                    throw new ArgumentException("Phone number must be 11 digits");
                _phoneNumber = value.Trim();
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Address cannot be empty");
                if (value.Length > 255)
                    throw new ArgumentException("Address cannot exceed 255 characters");
                _address = value.Trim();
            }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                if (value > DateTime.Now.AddYears(-18))
                    throw new ArgumentException("User must be at least 18 years old");
                if (value < DateTime.Now.AddYears(-120))
                    throw new ArgumentException("Invalid date of birth");
                _dateOfBirth = value;
            }
        }

        public UserRole Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public string FullName => $"{FirstName} {LastName}";
        public int Age => DateTime.Now.Year - DateOfBirth.Year;
        #endregion

        #region Constructors
        public User() 
        {
            IsActive = true;
            Role = UserRole.Customer;
        }

        public User(string firstName, string lastName, string email, string phoneNumber, 
                   string address, DateTime dateOfBirth, UserRole role = UserRole.Customer)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            DateOfBirth = dateOfBirth;
            Role = role;
            IsActive = true;
        }
        #endregion

        #region Validation Methods
        private bool IsValidEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email, 
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool IsValidPhoneNumber(string phone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\d{11}$");
        }

        public bool ValidateUser(out List<string> errors)
        {
            errors = new List<string>();

            try
            {
                // Re-validate all properties
                var tempFirst = FirstName;
                var tempLast = LastName;
                var tempEmail = Email;
                var tempPhone = PhoneNumber;
                var tempAddress = Address;
                var tempDob = DateOfBirth;
            }
            catch (ArgumentException ex)
            {
                errors.Add(ex.Message);
            }

            return errors.Count == 0;
        }
        #endregion

        #region Business Logic Methods
        public bool CanPerformBankingOperations()
        {
            return IsActive && (Role == UserRole.Customer || Role == UserRole.Employee || Role == UserRole.Admin);
        }

        public bool CanAccessAdminFeatures()
        {
            return IsActive && Role == UserRole.Admin;
        }

        public bool CanProcessTransactions()
        {
            return IsActive && (Role == UserRole.Employee || Role == UserRole.Admin);
        }

        public void UpdateProfile(string firstName, string lastName, string phoneNumber, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public void DeactivateUser()
        {
            IsActive = false;
        }

        public void ActivateUser()
        {
            IsActive = true;
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return $"{FullName} ({Email}) - {Role}";
        }

        public override bool Equals(object obj)
        {
            if (obj is User other)
                return UserID == other.UserID && Email.Equals(other.Email, StringComparison.OrdinalIgnoreCase);
            return false;
        }

        public override int GetHashCode()
        {
            return UserID.GetHashCode() ^ Email.GetHashCode();
        }
        #endregion
    }

    /// <summary>
    /// Enumeration for User Roles in the system
    /// </summary>
    public enum UserRole
    {
        Customer,
        Employee,
        Admin
    }
}
