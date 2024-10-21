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
    public class RolesContext : Roles
    {

        public RolesContext(int Id, string roles) : base(Id, roles) { }
        public static List<RolesContext> Select()
        {
            List<RolesContext> AllRoles = new List<RolesContext>();
            string SQL = "SELECT * FROM `Roles`;";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                AllRoles.Add(new RolesContext(
                    Data.GetInt32(0),
                    Data.GetString(1)));
            }
            Connection.CloseConnection(connection);
            return AllRoles;
        }
    }
}
