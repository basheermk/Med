using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma.EntityLibrary.Data
{
     public class Question : BaseReference
    {
        private tb_Question question;
        //public Question() { }  //added extra by basheer
        public Question(tb_Question quest) { question = quest; }
        public Question(long questionId) { question = _Entities.tb_Question.FirstOrDefault(x => x.QuestionId == questionId); }
      
        public long questionId { get { return question.QuestionId; } }
        public double mark { get { return question.Mark; } }
        public long questionTypeId { get { return question.QuestionTypeId; } }
        public string questionName { get { return question.Question; } }
        public bool? isActive { get { return question.IsActive; } }
        public string questionTimeStamp { get { return question.TimeStamp.ToLongDateString(); } }
        public string explanation { get { return question.Explanation; } }
        public double? negativeMark { get { return question.NegativeMark; } }

        public long LevelID { get { return question.LevelID; } }
        public string QuestionImage { get { return question.QuestionImage; } }
        public string AnswerExpImage { get { return question.AnswerExpImage; } }

        

        public List<Answers> GetAnswer()
        {
            var test = _Entities.tb_Answer.Where(x => x.QuestionId == question.QuestionId && x.IsActive == true).ToList()
                 .Select(y => new Answers(y)).ToList();
            return test;
        }

    }
}
