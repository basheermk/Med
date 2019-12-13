using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ma.ClassLibrary.Utility;
using System.Data.SqlClient;

namespace Ma.EntityLibrary.Data
{
    public class Exam : BaseReference
    {
        private tb_Exam Exams;
        public Exam() { }
        public Exam(tb_Exam crs) { Exams = crs; }
        public Exam(long examid) { Exams = _Entities.tb_Exam.FirstOrDefault(x => x.ExamId == examid); }


        public long ExamId { get { return Exams.ExamId; } }
        public string ExamName { get { return Exams.ExamName; } }
        public TimeSpan Duration { get { return Exams.Duration; } }
        public Nullable<System.Guid> ExamGUID { get { return Exams.ExamGUID; } }
        public System.DateTime TimeStamp { get { return Exams.TimeStamp; } }
        public bool IsActive { get { return Exams.IsActive; } }
        public bool ActiveStatus { get { return Exams.ActiveStatus; } }
        public string ExamEndDate { get { return Exams.ExamEndDate; } }
        public string ExamStartDate { get { return Exams.ExamStartDate; } }
        public Nullable<bool> UpcomingStatus { get { return Exams.UpcomingStatus; } }
        public bool PublishStatus { get { return Exams.PublishStatus; } }

        public List<Exam> GetExams()
        {
            return _Entities.tb_Exam.Where(x=> x.IsActive==true).ToList().Select(q => new Exam(q)).OrderByDescending(c => c.ExamId).ToList();
        }

        public List<ExamGroup> GetExamgroups(long examid)
        {
            var results = (from groups in _Entities.tb_Group
                           join examgrps in _Entities.tb_ExamGroup on groups.Group_ID equals examgrps.GroupId
                           where examgrps.ExamId == examid && examgrps.IsActive == true 
                           orderby groups.Groupname ascending
              select new
              {
                  groups.Groupname,
                  groups.Group_ID
              }).ToList();

            List<ExamGroup>  list = new List<ExamGroup>();
            foreach (var item in results)
            {
                ExamGroup one = new ExamGroup();
                one.Groups = item.Groupname;
                one.GroupID = item.Group_ID;

                list.Add(one);
            }
           
            return list;
        }

        public List<Exam> GetExamsById(long examid)
        {
            return _Entities.tb_Exam.Where(x => x.IsActive == true && x.ExamId == examid && x.ActiveStatus == true).ToList().Select(q => new Exam(q)).OrderByDescending(c => c.ExamId).ToList();
        }
        public List<Examview> GetExambygroupid(long groupid)
        {
            var results = (from exams in _Entities.tb_Exam
                           join examgrps in _Entities.tb_ExamGroup on exams.ExamId equals examgrps.ExamId
                           where examgrps.GroupId == groupid && examgrps.IsActive == true && exams.IsActive == true && exams.ActiveStatus == true && exams.PublishStatus == true
                           
                           orderby exams.ExamId ascending
                           select new
                           {
                               exams.ExamName,
                               exams.ExamId,
                               exams.ExamStartDate,
                               exams.ExamEndDate,
                           }).ToList();

            List<Examview> list = new List<Examview>();
            foreach (var item in results)
            {
                Examview one = new Examview();
                one.ExamName = item.ExamName;
                one.ExamId = item.ExamId;
                one.ExamStartDate = item.ExamStartDate;
                one.ExamEndDate = item.ExamEndDate;

                list.Add(one);
            }

            return list;
        }

        public List<ExamStartQuestions> GetQuestions(long examid)
        {
            //return _Entities.tb_ScholarshipQuestion.Where(z => z.ParentGuid == exams.ExamGUID && z.IsActive).ToList().Select(q => new Question(q)).ToList();

            var results = (from exams in _Entities.tb_Question
                           join examgrps in _Entities.tb_ExamQuestion on exams.QuestionId equals examgrps.QuestionId
                           where examgrps.ExamId == examid
                           orderby exams.QuestionId ascending
                           select new
                           {
                               exams.Question,
                               exams.QuestionId,
                               exams.Explanation,
                               exams.QuestionTypeId,
                               exams.Mark,
                               exams.NegativeMark,
                               exams.QuestionImage
                           }).ToList();

            List<ExamStartQuestions> list = new List<ExamStartQuestions>();
            foreach (var item in results)
            {
                ExamStartQuestions one = new ExamStartQuestions();
                one.questionName = item.Question;
                one.questionId = item.QuestionId;
                one.explanation = item.Explanation;
                one.questionTypeId = item.QuestionTypeId;
                one.mark = item.Mark;
                one.negativeMark = item.NegativeMark;
                one.QuestionImagse = item.QuestionImage;

                list.Add(one);
            }

            return list;
        }

        public List<Answers> GetExamAnswer(long questionid)
        {
            var test = _Entities.tb_Answer.Where(x => x.QuestionId == questionid && x.IsActive == true).ToList()
                 .Select(y => new Answers(y)).ToList();
            return test;
        }

        public List<Preparationview> GetPreparationbygroupid(long groupid)
        {
            var results = (from exams in _Entities.tb_Preparation
                           join examgrps in _Entities.tb_ExamGroup on exams.ExamId equals examgrps.ExamId
                           where examgrps.GroupId == groupid && examgrps.IsActive == true && exams.IsActive == true
                           orderby exams.ExamId ascending
                           select new
                           {
                               exams.PreparationName,
                               exams.ExamId,
                               exams.PreparationId,
                               exams.StartDate,
                               exams.EndDate
                               //exams.Duration,
                           }).ToList();

            List<Preparationview> list = new List<Preparationview>();
            foreach (var item in results)
            {
                Preparationview one = new Preparationview();
                one.PreparationName = item.PreparationName;
                one.ExamId = item.ExamId;
                one.PreparationId = item.PreparationId;
                one.PreparationStartDate = item.StartDate;
                one.PreparationEndDate = item.EndDate;

                list.Add(one);
            }

            return list;
        }

        public List<PreparationUser>GetPreparationById(long preparationid)
        {
            return _Entities.tb_Preparation.Where(x => x.IsActive == true && x.PreparationId == preparationid).ToList().Select(q => new PreparationUser(q)).OrderByDescending(c => c.ExamId).ToList();
        }

        public List<Sp_GetUserResultNew> GetUserResult(long examId)
        {

            // var data = _Entities.sp_GetUserResults(examId).ToList().Select(x => new Sp_GetUserResults(x)).ToList();
            var data = _Entities.sp_GetUserResultsNew(examId).ToList().Select(x => new Sp_GetUserResultNew(x)).ToList();
            

            
            return data;
        }
        public List<ExamRank> GetUserExamResult(long userid)
        {
            return _Entities.tb_ExamRank.Where(x => x.UserId == userid).ToList().Select(q => new ExamRank(q)).OrderByDescending(c => c.ExamId).ToList();
        }

        public ReportResult GetExamReport(long examId, long userId)
        {
            var questions = (from exams in _Entities.tb_Question
                             join examgrps in _Entities.tb_ExamQuestion on exams.QuestionId equals examgrps.QuestionId
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


            var userExamAttends = _Entities.tb_UserExamAttend.Where(z => z.UserId == userId && z.ExamId == examId && z.IsActive).ToList();
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
