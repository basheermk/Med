using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{
    public class Preparation
    {
        [Required(ErrorMessage = "Start Date Required")]
        public string stdate { get; set; }

        [Required(ErrorMessage = "End Date Required")]
        public string eddate { get; set; }

        public DateTime timestamp { get; set; }

        public long preparationid { get; set; }

        public long examid { get; set; }

        public string examname { get; set; }
        public bool Isactive { get; set; }

        public TimeSpan timer { get; set; }

        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }
    }
}