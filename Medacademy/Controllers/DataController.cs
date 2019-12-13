using Ma.EntityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Medacademy.Controllers
{
    public class DataController : Controller
    {
        protected static MAEntities _Entities = new MAEntities();
        // GET: Data
        public ActionResult Index()
        {
            return View();
        }
        public object LoadPackages(long courseid)
        {
            var result = Ma.EntityLibrary.Data.Dropdowndata.GetPackageDrop(courseid);
            return Json(new { status = result.Count > 0, list = result }, JsonRequestBehavior.AllowGet);
        }

        public object LoadSubjects(long packageid)
        {
            var result = Ma.EntityLibrary.Data.Dropdowndata.GetSubDrop(packageid);
            return Json(new { status = result.Count > 0, list = result }, JsonRequestBehavior.AllowGet);
        }

        public object LoadTopic(long subjectid)
        {
            var result = Ma.EntityLibrary.Data.Dropdowndata.GetTopicDrop(subjectid);
            return Json(new { status = result.Count > 0, list = result }, JsonRequestBehavior.AllowGet);
        }

        public object LoadSubtopic(long topicid)
        {
            var result = Ma.EntityLibrary.Data.Dropdowndata.GetSubtopics(topicid);
            return Json(new { status = result.Count > 0, list = result }, JsonRequestBehavior.AllowGet);
        }
        public object LoadLevel(long subtopicid)
        {
            var result = Ma.EntityLibrary.Data.Dropdowndata.GetLevels(subtopicid);
            return Json(new { status = result.Count > 0, list = result }, JsonRequestBehavior.AllowGet);
        }
        
    }
}