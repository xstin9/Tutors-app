using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Thusong_Tutors
{
    class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            // Generate a 16-byte salt
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            // Generate the hash using PBKDF2
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32); // 256-bit hash

            // Combine salt + hash into one array
            byte[] hashBytes = new byte[48]; // 16 bytes salt + 32 bytes hash
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 32);

            // Convert to Base64 string for storage
            return Convert.ToBase64String(hashBytes);
        }




        //public static bool VerifyPassword(string enteredPassword, string storedHash)
        //{
        //    byte[] hashBytes = Convert.FromBase64String(storedHash);

        //    // Extract salt (first 16 bytes)
        //    byte[] salt = new byte[16];
        //    Array.Copy(hashBytes, 0, salt, 0, 16);

        //    // Hash the entered password with the same salt
        //    var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 100000, HashAlgorithmName.SHA256);
        //    byte[] hash = pbkdf2.GetBytes(32);

        //    // Compare byte-by-byte
        //    for (int i = 0; i < 32; i++)
        //    {
        //        if (hashBytes[i + 16] != hash[i])
        //        {
        //            return false;

        //        }
        //    }



        //    return true;
        //}
        public  static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // Convert stored base64 hash to byte array
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            // Extract the salt (first 16 bytes)
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Hash the entered password using the same salt
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 100000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32); // 256-bit hash

            // added code
            



            //


            // Compare the newly generated hash with the stored hash (byte-by-byte)
            for (int i = 0; i < 32; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    
                    return false; // Hash mismatch
                }
            }

            return true; // Password matches
        }








    }
}
