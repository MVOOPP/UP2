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
    public class StoryContext : Story
    {
        public StoryContext(int Id, int Id_pc, int Code_Old, int Code_New, DateTime Data, int User_Old, int User_New, string Comment)
            : base(Id, Id_pc, Code_Old, Code_New, Data, User_Old, User_New, Comment) { }
        public static List<StoryContext> Select()
        {
            List<StoryContext> AllStories = new List<StoryContext>();
            string SQL = "SELECT * FROM `Story`;";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                AllStories.Add(new StoryContext(
                    Data.GetInt32(0),
                    Data.GetInt32(1),
                    Data.GetInt32(2), 
                    Data.GetInt32(3),
                    Data.GetDateTime(4),
                    Data.GetInt32(5),
                    Data.GetInt32(6),
                    Data.GetString(7)
                    ));
            }
            Connection.CloseConnection(connection);
            return AllStories;
        }
        public void Add()
        {
            string SQL = "INSERT INTO `Story`(`Id_pc`, `Code_Old`, `Code_New`, `Data`, `User_Old`, `User_New`, `Comment`) " +
                        " VALUES " +
                            $"('{this.Id_pc}', " +
                            $"'{this.Code_Old}', " +
                            $"'{this.Code_New}', " +
                            $"'{this.Data.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                            $"'{this.User_Old}', " +
                            $"'{this.User_New}', " +
                            $"'{this.Comment}')";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}
