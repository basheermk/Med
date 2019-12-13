using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{
    public class NewsModel
    {
        public long NewsId { get; set; }

        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Heading Required")]
        public string Head { get; set; }

        [Required(ErrorMessage = "Detailed Description Required")]
        public string DetailedDescription { get; set; }

        public string DateofNews { get; set; }

        public string Newsimage { get; set; }
        public string Newseditimage { get; set; }

        public DateTime NewsDateEdit { get; set; }


    }
}