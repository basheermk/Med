using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{
    public class QuestionExcelUpload
    {

        public long LevelID { get; set; }
        public long questionId { get; set; }
        public long mark { get; set; }
        public long negative { get; set; }
        public long questionTypeId { get; set; }
        public long answerTypeId { get; set; }
        public string Answer { get; set; }
        public string Question { get; set; }
        public string Explanation { get; set; }
        public bool isactive { get; set; }
        public int Rightstatus { get; set; }
        public DateTime timestamp { get; set; }
        public long CourseId { get; set; }
        
    }
}