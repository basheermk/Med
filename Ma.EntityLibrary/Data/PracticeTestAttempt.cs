using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ma.ClassLibrary.Utility;

namespace Ma.EntityLibrary.Data
{
    public class PracticeTestAttempt : BaseReference
    {

        private tb_PracticeAttempt attempt;
        public PracticeTestAttempt(tb_PracticeAttempt pa) { attempt = pa; }
        public PracticeTestAttempt(long uid) { attempt = _Entities.tb_PracticeAttempt.FirstOrDefault(x => x.Id == uid); }

        public long Id { get { return attempt.Id; } }
        public long UserId { get; set; }
        public long Levelid { get { return attempt.Levelid; } }
        public System.DateTime Date { get; set; }
        public long Attempt { get { return attempt.Attempt; } }
        //public Nullable<bool> UnlockedStatus { get; set; }
        public long SubtopicId { get { return attempt.SubtopicId; } }
        public long TopicId { get { return attempt.TopicId; } }

        public DateTime TimeStamp { get { return attempt.Date; } }


        public SubTopic subtopic { get { return new SubTopic(attempt.tb_Subtopic); } }

        public Level practiceTest { get { return new Level(attempt.tb_Level); } }
        public List<tb_UserAttend> Attend { get { return attempt.tb_UserAttend.Where(z => z.AttendStatus == 1).ToList(); } }
        public long RightAnswerCount { get { return Attend.Where(z => z.tb_Answer.RightStatus == 1).Count(); } }
        public long WrongAnswerCount { get { return Attend.Where(z => z.tb_Answer.RightStatus == 0).Count(); } }
        //public long TotalAnswerCount { get { return Entities.Database.SqlQuery<AnswerCount>(string.Format("select COUNT(*) AS COUNT from tb_Question where ParentGuid='{0}' AND IsActive=1", attempt.tb_PracticeTest.PracticeTestGuid)).FirstOrDefault().COUNT; } }
        public long TotalAnswerCount { get { return _Entities.Database.SqlQuery<AnswerCount>(string.Format("select COUNT(*) AS COUNT from tb_UserAttend where PracticeAttemptId='{0}' AND IsActive=1", attempt.Id)).FirstOrDefault().COUNT; } }
        public long UnattendAnswerCount { get { return TotalAnswerCount - (RightAnswerCount + WrongAnswerCount); } }
    }
}
