using Medacademy.Models;
using Ma.ClassLibrary.Utility;
using Ma.EntityLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;


namespace Medacademy.Controllers
{
    public class UserAttendController : BaseController
    {
        public DateTime CurrentTime = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        // GET: UserAttend
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public object SubmitPractice(PracticeWorksheetModel model)
        {

            long pointEarned = 0;
            long subtopicid = 0;
            long orderValue = 0;
            bool status = false;
            string practiceTestName = "";
            long Levelid = 0;
            string msg = string.Empty;
            int nowUnlocked = 2;
            long attempId;
            var answerList = JsonConvert.DeserializeObject<List<ParsePracticeTest>>(model.AnswerSet);
            var practiceAtm = Entities.tb_PracticeAttempt.Where(z => z.UserId == model.userId && z.Levelid == model.levelid && z.UnlockedStatus == true).ToList();
            var practAttempt = Entities.tb_PracticeAttempt.Where(z => z.UserId == model.userId && z.Levelid == model.levelid && z.Date == model.startTime).FirstOrDefault();
            if (practAttempt != null)
            {
                attempId = practAttempt.Id;
            }
            else
            {
                // var score = Entities.tb_ScoreRange.Where(z => z.MinMark >= model.correctAnsCount && z.MaxMark <= model.correctAnsCount && z.IsActive && z.OrderValue == 3).FirstOrDefault();
                var score = model.correctAnsCount;

                var PracticeTest = Entities.tb_Level.Where(z => z.LevelID == model.levelid).FirstOrDefault();
                var subtopic = Entities.tb_Subtopic.Where(z => z.SubTopicID == PracticeTest.SubTopicID).FirstOrDefault();
                var attempt = Entities.tb_PracticeAttempt.Where(z => z.UserId == model.userId && z.Levelid == model.levelid).Count();
                var obj = new tb_PracticeAttempt();
                obj.Attempt = attempt + 1;
                obj.SubtopicId = PracticeTest.SubTopicID;
                subtopicid = PracticeTest.SubTopicID;
                //  orderValue = PracticeTest.orderValue;
                obj.Date = model.startTime;
                obj.Levelid = model.levelid;
                obj.UserId = model.userId;
                obj.TopicId = subtopic.TopicID;

                obj.UnlockedStatus = false;
                // obj.LockedStatus = score == null ? false : true;
                practiceTestName = PracticeTest.LevelName;
                Levelid = PracticeTest.LevelID;
                Entities.tb_PracticeAttempt.Add(obj);
                Entities.SaveChanges();
                attempId = obj.Id;
            }

            var removeList = Entities.tb_UserAttend.Where(z => z.LevelId == model.levelid && z.UserId == model.userId && z.PracticeAttemptId == attempId).ToList();
            foreach (var item in removeList)
            {
                Entities.tb_UserAttend.Remove(item);
            }
            Entities.SaveChanges();
            try
            {
                foreach (var item in answerList)
                {
                    var userAttend = Entities.tb_UserAttend.Create();
                    userAttend.Mark = 4;
                    userAttend.LevelId = model.levelid;
                    userAttend.QuestionId = item.QuestionId;
                    userAttend.UserAnswerId = item.SelectedAnsId;
                    userAttend.AttendStatus = item.Attended;
                    userAttend.UserId = model.userId;
                    userAttend.RightAnswerId = 2;
                    userAttend.IsActive = true;
                    userAttend.TimeStamp = CurrentTime;
                    userAttend.PracticeAttemptId = attempId;
                    Entities.tb_UserAttend.Add(userAttend);
                }

                status = Entities.SaveChanges() > 0;
            }
            catch (Exception exx) { }


            if (model.correctAnsCount >= 8)
            {
                var isPractExist = Entities.tb_UnlockedLevel.Where(z => z.LevelId == Levelid && z.UserId == model.userId && z.IsActive && z.CompletedStatus == true).FirstOrDefault();
                var point = Entities.tb_PracticePoint.Where(z => z.UserId == model.userId).FirstOrDefault();
                var pract = Entities.tb_PracticeAttempt.Where(z => z.UserId == model.userId && z.Id == attempId).FirstOrDefault();
                var nextChapter = Entities.tb_Level.Where(z => z.SubTopicID == subtopicid && z.IsActive && z.orderValue > orderValue).OrderBy(z => z.orderValue).FirstOrDefault();



                if (isPractExist == null)
                {

                    var unlockAdd = Entities.tb_UnlockedLevel.Create();
                    unlockAdd.UserId = model.userId;
                    unlockAdd.LevelId = Levelid;
                    unlockAdd.SubtopicId = subtopicid;
                    // unlockAdd.UnlockedLevelGuid = Guid.NewGuid();
                    unlockAdd.IsActive = true;
                    unlockAdd.TimeStamp = CurrentTime;
                    unlockAdd.CompletedStatus = true;
                    Entities.tb_UnlockedLevel.Add(unlockAdd);
                    status = Entities.SaveChanges() > 0 ? true : false;
                    nowUnlocked = 1;

                }

                if (nextChapter != null)
                {
                    var unlockAdd = Entities.tb_UnlockedLevel.Create();
                    unlockAdd.UserId = model.userId;
                    unlockAdd.LevelId = nextChapter.LevelID;
                    unlockAdd.SubtopicId = subtopicid;
                    //unlockAdd.UnlockedLevelGuid = Guid.NewGuid();
                    unlockAdd.IsActive = true;
                    unlockAdd.TimeStamp = CurrentTime;
                    unlockAdd.CompletedStatus = false;
                    Entities.tb_UnlockedLevel.Add(unlockAdd);
                    Entities.SaveChanges();
                }

                if (practiceAtm.Count == 0)
                {
                    if (pract.Attempt < 4)
                    {
                        var points = Entities.tb_PracticePoint.Where(z => z.UserId == model.userId).FirstOrDefault();
                        if (points != null)
                        {
                            if (pract.UnlockedStatus == false && pract.Attempt == 3)
                                points.Points = points.Points + 1;
                            else if (pract.UnlockedStatus == false && pract.Attempt == 2)
                                points.Points = points.Points + 2;
                            else if (pract.UnlockedStatus == false && pract.Attempt == 1)
                                points.Points = points.Points + 3;


                            pract.UnlockedStatus = true;
                            Entities.SaveChanges();
                        }
                        else
                        {
                            var obj = new tb_PracticePoint();
                            obj.UserId = model.userId;
                            if (pract.UnlockedStatus == false && pract.Attempt == 3)
                                obj.Points = 1;
                            else if (pract.UnlockedStatus == false && pract.Attempt == 2)
                                obj.Points = 2;
                            else if (pract.UnlockedStatus == false && pract.Attempt == 1)
                                obj.Points = 3;

                            Entities.tb_PracticePoint.Add(obj);
                            pract.UnlockedStatus = true;
                            Entities.SaveChanges();
                        }
                        var obj2 = new tb_PracticePointDetails();
                        obj2.UserId = model.userId;
                        if (pract.Attempt == 3)
                            obj2.Points = 1;
                        else if (pract.Attempt == 2)
                            obj2.Points = 2;
                        else if (pract.Attempt == 1)
                            obj2.Points = 3;
                        obj2.TimeStamp = CurrentTime;
                        obj2.AttemptId = attempId;
                        pointEarned = obj2.Points;
                        obj2.LevelId = pract.Levelid;
                        Entities.tb_PracticePointDetails.Add(obj2);
                        Entities.SaveChanges();
                    }
                }
            }

            var scoreRange = Entities.tb_ScoreRange.Where(z => z.MinMark <= model.correctAnsCount && z.MaxMark >= model.correctAnsCount && z.LevelName == practiceTestName && z.IsActive).FirstOrDefault();
            var grade = scoreRange.Grade;



            //string id = model.PracticeTestGuid + "~" + attempId + "~" + model.practiceTest + "~" + model.subject + "~" + model.chapter + "~" + model.className + "~" + model.userId + "~" + grade + "~" + pointEarned;
            //if (status)
            //{
            //    Thread processThread = new Thread(() => new UserAttendController().PracticeMail(id));
            //    processThread.Start();
            //}
            return Json(new { status = status, msg = msg, attemptId = attempId, nowUnlocked = nowUnlocked }, JsonRequestBehavior.AllowGet);

        }
    }
}