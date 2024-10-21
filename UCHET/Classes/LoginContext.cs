using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCHET.Classes.Common;
using UCHET.Models;


namespace UCHET.Classes
{
    public class LoginContext : Login
    {
        public LoginContext(int Id, string Name, string Password) : base(Id, Name, Password) { }
        public static List<LoginContext> Select()
        {
            List<LoginContext> allLogins = new List<LoginContext>();
            string SQL = "SELECT * FROM `Login`;";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                allLogins.Add(new LoginContext(
                    Data.GetInt32(0),
                    Data.GetString(1),
                    Data.GetString(2)));
            }
            Connection.CloseConnection(connection);
            return allLogins;
        }
    }
}
