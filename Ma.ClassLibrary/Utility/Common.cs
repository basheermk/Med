using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma.ClassLibrary.Utility
{
   public class Common
    {
       
    }
    public class LevelWithLockStatus
    {

        public string Level { get; set; }
        public string LevelName { get; set; }
        public long LevelID { get; set; }
        public long Subtopic { get; set; }
        public int LockStatus { get; set; }
        public int firstlockid { get; set; } //added to check first level of all subjects
        public int paidfirstlockid { get; set; }//added to check first level of all subjects for registered user but not paid

       



        //public long SubTopicID { get; set; }
        //public System.DateTime Timestamp{get; set; }

        public int? OrderValue { get; set; }





    }
    public class PracticeAttempt
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long SubjectId { get; set; }
        public long SUbtopicid { get; set; }
        public long LevelId { get; set; }
        public long Attempt { get; set; }
    }
    public class Examview
    {
        public long ExamId { get; set; }
        public string ExamName { get; set; }
        public string ExamStartDate { get; set; }
        public string ExamEndDate { get; set; }
      
    }

    public class ExamStartQuestions
    {
        public long questionId { get; set; }
        public double mark { get; set; }
        public long questionTypeId { get; set; }
        public string questionName { get; set; }
        public bool? isActive { get; set; }
        public string questionTimeStamp { get; set; }
        public string explanation { get; set; }
        public double? negativeMark { get; set; }
        public string QuestionImagse { get; set; }

    }

    public class Preparationview
    {

        public long ExamId { get; set; }
        public long PreparationId { get; set; }
        public string PreparationName { get; set; }
        public string PreparationStartDate { get; set; }
        public string PreparationEndDate { get; set; }
        //public string duration { get; set; }
        //public string PreparationStartDate { get; set; }
        //public string PreparationEndDate { get; set; }

    }
    public class StudentRank
    {
        public string Name { get; set; }
        public long UserId { get; set; }
        //public string Name { get; set; }
        public long ClassId { get; set; }
        public long ExamId { get; set; }
        public Guid ExamGuid { get; set; }
        public string ClassName { get; set; }
        public string SchoolName { get; set; }
        public string Location { get; set; }
        public string ExamName { get; set; }
        public DateTime ExamDate { get; set; }
        public float Mark { get; set; }
        public Guid UserGuid { get; set; }
        public int Rank { get; set; }
        public List<RankList> RankList { get; set; }
        public string FilePath { get; set; }
        // public Files File { get; set; }

        public long Examtime { get; set; }

    }
    public class RankList
    {
        public int Rank { get; set; }
        public long UserId { get; set; }
    }

    public class ReportResult
    {
        public long TotalQuestion { get; set; }
        public long AttendedNo { get; set; }
        public long UnattendedNo { get; set; }
        public long RightNo { get; set; }
        public long WrongNo { get; set; }
        public List<ReportList> ReportList { get; set; }
    }

    public class ReportList
    {
        public long QuestionId { get; set; }
        public string QuestionName { get; set; }
        public AnswerStatus Status { get; set; }
        public long UserAnswerId { get; set; }
    }

    public class AnswerCount
    {
        public int COUNT { get; set; }
    }

    public class UserPointsAchievements
    {
        public long UserId { get; set; }
        public string SchoolName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PracticeTestName { get; set; }
        public long Points { get; set; }
        public long UserGuid { get; set; }

    }


}
