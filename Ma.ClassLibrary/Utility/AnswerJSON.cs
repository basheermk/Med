using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma.ClassLibrary.Utility
{
    public class AnswerJSON
    {
    }

    public class ParseAnswer
    {
        public string AnswerText { get; set; }
        public bool AnswerStatus { get; set; }
    }

    public class ParsePracticeTest
    {
        public long QuestionId { get; set; }
        public long SelectedAnsId { get; set; }
        public int RightAnsStatus { get; set; }
        public float Mark { get; set; }
        public double NegativeMark { get; set; }
        public int Attended { get; set; }
    }

    public class ParseScholarshipTest
    {
        public long QuestionId { get; set; }
        public long SelectedAnsId { get; set; }
        public long RightAnsStatus { get; set; }
        public float Mark { get; set; }
        public double NegativeMark { get; set; }
       // public int Attended { get; set; }
    }
}
