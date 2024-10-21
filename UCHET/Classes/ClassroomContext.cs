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
    public class ClassroomContext : Classroom
    {
        public ClassroomContext(int Id, string Name, string Code) : base(Id, Name, Code) { }
        public static List<ClassroomContext> Select()
        {
            List<ClassroomContext> AllClassrooms = new List<ClassroomContext>();
            string SQL = "SELECT * FROM `classroom`;";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                AllClassrooms.Add(new ClassroomContext(
                    Data.GetInt32(0),
                    Data.GetString(1),
                    Data.GetString(2)));
            }
            Connection.CloseConnection(connection);
            return AllClassrooms;
        }
        public void Add()
        {
            string SQL = "INSERT INTO `classroom`(`Name`, `Code`) " +
                        " VALUES " +
                            $"('{this.Name}'," +
                            $"'{this.Code}')";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
        public void Update()
        {

            string SQL = "UPDATE " +
                            "`classroom` " +
                        "SET " +
                            $"`Name`='{this.Name}', " +
                            $"`Code`='{this.Code}' " +
                        " WHERE " +
                            $"`Id`='{this.Id}';";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
        public void Delete()
        {
            string SQL = $"DELETE FROM `classroom` WHERE `Id`='{this.Id}'";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}
