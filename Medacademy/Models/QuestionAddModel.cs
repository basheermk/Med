using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{
    public class QuestionAddModel
    {
        public long LevelID { get; set; }
        public long questionId { get; set; }
        public long questionTypeId { get; set; }
        public long answerTypeId { get; set; }
        public string answerData { get; set; }
        public string question { get; set; }
        public string answerExplanation { get; set; }
        public string questionImage { get; set; }
        public string ansExpImage { get; set; }

        //public long imageedit { get; set; }

    }
}