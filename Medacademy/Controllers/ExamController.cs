using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Medacademy.Models;
using Ma.ClassLibrary;
using Ma.EntityLibrary;
using System.IO;
using Ma.EntityLibrary.Data;
using System.Globalization;

namespace Medacademy.Controllers
{
    public class ExamController : Controller
    {
        public DateTime CurrentTime = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        public MAEntities _Entities = new MAEntities();

        // GET: Exam
        public ActionResult Exam()
        {
            return View();
        }

        [HttpPost]
        public object AddExam(ExamModel model)
        {


            bool status = false;
            string msg = string.Empty;
            try
            {
                var exam = new tb_Exam();
                exam.ExamName = model.ExamName;
                exam.Duration = model.Duration;
                exam.TimeStamp = CurrentTime;
                exam.IsActive = true;
                exam.ActiveStatus = true;


                /* Converting Date time to Utc Time to avoid server timechange in future*/

                //DateTime stdate = Convert.ToDateTime(model.StartDate);
                //DateTime endate = Convert.ToDateTime(model.EndDate);





                //DateTime utcstartDateTime = TimeZoneInfo.ConvertTimeToUtc(stdate, TimeZoneInfo.Local);
                //DateTime utcendDateTime = TimeZoneInfo.ConvertTimeToUtc(endate, TimeZoneInfo.Local);

                /* Converting Date time to Utc Time to avoid server timechange in future*/

                exam.ExamStartDate = model.StartDate;// Convert.ToDateTime(model.StartDate).ToString("dd-MM-yyyy HH:mm"); //utcstartDateTime.ToString(); 
                exam.ExamEndDate = model.EndDate; //Convert.ToDateTime(model.EndDate).ToString("dd-MM-yyyy HH:mm"); //utcendDateTime.ToString();
                exam.UpcomingStatus = true;
                exam.PublishStatus = false;
                exam.PublishResult = false;


                _Entities.tb_Exam.Add(exam);
                status = _Entities.SaveChanges() > 0;
                {
                    if (model.GroupData != "" && model.GroupData != string.Empty && model.GroupData != null)
                    {
                        string[] splitData = model.GroupData.Split('~');
                        foreach (var item in splitData)
                        {
                            var group = _Entities.tb_ExamGroup.Create();
                            group.ExamId = exam.ExamId;
                            group.GroupId = Convert.ToInt64(item);
                            group.IsActive = true;

                            _Entities.tb_ExamGroup.Add(group);
                            _Entities.SaveChanges();
                            msg = "Success";
                            status = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }


        public object ExamCancel(long examid)
        {
            bool status = false;
            string msg = string.Empty;
            var topics = _Entities.tb_Exam.Where(z => z.ExamId == examid && z.IsActive).FirstOrDefault();
            if (topics != null)
            {
                topics.ActiveStatus = false;
                status = _Entities.SaveChanges() > 0;
                msg = status ? " Exam Canceled!" : "Failed to Cancel!";
            }
            else
            {
                status = false;
                msg = "Failed to Cancel!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult RefreshExam()
        {
            return PartialView("~/Views/Exam/_pv_Exam_Grid.cshtml", new Medacademy.Models.LevelsModel());
        }

        public ActionResult AddQuestoExam(string examid)
        {
            string[] splitData = examid.Split('~');
            var model = new Medacademy.Models.AddQuestoExam();
            model.ExamId = Convert.ToInt32(splitData[0]);
            model.ExamName = splitData[1].ToString();
            return View(model);
        }

        [HttpGet]
        public PartialViewResult GetQuestionView(long testId)
        {
            var model = new Medacademy.Models.AddQuestoExam();
            model.LevelID = testId;
            return PartialView("~/Views/Exam/_pv_ExamQuestionView.cshtml", model);
        }
        [HttpPost]
        public object AddQuesttoExam(AddQuestoExam model)
        {
            bool status = false;
            string msg = string.Empty;
            try
            {
                if (model.SelectedData != "" && model.SelectedData != string.Empty && model.SelectedData != null)
                {
                    string[] splitData = model.SelectedData.Split('~');
                    foreach (var item in splitData)
                    {
                        //var ExamQuestion = _Entities.tb_ExamQuestion.Create();
                        int questionid = Convert.ToInt32(item);
                        int examid = Convert.ToInt32(model.ExamId);
                        string exname = model.ExamName;
                        var ExamQuestion = _Entities.tb_ExamQuestion.Where(z => z.ExamId == model.ExamId && z.QuestionId == questionid).FirstOrDefault();
                        if (ExamQuestion == null)
                        {
                            var ExamQuestion1 = new tb_ExamQuestion();
                            ExamQuestion1.ExamId = examid;
                            ExamQuestion1.ExamName = exname;
                            ExamQuestion1.QuestionId = Convert.ToInt64(item);

                            _Entities.tb_ExamQuestion.Add(ExamQuestion1);
                            _Entities.SaveChanges();
                            msg = "Success";
                            status = true;
                        }
                        else
                        {
                            msg = "Question already Exists";
                            status = false;
                            break;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult RefreshExamQuestionGrid(long testId)
        {
            var model = new Models.AddQuestoExam();
            model.LevelID = testId;
            return PartialView("~/Views/Exam/_pv_ExamQuestionView.cshtml", model);
        }

        public PartialViewResult PreparationView(string examid)
        {
            string[] splitData = examid.Split('~');
            var model = new Medacademy.Models.Preparation();
            model.examid = Convert.ToInt32(splitData[0]);
            model.examname = splitData[1].ToString();
            var preparation = _Entities.tb_Preparation.Where(z => z.ExamId == model.examid && z.IsActive).FirstOrDefault();
            if (preparation != null)
            {
                model.Name = preparation.PreparationName;
                model.timer = preparation.Duration;
                model.stdate = preparation.StartDate;
                model.eddate = preparation.EndDate;
            }
            return PartialView("~/Views/Exam/_pv_Add_Preparation.cshtml", model);
        }

        [HttpPost]
        public object AddPreparation(Preparation model)
        {
            bool status = false;
            string msg = string.Empty;

            var Preparation = _Entities.tb_Preparation.Where(z => z.ExamId == model.examid && z.IsActive).FirstOrDefault();
            if (Preparation == null)
            {

                var prep = new tb_Preparation();
                prep.ExamId = Convert.ToInt32(model.examid);
                prep.StartDate = model.stdate;
                prep.EndDate = model.eddate;
                prep.IsActive = true;
                prep.Timestamp = CurrentTime;
                prep.Duration = model.timer;
                prep.PreparationName = model.Name;

                _Entities.tb_Preparation.Add(prep);
                status = _Entities.SaveChanges() > 0;



                msg = status ? "Preparation added successfully!" : "Failed to add Preparation!";

            }
            else
            {
                msg = "Preparation already Exists";
                status = false;

            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ViewQuestion(string examid)
        {
            string[] splitData = examid.Split('~');
            var model = new Medacademy.Models.AddQuestoExam();
            model.ExamId = Convert.ToInt32(splitData[0]);
            model.ExamName = splitData[1].ToString();

            return PartialView("~/Views/Exam/_pv_ExamAllQuestionView.cshtml", model);
        }

        [HttpGet]
        public PartialViewResult RefreshExamQuestionCount(string examid)
        {
            string[] splitData = examid.Split('~');
            var model = new Models.AddQuestoExam();
            model.ExamId = Convert.ToInt32(splitData[0]);
            model.LevelID = Convert.ToInt32(splitData[1]);
            return PartialView("~/Views/Exam/_pv_Exam_QuestionCount.cshtml", model);
        }

        public PartialViewResult EditExamView(long examid)
        {
            var exam = _Entities.tb_Exam.Where(z => z.ExamId == examid && z.IsActive).FirstOrDefault();
            var model = new Medacademy.Models.ExamModel();
            model.ExamID = exam.ExamId;
            model.ExamName = exam.ExamName;
            model.StrtDate = exam.ExamStartDate;
            model.EnDate = exam.ExamEndDate;
            model.Duration = exam.Duration;



            string groups = "";
            var list = new Ma.EntityLibrary.Data.Exam().GetExamgroups(exam.ExamId).ToList();
            if (list != null)
                groups = String.Join("~", from item in list select item.GroupID);
            model.GroupData = groups;


            return PartialView("~/Views/Exam/_pv_ExamEdit.cshtml", model);
        }

        [HttpPost]
        public object EditExam(ExamModel model)
        {


            bool status = false;
            string msg = string.Empty;
            try
            {
                var exam = _Entities.tb_Exam.Where(z => z.ExamId == model.ExamID && z.IsActive).FirstOrDefault();
                exam.ExamName = model.ExamName;
                exam.Duration = model.Duration;



                /* Converting Date time to Utc Time to avoid server timechange in future*/

                //DateTime stdate = Convert.ToDateTime(model.StartDate);
                //DateTime endate = Convert.ToDateTime(model.EndDate);





                //DateTime utcstartDateTime = TimeZoneInfo.ConvertTimeToUtc(stdate, TimeZoneInfo.Local);
                //DateTime utcendDateTime = TimeZoneInfo.ConvertTimeToUtc(endate, TimeZoneInfo.Local);

                /* Converting Date time to Utc Time to avoid server timechange in future*/

                exam.ExamStartDate = model.StrtDate;// Convert.ToDateTime(model.StartDate).ToString("dd-MM-yyyy HH:mm"); //utcstartDateTime.ToString(); 
                exam.ExamEndDate = model.EnDate; //Convert.ToDateTime(model.EndDate).ToString("dd-MM-yyyy HH:mm"); //utcendDateTime.ToString();



                status = _Entities.SaveChanges() > 0 ? true : false;
                {
                    if (model.GroupDataEdit != "" && model.GroupDataEdit != string.Empty && model.GroupDataEdit != null)
                    {
                        string[] splitData = model.GroupDataEdit.Split('~');
                        foreach (var item in splitData)
                        {
                            long groupid = Convert.ToInt64(item);
                            var groupold = _Entities.tb_ExamGroup.Where(z => z.ExamId == model.ExamID && z.IsActive == true).ToList();
                            foreach (var items in groupold)
                            {
                                items.IsActive = false;
                                _Entities.SaveChanges();
                            }


                        }
                        foreach (var item in splitData)
                        {
                            var groupnew = _Entities.tb_ExamGroup.Create();
                            groupnew.ExamId = exam.ExamId;
                            groupnew.GroupId = Convert.ToInt64(item);
                            groupnew.IsActive = true;

                            _Entities.tb_ExamGroup.Add(groupnew);
                            _Entities.SaveChanges();
                            msg = "Success";
                            status = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }


        public object PublishExam(long examid)
        {
            bool status = false;
            string msg = string.Empty;
            var publish = _Entities.tb_Exam.Where(z => z.ExamId == examid && z.IsActive).FirstOrDefault();
            if (publish != null)
            {
                publish.PublishStatus = true;
                status = _Entities.SaveChanges() > 0;
                msg = status ? " Exam Published" : "Failed to publishexam!";
            }
            else
            {
                status = false;
                msg = "Failed to publish!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }


        public object ExamDelete(long examid)
        {
            bool status = false;
            string msg = string.Empty;
            var topics = _Entities.tb_Exam.Where(z => z.ExamId == examid).FirstOrDefault();
            if (topics != null)
            {
                topics.IsActive = false;
                status = _Entities.SaveChanges() > 0;
                var preparation = _Entities.tb_Preparation.Where(z => z.ExamId == examid).FirstOrDefault();
                if (preparation != null)
                {
                    preparation.IsActive = false;
                    status = _Entities.SaveChanges() > 0;
                }
                msg = status ? " Exam Deleted!" : "Failed to Deleted!";
            }
            else
            {
                status = false;
                msg = "Failed to Deleted!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExamResult(string id)
        {
            ExamModel model = new ExamModel();
            model.ExamID = Convert.ToInt64(id);
            return View(model);
        }

        public object PublishResult(long examId)
        {

            bool status = false;
            string msg = string.Empty;
            int index = 1;
            var exam = _Entities.tb_Exam.Where(z => z.ExamId == examId && z.IsActive).FirstOrDefault();
            if (exam != null)
            {
                exam.PublishResult = true;
                status = _Entities.SaveChanges() > 0;


                //Adding the rank list

                var examResult = new Ma.EntityLibrary.Data.Exam().GetUserResult(examId);
                var removeList = _Entities.tb_ExamRank.Where(z => z.ExamId == examId).ToList();
                foreach (var item in removeList)
                {
                    _Entities.tb_ExamRank.Remove(item);
                }
                _Entities.SaveChanges();

                foreach (var item in examResult)
                {
                    var tb_ExamRanks = _Entities.tb_ExamRank.Create();
                    tb_ExamRanks.ExamId = item.ExamId;
                    tb_ExamRanks.UserId = item.UserId;
                    tb_ExamRanks.UserName = item.Name;
                    tb_ExamRanks.Examtime = item.Examtime.HasValue ? item.Examtime.Value : 0;
                    tb_ExamRanks.Total = item.Total.HasValue ? item.Total.Value : 0;
                    tb_ExamRanks.Rank = index;
                    _Entities.tb_ExamRank.Add(tb_ExamRanks);
                    index++;
                }
                status = _Entities.SaveChanges() > 0;
                msg = status ? "Published!" : "Failed to publish!";

            }
            else
            {
                status = false;
                msg = "Failed to publish!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }




    }
}