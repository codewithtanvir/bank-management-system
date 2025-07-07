using System;
using BCrypt.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace BankManagementSystem.Classes
//{
//    internal class PasswordHasher
//    {
//    }
//}


public static class PasswordHasher
{
    // Hashes a password using BCrypt
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    // Verifies a given password against a stored hash
    public static bool VerifyPassword(string enteredPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
    }

    // Placeholder for decryption logic (if needed)
    public static string DecryptPassword(string hashedPassword)
    {
        throw new NotImplementedException("Decryption is not supported for hashed passwords.");
    }
}
