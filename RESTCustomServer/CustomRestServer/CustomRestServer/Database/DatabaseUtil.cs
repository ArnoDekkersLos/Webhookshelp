using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CustomRestServer.Database
{
    public class DatabaseUtil
    {
        private static string connectionString = @"intreString";

        public static SqlConnection getSqlConn()
        {
            SqlConnection newConnection = null;
            newConnection = new SqlConnection(connectionString);
            newConnection.Open();
            return newConnection;
        }
    }
}