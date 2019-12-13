using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma.EntityLibrary.Data
{
    public class ExamQuestion : BaseReference

    {
        private tb_ExamQuestion ExamQuestions;
        public ExamQuestion() { }
        public ExamQuestion(tb_ExamQuestion crs) { ExamQuestions = crs; }
        public ExamQuestion(long ID) { ExamQuestions = _Entities.tb_ExamQuestion.FirstOrDefault(x => x.Id == ID); }


        public long? ExamID { get { return ExamQuestions.ExamId; } }
        public long? QuestionID { get { return ExamQuestions.QuestionId; } }
        public string ExamName { get { return ExamQuestions.ExamName; } }
        public long ID { get { return ExamQuestions.Id; } }

        public int GetCount(long examid)
        {
            return _Entities.tb_ExamQuestion.Where(x => x.ExamId == examid).Count();
        }
       

        public List<ExamQuestions> GetQuestions(long examid)
        {
            var results = (from question in _Entities.tb_Question
                           join examquestion in _Entities.tb_ExamQuestion on question.QuestionId equals examquestion.QuestionId
                           where examquestion.ExamId == examid
                           orderby question.QuestionId ascending
                           select new
                           {
                               question.Question,
                               question.QuestionId
                           }).ToList();

            List<ExamQuestions> list = new List<ExamQuestions>();
            foreach (var item in results)
            {
                ExamQuestions one = new ExamQuestions();
                one.Question = item.Question;
                one.QuestionId = item.QuestionId;

                list.Add(one);
            }

            return list;
        }

    }

}
