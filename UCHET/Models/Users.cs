using System;
using System.Collections.Generic;
using System.Text;

namespace UCHET.Models
{
    public class Users
    {/// <summary>
     /// ID
     /// </summary>
        public int Id { get; set; }
        public string login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public Users(int Id, string login, string Password, string Role, string Email, string Surname, string Name, string Patronymic, string Phone)
        {
            this.Id = Id;
            this.login = login;
            this.Password = Password;
            this.Role = Role;
            this.Email = Email;
            this.Surname = Surname;
            this.Name = Name;
            this.Patronymic = Patronymic;
            this.Phone = Phone;
        }
        public Users() { }

    }
}
