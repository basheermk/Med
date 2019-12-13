using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ma.EntityLibrary;

namespace Medacademy.Models
{
    public class PracticeModel
    {

        public long userId { get; set; }
        public long levelid { get; set; }
        public long QuestionId { get; set; }
        public string AnswerSet { get; set; }
        public DateTime startTime { get; set; }
        public long subjectId { get; set; }
        public tb_Login Student { get; set; }
        public long attemptId { get; set; }
        //public string className { get; set; }
        public string subject { get; set; }
        public string Subtopic { get; set; }
        public long packageid { get; set; }
        public long orderValue { get; set; }
        public string level { get; set; }
        public int correctAnsCount { get; set; }
        public long SubtopicId { get; set; }

        public long TopicId { get; set; }

    }
}