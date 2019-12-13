using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{
    public class LocalModel
    {
        public PaginationModel PaginationModel { get; set; }
        public List<PackageModel> LI_Pacages { get; set; }
        public CourseModel CourseModel { get; set; }
        public List<CourseModel> CourseModel_Lists { get; set; }
    }
}