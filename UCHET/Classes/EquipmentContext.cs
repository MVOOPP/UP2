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
    public class EquipmentContext : Equipment
    {
        public EquipmentContext(int Id, int Id_user, int Id_classroom, int Id_status, string Name, byte[] Photo, string Code, int Price)
            : base(Id, Id_user, Id_classroom, Id_status, Name, Photo, Code, Price) { }
        public EquipmentContext() : base() { }
        public static List<EquipmentContext> Select()
        {
            List<EquipmentContext> AllEquipments = new List<EquipmentContext>();
            string SQL = "SELECT * FROM `Equipment`;";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                EquipmentContext equipment = new EquipmentContext();
                equipment.Id = Data.GetInt32(0);
                equipment.Id_user = Data.GetInt32(1);
                equipment.Id_classroom = Data.GetInt32(2);
                equipment.Id_status = Data.GetInt32(3);
                equipment.Name = Data.GetString(4);
                if (!Data.IsDBNull(5))
                {
                    equipment.Photo = new byte[64 * 1024];
                    Data.GetBytes(5, 0, equipment.Photo, 0, equipment.Photo.Length);
                }
                equipment.Code = Data.GetString(6);
                equipment.Price = Data.GetInt32(7);
                AllEquipments.Add(equipment);
            }
            Connection.CloseConnection(connection);
            return AllEquipments;
        }
        public void Add()
        {
            string SQL = "INSERT INTO `Equipment`(`Id_user`, `Id_classroom`, `Id_status`, `Name`, `Photo`, `Code`, `Price`) " +
                        " VALUES " +
                            $"('{this.Id_user}', " +
                            $"'{this.Id_classroom}', " +
                            $"'{this.Id_status}', " +
                            $"'{this.Name}'," +
                            $"'{this.Photo}'," +
                            $"'{this.Code}'," +
                            $"'{this.Price}')";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
        public void Update()
        {
            string SQL = "UPDATE " +
                            "`Equipment` " +
                        "SET " +
                            $"`Id_user`='{this.Id_user}', " +
                            $"`Id_classroom`='{this.Id_classroom}', " +
                            $"`Id_status`='{this.Id_status}', " +
                            $"`Name`='{this.Name}', " +
                            $"`Photo`='{this.Photo}', " +
                            $"`Code`='{this.Code}', " +
                            $"`Price`='{this.Price}' " +
                        " WHERE " +
                            $"`Id`='{this.Id}';";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
        public void Delete()
        {
            string SQL = $"DELETE FROM `Equipment` WHERE `Id`='{this.Id}'";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}
