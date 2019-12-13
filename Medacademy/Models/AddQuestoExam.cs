using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{
    public class AddQuestoExam
    { 

        public long ID { get; set; }
        public long LevelID { get; set; }
        public long QuestionId { get; set; }
        public long ExamId { get; set; }
        public string ExamName { get; set; }
        public bool IsActive { get; set; }
        public long QuestionCount { get; set; }
        public long CourseId { get; set; }
        public string CourseName { get; set; }

        public string Question { get; set; }

        public string SelectedData { get; set; }

    }
}