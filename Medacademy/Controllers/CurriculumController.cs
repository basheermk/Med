using Ma.EntityLibrary;
using Medacademy.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Ma.EntityLibrary.Data;
using Ma.ClassLibrary.Utility;

namespace Medacademy.Controllers
{
    public class CurriculumController : Controller
    {
        public DateTime CurrentTime = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        public MAEntities _Entities = new MAEntities();
       

        public ActionResult Index()
        {
            return View();
        }


        #region Worksheet 

        public ActionResult Worksheet()
        {
            var datas = (UserModel)Session["UserLoginFirstTime"];
            if(datas== null)
            {
               
                return View();
            }
           else
            {
                return View(datas);
            }          
            
        }
        
        public PartialViewResult NavigateCourse(long courseid)
        {
            var model = new Medacademy.Models.UserModel();
            model.Nonusercourseid = courseid;
            return PartialView("~/Views/Curriculum/pv_worksheetcourse.cshtml", model);
        }

        public PartialViewResult NavigatePackage(long packageid)
        {
            var model = new Medacademy.Models.UserModel();
            model.PackageID = packageid;
            return PartialView("~/Views/Curriculum/pv_worksheetpackage.cshtml", model);
        }

        public PartialViewResult NavigateSubtopic(long subtopicid)
        {
            var model = new Medacademy.Models.UserModel();
            model.SubtopicID = subtopicid;
            return PartialView("~/Views/Curriculum/pv_worksheetsubtopic.cshtml", model);
        }

        public object Subtopicchange(long subtopicid)
        {
            bool status = false;
            string msg = string.Empty;
            var subtopics = _Entities.tb_Subtopic.Where(z => z.SubTopicID == subtopicid && z.Isactive).FirstOrDefault();
            string video = string.Empty;
            string types = string.Empty;
            string pdf = subtopics.Pdfpath;
            if(subtopics.YouTubeVideo == null)
            {
                video = subtopics.Videopath;
                types = "1";
            }
            else
            {
                video = subtopics.YouTubeVideo;
                types = "0";
            }
            
            return Json(new { status = status, msg = msg, Pdf = pdf, Video = video, type = types }, JsonRequestBehavior.AllowGet);
        }

        public object Subtopicchangelevel(string subtopicid)
        {
            string[] splitData = subtopicid.Split('~');
                long subid = long.Parse(splitData[0]);
            long userid = long.Parse(splitData[1]);
            bool status = false;
            string msg = string.Empty;
            int lockid = 1;
            _Entities.Configuration.ProxyCreationEnabled = false;  
                                                                  
            var levels = _Entities.tb_Level.Where(x => x.IsActive == true && x.SubTopicID == subid).ToList().Select(q => new Level(q)).OrderBy(c => c.OrderValue).ToList();

            List<LevelWithLockStatus> LevelList = new List<LevelWithLockStatus>();
            foreach (var lvls in levels)
            {
                LevelWithLockStatus Leveltest = new LevelWithLockStatus();
                Leveltest.LevelName = lvls.LevelName;
                Leveltest.LevelID = lvls.LevelID;
                if (lockid == 1)
                {
                    Leveltest.firstlockid = lockid;
                    Leveltest.LockStatus = 0;
                }
                else
                {
                    Leveltest.firstlockid = 0;
                    Leveltest.LockStatus = _Entities.tb_Level.SelectMany(z => z.tb_UnlockedLevel).Any(z => z.LevelId == lvls.LevelID && z.IsActive && z.UserId == userid) ? 0 : 1; //return 1 if it is unlocked level

                }


                LevelList.Add(Leveltest);
                lockid++;
            }
            var json = new JavaScriptSerializer().Serialize(LevelList);

            return Json(new { status = status, msg = msg, subtopic = json }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ViewPdf(string Pdfdata)
        {
          

            
            var model = new Medacademy.Models.ViewPdf();
            model.pdfdata = Pdfdata;
          
            return PartialView("~/Views/Curriculum/pv_ViewPdf.cshtml", model);
        }

        public PartialViewResult ViewVideo(string Videodata)
        {
            string[] splitData = Videodata.Split('~');
            string Videofile =splitData[1].ToString();
            string Videotype =splitData[0].ToString();


            var model = new Medacademy.Models.ViewPdf();
            model.videodata = Videofile;
            model.videotype = Videotype;

            return PartialView("~/Views/Curriculum/pv_ViewVideo.cshtml", model);
        }

        #endregion Worksheet

        #region WorksheetPractice 


        public ActionResult PracticeWorksheet(string id)
        {
            string[] splitData = id.Split('~');
          

            var user = (Medacademy.Models.UserModel)Session["UserLoginFirstTime"];
            if(user!= null)
            {
                PracticeWorksheetModel model = new PracticeWorksheetModel();
                model.userId = user.UserId;
                model.levelid = Convert.ToInt32(splitData[0]);
                model.packageid = Convert.ToInt32(splitData[1]);
                model.startTime = CurrentTime;
                ViewBag.Navigate = 0.ToString();
                ViewBag.SearchType = 1;
                return View(model);
            }
            else
            {
                PracticeWorksheetModel model = new PracticeWorksheetModel();
                model.userId = 61;
                model.levelid = Convert.ToInt32(splitData[0]);
                model.packageid = Convert.ToInt32(splitData[1]);
                model.startTime = CurrentTime;
                ViewBag.Navigate = 0.ToString();
                ViewBag.SearchType = 1;
                return View(model);
            }                 
        }


        [HttpPost]
        public object SubmitPractice(PracticeWorksheetModel model)
        {
           
            long pointEarned = 0;
            long subtopicid = 0;
            int? orderValue = 0;
            bool status = false;
            string practiceTestName = "";
            long Levelid = 0;
            string msg = "Test Completed";
            int nowUnlocked = 2;
            long attempId;
            var answerList = JsonConvert.DeserializeObject<List<ParsePracticeTest>>(model.AnswerSet);
            var practiceAtm = _Entities.tb_PracticeAttempt.Where(z => z.UserId == model.userId && z.Levelid == model.levelid && z.UnlockedStatus == true).ToList();
            var practAttempt = _Entities.tb_PracticeAttempt.Where(z => z.UserId == model.userId && z.Levelid == model.levelid && z.Date == model.startTime).FirstOrDefault();
            if (practAttempt != null)
            {
                attempId = practAttempt.Id;
            }
            else
            {
                // var score = Entities.tb_ScoreRange.Where(z => z.MinMark >= model.correctAnsCount && z.MaxMark <= model.correctAnsCount && z.IsActive && z.OrderValue == 3).FirstOrDefault();
                var score = model.correctAnsCount;

                var PracticeTest = _Entities.tb_Level.Where(z => z.LevelID == model.levelid).FirstOrDefault();
                var subtopic = _Entities.tb_Subtopic.Where(z => z.SubTopicID == PracticeTest.SubTopicID).FirstOrDefault();
                var attempt = _Entities.tb_PracticeAttempt.Where(z => z.UserId == model.userId && z.Levelid == model.levelid).Count();
                var obj = new tb_PracticeAttempt();
                obj.Attempt = attempt + 1;
                obj.SubtopicId = PracticeTest.SubTopicID;
                subtopicid = PracticeTest.SubTopicID;
                orderValue = PracticeTest.orderValue;
                obj.Date = model.startTime;
                obj.Levelid = model.levelid;
                obj.UserId = model.userId;
                obj.TopicId = subtopic.TopicID;

                obj.UnlockedStatus = false;
                // obj.LockedStatus = score == null ? false : true;
                practiceTestName = PracticeTest.LevelName;
                Levelid = PracticeTest.LevelID;
                _Entities.tb_PracticeAttempt.Add(obj);
                _Entities.SaveChanges();
                attempId = obj.Id;
            }

            var removeList = _Entities.tb_UserAttend.Where(z => z.LevelId == model.levelid && z.UserId == model.userId && z.PracticeAttemptId == attempId).ToList();
            foreach (var item in removeList)
            {
                _Entities.tb_UserAttend.Remove(item);
            }
            _Entities.SaveChanges();
            try
            {
                foreach (var item in answerList)
                {
                    var userAttend = _Entities.tb_UserAttend.Create();
                    userAttend.Mark = 1;
                    userAttend.LevelId = model.levelid;
                    userAttend.QuestionId = item.QuestionId;
                    userAttend.UserAnswerId = item.SelectedAnsId;
                    userAttend.AttendStatus = item.Attended;
                    userAttend.UserId = model.userId;
                    userAttend.RightAnswerId = 2;
                    userAttend.IsActive = true;
                    userAttend.TimeStamp = CurrentTime;
                    userAttend.PracticeAttemptId = attempId;
                    _Entities.tb_UserAttend.Add(userAttend);
                }

                status = _Entities.SaveChanges() > 0;
            }
            catch (Exception ex) { }


            if (model.correctAnsCount >= 8)
            {
                var isPractExist = _Entities.tb_UnlockedLevel.Where(z => z.LevelId == Levelid && z.UserId == model.userId && z.IsActive && z.CompletedStatus == true).FirstOrDefault();
                var point = _Entities.tb_PracticePoint.Where(z => z.UserId == model.userId).FirstOrDefault();
                var pract = _Entities.tb_PracticeAttempt.Where(z => z.UserId == model.userId && z.Id == attempId).FirstOrDefault();
                var nextChapter = _Entities.tb_Level.Where(z => z.SubTopicID == subtopicid  && z.IsActive && z.orderValue > orderValue).OrderBy(z => z.orderValue).FirstOrDefault();
                            //      Entities.tb_PracticeTest.Where(z => z.ChapterId == chapterId && z.PublishStatus == true && z.IsActive && z.OrderValue > orderValue && z.OrderValue != 6).OrderBy(z => z.OrderValue).FirstOrDefault();
                if (isPractExist == null)
                {

                    var unlockAdd = _Entities.tb_UnlockedLevel.Create();
                    unlockAdd.UserId = model.userId;
                    unlockAdd.LevelId = Levelid;
                    unlockAdd.SubtopicId = subtopicid;
                   // unlockAdd.UnlockedLevelGuid = Guid.NewGuid();
                    unlockAdd.IsActive = true;
                    unlockAdd.TimeStamp = CurrentTime;
                    unlockAdd.CompletedStatus = true;
                    _Entities.tb_UnlockedLevel.Add(unlockAdd);
                    status = _Entities.SaveChanges() > 0 ? true : false;
                    nowUnlocked = 1;

                }

                if (nextChapter != null)
                {
                    var unlockAdd = _Entities.tb_UnlockedLevel.Create();
                    unlockAdd.UserId = model.userId;
                    unlockAdd.LevelId = nextChapter.LevelID;
                    unlockAdd.SubtopicId = subtopicid;
                    //unlockAdd.UnlockedLevelGuid = Guid.NewGuid();
                    unlockAdd.IsActive = true;
                    unlockAdd.TimeStamp = CurrentTime;
                    unlockAdd.CompletedStatus = false;
                    _Entities.tb_UnlockedLevel.Add(unlockAdd);
                    _Entities.SaveChanges();
                }

                if (practiceAtm.Count == 0)
                {
                    if (pract.Attempt < 4)
                    {
                        var points = _Entities.tb_PracticePoint.Where(z => z.UserId == model.userId).FirstOrDefault();
                        if (points != null)
                        {
                            if (pract.UnlockedStatus == false && pract.Attempt == 3)
                                points.Points = points.Points + 1;
                            else if (pract.UnlockedStatus == false && pract.Attempt == 2)
                                points.Points = points.Points + 2;
                            else if (pract.UnlockedStatus == false && pract.Attempt == 1)
                                points.Points = points.Points + 3;


                            pract.UnlockedStatus = true;
                            _Entities.SaveChanges();
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

                            _Entities.tb_PracticePoint.Add(obj);
                            pract.UnlockedStatus = true;
                            _Entities.SaveChanges();
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
                        _Entities.tb_PracticePointDetails.Add(obj2);
                        _Entities.SaveChanges();
                    }
                }
            }

            //var scoreRange = _Entities.tb_ScoreRange.Where(z => z.MinMark <= model.correctAnsCount && z.MaxMark >= model.correctAnsCount && z.LevelName == practiceTestName && z.IsActive).FirstOrDefault();
            //var grade = scoreRange.Grade;



            //string id = model.PracticeTestGuid + "~" + attempId + "~" + model.practiceTest + "~" + model.subject + "~" + model.chapter + "~" + model.className + "~" + model.userId + "~" + grade + "~" + pointEarned;
            //if (status)
            //{
            //    Thread processThread = new Thread(() => new UserAttendController().PracticeMail(id));
            //    processThread.Start();
            //}
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);

        }


        #endregion WorksheetPractice

        #region Exam 

        public ActionResult Exam()
        {
           
            var datas = (UserModel)Session["UserLoginFirstTime"];
           
            if (datas == null)
            {
                 var model = new Medacademy.Models.UserModel();
                return View("~/Views/Accounts/SignUp.cshtml", model);
            }
            else
            {
                if (datas.PackageID == 0)
                {
                    return RedirectToAction("Packages", "Accounts");
                }
                else
                {
                    return View(datas);
                }

            }

        }


        public ActionResult ExamStart(string id)
        {
                string[] splitData = id.Split('~');
                SubmitExamModel model = new SubmitExamModel();
                model.userId = Convert.ToInt32( splitData[0]);
                model.ExamId = Convert.ToInt32(splitData[1]);
                //model.ExamstartTime = CurrentTime.Second();
                return View(model);
            
        }

        public object SubmitExam(SubmitExamModel model)
        {
            bool status = false;
            string msg = string.Empty;

            float totalmark = 0;

            var answerList = JsonConvert.DeserializeObject<List<ParseScholarshipTest>>(model.AnswerSet);

            var Exam = _Entities.tb_Exam.Where(z => z.ExamId == model.ExamId).FirstOrDefault();

            var removeList = _Entities.tb_UserExamAttend.Where(z => z.ExamId == model.ExamId && z.UserId == model.userId).ToList();
            foreach (var item in removeList)
            {
                _Entities.tb_UserExamAttend.Remove(item);
            }
            _Entities.SaveChanges();

            foreach (var item in answerList)
            {
                var userExamAttend = _Entities.tb_UserExamAttend.Create();
                userExamAttend.RightAnswerStatus = item.RightAnsStatus;// == 1 ? true : false;
                userExamAttend.Mark = item.RightAnsStatus == 1 ? item.Mark : item.NegativeMark;
              //  userExamAttend.ParentGuid = model.ExamGuid;
                userExamAttend.QuestionId = item.QuestionId;
                userExamAttend.UserAnswerId = item.SelectedAnsId;
                userExamAttend.UserId = model.userId;
                userExamAttend.IsActive = true;
                userExamAttend.ExamId = model.ExamId;
                //userExamAttend.AttemptDate = CurrentTime;
                userExamAttend.TimeStamp = CurrentTime;
                userExamAttend.Examtime = model.examfinishtime;
                _Entities.tb_UserExamAttend.Add(userExamAttend);
                float mark1 = Convert.ToSingle(userExamAttend.Mark);
                totalmark = totalmark + mark1;
            }
            
            //Used to store student marks
            var userMark = _Entities.UserExamResults.Create();
            userMark.Mark = Math.Round(totalmark, 2, MidpointRounding.ToEven);
            userMark.ExamID = model.ExamId;
            userMark.UserId = model.userId;
            _Entities.UserExamResults.Add(userMark);
            //used to store the students mark

            status = _Entities.SaveChanges() > 0;
            msg = status ? "Success" : "Failed";

            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public object CheckexambyUser(string id)
        {
            string[] splitData = id.Split('~');
            long userid = long.Parse(splitData[0]);
            long examid = long.Parse(splitData[1]);
            bool status = true;

            var result = _Entities.tb_UserExamAttend.Where(x => x.UserId == userid && x.ExamId == examid).Count();
           
                return Json(new { status = status,list = result }, JsonRequestBehavior.AllowGet);
            
            
        }

        #endregion Exam


        #region Preparation
        public ActionResult PreparationUser()
        {

            var datas = (UserModel)Session["UserLoginFirstTime"];

            if (datas == null)
            {
                var model = new Medacademy.Models.UserModel();
                return View("~/Views/Accounts/SignUp.cshtml", model);
            }
            else
            {
                if (datas.PackageID == 0)
                {
                    return RedirectToAction("Packages", "Accounts");
                }
                else
                {
                    return View(datas);
                }
                    
            }

        }

        public ActionResult PreparationStart(string id)
        {
            string[] splitData = id.Split('~');
            SubmitPreparationModel model = new SubmitPreparationModel();
            model.userId = Convert.ToInt32(splitData[0]);
            model.ExamId = Convert.ToInt32(splitData[1]);
            model.preparationid = Convert.ToInt32(splitData[2]);
            //model.ExamstartTime = CurrentTime.Second();
            return View(model);

        }

        public object SubmitPreparation(SubmitPreparationModel model)
        {
            bool status = false;
            string msg = string.Empty;

            float totalmark = 0;

            var answerList = JsonConvert.DeserializeObject<List<ParseScholarshipTest>>(model.AnswerSet);

          //  var Exam = _Entities.tb_Exam.Where(z => z.ExamId == model.ExamId).FirstOrDefault();

            var removeList = _Entities.tb_UserPreparationAttend.Where(z => z.PreparationId == model.preparationid && z.UserId == model.userId).ToList();
            foreach (var item in removeList)
            {
                _Entities.tb_UserPreparationAttend.Remove(item);
            }
            _Entities.SaveChanges();

            foreach (var item in answerList)
            {
                var tb_UserPreparationAttend = _Entities.tb_UserPreparationAttend.Create();
                tb_UserPreparationAttend.RightAnswerStatus = item.RightAnsStatus;// == 1 ? true : false;
                tb_UserPreparationAttend.Mark = item.RightAnsStatus == 1 ? item.Mark : item.NegativeMark;
                //  userExamAttend.ParentGuid = model.ExamGuid;
                tb_UserPreparationAttend.QuestionId = item.QuestionId;
                tb_UserPreparationAttend.UserAnswerId = item.SelectedAnsId;
                tb_UserPreparationAttend.UserId = model.userId;
                tb_UserPreparationAttend.IsActive = true;
                tb_UserPreparationAttend.PreparationId = model.preparationid;
                //userExamAttend.AttemptDate = CurrentTime;
                tb_UserPreparationAttend.TimeStamp = CurrentTime;
                tb_UserPreparationAttend.Preparationtime = model.examfinishtime;            
                _Entities.tb_UserPreparationAttend.Add(tb_UserPreparationAttend);
                float mark1 = Convert.ToSingle(tb_UserPreparationAttend.Mark);
                totalmark = totalmark + mark1;


            }

            var rmList = _Entities.tb_UserPreparationResult.Where(z => z.PreparationId == model.preparationid && z.UserId == model.userId).ToList();
            foreach (var item in rmList)
            {
                _Entities.tb_UserPreparationResult.Remove(item);
            }
            _Entities.SaveChanges();

            //Used to store student marks
            var userMark = _Entities.tb_UserPreparationResult.Create();
            userMark.Mark = Math.Round(totalmark, 2, MidpointRounding.ToEven);
            userMark.PreparationId = model.preparationid;
            userMark.UserId = model.userId;
            userMark.Time = model.examfinishtime;
            _Entities.tb_UserPreparationResult.Add(userMark);
            //used to store the students mark

            status = _Entities.SaveChanges() > 0;
            msg = status ? "Success" : "Failed";

            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        #endregion 
    }
}