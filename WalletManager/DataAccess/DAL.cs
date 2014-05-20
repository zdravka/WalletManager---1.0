using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WalletManager.DataAccess
{
    public class DAL
    {
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Wallet"].ToString());
        public static bool UserIsValid(string email, string password)
        {
            bool authenticated = false;
            string query = string.Format("SELECT * FROM [user] WHERE email = '{0}' AND password = '{1}'", email, password);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            authenticated = sdr.HasRows;
            conn.Close();
            return (authenticated);
        }
    }
}