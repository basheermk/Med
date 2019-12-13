using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma.EntityLibrary.Data
{
    public class Answers :BaseReference
    {
        private tb_Answer answers;
        public Answers(tb_Answer ans) { answers = ans; }
        public Answers(long answerid) { answers = _Entities.tb_Answer.FirstOrDefault(x => x.AnswerId == answerid); }
        public long AnswerId { get { return answers.AnswerId; } }
        public string Answer { get { return answers.Answer; } }
        public long QuestionId { get { return answers.QuestionId; } }
        public long AnswerTypeId { get { return answers.AnswerTypeId; } }
        public int Status { get { return answers.RightStatus; } }
        public DateTime? TimeStamp { get { return answers.TimeStamp; } }
        public bool? IsActive { get { return answers.IsActive; } }
        public string AnswerImage { get { return answers.AnswerImage; } }
    }
}
