using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Medacademy.Models;
using Ma.ClassLibrary;
using Ma.EntityLibrary;
using System.IO;
using Newtonsoft.Json;
using Ma.ClassLibrary.Utility;

namespace Medacademy.Controllers
{
    public class QuestionController : Controller
    {
        public DateTime CurrentTime = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        public MAEntities _Entities = new MAEntities();
        // GET: Question
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Question()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult GetQuestionView(long testId)
        {
            var model = new Medacademy.Models.QuestionAddModel();
            model.LevelID = testId;
           // TempData["LEVELID"]= testId;
            return PartialView("~/Views/Question/_pv_QuestionAdd.cshtml", model);
        }

        [HttpGet]
        public PartialViewResult GetAddQuestionView(long testId)
        {
            var model = new Models.QuestionAddModel();
            model.LevelID = testId;
            return PartialView("~/Views/Question/_pv_QuestionAdd_Level.cshtml", model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public object AddQuestion(QuestionAddModel model)
        {
            bool status = false;
            string msg = string.Empty;
            var level = _Entities.tb_Level.Where(z => z.LevelID == model.LevelID).FirstOrDefault();
            string dbQuestion = model.question.Replace("\n", "").Replace("\r", "");
            var question = _Entities.tb_Question.Create();
            var repQuestion = _Entities.tb_Question.Where(z => z.Question == dbQuestion).OrderByDescending(z => z.QuestionId).FirstOrDefault();
            if (repQuestion == null)

            {
                question.LevelID = level.LevelID;
                question.IsActive = true;
                question.Mark = 1;
                question.Question = model.question.Replace("\n", "").Replace("\r", "");
                question.QuestionTypeId = model.questionTypeId;
                question.TimeStamp = CurrentTime;
                question.Explanation = model.answerExplanation == null ? "" : model.answerExplanation.Replace("\n", "").Replace("\r", "");

                if (model.questionTypeId == 2 && model.questionImage != null && model.questionImage != string.Empty)
                {
                    string folderPath = Server.MapPath("~/Files/QuestionImage/");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);
                    var imageString = model.questionImage.Substring(model.questionImage.IndexOf(',') + 1);
                    byte[] imageByte = Convert.FromBase64String(imageString);
                    string imageName = Guid.NewGuid().ToString() + ".jpeg";
                    var filePath = Server.MapPath("~/Files/QuestionImage/" + imageName);
                    var fileSave = "/Files/QuestionImage/" + imageName;

                    using (var imageFile = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.Write(imageByte, 0, imageByte.Length);
                        imageFile.Flush();
                        question.QuestionImage = fileSave;
                    }
                }
                if (model.ansExpImage != null && model.ansExpImage != string.Empty)
                {
                    string folderPath = Server.MapPath("~/Files/QuestionImage/");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);
                    var imageString = model.ansExpImage.Substring(model.ansExpImage.IndexOf(',') + 1);
                    byte[] imageByte = Convert.FromBase64String(imageString);
                    string imageName = Guid.NewGuid().ToString() + ".jpeg";
                    var filePath = Server.MapPath("~/Files/QuestionImage/" + imageName);
                    var fileSave = "/Files/QuestionImage/" + imageName;

                    using (var imageFile = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.Write(imageByte, 0, imageByte.Length);
                        imageFile.Flush();
                        question.AnswerExpImage = fileSave;
                    }
                }
                _Entities.tb_Question.Add(question);
                status = _Entities.SaveChanges() > 0;
            }

            if (status && model.answerData != null && model.answerData != string.Empty)
            {
                var answerList = JsonConvert.DeserializeObject<List<ParseAnswer>>(model.answerData);
                if (model.answerTypeId == 1)
                {
                    foreach (var item in answerList)
                    {
                        var answer = _Entities.tb_Answer.Create();
                        answer.Answer = item.AnswerText;
                        answer.AnswerTypeId = model.answerTypeId;
                        answer.QuestionId = question.QuestionId;
                        answer.IsActive = true;
                        answer.TimeStamp = CurrentTime;
                        answer.RightStatus = item.AnswerStatus == true ? 1 : 0;
                        _Entities.tb_Answer.Add(answer);
                        _Entities.SaveChanges();
                    }
                }

                if (model.answerTypeId == 2)
                {
                    foreach (var item in answerList)
                    {
                        var answer = _Entities.tb_Answer.Create();
                        answer.Answer = string.Empty;
                        answer.AnswerTypeId = model.answerTypeId;
                        answer.QuestionId = question.QuestionId;
                        answer.RightStatus = item.AnswerStatus == true ? 1 : 0;
                        answer.IsActive = true;
                        answer.TimeStamp = CurrentTime;


                        string folderPath = Server.MapPath("~/Files/AnswerImage/");
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);
                        var imageString = item.AnswerText.Substring(item.AnswerText.IndexOf(',') + 1);
                        byte[] imageByte = Convert.FromBase64String(imageString);
                        string imageName = Guid.NewGuid().ToString() + ".jpeg";
                        var filePath = Server.MapPath("~/Files/AnswerImage/" + imageName);
                        var fileSave = "/Files/AnswerImage/" + imageName;

                        using (var imageFile = new FileStream(filePath, FileMode.Create))
                        {
                            imageFile.Write(imageByte, 0, imageByte.Length);
                            imageFile.Flush();

                            answer.AnswerImage = fileSave;


                        }
                        _Entities.tb_Answer.Add(answer);
                        status = _Entities.SaveChanges() > 0;

                    }
                }
                msg = status ? " Success!" : "Failed!";
            }



            return Json(new { status = status, msg = msg, id = model.LevelID }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public PartialViewResult RefreshAddQuestionView(long testId)
        {
            var model = new Models.QuestionAddModel();
            model.LevelID = testId;
            return PartialView("~/Views/Question/_pv_QuestionAdd_Level_Adding.cshtml", model);
        }

        [HttpGet]
        public PartialViewResult GetEditQuestionView(long questionId, long levelid)
        {
            var model = new Models.QuestionAddModel();
            var question = new Ma.EntityLibrary.Data.Question(questionId);
            var answer = question.GetAnswer().FirstOrDefault();
            model.LevelID = levelid;
            model.questionId = questionId;
            model.answerTypeId = answer.AnswerTypeId;
            model.questionTypeId = question.questionTypeId;
            return PartialView("~/Views/Question/_pv_Question_Edit.cshtml", model);
        }
        public object DeleteQuestion(long questionId)
        {
            bool status = false;
            string msg = string.Empty;
            var question = _Entities.tb_Question.Where(z => z.QuestionId == questionId && z.IsActive).FirstOrDefault();
            if (question != null)
            {
                question.IsActive = false;
                status = _Entities.SaveChanges() > 0;
                msg = status ? " Deleted!" : "Failed to delete!";

            }
            else
            {
                status = false;
                msg = "Failed to delete!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult RefreshQuestionsGrid(long testId)
        {
            var model = new Models.QuestionAddModel();
            model.LevelID = testId;
            return PartialView("~/Views/Question/_pv_QuestionAdd.cshtml", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public object EditQuestion(QuestionAddModel model)
        {
            bool status = false;
            string msg = string.Empty;

            //remove question
            var ansList = _Entities.tb_Answer.Where(z => z.QuestionId == model.questionId).ToList();
            foreach (var item in ansList)
            {
                item.IsActive = false;
               
            }
            _Entities.SaveChanges();

            var level = _Entities.tb_Level.Where(z => z.LevelID == model.LevelID).FirstOrDefault();
            var question = _Entities.tb_Question.Where(z => z.QuestionId == model.questionId).FirstOrDefault();
            //question.AnswerId = 1;
            question.IsActive = true;
            question.Mark = 1;
            question.Question = model.question.Replace("\n", "").Replace("\r", "");
            question.QuestionTypeId = model.questionTypeId;
            question.TimeStamp = CurrentTime;
            question.Explanation = model.answerExplanation == null ? "" : model.answerExplanation.Replace("\n", "").Replace("\r", "");
           

            if (model.questionTypeId == 2 && model.questionImage != null && model.questionImage != string.Empty)
            {
                string folderPath = Server.MapPath("~/Files/QuestionImage/");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                var imageString = model.questionImage.Substring(model.questionImage.IndexOf(',') + 1);
                byte[] imageByte = Convert.FromBase64String(imageString);
                string imageName = Guid.NewGuid().ToString() + ".jpeg";
                var filePath = Server.MapPath("~/Files/QuestionImage/" + imageName);
                var fileSave = "/Files/QuestionImage/" + imageName;

                using (var imageFile = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.Write(imageByte, 0, imageByte.Length);
                    imageFile.Flush();
                    question.QuestionImage = fileSave;
                }
            }
            if (model.ansExpImage != null && model.ansExpImage != string.Empty)
            {
                string folderPath = Server.MapPath("~/Files/QuestionImage/");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                var imageString = model.ansExpImage.Substring(model.ansExpImage.IndexOf(',') + 1);
                byte[] imageByte = Convert.FromBase64String(imageString);
                string imageName = Guid.NewGuid().ToString() + ".jpeg";
                var filePath = Server.MapPath("~/Files/QuestionImage/" + imageName);
                var fileSave = "/Files/QuestionImage/" + imageName;

                using (var imageFile = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.Write(imageByte, 0, imageByte.Length);
                    imageFile.Flush();
                    question.AnswerExpImage = fileSave;
                }
            }
            status = _Entities.SaveChanges() > 0;
            msg = status ? " Success!" : "Failed!";


            if (status && model.answerData != null && model.answerData != string.Empty)
            {
                var answerList = JsonConvert.DeserializeObject<List<ParseAnswer>>(model.answerData);
                if (model.answerTypeId == 1)
                {
                    foreach (var item in answerList)
                    {
                        var answer = _Entities.tb_Answer.Create();
                        answer.Answer = item.AnswerText;                        
                        answer.AnswerTypeId = model.answerTypeId;
                        answer.QuestionId = question.QuestionId;
                        answer.RightStatus = item.AnswerStatus == true ? 1 : 0;
                        answer.IsActive = true;
                        answer.TimeStamp = CurrentTime;
                        _Entities.tb_Answer.Add(answer);
                        _Entities.SaveChanges();
                    }
                }

                if (model.answerTypeId == 2)
                {
                    foreach (var item in answerList)
                    {
                        var answer = _Entities.tb_Answer.Create();
                        answer.Answer = string.Empty;
                        answer.AnswerTypeId = model.answerTypeId;
                        answer.QuestionId = question.QuestionId;
                        answer.RightStatus = item.AnswerStatus == true ? 1 : 0;
                        answer.IsActive = true;
                        answer.TimeStamp = CurrentTime;

                        string folderPath = Server.MapPath("~/Files/AnswerImage/");
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);
                        var imageString = item.AnswerText.Substring(item.AnswerText.IndexOf(',') + 1);
                        byte[] imageByte = Convert.FromBase64String(imageString);
                        string imageName = Guid.NewGuid().ToString() + ".jpeg";
                        var filePath = Server.MapPath("~/Files/AnswerImage/" + imageName);
                        var fileSave = "/Files/AnswerImage/" + imageName;

                        using (var imageFile = new FileStream(filePath, FileMode.Create))
                        {
                            imageFile.Write(imageByte, 0, imageByte.Length);
                            imageFile.Flush();

                            answer.AnswerImage = fileSave;


                        }
                        _Entities.tb_Answer.Add(answer);
                        status = _Entities.SaveChanges() > 0;
                    }
                }



                msg = status ? " Success!" : "Failed!";
            }



            return Json(new { status = status, msg = msg, id = model.LevelID }, JsonRequestBehavior.AllowGet);
        }



    }
}