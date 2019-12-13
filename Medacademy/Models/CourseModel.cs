using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{
    public class CourseModel
    {
        [Required(ErrorMessage = "Course Name Required")]
        public string CourseName { get; set; }
        public long CourseId { get; set; }
        [Required(ErrorMessage = "Course Subject Name Required")]
        public string CourseSubjectName { get; set; }
        [Required(ErrorMessage = "Course Price Required")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Course Duration Required")]
        public string Duration { get; set; }
        [Required(ErrorMessage = "Course Details Required")]
        public string Details { get; set; }
        [Required(ErrorMessage = "Course Image Required")]
        public string Courseimage { get; set; }
            public string Courseeditimage { get; set; }
        public bool Isactive { get; set; }
        //sibi
        public System.DateTime TimeStamp { get; set; }
        public string Files { get; set; }
        public PaginationModel PaginationModel { get; set; }
    }
}