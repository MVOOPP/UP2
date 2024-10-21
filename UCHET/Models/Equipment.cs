using System;
using System.Collections.Generic;
using System.Text;

namespace UCHET.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public int Id_user { get; set; }
        public int Id_classroom { get; set; }
        public int Id_status { get; set; }
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public string Code { get; set; }
        public int Price { get; set; }
        public Equipment() { }
        public Equipment(int Id, int Id_user, int Id_classroom, int Id_status, string Name, byte[] Photo, string Code, int Price)
        {
            this.Id = Id;
            this.Id_user = Id_user;
            this.Id_classroom = Id_classroom;
            this.Id_status = Id_status;
            this.Name = Name;
            this.Photo = new byte[0];
            this.Code = Code;
            this.Price = Price;
        }
    }
}
