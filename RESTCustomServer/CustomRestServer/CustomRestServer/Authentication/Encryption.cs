using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace CustomRestServer.Authentication
{
    public class Encryption
    {
        public static string EncryptString(string textToEncrypt)
        {
            return textToEncrypt;
        }

        public static string Encrypt(string textToEncrypt, string encryptionPassword)
        {
            return textToEncrypt;
        }
    }
}