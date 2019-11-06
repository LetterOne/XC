using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XC.Admin.Models
{
    public class EditModel
    {
        public EditModel()
        {

        }
        [BindProperty]
        public Instructor Instructor { get; set; }
    }

    [BindProperties(SupportsGet = true)]
    public class CreateModel : InstructorsPageModel
    {
        public CreateModel() : base()
        {
        }

        public Instructor Instructor { get; set; }
        [BindNever]
        public int ID { get; set; }
        [BindProperty(Name = "ai_user", SupportsGet = true)]
        public string ApplicationInsightsCookie { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        [BindRequired]
        public DateTime HireDate { get; set; }

        [FromQuery(Name = "Note")]
        public string NoteFromQueryString { get; set; }

        public void OnGet([FromHeader(Name = "Accept-Language")] string language)
        {

        }
    } 
}
