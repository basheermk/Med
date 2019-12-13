using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{
    public class TopicModel
    {
        public long TopicID { get; set; }

        [Required(ErrorMessage = "Subject Required")]
        public long SubjectID { get; set; }
             
        [Required(ErrorMessage = "Topic Name Required")]
        public string TopicName { get; set; }

        public long CourseID { get; set; }
        public long PackageID { get; set; }
   
    }
}