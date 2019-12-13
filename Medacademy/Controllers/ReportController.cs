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
using PagedList;
using PagedList.Mvc;

namespace Medacademy.Controllers
{
    public class ReportController : Controller
    {
        public DateTime CurrentTime = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        public MAEntities _Entities = new MAEntities();

        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserExamReult()
        {
            return View();
        }

        public ActionResult ExamWiseReport(string id)
        {
            ExamModel model = new ExamModel();
            model.ExamID = Convert.ToInt64(id);
            return View(model);
        }
        public ActionResult ExamReview(string id)
        {
            var userData = (Medacademy.Models.UserModel)Session["UserLoginFirstTime"];
            string[] splitData = id.Split('~');
            SubmitExamModel model = new SubmitExamModel();
            model.userId = userData.UserId;
            model.ExamId = Convert.ToInt64(splitData[0]);
            model.QuestionId = Convert.ToInt64(splitData[1]);
            ViewBag.Navigate = 1.ToString();
            //return View("StartPractice", model);
            return View(model);
        }
        public ActionResult ReportMain(int? page)
        {
            var userData = (Medacademy.Models.UserModel)Session["UserLoginFirstTime"];

            long id = Convert.ToInt64(userData.UserId);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var model = new Medacademy.Models.PracticeWorksheetModel();
            var report = new Ma.EntityLibrary.Data.StudentsList(id).GetPracticeTestReport();
            model.reportCount = report.Count;
            var onePageOfProducts = report.ToPagedList(pageNumber, pageSize);
            model.report = onePageOfProducts.ToList();
            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View(model);
        }
        public ActionResult ReportNavigate(string id)
        {
            var user = (Medacademy.Models.UserModel)Session["UserLoginFirstTime"];
            string[] splitData = id.Split('~');
            long practiceTestGuid = Convert.ToInt32(splitData[0]);
            var model = new Medacademy.Models.PracticeWorksheetModel();
            model.levelid = practiceTestGuid;
            model.attemptId = Convert.ToInt64(splitData[1]);
            return View(model);
        }
        public ActionResult NavigatePractice(string id)
        {
            var user = (Medacademy.Models.UserModel)Session["UserLoginFirstTime"];
            string[] splitData = id.Split('~');
            PracticeWorksheetModel model = new PracticeWorksheetModel();
            model.userId = user.UserId;
            model.levelid = Convert.ToInt32(splitData[0]);
            model.attemptId = Convert.ToInt64(splitData[1]);
            model.QuestionId = Convert.ToInt64(splitData[2]);
            ViewBag.Navigate = 1.ToString();
            //return View("StartPractice", model);
            return View("ReviewPractice", model);
        }
        public ActionResult AdminReportuserid(long id)
        {


            Session["id"] = id;

            return RedirectToAction("ReportMainAdmin", "Report");


        }
        public ActionResult ReportMainAdmin(int? page)
        {
            long id = Convert.ToInt64(Session["id"]);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var model = new Medacademy.Models.PracticeWorksheetModel();
            var report = new Ma.EntityLibrary.Data.StudentsList(id).GetPracticeTestReport();
            model.reportCount = report.Count;
            var onePageOfProducts = report.ToPagedList(pageNumber, pageSize);
            model.report = onePageOfProducts.ToList();
            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View(model);
        }
        public ActionResult ReportNavigateAdmin(string id)
        {
            string[] splitData = id.Split('~');
            long practiceTestGuid = Convert.ToInt32( (splitData[0]));
            var model = new Medacademy.Models.PracticeWorksheetModel();
            model.levelid = practiceTestGuid;
            model.attemptId = Convert.ToInt64(splitData[1]);
           
            return View(model);
        }
        public ActionResult NavigatePracticeAdmin(string id)
        {
            string[] splitData = id.Split('~');
            PracticeWorksheetModel model = new PracticeWorksheetModel();
            model.userId = Convert.ToInt64(Session["id"]);
            model.levelid = Convert.ToInt32((splitData[0]));
            model.attemptId = Convert.ToInt64(splitData[1]);
            model.QuestionId = Convert.ToInt64(splitData[2]);
            ViewBag.Navigate = 1.ToString();
            //return View("StartPractice", model);
            return View("ReviewPracticeAdmin", model);
        }


        #region PreparationReport

        public ActionResult UserPreparationReSult()
        {
            return View();
        }

        public ActionResult PreparationWiseReport(string id)
        {
            Preparation model = new Preparation();
            model.preparationid = Convert.ToInt64(id);
            model.examid = _Entities.tb_Preparation.Where(z => z.PreparationId == model.preparationid && z.IsActive).FirstOrDefault().ExamId;
            return View(model);
        }

        public ActionResult PreparatiomReview(string id)
        {
            var userData = (Medacademy.Models.UserModel)Session["UserLoginFirstTime"];
            string[] splitData = id.Split('~');
            SubmitExamModel model = new SubmitExamModel(); //Using the same model class for preparation 
            model.userId = userData.UserId;
            model.ExamId = Convert.ToInt64(splitData[0]);
            model.QuestionId = Convert.ToInt64(splitData[1]);
            ViewBag.Navigate = 1.ToString();
            //return View("StartPractice", model);
            TempData["PreparationId"] = splitData[2];
            return View(model);
        }

        #endregion
    }

}