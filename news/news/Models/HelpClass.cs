using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace news.Models
{

    class HelpClass
    {

        /// <summary>
        /// Encrypt password for storage and comparision
        /// </summary>
        /// <param name="pPassword">Password to encrypt</param>
        /// <returns>SHA256-hash of the entered password</returns>
        public static string Encrypt(string pPassword)
        {

            SHA256 sha = SHA256.Create();
            StringBuilder sb = new StringBuilder();

            ///Get sha256-hash
            byte[] inputBytes = Encoding.UTF8.GetBytes(pPassword);
            byte[] hash = sha.ComputeHash(inputBytes);

            ///Create a string with the hash
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Use to validate email-adress entered by the user when registering.
        /// </summary>
        /// <param name="pEmail">Email adress to valiadet</param>
        /// <returns>True/False</returns>
        public static bool ValidateEmail(string pEmail)
        {
            try
            {
                MailAddress tmpMail = new MailAddress(pEmail);
                return true;
            }

            catch
            {
                return false;
            }
        }
    }
}
