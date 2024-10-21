using System;
using System.Collections.Generic;
using System.Text;

namespace UCHET.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string roles { get; set; }
        public Roles(int Id, string roles)
        {
            this.Id = Id;
            this.roles = roles;
        }
    }
}
