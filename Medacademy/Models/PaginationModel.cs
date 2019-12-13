using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{
    public class PaginationModel
    {
        public int TotalItems { get; set; }
        public int DividNumber { get; set; }
        public int NumberOfPages { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; } = 1;
        public long CourseID { get; set; }


    }
}