using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace XC.Web.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [Display(Name ="电影名称")]
        public string Title { get; set; }
        [Display(Name ="上映日期")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Display(Name ="类型")]
        public string Genre { get; set; }
        [Display(Name ="价格")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
