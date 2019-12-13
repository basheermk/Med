using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{
    public class ExamModel
    {
        public long ExamID { get; set; }      
        public long GroupID { get; set; }

        public string GroupData { get; set; }

        [Required(ErrorMessage = "Exam Name Required")]
        public string ExamName { get; set; }
        [Required(ErrorMessage = "Duration Required")]
        public TimeSpan Duration { get; set; }
        [Required(ErrorMessage = "StartDate Required")]
        public string StartDate { get; set; }
        [Required(ErrorMessage = "EndDate Required")]
        public string EndDate { get; set; }
        public bool IsActive { get; set; }

        public bool ActiveStatus { get; set; }

        public bool Upcomingstatus { get; set; }
        public bool PublishStatus { get; set; }
        public System.DateTime Timestamp { get; set; }

        public string StrtDate { get; set; }
     
        public string EnDate { get; set; }

        public string GroupDataEdit { get; set; }
    }
}