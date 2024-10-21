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
    public class UsersContext : Users
    {
        public UsersContext(int Id, string login, string Password, string Role, string Email, string Surname, string Name, string Patronymic, string Phone)
            : base(Id, login, Password, Role, Email, Surname, Name, Patronymic, Phone) { }
        public static List<UsersContext> Select()
        {
            List<UsersContext> AllUsers = new List<UsersContext>();
            string SQL = "SELECT * FROM `Users`;";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                AllUsers.Add(new UsersContext(
                    Data.GetInt32(0),
                    Data.GetString(1),
                    Data.GetString(2),
                    Data.GetString(3),
                    Data.GetString(4),
                    Data.GetString(5),
                    Data.GetString(6),
                    Data.GetString(7),
                    Data.GetString(8)
                    ));
            }
            Connection.CloseConnection(connection);
            return AllUsers;
        }
        public void Add()
        {
            string SQL = "INSERT INTO `Users`(`Login`, `Password`, `Role`, `Email`, `Surname`, `Name`, `Patronymic`, `Phone`) " +
                        " VALUES " +
                            $"('{this.login}', " +
                            $"'{this.Password}', " +
                            $"'{this.Role}', " +
                            $"'{this.Email}'," +
                            $"'{this.Surname}'," +
                            $"'{this.Name}'," +
                            $"'{this.Patronymic}'," +
                            $"'{this.Phone}')";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
        public void Update()
        {
            string SQL = "UPDATE " +
                            "`Users` " +
                        "SET " +
                            $"`Login`='{this.login}', " +
                            $"`Password`='{this.Password}', " +
                            $"`Role`='{this.Role}', " +
                            $"`Email`='{this.Email}', " +
                            $"`Surname`='{this.Surname}', " +
                            $"`Name`='{this.Name}', " +
                            $"`Patronymic`='{this.Patronymic}', " +
                            $"`Phone`='{this.Phone}' " +
                        " WHERE " +
                            $"`Id`='{this.Id}';";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
        public void Delete()
        {
            string SQL = $"DELETE FROM `Users` WHERE `Id`='{this.Id}'";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}
