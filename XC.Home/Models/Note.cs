using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XC.Home.Models
{
    public class Note
    {
        public Note()
        {
            this.Date = DateTime.Now;
        }
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Display(Name ="您的留言")]
        public string Text { get; set; }
        [Display(Name ="日期")]
        public DateTime Date { get; set; }

    }
}
