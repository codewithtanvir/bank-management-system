using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Classes
{
    /// <summary>
    /// Manages user session data and provides session-related functionality
    /// Demonstrates proper session management for banking applications
    /// </summary>
    public static class SessionManager
    {
        #region Private Fields
        private static int _userID;
        private static string _name;
        private static string _role;
        private static DateTime _loginTime;
        private static DateTime _lastActivity;
        #endregion

        #region Public Properties
        public static int UserID
        {
            get { return _userID; }
            set
            {
                _userID = value;
                UpdateLastActivity();
            }
        }

        public static string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                UpdateLastActivity();
            }
        }

        public static string Role
        {
            get { return _role; }
            set
            {
                _role = value;
                UpdateLastActivity();
            }
        }

        public static DateTime LoginTime
        {
            get { return _loginTime; }
            private set { _loginTime = value; }
        }

        public static DateTime LastActivity
        {
            get { return _lastActivity; }
            private set { _lastActivity = value; }
        }

        // Computed Properties
        public static bool IsLoggedIn => UserID > 0 && !string.IsNullOrEmpty(Name);
        public static bool IsAdmin => Role == "Admin";
        public static bool IsEmployee => Role == "Employee";
        public static bool IsCustomer => Role == "Customer";
        public static TimeSpan SessionDuration => DateTime.Now - LoginTime;
        public static bool IsSessionExpired => (DateTime.Now - LastActivity).TotalMinutes > 30; // 30 minutes timeout
        #endregion

        #region Public Methods
        /// <summary>
        /// Initializes a new user session
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="name">User name</param>
        /// <param name="role">User role</param>
        public static void StartSession(int userID, string name, string role)
        {
            UserID = userID;
            Name = name;
            Role = role;
            LoginTime = DateTime.Now;
            LastActivity = DateTime.Now;

            // Log session start
            DatabaseHelper.LogAction(userID, $"Session started - Role: {role}");
        }

        /// <summary>
        /// Ends the current user session
        /// </summary>
        public static void EndSession()
        {
            if (IsLoggedIn)
            {
                // Log session end
                DatabaseHelper.LogAction(UserID, $"Session ended - Duration: {SessionDuration.TotalMinutes:F1} minutes");
            }

            // Clear session data
            _userID = 0;
            _name = null;
            _role = null;
            _loginTime = default(DateTime);
            _lastActivity = default(DateTime);
        }

        /// <summary>
        /// Updates the last activity timestamp
        /// </summary>
        public static void UpdateLastActivity()
        {
            _lastActivity = DateTime.Now;
        }

        /// <summary>
        /// Validates if the current session is still valid
        /// </summary>
        /// <returns>True if session is valid</returns>
        public static bool ValidateSession()
        {
            if (!IsLoggedIn)
                return false;

            if (IsSessionExpired)
            {
                EndSession();
                return false;
            }

            UpdateLastActivity();
            return true;
        }

        /// <summary>
        /// Checks if the current user has permission for the specified operation
        /// </summary>
        /// <param name="operation">Operation to check</param>
        /// <returns>True if authorized</returns>
        public static bool HasPermission(string operation)
        {
            if (!ValidateSession())
                return false;

            switch (operation.ToLower())
            {
                case "admin":
                    return IsAdmin;
                case "employee":
                    return IsEmployee || IsAdmin;
                case "customer":
                    return IsCustomer || IsEmployee || IsAdmin;
                case "transaction":
                    return IsLoggedIn;
                case "viewaccount":
                    return IsLoggedIn;
                case "manageusers":
                    return IsAdmin;
                case "processtransaction":
                    return IsEmployee || IsAdmin;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets session information as a formatted string
        /// </summary>
        /// <returns>Session information</returns>
        public static string GetSessionInfo()
        {
            if (!IsLoggedIn)
                return "No active session";

            return $"User: {Name} | Role: {Role} | Login: {LoginTime:HH:mm:ss} | Duration: {SessionDuration.TotalMinutes:F0} mins";
        }
        #endregion
    }
}
