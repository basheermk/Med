using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{
    public class GroupModel
    {
        [Required(ErrorMessage = "Course Required")]
        public long CourseID { get; set; }
        [Required(ErrorMessage = "Group Required")]
        public long PackageID { get; set; }
        public long GroupID { get; set; }
        [Required(ErrorMessage = "Group Name Required")]
        public string GroupName { get; set; }
    }
}