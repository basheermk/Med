using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ma.ClassLibrary;
using Ma.ClassLibrary.Utility;

namespace Ma.EntityLibrary.Data
{
    public class StudentsList : BaseReference
    {
        private tb_Login StudentsLists;
        public StudentsList() { }
        public StudentsList(tb_Login crs) { StudentsLists = crs; }
        public StudentsList(long studentid) { StudentsLists = _Entities.tb_Login.FirstOrDefault(x => x.UserId == studentid); }
        public long UserId { get { return StudentsLists.UserId; } }
        public string Password { get { return StudentsLists.Password; } }
        public string FirstName { get { return StudentsLists.FirstName; } }
        public string LastName { get { return StudentsLists.LastName; } }
        public string Gender { get { return StudentsLists.Gender; } }
        public string Email { get { return StudentsLists.Email; } }
        public string ContactNo { get { return StudentsLists.ContactNo; } }
        public Nullable<System.DateTime> DOB { get { return StudentsLists.DOB; } }
        public string SchoolName { get { return StudentsLists.SchoolName; } }
        public Nullable<int> RoleId { get { return StudentsLists.RoleId; } }
        public Nullable<bool> IsActive { get { return StudentsLists.IsActive; } }
        public Nullable<System.DateTime> TimeStamp { get { return StudentsLists.TimeStamp; } }
        public Nullable<System.Guid> UserGuid { get { return StudentsLists.UserGuid; } }
        public string Location { get { return StudentsLists.Location; } }
        public string State { get { return StudentsLists.State; } }
        public string Address { get { return StudentsLists.Address; } }
        public string PostalCode { get { return StudentsLists.PostalCode; } }
        public Nullable<bool> DisableStatus { get { return StudentsLists.DisableStatus; } }
        public string ReferenceCode { get { return StudentsLists.ReferenceCode; } }
        public string PromoCode { get { return StudentsLists.PromoCode; } }
        public string Pin { get { return StudentsLists.Pin; } }
        public string FilesName { get { return StudentsLists.FilesName; } }
        public string SessionId { get { return StudentsLists.SessionId; } }

        public int isPaid { get { return StudentsLists.tb_Payment.Where(x => x.PaymentType == 1).ToList().Select(y => new Payment(y)).Count(); ; } }

        public List<StudentViewList> GetStudentdetails()
        {


            List<StudentViewList> list = new List<StudentViewList>();
            var results = (from login in _Entities.tb_Login
                           join studentgroup in _Entities.tb_GroupStudent on login.UserId equals studentgroup.StudentID into ps
                           from studentgroup in ps.DefaultIfEmpty()
                           where login.IsActive == true && login.RoleId==2
                           orderby login.UserId descending
                           select new
                           {
                               login.FirstName,
                               login.LastName,
                               GroupID = studentgroup == null ? 0 : studentgroup.GroupID,
                               login.UserId
                           }).ToList();

            var a = results.Select(x => x.UserId).Distinct().ToList();
            foreach (var item in a)
            {
                var b = results.Where(x => x.UserId == item).ToList();
                string group = "";
                StudentViewList one = new StudentViewList();
                one.Name = b.FirstOrDefault().FirstName + " " + b.FirstOrDefault().LastName;
                foreach (var item2 in b)
                {
                    var c = _Entities.tb_Group.Where(x => x.Group_ID == item2.GroupID).FirstOrDefault();
                    if (c == null)
                    {

                    }
                    else if (group == "")
                    {
                        group = c.Groupname;
                    }
                    else
                        group = group + " | " + c.Groupname;
                }
                one.GroupName = group;
                one.GroupID = b.FirstOrDefault().GroupID;
                one.StudentId = item;
                list.Add(one);
            }
            return list;
        }
        public List<StudentViewList> GetStudentdetails(long groupid)
        {
            List<StudentViewList> list = new List<StudentViewList>();
            if (groupid == 0)
            {

                var results = (from login in _Entities.tb_Login
                               join studentgroup in _Entities.tb_GroupStudent on login.UserId equals studentgroup.StudentID
                               where studentgroup.IsActive == true && login.IsActive == true && login.RoleId == 2
                               orderby login.UserId descending
                               select new
                               {
                                   login.FirstName,
                                   login.LastName,
                                   studentgroup.GroupID,
                                   login.UserId
                               }).ToList();

                var a = results.Select(x => x.UserId).Distinct().ToList();
                foreach (var item in a)
                {
                    var b = results.Where(x => x.UserId == item).ToList();
                    string group = "";
                    StudentViewList one = new StudentViewList();
                    one.Name = b.FirstOrDefault().FirstName + " " + b.FirstOrDefault().LastName;
                    foreach (var item2 in b)
                    {
                        var c = _Entities.tb_Group.Where(x => x.Group_ID == item2.GroupID).FirstOrDefault();
                        if (group == "")
                            group = c.Groupname;
                        else
                            group = group + " | " + c.Groupname;
                    }
                    one.GroupName = group;
                    one.GroupID = b.FirstOrDefault().GroupID;
                    one.StudentId = item;
                    list.Add(one);
                }


            }
            else
            {
                var results = (from login in _Entities.tb_Login
                               join studentgroup in _Entities.tb_GroupStudent on login.UserId equals studentgroup.StudentID
                               where studentgroup.GroupID == groupid && studentgroup.IsActive == true && login.IsActive == true && login.RoleId == 2
                               orderby login.UserId descending
                               select new
                               {
                                   login.FirstName,
                                   login.LastName,
                                   studentgroup.GroupID,
                                   login.UserId
                               }).ToList();

                var a = results.Select(x => x.UserId).Distinct().ToList();
                foreach (var item in a)
                {
                    var b = results.Where(x => x.UserId == item).ToList();
                    string group = "";
                    StudentViewList one = new StudentViewList();
                    one.Name = b.FirstOrDefault().FirstName + " " + b.FirstOrDefault().LastName;
                    foreach (var item2 in b)
                    {
                        var c = _Entities.tb_Group.Where(x => x.Group_ID == item2.GroupID).FirstOrDefault();
                        if (group == "")
                            group = c.Groupname;
                        else
                            group = group + " | " + c.Groupname;
                    }
                    one.GroupName = group;
                    one.GroupID = b.FirstOrDefault().GroupID;
                    one.StudentId = item;
                    list.Add(one);
                }

                //var results = (from login in _Entities.tb_Login
                //               join studentgroup in _Entities.tb_GroupStudent on login.UserId equals studentgroup.StudentID
                //               where studentgroup.GroupID == groupid
                //               orderby login.UserId ascending
                //               select new
                //               {
                //                   login.FirstName,
                //                   login.LastName,
                //                   studentgroup.GroupID,
                //                   login.UserId
                //               }).ToList();


                //foreach (var item in results)
                //{
                //    StudentViewList one = new StudentViewList();
                //    one.Name = item.FirstName + " " + item.LastName;

                //    one.GroupID = item.GroupID;
                //    one.StudentId = item.UserId;

                //    list.Add(one);
                //}
            }
            return list;
        }

        public List<StudentViewList> Getgroupname(long userid)
        {

            var results = (from groups in _Entities.tb_Group
                           join studentgroup in _Entities.tb_GroupStudent on groups.Group_ID equals studentgroup.GroupID
                           where studentgroup.StudentID == userid && studentgroup.IsActive == true 
                           orderby groups.Group_ID ascending
                           select new
                           {
                               groups.Groupname,
                               groups.Group_ID

                           }).ToList();

            List<StudentViewList> list = new List<StudentViewList>();
            foreach (var item in results)
            {
                StudentViewList one = new StudentViewList();
                one.GroupName = item.Groupname;

                one.GroupID = item.Group_ID;

                list.Add(one);
            }
            return list;

        }

        public List<StudentsList> GetRegisteredStudents()
        {
            return _Entities.tb_Login.Where(x => x.IsActive == true && x.RoleId == 2).OrderByDescending(x=> x.UserId).ToList().Select(y => new StudentsList(y)).ToList();
        }
        //public bool IsExamPaid(long userId)
        //{
        //    bool result = false;
        //    var status = _Entities.tb_Payment.Where(x => x.UserId == userId  && x.PaymentType == 1 && x.PaidStatus).FirstOrDefault();
        //    if (status != null)
        //        result = status.PaidStatus;
        //    return result;
        //}

        public List<PracticeTestAttempt> GetPracticeTestReport()
        {
            var quest = _Entities.tb_PracticeAttempt.Where(z => z.UserId == StudentsLists.UserId).OrderByDescending(z => z.Id)
                .ToList().Select(z => new PracticeTestAttempt(z)).ToList();
            return quest;
        }

        public ReportResult GetReport(long practiceTestGuid, long attemptId)
        {
            var questions = _Entities.tb_Question.Where(z => z.LevelID == practiceTestGuid && z.IsActive).ToList();
            var userAttends = _Entities.tb_UserAttend.Where(z => z.UserId == StudentsLists.UserId && z.LevelId == practiceTestGuid && z.PracticeAttemptId == attemptId && z.IsActive).ToList();
            ReportResult result = new ReportResult();
            result.ReportList = new List<ReportList>();
            //result.TotalQuestion = questions.Count;
            //result.AttendedNo = userAttends.Where(z => z.AttendStatus == 1).ToList().Count;
            //result.UnattendedNo = result.TotalQuestion - result.AttendedNo;
            //result.RightNo = userAttends.Where(z => z.AttendStatus == 1).ToList().Count(z => z.tb_Answer.RightStatus == 1);
            //result.WrongNo = userAttends.Where(z => z.AttendStatus == 1).ToList().Count(z => z.tb_Answer.RightStatus == 0);
            List<tb_Question> questionList = new List<tb_Question>();
            foreach (var item in userAttends)
            {
                if (questions.Any(z => z.QuestionId == item.QuestionId))
                {
                    var tempQuest = questions.Where(z => z.QuestionId == item.QuestionId).FirstOrDefault();
                    questionList.Add(tempQuest);
                }
            }
            foreach (var item in questionList)
            {
                var entry = new ReportList();
                entry.QuestionId = item.QuestionId;
                entry.QuestionName = item.Question;
                if (userAttends.Any(z => z.QuestionId == item.QuestionId && z.AttendStatus == 1))
                    entry.Status = (AnswerStatus)userAttends.Where(z => z.QuestionId == item.QuestionId).Select(z => z.tb_Answer.RightStatus).FirstOrDefault();
                else
                    entry.Status = AnswerStatus.NotAnswered;
                entry.UserAnswerId = item.tb_UserAttend.Where(z => z.QuestionId == item.QuestionId).Select(z => z.UserAnswerId).FirstOrDefault();
                result.ReportList.Add(entry);
            }
            result.TotalQuestion = result.ReportList.Count;
            result.AttendedNo = userAttends.Where(z => z.AttendStatus == 1).ToList().Count;
            result.UnattendedNo = result.TotalQuestion - result.AttendedNo;
            result.RightNo = userAttends.Where(z => z.AttendStatus == 1).ToList().Count(z => z.tb_Answer.RightStatus == 1);
            result.WrongNo = userAttends.Where(z => z.AttendStatus == 1).ToList().Count(z => z.tb_Answer.RightStatus == 0);
            return result;
        }

        public List<UserPointsAchievements> NewPointAchievement()
        {
            //return _Entities.Database.SqlQuery<UserPointsAchievements>(string.Format("SELECT TOP 10 [UserId],(SELECT DISTINCT TOP 1 A.[PracticeTestId] FROM tb_UnlockedLevel AS A WHERE A.[UserId]=tb_UnlockedLevel.[UserId]) AS [PracticeTestId], (SELECT [FirstName] FROM  [tb_Login] WHERE [tb_Login].[UserId]=tb_UnlockedLevel.[UserId]) AS FirstName, (SELECT [LastName] FROM [tb_Login] WHERE [tb_Login].[UserId]= tb_UnlockedLevel.[UserId])AS SecondName,(SELECT [UserGuid] FROM  [tb_Login] WHERE [tb_Login].[UserId]=tb_UnlockedLevel.[UserId]) AS UserGuid,(SELECT [PracticeTestName] FROM [tb_PracticeTest] WHERE [tb_PracticeTest].[PracticeTestId]=[tb_UnlockedLevel].[PracticeTestId]) AS PracticeTestName,isnull((SELECT [Points] FROM [tb_PracticePoint] WHERE [tb_PracticePoint].[UserId]=tb_UnlockedLevel.[UserId]),0) AS Points, (SELECT rtrim(ltrim([SchoolName])) FROM [tb_Login] WHERE [tb_Login].[UserId]=tb_UnlockedLevel.[UserId])AS SchoolName FROM tb_UnlockedLevel WHERE [UnlockedLevelId]=(SELECT TOP 1[UnlockedLevelId] FROM tb_UnlockedLevel AS A WHERE A.[UserId]=tb_UnlockedLevel.[UserId] ORDER BY [UnlockedLevelId] DESC ) ORDER BY [UnlockedLevelId] DESC")).ToList();
            return _Entities.Database.SqlQuery<UserPointsAchievements>(string.Format("SELECT TOP 20 [UserId],(SELECT DISTINCT TOP 1 A.[LevelID] FROM tb_UnlockedLevel AS A WHERE A.[UserId] = tb_UnlockedLevel.[UserId]) AS[PracticeTestId],(SELECT[FirstName] FROM[tb_Login] WHERE[tb_Login].[UserId] = tb_UnlockedLevel.[UserId]) AS FirstName,(SELECT[LastName] FROM[tb_Login] WHERE[tb_Login].[UserId] = tb_UnlockedLevel.[UserId])AS SecondName,(SELECT[UserId] FROM[tb_Login] WHERE[tb_Login].[UserId] = tb_UnlockedLevel.[UserId]) AS UserGuid,(SELECT[LevelName] FROM[dbo].[tb_Level] WHERE[tb_Level].[LevelID] =[tb_UnlockedLevel].[LevelID]) AS PracticeTestName,isnull((SELECT[Points] FROM[tb_PracticePoint] WHERE[tb_PracticePoint].[UserId] = tb_UnlockedLevel.[UserId]),0) AS Points,(SELECT rtrim(ltrim([SchoolName])) FROM[tb_Login] WHERE[tb_Login].[UserId] = tb_UnlockedLevel.[UserId])ASSchoolName FROM tb_UnlockedLevel WHERE[UnlockedLevelId] = (SELECT TOP 1[UnlockedLevelId] FROM tb_UnlockedLevel AS A WHERE A.[UserId]=tb_UnlockedLevel.[UserId] ORDER BY[UnlockedLevelId] DESC ) ORDER BY[UnlockedLevelId] DESC")).ToList();
        }
      
    }
}
