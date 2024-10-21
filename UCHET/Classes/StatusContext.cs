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
    public class StatusContext : Status
    {
        public StatusContext(int Id, string Name) : base(Id, Name) { }
        public static List<StatusContext> Select()
        {
            List<StatusContext> AllStatuses = new List<StatusContext>();
            string SQL = "SELECT * FROM `Status`;";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                AllStatuses.Add(new StatusContext(
                    Data.GetInt32(0),
                    Data.GetString(1)));
            }
            Connection.CloseConnection(connection);
            return AllStatuses;
        }
        public void Add()
        {
            string SQL = "INSERT INTO `Status`(`Name`) " +
                        " VALUES " +
                            $"('{this.Name}')";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
        public void Update()
        {
            string SQL = "UPDATE " +
                            "`Status` " +
                        "SET " +
                            $"`Name`='{this.Name}' " +
                        " WHERE " +
                            $"`Id`='{this.Id}';";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
        public void Delete()
        {
            string SQL = $"DELETE FROM `Status` WHERE `Id`='{this.Id}'";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}
