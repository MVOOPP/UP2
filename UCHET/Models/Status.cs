using System;
using System.Collections.Generic;
using System.Text;

namespace UCHET.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}
