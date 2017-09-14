using CustomRestServer.Database;
using CustomRestServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomRestServer.Authentication
{
    public class UserService
    {
        public static User GetUserByCredentials(string name, string password)
        {
            //-- Encrypt the entered password
            password = Encryption.EncryptString(password);

            //-- Retrieve the user that matches the login
            User signedUser = EmployeeModifier.GetUserByLogin(name, password);
            return signedUser;
        }
    }
}