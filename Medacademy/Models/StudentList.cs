using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Medacademy.Models
{
    public class StudentList
    {
        
        public long GroupID { get; set; }
        public string StudentIDData { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }

    }
}