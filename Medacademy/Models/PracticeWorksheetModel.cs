using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ma.EntityLibrary;
using Ma.EntityLibrary.Data;
namespace Medacademy.Models
{
    public class PracticeWorksheetModel
    {
        public long userId { get; set; }
        public long levelid { get; set; }
        public long QuestionId { get; set; }
        public string AnswerSet { get; set; }
        public DateTime startTime { get; set; }
        public tb_Login Student { get; set; }
        public long attemptId { get; set; }
        //public string className { get; set; }
        public string Subtopic { get; set; }
        public long packageid { get; set; }
        public long orderValue { get; set; }
        public string level { get; set; }
        public int correctAnsCount { get; set; }
        public long SubtopicId { get; set; }
        public long TopicId { get; set; }

        public string Answers{ get; set; }


        public object PageSize { get; set; }
        public object PageNumber { get; set; }
        public object TotalItemCount { get; set; }
        public int reportCount { get; set; }
        public int levelStatus { get; set; }
        public int nowUnlocked { get; set; }

        public List<PracticeTestAttempt> report { get; set; }
    }
}