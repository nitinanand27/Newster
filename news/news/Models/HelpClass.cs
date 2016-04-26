using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
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
    }
}
