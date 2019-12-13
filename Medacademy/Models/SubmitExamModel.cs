using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ma.EntityLibrary;

namespace Medacademy.Models
{
    public class SubmitExamModel
    {
        public long userId { get; set; }
        public long ExamId { get; set; }
        public long QuestionId { get; set; }
        public string AnswerSet { get; set; }
        public DateTime startTime { get; set; }
        public long subjectId { get; set; }
        public tb_Login Student { get; set; }
        public long attemptId { get; set; }

        public long examfinishtime { get; set; }

        public TimeSpan ExamstartTime { get; set; }
    }
}