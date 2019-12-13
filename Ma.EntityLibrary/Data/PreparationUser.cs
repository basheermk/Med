using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ma.ClassLibrary.Utility;

namespace Ma.EntityLibrary.Data
{
    public class PreparationUser : BaseReference
    {
        private tb_Preparation preparations;
        public PreparationUser() { }
        public PreparationUser(tb_Preparation crs) { preparations = crs; }
        public PreparationUser(long preparationid) { preparations = _Entities.tb_Preparation.FirstOrDefault(x => x.PreparationId == preparationid); }
        public long PreparationId { get { return preparations.PreparationId; } }
        public long ExamId { get { return preparations.ExamId; } }
        public string StartDate { get { return preparations.StartDate; } }
        public string EndDate { get { return preparations.EndDate; } }
        public bool IsActive { get { return preparations.IsActive; } }
        public System.DateTime Timestamp { get { return preparations.Timestamp; } }
        public System.TimeSpan Duration { get { return preparations.Duration; } }
        public string PreparationName { get { return preparations.PreparationName; } }


        public ReportResult GetPreparationReport(long examId, long userId, long preparationid)
        {
            var questions = (from exams in _Entities.tb_Question
                             join examgrps in _Entities.tb_ExamQuestion on exams.QuestionId equals examgrps.QuestionId
                             join exampreparation in _Entities.tb_UserPreparationAttend on examgrps.QuestionId equals exampreparation.QuestionId
                             where examgrps.ExamId == examId
                             orderby exams.QuestionId ascending
                             select new
                             {
                                 exams.Question,
                                 exams.QuestionId,
                                 exams.Explanation,
                                 exams.QuestionTypeId,
                                 exams.Mark,
                                 exams.NegativeMark,
                             }).ToList();


            var userExamAttends = _Entities.tb_UserPreparationAttend.Where(z => z.UserId == userId && z.PreparationId == preparationid && z.IsActive).ToList();
            ReportResult result = new ReportResult();
            result.ReportList = new List<ReportList>();
            result.TotalQuestion = questions.Count;
            result.AttendedNo = userExamAttends.Count;
            result.UnattendedNo = result.TotalQuestion - result.AttendedNo;
            result.RightNo = userExamAttends.Count(z => z.tb_Answer.RightStatus == 1);
            result.WrongNo = userExamAttends.Count(z => z.tb_Answer.RightStatus == 0);
            foreach (var item in questions)
            {
                var entry = new ReportList();
                entry.QuestionId = item.QuestionId;
                entry.QuestionName = item.Question;
                if (userExamAttends.Any(z => z.QuestionId == item.QuestionId))
                    entry.Status = (AnswerStatus)userExamAttends.Where(z => z.QuestionId == item.QuestionId).Select(z => z.tb_Answer.RightStatus == 0 ? 0 : 1).FirstOrDefault();
                else
                    entry.Status = AnswerStatus.NotAnswered;
                entry.UserAnswerId = _Entities.tb_UserExamAttend.Where(z => z.QuestionId == item.QuestionId).Select(z => z.UserAnswerId).FirstOrDefault();
                result.ReportList.Add(entry);
            }
            return result;
        }

        public ReportResult GetPreparationReportQuestionwise(long examId, long userId, long preparationid)
        {
            var questions = (from exams in _Entities.tb_Question
                             join examgrps in _Entities.tb_ExamQuestion on exams.QuestionId equals examgrps.QuestionId
                             join exampreparation in _Entities.tb_UserPreparationAttend on examgrps.QuestionId equals exampreparation.QuestionId
                             where examgrps.ExamId == examId
                             orderby exams.QuestionId ascending
                             select new
                             {
                                 exams.Question,
                                 exams.QuestionId,
                                 exams.Explanation,
                                 exams.QuestionTypeId,
                                 exams.Mark,
                                 exams.NegativeMark,
                             }).ToList();


            var userExamAttends = _Entities.tb_UserPreparationAttend.Where(z => z.UserId == userId && z.PreparationId == preparationid && z.IsActive).ToList();
            ReportResult result = new ReportResult();
            result.ReportList = new List<ReportList>();
            result.TotalQuestion = questions.Count;
            result.AttendedNo = userExamAttends.Count;
            result.UnattendedNo = result.TotalQuestion - result.AttendedNo;
            result.RightNo = userExamAttends.Count(z => z.tb_Answer.RightStatus == 1);
            result.WrongNo = userExamAttends.Count(z => z.tb_Answer.RightStatus == 0);
            foreach (var item in questions)
            {
                var entry = new ReportList();
                entry.QuestionId = item.QuestionId;
                entry.QuestionName = item.Question;
                if (userExamAttends.Any(z => z.QuestionId == item.QuestionId))
                    entry.Status = (AnswerStatus)userExamAttends.Where(z => z.QuestionId == item.QuestionId).Select(z => z.tb_Answer.RightStatus == 0 ? 0 : 1).FirstOrDefault();
                else
                    entry.Status = AnswerStatus.NotAnswered;
                entry.UserAnswerId = _Entities.tb_UserExamAttend.Where(z => z.QuestionId == item.QuestionId).Select(z => z.UserAnswerId).FirstOrDefault();
                result.ReportList.Add(entry);
            }
            return result;
        }

    }
}
