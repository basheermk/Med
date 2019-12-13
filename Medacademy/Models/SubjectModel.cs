using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Medacademy.Models
{
    public class SubjectModel
    {
        public long SubjectID { get; set; }

        [Required(ErrorMessage = "Package Required")]
        public long PackageID { get; set; }

        [Required(ErrorMessage = "Group Required")]
        public long GroupID { get; set; }
        [Required(ErrorMessage = "Subject Name Required")]
        public string SubjectName { get; set; }

        public long CourseID { get; set; }






    }
}