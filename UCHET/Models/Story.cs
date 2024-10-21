using System;
using System.Collections.Generic;
using System.Text;

namespace UCHET.Models
{
    public class Story
    {
        public int Id { get; set; }
        public int Id_pc { get; set; }
        public int Code_Old { get; set; }
        public int Code_New { get; set; }
        public DateTime Data { get; set; }
        public int User_Old { get; set; }
        public int User_New { get; set; }
        public string Comment { get; set; }
        public Story(int Id, int Id_pc, int Code_Old, int Code_New, DateTime Data, int User_Old, int User_New, string Comment)
        {
            this.Id = Id;
            this.Id_pc = Id_pc;
            this.Code_Old = Code_Old;
            this.Code_New = Code_New;
            this.User_Old = User_Old;
            this.User_New = User_New;
            this.Comment = Comment;
            this.Data = Data;
        }
    }
}
