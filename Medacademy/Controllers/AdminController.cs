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
using LinqToExcel;
using System.Web.Security;

namespace Medacademy.Controllers
{
    public class AdminController : Controller
    {
        public DateTime CurrentTime = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        public MAEntities _Entities = new MAEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("../Accounts/Index");
        }

        #region Course
        public ActionResult Course()
        {
            return View();
        }
        public PartialViewResult AddCourse()
        {
            return PartialView("~/Views/Admin/_pv_Add_Course.cshtml", new Medacademy.Models.CourseModel());
        }
        [HttpPost]
        public object AddCourse(CourseModel model)
        {
            bool status = false;
            string msg = string.Empty;
            //if (ModelState.IsValid)
            //{
            var Courses = _Entities.tb_Course.Where(z => z.CourseName.Trim() == model.CourseName.Trim() && z.IsActive).ToList();
            if (Courses.Count == 0)
            {
                var cls = new tb_Course();
                cls.CourseName = model.CourseName;
                cls.CourseSubjectName = model.CourseSubjectName;
                cls.IsActive = true;
                cls.TimeStamp = CurrentTime;
                cls.Price = model.Price;
                cls.Duration = model.Duration;
                cls.Details = model.Details;

                //Course Pic
                if (model.Courseimage != null)
                {
                    string folderPath = Server.MapPath("~/Files/CourseImage/");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);
                    var imageString = model.Courseimage.Substring(model.Courseimage.IndexOf(',') + 1);
                    byte[] imageByte = Convert.FromBase64String(imageString);
                    string imageName = Guid.NewGuid().ToString() + ".jpeg";
                    var imgFilePath = Server.MapPath("~/Files/CourseImage/" + imageName);
                    var fileSave = "/Files/CourseImage/" + imageName;

                    using (var imageFile = new FileStream(imgFilePath, FileMode.Create))
                    {
                        imageFile.Write(imageByte, 0, imageByte.Length);
                        imageFile.Flush();
                        cls.Files = fileSave;
                    }
                }


                //Course Pic
                _Entities.tb_Course.Add(cls);
                status = _Entities.SaveChanges() > 0;
                msg = status ? "Course added successfully!" : "Failed to add Course!";



            }
            else
            {
                status = false;
                msg = "Course already exists!";
            }
            //}
            //else
            //{
            //    status = false;
            //    msg = "Failed to add Course!";
            //}
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult RefreshCourse()
        {
            return PartialView("~/Views/Admin/_pv_Course_Grid.cshtml", new Medacademy.Models.CourseModel());
        }
        public PartialViewResult EditCourseView(long courseid)
        {
            var classes = _Entities.tb_Course.Where(z => z.CourseId == courseid && z.IsActive).FirstOrDefault();
            var model = new Medacademy.Models.CourseModel();
            model.CourseId = classes.CourseId;
            model.CourseName = classes.CourseName;
            model.CourseSubjectName = classes.CourseSubjectName;
            model.Price = classes.Price;
            model.Details = classes.Details;
            model.Duration = classes.Duration;
            model.Courseimage = classes.Files;

            return PartialView("~/Views/Admin/_pv_Course_EditCourse.cshtml", model);
        }
        [HttpPost]
        public object EditCourse(CourseModel model)
        {
            bool status = false;
            string msg = string.Empty;
            var cls = _Entities.tb_Course.FirstOrDefault(z => z.CourseId == model.CourseId && z.IsActive);
            if (cls != null)
            {

                cls.CourseName = model.CourseName;
                cls.CourseSubjectName = model.CourseSubjectName;
                // cls.IsActive = true;
                // cls.TimeStamp = CurrentTime;
                cls.Price = model.Price;
                cls.Duration = model.Duration;
                cls.Details = model.Details;

                //Course Pic
                if (model.Courseeditimage != null)
                {
                    string folderPath = Server.MapPath("~/Files/CourseImage/");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);
                    var imageString = model.Courseeditimage.Substring(model.Courseeditimage.IndexOf(',') + 1);
                    byte[] imageByte = Convert.FromBase64String(imageString);
                    string imageName = Guid.NewGuid().ToString() + ".jpeg";
                    var imgFilePath = Server.MapPath("~/Files/CourseImage/" + imageName);
                    var fileSave = "/Files/CourseImage/" + imageName;

                    using (var imageFile = new FileStream(imgFilePath, FileMode.Create))
                    {
                        imageFile.Write(imageByte, 0, imageByte.Length);
                        imageFile.Flush();
                        cls.Files = fileSave;
                    }
                }
                //Course Pic

                status = _Entities.SaveChanges() > 0 ? true : false;
                msg = status ? "Course Updated successfully!" : "Failed to Updated Course!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }
        public object CourseDelete(long courseid)
        {
            bool status = false;
            string msg = string.Empty;
            var courses = _Entities.tb_Course.Where(z => z.CourseId == courseid && z.IsActive).FirstOrDefault();
            if (courses != null)
            {
                courses.IsActive = false;
                status = _Entities.SaveChanges() > 0;

                //delete all records from all the table

                if (status)
                {
                    var package = _Entities.tb_Package.Where(z => z.CourseID == courseid && z.Isactive).ToList();
                    if (package != null)
                    {
                        foreach (var item in package)
                        {


                            item.Isactive = false;

                            _Entities.SaveChanges();
                        }
                    }
                    var subject = _Entities.tb_Subjects.Where(z => z.CourseID == courseid && z.Isactive).ToList();
                    if (subject != null)
                    {
                        foreach (var item in subject)
                        {


                            item.Isactive = false;

                            _Entities.SaveChanges();
                        }
                    }
                    var topic = _Entities.tb_Topic.Where(z => z.CourseID == courseid && z.IsActive).ToList();
                    if (topic != null)
                    {
                        foreach (var item in topic)
                        {


                            item.IsActive = false;

                            _Entities.SaveChanges();
                        }
                    }
                    var subtopic = _Entities.tb_Subtopic.Where(z => z.CourseID == courseid && z.Isactive).ToList();
                    if (subtopic != null)
                    {
                        foreach (var item in subtopic)
                        {


                            item.Isactive = false;

                            _Entities.SaveChanges();
                        }
                    }
                    var level = _Entities.tb_Level.Where(z => z.CourseID == courseid && z.IsActive).ToList();
                    if (level != null)
                    {
                        foreach (var item in level)
                        {


                            item.IsActive = false;

                            _Entities.SaveChanges();
                        }
                    }
                }
                msg = status ? " Course Deleted!" : "Failed to delete!";
            }
            else
            {
                status = false;
                msg = "Failed to delete!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult CourseView(long courseid)
        {
            var classes = _Entities.tb_Course.Where(z => z.CourseId == courseid && z.IsActive).FirstOrDefault();
            var model = new Medacademy.Models.CourseModel();
            model.CourseId = classes.CourseId;
            model.CourseName = classes.CourseName;
            model.CourseSubjectName = classes.CourseSubjectName;
            //model.Price = classes.Price;
            model.Details = classes.Details;
            model.Duration = classes.Duration;
            model.Courseimage = classes.Files;
            model.Isactive = classes.IsActive;

            return PartialView("~/Views/Admin/_pv_Course_View.cshtml", model);
        }

        #endregion

        #region Package
        public ActionResult Package()
        {
            return View();
        }
        public ActionResult AddPackage()
        {
            return View();
        }
        [HttpPost]
        public object AddPackage(PackageModel model)
        {
            bool status = false;
            string msg = string.Empty;
            //if (ModelState.IsValid)
            //{
            var Packages = _Entities.tb_Package.Where(z => z.Name.Trim() == model.PackageName.Trim() && z.Isactive).ToList();
            if (Packages.Count == 0)
            {
                if (model.CourseID != 0)
                {
                    var cls = new tb_Package();
                    cls.CourseID = model.CourseID;
                    cls.Name = model.PackageName;
                    cls.Type = model.Type;
                    cls.Amount = model.Amount;
                    cls.DiscountAmount = model.DiscountAmount;
                    cls.ExpiryDays = model.ExpiryDays;
                    cls.Description = model.Description;
                    cls.Isactive = true;
                    cls.TimeStamp = CurrentTime;

                    TimeSpan tp = TimeSpan.FromDays(model.ExpiryDays);
                    DateTime expirydate = DateTime.Now.Add(tp);
                    cls.ExpiryDate = expirydate;

                    //Course Pic
                    if (model.Packageimage != null)
                    {
                        string folderPath = Server.MapPath("~/Files/PackageImage/");
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);
                        var imageString = model.Packageimage.Substring(model.Packageimage.IndexOf(',') + 1);
                        byte[] imageByte = Convert.FromBase64String(imageString);
                        string imageName = Guid.NewGuid().ToString() + ".jpeg";
                        var imgFilePath = Server.MapPath("~/Files/PackageImage/" + imageName);
                        var fileSave = "/Files/PackageImage/" + imageName;

                        using (var imageFile = new FileStream(imgFilePath, FileMode.Create))
                        {
                            imageFile.Write(imageByte, 0, imageByte.Length);
                            imageFile.Flush();
                            cls.Files = fileSave;
                        }
                    }


                    //Course Pic
                    _Entities.tb_Package.Add(cls);
                    status = _Entities.SaveChanges() > 0;
                    msg = status ? "Package added successfully!" : "Failed to add Package!";

                }
                else
                {
                    msg = status ? "Level added successfully!" : "Please Choose Course";
                }


            }
            else
            {
                status = false;
                msg = "Package already exists!";

            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }
        //else
        //{
        //    status = false;
        //    msg = "Failed to add Package!";
        //}

        public PartialViewResult RefreshPackage()
        {
            return PartialView("~/Views/Admin/_pv_Package_Grid.cshtml", new Medacademy.Models.PackageModel());
        }
        public object PackageDelete(long packageid)
        {
            bool status = false;
            string msg = string.Empty;
            var packages = _Entities.tb_Package.Where(z => z.PackageID == packageid && z.Isactive).FirstOrDefault();
            if (packages != null)
            {
                packages.Isactive = false;
                status = _Entities.SaveChanges() > 0;
                if (status)
                {

                    var subject = _Entities.tb_Subjects.Where(z => z.PackageID == packageid && z.Isactive).ToList();
                    if (subject != null)
                    {
                        foreach (var item in subject)
                        {


                            item.Isactive = false;

                            _Entities.SaveChanges();
                        }
                    }
                    var topic = _Entities.tb_Topic.Where(z => z.PackageID == packageid && z.IsActive).ToList();
                    if (topic != null)
                    {
                        foreach (var item in topic)
                        {


                            item.IsActive = false;

                            _Entities.SaveChanges();
                        }
                    }
                    var subtopic = _Entities.tb_Subtopic.Where(z => z.PackageID == packageid && z.Isactive).ToList();
                    if (subtopic != null)
                    {
                        foreach (var item in subtopic)
                        {


                            item.Isactive = false;

                            _Entities.SaveChanges();
                        }
                    }
                    var level = _Entities.tb_Level.Where(z => z.PackageID == packageid && z.IsActive).ToList();
                    if (level != null)
                    {
                        foreach (var item in level)
                        {


                            item.IsActive = false;

                            _Entities.SaveChanges();
                        }
                    }
                }
                msg = status ? " Package Deleted!" : "Failed to delete!";
            }
            else
            {
                status = false;
                msg = "Failed to delete!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult EditPackageView(long packageid)
        {
            var packages = _Entities.tb_Package.Where(z => z.PackageID == packageid && z.Isactive).FirstOrDefault();
            var model = new Medacademy.Models.PackageModel();
            model.PackageID = packages.PackageID;
            model.CourseID = packages.CourseID;
            model.PackageName = packages.Name;
            model.Type = packages.Type;
            model.Amount = packages.Amount;
            model.DiscountAmount = Convert.ToDecimal(packages.DiscountAmount);
            model.ExpiryDays = packages.ExpiryDays;
            model.Description = packages.Description;
            model.Packageimage = packages.Files;

            return PartialView("~/Views/Admin/_pv_Package_EditPackage.cshtml", model);
        }

        [HttpPost]
        public object EditPackage(PackageModel model)
        {
            bool status = false;
            string msg = string.Empty;
            var cls = _Entities.tb_Package.FirstOrDefault(z => z.PackageID == model.PackageID && z.Isactive);
            if (cls != null)
            {
                if (model.CourseID != 0)
                {

                    cls.CourseID = model.CourseID;
                    cls.Name = model.PackageName;
                    cls.Type = model.Type;
                    cls.Amount = model.Amount;
                    cls.DiscountAmount = model.DiscountAmount;

                    cls.Description = model.Description;

                    //Expiry date calculation
                    DateTime firstDate = DateTime.Now;
                    DateTime secondDate = cls.ExpiryDate;

                    System.TimeSpan diff = secondDate.Subtract(firstDate);

                    int daysremaining = Convert.ToInt32(diff.TotalDays);

                    int totaldays = model.ExpiryDays + daysremaining;

                    TimeSpan tp = TimeSpan.FromDays(totaldays);
                    DateTime expirydate = DateTime.Now.Add(tp);

                    cls.ExpiryDate = expirydate;
                    cls.ExpiryDays = totaldays;
                    //Expiry date calculation


                    //Course Pic
                    if (model.Packageeditimage != null)
                    {
                        string folderPath = Server.MapPath("~/Files/PackageImage/");
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);
                        var imageString = model.Packageeditimage.Substring(model.Packageeditimage.IndexOf(',') + 1);
                        byte[] imageByte = Convert.FromBase64String(imageString);
                        string imageName = Guid.NewGuid().ToString() + ".jpeg";
                        var imgFilePath = Server.MapPath("~/Files/PackageImage/" + imageName);
                        var fileSave = "/Files/PackageImage/" + imageName;

                        using (var imageFile = new FileStream(imgFilePath, FileMode.Create))
                        {
                            imageFile.Write(imageByte, 0, imageByte.Length);
                            imageFile.Flush();
                            cls.Files = fileSave;
                        }
                    }

                    status = _Entities.SaveChanges() > 0 ? true : false;
                    msg = status ? "Package Updated successfully!" : "Failed to Updated Package!";

                    if (status == true)
                    {
                        var payment = _Entities.tb_Payment.Where(z => z.PackageID == model.PackageID && z.IsActive).ToList();
                        if (payment != null)
                        {
                            foreach (var item in payment)
                            {
                                //  var paymentupdate = _Entities.tb_Payment.Create();


                                //Expiry date calculation
                                DateTime firstDate1 = DateTime.Now;
                                DateTime secondDate1 = item.Expirydate;

                                System.TimeSpan diff1 = secondDate.Subtract(firstDate);

                                int daysremaining1 = Convert.ToInt32(diff1.TotalDays);

                                int totaldays1 = model.ExpiryDays + daysremaining1;

                                TimeSpan tp1 = TimeSpan.FromDays(totaldays1);
                                DateTime expirydate1 = DateTime.Now.Add(tp1);

                                item.Expirydate = expirydate1;

                                // _Entities.tb_Payment.Add(paymentupdate);
                                _Entities.SaveChanges();
                            }
                        }
                    }
                }
                else
                {
                    msg = status ? "Level added successfully!" : "Please Choose Course";
                }
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult ViewPackage(long packageid)
        {
            var packages = _Entities.tb_Package.Where(z => z.PackageID == packageid && z.Isactive).FirstOrDefault();
            var model = new Medacademy.Models.PackageModel();
            model.PackageID = packages.PackageID;
            model.CourseID = packages.CourseID;
            model.PackageName = packages.Name;
            model.Type = packages.Type;
            model.Amount = packages.Amount;
            model.DiscountAmount = Convert.ToDecimal(packages.DiscountAmount);
            model.ExpiryDays = packages.ExpiryDays;
            model.Description = packages.Description;
            model.Packageimage = packages.Files;
            model.Isactive = packages.Isactive;

            return PartialView("~/Views/Admin/_pv_Package_View.cshtml", model);
        }

        #endregion

        #region Group
        public ActionResult Group()
        {
            return View();
        }

        public ActionResult AddGroup()
        {
            return View();
        }

        public object AddGroups(GroupModel model)
        {
            bool status = false;
            string msg = string.Empty;
            //if (ModelState.IsValid)
            //{
            var Groups = _Entities.tb_Group.Where(z => z.Groupname.Trim() == model.GroupName.Trim() && z.Isactive).ToList();
            if (Groups.Count == 0)
            {
                var cls = new tb_Group();
                cls.Course_ID = model.CourseID;
                cls.Groupname = model.GroupName;
                cls.PackageID = model.PackageID;
                cls.Isactive = true;
                cls.Timestamp = CurrentTime;
                _Entities.tb_Group.Add(cls);
                status = _Entities.SaveChanges() > 0;
                msg = status ? "Group added successfully!" : "Failed to add Group!";
            }
            else
            {
                status = false;
                msg = "Group already exists!";
            }
            //}
            //else
            //{
            //    status = false;
            //    msg = "Failed to add Group!";
            //}
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult RefreshGroup()
        {
            return PartialView("~/Views/Admin/_pv_Group_Grid.cshtml", new Medacademy.Models.GroupModel());
        }


        public object EditGroup(GroupModel model)
        {
            bool status = false;
            string msg = string.Empty;
            var cls = _Entities.tb_Group.FirstOrDefault(z => z.Group_ID == model.GroupID && z.Isactive);
            if (cls != null)
            {

                cls.Course_ID = model.CourseID;
                cls.Groupname = model.GroupName;
                cls.PackageID = model.PackageID;

                status = _Entities.SaveChanges() > 0 ? true : false;
                msg = status ? "Group Updated successfully!" : "Failed to Updated Group!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public object GroupDelete(long groupid)
        {
            bool status = false;
            string msg = string.Empty;
            var groups = _Entities.tb_Group.Where(z => z.Group_ID == groupid && z.Isactive).FirstOrDefault();
            if (groups != null)
            {
                groups.Isactive = false;
                status = _Entities.SaveChanges() > 0;

                if (status)
                {
                    var groupold = _Entities.tb_GroupStudent.Where(z => z.GroupID == groupid && z.IsActive == true).ToList();
                    foreach (var items in groupold)
                    {
                        _Entities.tb_GroupStudent.Remove(items);

                        //items.IsActive = false;
                        _Entities.SaveChanges();
                    }
                }



                msg = status ? " Group Deleted!" : "Failed to delete!";
            }
            else
            {
                status = false;
                msg = "Failed to delete!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public object Getpackages(long courseid)
        {

            bool status = false;
            string message = "Failed";
            var List = Ma.EntityLibrary.Data.Dropdowndata.GetPackageDrop(courseid);
            return Json(new { status = status, msg = message, List = List }, JsonRequestBehavior.AllowGet);

        }

        public PartialViewResult EditGroupView(long groupid)
        {
            var groups = _Entities.tb_Group.Where(z => z.Group_ID == groupid && z.Isactive).FirstOrDefault();
            var model = new Medacademy.Models.GroupModel();
            //model.PackageID = (long?)groups.PackageID;
            //model.CourseID = groups.Course_ID;
            model.GroupName = groups.Groupname;
            model.GroupID = groups.Group_ID;
            return PartialView("~/Views/Admin/_pv_Group_EditGroup.cshtml", model);
        }

        #endregion

        #region Subject
        public ActionResult Subject()
        {
            return View();
        }
        public ActionResult AddSubject()
        {
            return View();
        }

        public object GetPackage(long courseid)
        {
            var result = Ma.EntityLibrary.Data.Dropdowndata.GetPackageDrop(courseid);
            return Json(new { status = result.Count > 0, list = result }, JsonRequestBehavior.AllowGet);
        }



        public PartialViewResult RefreshSubject()
        {
            return PartialView("~/Views/Admin/_pv_Subject_Grid.cshtml", new Medacademy.Models.SubjectModel());
        }

        public object AddSubjects(SubjectModel model)
        {
            bool status = false;
            string msg = string.Empty;
            //if (ModelState.IsValid)
            //{
            if (model.PackageID != 0)
            {
                if (model.CourseID != 0)
                {
                    var cls = new tb_Subjects();
                    cls.GroupID = model.GroupID;
                    cls.SubjectName = model.SubjectName;
                    cls.PackageID = model.PackageID;
                    cls.Isactive = true;
                    cls.Timestamp = CurrentTime;
                    cls.CourseID = model.CourseID;

                    _Entities.tb_Subjects.Add(cls);
                    status = _Entities.SaveChanges() > 0;
                    msg = status ? "Subject added successfully!" : "Failed to add Subject!";
                }
                else
                {
                    msg = status ? "Level added successfully!" : "Please Choose Course";
                }

            }
            else
            {
                msg = status ? "Level added successfully!" : "Please Choose Package";
            }



            //}
            //else
            //{
            //    status = false;
            //    msg = "Failed to add Subject!";
            //}
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult EditSubjectView(long subjectid)
        {
            var groups = _Entities.tb_Subjects.Where(z => z.SubjectID == subjectid && z.Isactive).FirstOrDefault();
            var model = new Medacademy.Models.SubjectModel();
            model.SubjectID = groups.SubjectID;
            model.PackageID = groups.PackageID;
            model.CourseID = groups.CourseID;
            model.SubjectName = groups.SubjectName;
            return PartialView("~/Views/Admin/_pv_Subject_EditSubject.cshtml", model);
        }

        public object EditSubject(SubjectModel model)
        {
            bool status = false;
            string msg = string.Empty;
            var cls = _Entities.tb_Subjects.FirstOrDefault(z => z.SubjectID == model.SubjectID && z.Isactive);
            if (cls != null)
            {
                if (model.PackageID != 0)
                {
                    if (model.CourseID != 0)
                    {

                        // cls.GroupID = model.GroupID;
                        cls.SubjectName = model.SubjectName;
                        cls.PackageID = model.PackageID;
                        cls.CourseID = model.CourseID;

                        status = _Entities.SaveChanges() > 0 ? true : false;
                        msg = status ? "Subject Updated successfully!" : "Failed to Updated Subject!";
                    }
                    else
                    {
                        msg = status ? "Level added successfully!" : "Please Choose Course";
                    }

                }
                else
                {
                    msg = status ? "Level added successfully!" : "Please Choose Package";
                }
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public object SubjectDelete(long subjectid)
        {
            bool status = false;
            string msg = string.Empty;
            var subjects = _Entities.tb_Subjects.Where(z => z.SubjectID == subjectid && z.Isactive).FirstOrDefault();
            if (subjects != null)
            {
                subjects.Isactive = false;
                status = _Entities.SaveChanges() > 0;
                if (status)
                {
                    var topic = _Entities.tb_Topic.Where(z => z.SubjectID == subjectid && z.IsActive).ToList();
                    if (topic != null)
                    {
                        foreach (var item in topic)
                        {


                            item.IsActive = false;

                            _Entities.SaveChanges();
                        }
                    }
                    var subtopic = _Entities.tb_Subtopic.Where(z => z.SubjectID == subjectid && z.Isactive).ToList();
                    if (subtopic != null)
                    {
                        foreach (var item in subtopic)
                        {


                            item.Isactive = false;

                            _Entities.SaveChanges();
                        }
                    }
                    var level = _Entities.tb_Level.Where(z => z.SubjectID == subjectid && z.IsActive).ToList();
                    if (level != null)
                    {
                        foreach (var item in level)
                        {


                            item.IsActive = false;

                            _Entities.SaveChanges();
                        }
                    }
                }
                msg = status ? " Subject Deleted!" : "Failed to delete!";
            }
            else
            {
                status = false;
                msg = "Failed to delete!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Topic
        public ActionResult Topic()
        {
            return View();
        }

        public ActionResult AddTopic()
        {
            return View();
        }

        public object AddTopics(TopicModel model)
        {
            bool status = false;
            string msg = string.Empty;

            if (model.SubjectID != 0)
            {
                if (model.PackageID != 0)
                {
                    if (model.CourseID != 0)
                    {
                        var cls = new tb_Topic();
                        cls.SubjectID = model.SubjectID;
                        cls.TopicName = model.TopicName;
                        cls.IsActive = true;
                        cls.Timestamp = CurrentTime;
                        cls.CourseID = model.CourseID;
                        cls.PackageID = model.PackageID;

                        _Entities.tb_Topic.Add(cls);
                        status = _Entities.SaveChanges() > 0;
                    }
                    else
                    {
                        msg = status ? "Level added successfully!" : "Please Choose Course";
                    }

                }
                else
                {
                    msg = status ? "Level added successfully!" : "Please Choose Package";
                }
            }
            else
            {
                msg = status ? "Level added successfully!" : "Please Choose Subject";
            }



            msg = status ? "Topic added successfully!" : "Failed to add Topic!";

            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult RefreshTopic()
        {
            return PartialView("~/Views/Admin/_pv_Topic_Grid.cshtml", new Medacademy.Models.TopicModel());
        }

        public PartialViewResult EditTopicView(long topicid)
        {
            var topic = _Entities.tb_Topic.Where(z => z.TopicID == topicid && z.IsActive).FirstOrDefault();
            var model = new Medacademy.Models.TopicModel();
            model.SubjectID = topic.SubjectID;
            model.TopicID = topic.TopicID;
            //model.GroupID = groups.GroupID;
            model.CourseID = topic.CourseID;
            model.PackageID = topic.PackageID;
            model.TopicName = topic.TopicName;
            return PartialView("~/Views/Admin/_pv_Topic_EditTopic.cshtml", model);
        }

        public object EditTopic(TopicModel model)
        {
            bool status = false;
            string msg = string.Empty;
            var cls = _Entities.tb_Topic.FirstOrDefault(z => z.TopicID == model.TopicID && z.IsActive);
            if (cls != null)
            {
                if (model.SubjectID != 0)
                {
                    if (model.PackageID != 0)
                    {
                        if (model.CourseID != 0)
                        {

                            cls.SubjectID = model.SubjectID;
                            cls.TopicName = model.TopicName;
                            cls.CourseID = model.CourseID;
                            cls.PackageID = model.PackageID;

                            status = _Entities.SaveChanges() > 0 ? true : false;
                            msg = status ? "Topic Updated successfully!" : "Failed to Updated Topic!";
                        }
                        else
                        {
                            msg = status ? "Level added successfully!" : "Please Choose Course";
                        }

                    }
                    else
                    {
                        msg = status ? "Level added successfully!" : "Please Choose Package";
                    }
                }
                else
                {
                    msg = status ? "Level added successfully!" : "Please Choose Subject";
                }
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public object TopicDelete(long topicid)
        {
            bool status = false;
            string msg = string.Empty;
            var topics = _Entities.tb_Topic.Where(z => z.TopicID == topicid && z.IsActive).FirstOrDefault();
            if (topics != null)
            {
                topics.IsActive = false;
                status = _Entities.SaveChanges() > 0;

                if (status)
                {
                    var subtopic = _Entities.tb_Subtopic.Where(z => z.TopicID == topicid && z.Isactive).ToList();
                    if (subtopic != null)
                    {
                        foreach (var item in subtopic)
                        {


                            item.Isactive = false;

                            _Entities.SaveChanges();
                        }
                    }
                    var level = _Entities.tb_Level.Where(z => z.TopicID == topicid && z.IsActive).ToList();
                    if (level != null)
                    {
                        foreach (var item in level)
                        {


                            item.IsActive = false;

                            _Entities.SaveChanges();
                        }
                    }
                }


                msg = status ? " Topic Deleted!" : "Failed to delete!";
            }
            else
            {
                status = false;
                msg = "Failed to delete!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }


        public object GetSubject(long packageid)
        {

            bool status = false;
            string message = "Failed";
            var List = Ma.EntityLibrary.Data.Dropdowndata.GetSubDrop(packageid);
            return Json(new { status = status, msg = message, List = List }, JsonRequestBehavior.AllowGet);

        }


        #endregion

        #region SubTopoic

        public ActionResult SubTopic()
        {
            return View();
        }

        public ActionResult AddSubTopic()
        {
            SubTopicModel model = new SubTopicModel();
            List<SubTopicModel> videolist = new List<SubTopicModel>();

            long subtopicNum;
            var checksubisnull = _Entities.tb_Subtopic.FirstOrDefault(); //to check if subtopic table has values issue while emptying database
           if(checksubisnull != null)
            {
                var getSubtopic = _Entities.tb_Subtopic.Max(x => x.SubTopicID);
                subtopicNum = getSubtopic;
            }
            else
            {
                subtopicNum = 0;
            }
           
           
            subtopicNum = subtopicNum + 1;
            string subNum = subtopicNum.ToString();

            var a1 = _Entities.tb_VideoFiles.Where(x => x.IsActive == true && x.SubTopicID == subNum).ToList();
            if (a1.Count != 0)
            {
                foreach (var a2 in a1)
                {
                    SubTopicModel video = new SubTopicModel();
                    video.ID = a2.ID;
                    video.Name = a2.Name;
                    video.FileSize = a2.FileSize;
                    video.FilePath = a2.FilePath;
                    videolist.Add(video);
                }
                model.SubTopicModel_List = videolist;

                return View(model);
            }

            return View(model);


        }

        [HttpPost]
        public ActionResult UploadVideo(HttpPostedFileBase fileupload)
        {
            if (fileupload != null)
            {
                string dirFullPath = HttpContext.Server.MapPath("~/VideoFileUpload/");
                if (!Directory.Exists(dirFullPath))
                {
                    Directory.CreateDirectory(dirFullPath);
                }

                long subtopicNum;
                var checksubisnull = _Entities.tb_Subtopic.FirstOrDefault(); //to check if subtopic table has values issue while emptying database
                if (checksubisnull != null)
                {
                    var getSubtopic = _Entities.tb_Subtopic.Max(x => x.SubTopicID);
                    subtopicNum = getSubtopic;
                }
                else
                {
                    subtopicNum = 0;
                }
              
                subtopicNum = subtopicNum + 1;
                string subNum = subtopicNum.ToString();


                var searchFile = _Entities.tb_VideoFiles.Where(x => x.SubTopicID == subNum && x.IsActive == true).ToList();
                foreach (var a1 in searchFile)
                {
                    string rootFolder = HttpContext.Server.MapPath("~/VideoFileUpload/");
                    string authorsFile = a1.Name;
                    string fullPath = rootFolder + authorsFile;
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    var updateVideofileDB = _Entities.tb_VideoFiles.Where(x => x.ID == a1.ID && x.IsActive == true).FirstOrDefault();
                    if (updateVideofileDB != null)
                    {
                        updateVideofileDB.IsActive = false;
                        _Entities.SaveChanges();
                    }
                    rootFolder = string.Empty;
                    authorsFile = string.Empty;
                    fullPath = string.Empty;
                }

                string Extention = Path.GetExtension(fileupload.FileName);
                string fileName = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}" + Extention, DateTime.Now);


                int fileSize = fileupload.ContentLength;
                int Size = fileSize / 1000;
                fileupload.SaveAs(Server.MapPath("~/VideoFileUpload/" + fileName));

                var create = _Entities.tb_VideoFiles.Create();
                create.Name = fileName;
                create.FileSize = Size;
                create.IsActive = true;
                create.SubTopicID = subtopicNum.ToString();
                create.FilePath = "/VideoFileUpload/" + fileName;
                _Entities.tb_VideoFiles.Add(create);
                _Entities.SaveChanges();

                Session["UploadFilesName"] = "/VideoFileUpload/" + fileName;

            }
            return RedirectToAction("AddSubTopic");
        }



        public JsonResult RemoveVideoFiles(SubTopicModel model)
        {
            var dats = _Entities.tb_VideoFiles.Where(x => x.ID == model.ID).FirstOrDefault();
            dats.IsActive = false;
            _Entities.SaveChanges();

            string rootFolder = HttpContext.Server.MapPath("~/VideoFileUpload/");
            string authorsFile = dats.Name;
            string fullPath = rootFolder + authorsFile;
            try
            {

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    return Json("Your imaginary file has been deleted.", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("File not found", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult RemoveSubtopic(SubTopicModel model)
        {
            string id = model.ID.ToString();
            var dats = _Entities.tb_VideoFiles.Where(x => x.SubTopicID == id).FirstOrDefault();
            var datas1 = _Entities.tb_Subtopic.Where(x => x.SubTopicID == model.ID).FirstOrDefault();
            datas1.Isactive = false;

            //Delete levels
            var level = _Entities.tb_Level.Where(z => z.SubTopicID == model.ID && z.IsActive).ToList();
            if (level != null)
            {
                foreach (var item in level)
                {


                    item.IsActive = false;

                    _Entities.SaveChanges();
                }
            }
            //Delete level end
            if (dats != null)
            {
                dats.IsActive = false;
            }
            _Entities.SaveChanges();

            if (dats != null)
            {

                string rootFolder = HttpContext.Server.MapPath("~/VideoFileUpload/");
                string authorsFile = dats.Name;
                string fullPath = rootFolder + authorsFile;
                try
                {

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                        return Json("Your file has been deleted.", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("File not found", JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
            }

            return Json("Your file has been deleted.", JsonRequestBehavior.AllowGet);




        }

        public PartialViewResult RefreshSubTopic()
        {
            return PartialView("~/Views/Admin/_pv_SubTopic_Grid.cshtml", new Medacademy.Models.SubTopicModel());
        }

        [HttpPost]
        public ActionResult EditUploadVideo(HttpPostedFileBase fileupload)
        {
            long passPara = 0;
            passPara = Convert.ToInt64(Session["EditSubTopic"]);
            string subNum = Session["EditSubTopic"].ToString();

            if (fileupload != null)
            {


                string Extention = Path.GetExtension(fileupload.FileName);
                string fileName = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}" + Extention, DateTime.Now);


                int fileSize = fileupload.ContentLength;
                int Size = fileSize / 1000;
                fileupload.SaveAs(Server.MapPath("~/VideoFileUpload/" + fileName));

                var create = _Entities.tb_VideoFiles.Where(x => x.IsActive == true && x.SubTopicID == subNum).FirstOrDefault(); ;

                string rootFolder = HttpContext.Server.MapPath("~/VideoFileUpload/");
                string authorsFile = null;
                if (create != null)
                {
                    authorsFile = create.Name;

                    string fullPath = rootFolder + authorsFile;
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);

                    }

                    create.Name = fileName;
                    create.FileSize = Size;
                    create.FilePath = "/VideoFileUpload/" + fileName; //CHange
                    _Entities.SaveChanges();

                    Session["UploadFilesName"] = "/VideoFileUpload/" + fileName;//CHange
                }
                else
                {

                    string fullPath = rootFolder + fileName;

                    create = _Entities.tb_VideoFiles.Create();
                    create.Name = fileName;
                    create.FileSize = Size;
                    create.SubTopicID = subNum;
                    create.IsActive = true;
                    create.FilePath = "/VideoFileUpload/" + fileName;
                    _Entities.tb_VideoFiles.Add(create);
                    _Entities.SaveChanges();

                    Session["UploadFilesName"] = "/VideoFileUpload/" + fileName;//CHange
                }



            }
            return RedirectToAction("EditSubTopic", "Admin", new { topicid = passPara });

        }
        public ActionResult EditSubTopic(long topicid)
        {
            Session["EditSubTopic"] = topicid;

            SubTopicModel model = new SubTopicModel();
            List<SubTopicModel> videolist = new List<SubTopicModel>();

            string subNum = topicid.ToString();
            var var_subTop = _Entities.tb_Subtopic.Where(x => x.SubTopicID == topicid).FirstOrDefault();
            var a1 = _Entities.tb_VideoFiles.Where(x => x.IsActive == true && x.SubTopicID == subNum).ToList();
            if (a1.Count != 0)
            {
                foreach (var a2 in a1)
                {
                    SubTopicModel video = new SubTopicModel();
                    video.ID = a2.ID;
                    video.Name = a2.Name;
                    video.FileSize = a2.FileSize;
                    video.FilePath = a2.FilePath;
                    videolist.Add(video);
                }
                model.SubTopicID = topicid;
                model.SubTopicModel_List = videolist;
                model.SubTopicName = var_subTop.SubTopicName;
                model.TopicID = var_subTop.TopicID;
                model.TopicID_Edit = var_subTop.TopicID;
                model.PDFName = var_subTop.FileNameOrginalPDF;
                if (var_subTop.YouTubeVideo != null)
                {
                    model.YouTubeVideo = var_subTop.YouTubeVideo;
                }

                //basheer for parent view

                model.CourseID = var_subTop.CourseID;
                model.PackageID = var_subTop.PackageID;
                model.SubjectID = var_subTop.SubjectID;

            }
            else
            {
                model.SubTopicID = topicid;
                model.SubTopicName = var_subTop.SubTopicName;
                model.TopicID = var_subTop.TopicID;
                model.TopicID_Edit = var_subTop.TopicID;
                model.PDFName = var_subTop.FileNameOrginalPDF;
                if (var_subTop.YouTubeVideo != null)
                {
                    model.YouTubeVideo = var_subTop.YouTubeVideo;
                }
                else
                {
                    model.FilePath = var_subTop.Videopath;
                }

                //basheer for parent view

                model.CourseID = var_subTop.CourseID;
                model.PackageID = var_subTop.PackageID;
                model.SubjectID = var_subTop.SubjectID;
            }
            return View(model);
        }

        public object EditAddSubTopics(SubTopicModel model)
        {
            long subtopicId = Convert.ToInt64(Session["EditSubTopic"]);

            bool status = false;
            string msg = string.Empty;

            if (model.TopicID_Edit != 0)
            {
                if (model.SubjectID != 0)
                {
                    if (model.PackageID != 0)
                    {
                        if (model.CourseID != 0)
                        {
                            string subtopicId_str = subtopicId.ToString();


                            var get_VideoFiles = _Entities.tb_VideoFiles.Where(x => x.IsActive == true && x.SubTopicID == subtopicId_str).FirstOrDefault();
                            var cls = _Entities.tb_Subtopic.Where(x => x.Isactive == true && x.SubTopicID == subtopicId).FirstOrDefault();
                            if (model.TopicID_Edit != null)
                            {
                                cls.TopicID = model.TopicID_Edit;
                            }

                            cls.SubTopicName = model.SubTopicName;

                            cls.Timestamp = CurrentTime;
                            if (get_VideoFiles != null)
                            {

                                cls.Videopath = get_VideoFiles.FilePath;
                                cls.YouTubeVideo = null;
                            }

                            if (model.YouTubeVideo != null)
                            {
                                cls.YouTubeVideo = model.YouTubeVideo;
                                cls.Videopath = null;
                            }
                            else
                            {
                                cls.YouTubeVideo = null;
                            }

                            if (model.Pdfpath != null)
                            {
                                string folderPath = Server.MapPath("~/Files/PDF/");
                                if (!Directory.Exists(folderPath))
                                    Directory.CreateDirectory(folderPath);
                                var imageString = model.Pdfpath.Substring(model.Pdfpath.IndexOf(',') + 1);
                                byte[] imageByte = Convert.FromBase64String(imageString);
                                string imageName = Guid.NewGuid().ToString() + ".pdf";
                                var imgFilePath = Server.MapPath("~/Files/PDF/" + imageName);
                                var fileSave = "/Files/PDF/" + imageName;

                                using (var imageFile = new FileStream(imgFilePath, FileMode.Create))
                                {
                                    imageFile.Write(imageByte, 0, imageByte.Length);
                                    imageFile.Flush();
                                    cls.Pdfpath = fileSave;
                                    cls.FileNameOrginalPDF = model.PDFName;
                                }
                            }

                            status = _Entities.SaveChanges() > 0;
                            msg = status ? "Subtopic Update successfully!" : "Failed to Update Subtopic!";
                        }
                        else
                        {
                            msg = status ? "Level added successfully!" : "Please Choose Course";
                        }


                    }
                    else
                    {
                        msg = status ? "Level added successfully!" : "Please Choose Subject";
                    }
                }
                else
                {
                    msg = status ? "Level added successfully!" : "Please Choose Package";
                }

            }
            else
            {
                msg = status ? "Level added successfully!" : "Please Choose Topic";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult EditSubTopicView(long topicid)
        {
            SubTopicModel model = new SubTopicModel();
            List<SubTopicModel> videolist = new List<SubTopicModel>();

            string subNum = topicid.ToString();
            var var_subTop = _Entities.tb_Subtopic.Where(x => x.SubTopicID == topicid).FirstOrDefault();
            var var_Topic = _Entities.tb_Topic.Where(x => x.TopicID == var_subTop.TopicID).FirstOrDefault();
            var a1 = _Entities.tb_VideoFiles.Where(x => x.IsActive == true && x.SubTopicID == subNum).ToList();
            if (a1.Count != 0)
            {
                foreach (var a2 in a1)
                {
                    SubTopicModel video = new SubTopicModel();
                    video.ID = a2.ID;
                    video.Name = a2.Name;
                    video.FileSize = a2.FileSize;
                    video.FilePath = a2.FilePath;
                    videolist.Add(video);
                }
                model.SubTopicModel_List = videolist;
                model.SubTopicName = var_subTop.SubTopicName;
                model.TopicID = var_Topic.TopicID;
                model.Pdfpath = var_subTop.FileNameOrginalPDF;
                model.SelectListItems = Dropdowndata.GetTopicDrop();
                return PartialView("~/Views/Admin/_pv_SubTopic_EditTopic.cshtml", model);
            }


            return PartialView("~/Views/Admin/_pv_SubTopic_EditTopic.cshtml", model);


        }

        public object AddSubTopics_Video(SubTopicModel model)
        {
            bool status = false;
            string msg = string.Empty;
            try
            {
                if (model.TopicID != 0)
                {
                    if (model.SubjectID != 0)
                    {
                        if (model.PackageID != 0)
                        {
                            if (model.CourseID != 0)
                            {
                                string videoFiles = Session["UploadFilesName"].ToString();
                                if (videoFiles == string.Empty)
                                {
                                    videoFiles = null;
                                }

                                var cls = new tb_Subtopic();
                                cls.TopicID = model.TopicID;
                                cls.SubTopicName = model.SubTopicName;
                                cls.Isactive = true;
                                cls.Timestamp = CurrentTime;
                                cls.Videopath = videoFiles;
                                cls.CourseID = model.CourseID;
                                cls.PackageID = model.PackageID;
                                cls.SubjectID = model.SubjectID;

                                if (model.YouTubeVideo != null)
                                {
                                    cls.YouTubeVideo = model.YouTubeVideo;
                                }

                                if (model.Pdfpath != null)
                                {
                                    cls.Pdfpath = model.Pdfpath;
                                    cls.FileNameOrginalPDF = model.PDFName;
                                }

                                _Entities.tb_Subtopic.Add(cls);
                                status = _Entities.SaveChanges() > 0;
                                msg = status ? "Subtopic added successfully!" : "Failed to add Subtopic!";
                                Session.Remove("UploadFilesName");
                            }
                            else
                            {
                                msg = status ? "Level added successfully!" : "Please Choose Course";
                            }


                        }
                        else
                        {
                            msg = status ? "Level added successfully!" : "Please Choose Subject";
                        }
                    }
                    else
                    {
                        msg = status ? "Level added successfully!" : "Please Choose Package";
                    }

                }
                else
                {
                    msg = status ? "Level added successfully!" : "Please Choose Topic";
                }

                return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    Session["UploadFilesName"] = "~/VideoFileUpload/Temp.mp4";
                    return RedirectToAction("AddSubTopics_Video", model);
                }
                return Json(new { status = true, msg = "Failed to add Subtopic!" }, JsonRequestBehavior.AllowGet);
            }

        }

        public object AddSubTopics(SubTopicModel model)
        {
            try
            {
                //pdf upload

                if (model.Pdfpath != null)
                {
                    string folderPath = Server.MapPath("~/Files/PDF/");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);
                    var imageString = model.Pdfpath.Substring(model.Pdfpath.IndexOf(',') + 1);
                    byte[] imageByte = Convert.FromBase64String(imageString);
                    string imageName = Guid.NewGuid().ToString() + ".pdf";
                    var imgFilePath = Server.MapPath("~/Files/PDF/" + imageName);
                    var fileSave = "/Files/PDF/" + imageName;

                    using (var imageFile = new FileStream(imgFilePath, FileMode.Create))
                    {
                        imageFile.Write(imageByte, 0, imageByte.Length);
                        imageFile.Flush();
                        //cls.Pdfpath = fileSave;
                        model.Pdfpath = fileSave;
                    }
                }

                return RedirectToAction("AddSubTopics_Video", model);
            }
            catch (Exception ex)
            {

                return RedirectToAction("AddSubTopics_Video", model);
            }

        }
        #endregion

        #region Level
        public ActionResult Level()
        {
            return View();
        }
        public ActionResult AddLevel()
        {
            return View();
        }

        [HttpPost]
        public object AddLevels(LevelsModel model)
        {
            bool status = false;
            string msg = string.Empty;
            if (model.SubTopicID != 0)
            {
                if (model.TopicID != 0)
                {
                    if (model.SubjectID != 0)
                    {
                        if (model.PackageID != 0)
                        {
                            if (model.CourseID != 0)
                            {
                                int? odrvalue = _Entities.tb_Level.Where(x => x.SubTopicID == model.SubTopicID && x.IsActive == true).OrderByDescending(x => x.orderValue).Select(x => x.orderValue).FirstOrDefault();

                                var cls = new tb_Level();
                                cls.SubTopicID = model.SubTopicID;
                                cls.LevelName = model.LevelName;
                                cls.IsActive = true;
                                cls.Timestamp = CurrentTime;
                                cls.Duration = model.timer;
                                if (odrvalue == null)
                                {
                                    cls.orderValue = 1;
                                }
                                else
                                {
                                    cls.orderValue = odrvalue + 1;
                                }
                                cls.CourseID = model.CourseID;
                                cls.PackageID = model.PackageID;
                                cls.TopicID = model.TopicID;
                                cls.SubjectID = model.SubjectID;

                                _Entities.tb_Level.Add(cls);
                                status = _Entities.SaveChanges() > 0;
                                msg = status ? "Level added successfully!" : "Failed to add Level!";
                            }
                            else
                            {
                                msg = status ? "Level added successfully!" : "Please Choose Course";
                            }

                        }
                        else
                        {
                            msg = status ? "Level added successfully!" : "Please Choose Package";
                        }
                    }
                    else
                    {
                        msg = status ? "Level added successfully!" : "Please Choose Subject";
                    }

                }
                else
                {
                    msg = status ? "Level added successfully!" : "Please Choose Topic";
                }
            }
            else
            {
                msg = status ? "Level added successfully!" : "Please Choose Subtopic";
            }



            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult RefreshLevel()
        {
            return PartialView("~/Views/Admin/_pv_Level_Grid.cshtml", new Medacademy.Models.LevelsModel());
        }

        public PartialViewResult EditLevelView(long levelid)
        {
            var topic = _Entities.tb_Level.Where(z => z.LevelID == levelid && z.IsActive).FirstOrDefault();
            var model = new Medacademy.Models.LevelsModel();
            model.SubTopicID = topic.SubTopicID;
            model.LevelID = topic.LevelID;
            //model.GroupID = groups.GroupID;
            model.LevelName = topic.LevelName;
            model.timer = topic.Duration;
            model.CourseID = topic.CourseID;
            model.PackageID = topic.PackageID;
            model.TopicID = topic.TopicID;
            model.SubjectID = topic.SubjectID;

            return PartialView("~/Views/Admin/_pv_Level_EditTopic.cshtml", model);
        }

        public object EditLevel(LevelsModel model)
        {
            bool status = false;
            string msg = string.Empty;
            var cls = _Entities.tb_Level.FirstOrDefault(z => z.LevelID == model.LevelID && z.IsActive);
            if (cls != null)
            {
                if (model.SubTopicID != 0)
                {
                    if (model.TopicID != 0)
                    {
                        if (model.SubjectID != 0)
                        {
                            if (model.PackageID != 0)
                            {
                                if (model.CourseID != 0)
                                {

                                    cls.SubTopicID = model.SubTopicID;
                                    cls.LevelName = model.LevelName;
                                    cls.Duration = model.timer;
                                    cls.PackageID = model.PackageID;
                                    cls.CourseID = model.CourseID;
                                    cls.SubjectID = model.SubjectID;
                                    cls.TopicID = model.TopicID;

                                    status = _Entities.SaveChanges() > 0 ? true : false;
                                    msg = status ? "Level Updated successfully!" : "Failed to Updated Level!";
                                }
                                else
                                {
                                    msg = status ? "Level added successfully!" : "Please Choose Course";
                                }

                            }
                            else
                            {
                                msg = status ? "Level added successfully!" : "Please Choose Package";
                            }
                        }
                        else
                        {
                            msg = status ? "Level added successfully!" : "Please Choose Subject";
                        }

                    }
                    else
                    {
                        msg = status ? "Level added successfully!" : "Please Choose Topic";
                    }
                }
                else
                {
                    msg = status ? "Level added successfully!" : "Please Choose Subtopic";
                }
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public object LevelDelete(long levelid)
        {
            bool status = false;
            string msg = string.Empty;
            var topics = _Entities.tb_Level.Where(z => z.LevelID == levelid && z.IsActive).FirstOrDefault();
            if (topics != null)
            {
                topics.IsActive = false;
                status = _Entities.SaveChanges() > 0;
                msg = status ? " Topic Deleted!" : "Failed to delete!";
            }
            else
            {
                status = false;
                msg = "Failed to delete!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }
        public object GetTopic(long subjectid)
        {

            bool status = false;
            string message = "Failed";
            var List = Ma.EntityLibrary.Data.Dropdowndata.GetTopicDrop(subjectid);
            return Json(new { status = status, msg = message, List = List }, JsonRequestBehavior.AllowGet);

        }
        public object GetSubTopic(long topicid)
        {

            bool status = false;
            string message = "Failed";
            var List = Ma.EntityLibrary.Data.Dropdowndata.GetSubtopics(topicid);
            return Json(new { status = status, msg = message, List = List }, JsonRequestBehavior.AllowGet);

        }


        #endregion

        #region Students
        public ActionResult StudentsList()
        {
            return View();
        }

        public ActionResult StudentsGroupAdd()
        {
            return View();
        }


        [HttpPost]
        public object AddStudentstoGroup(StudentList model)
        {
            bool status = false;
            string msg = string.Empty;
            try
            {
                if (model.StudentIDData != "" && model.StudentIDData != string.Empty && model.StudentIDData != null)
                {
                    string[] splitData = model.StudentIDData.Split('~');
                    foreach (var item in splitData)
                    {
                        int studentid = Convert.ToInt32(item);
                        int groupid = Convert.ToInt32(model.GroupID);
                        var studentsgroup = _Entities.tb_GroupStudent.Where(z => z.StudentID == studentid && z.IsActive).FirstOrDefault();
                        if (studentsgroup == null)
                        {
                            //var ExamQuestion = _Entities.tb_ExamQuestion.Create();



                            var Groupstudent = new tb_GroupStudent();
                            Groupstudent.GroupID = groupid;
                            Groupstudent.StudentID = studentid;
                            Groupstudent.IsActive = true;

                            _Entities.tb_GroupStudent.Add(Groupstudent);
                            _Entities.SaveChanges();
                            msg = "Success";
                            status = true;
                        }
                        else
                        {
                            msg = "Student already exists in another Group!";
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

        public PartialViewResult ViewStudentList(string groupid)
        {
            var model = new Medacademy.Models.StudentList();
            model.GroupID = Convert.ToInt32(groupid);
            return PartialView("~/Views/Admin/_pv_Students_View.cshtml", model);
        }




        #endregion

        #region excelupload
        public ActionResult QuestionExcelUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadExcel(int levelid, QuestionExcelUpload objEmpDetail, HttpPostedFileBase FileUpload)
        {

            QuestionExcelUpload objEntity = new QuestionExcelUpload();
            string data = "";
            int rtqnstnid = 0;
            if (FileUpload != null)
            {
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;

                    if (filename.EndsWith(".xlsx") || filename.EndsWith(".xls"))
                    {
                        string targetpath = Server.MapPath("~/Files/DetailFormatInExcel/");
                        FileUpload.SaveAs(targetpath + filename);
                        string pathToExcelFile = targetpath + filename;

                        string sheetName = "Sheet1";

                        var excelFile = new ExcelQueryFactory(pathToExcelFile);
                        var questionsdetails = from a in excelFile.Worksheet<QuestionExcelUpload>(sheetName) select a;
                        foreach (var a in questionsdetails)
                        {
                            a.questionId = rtqnstnid;
                            a.LevelID = levelid;
                            if (a.Question != null || a.LevelID != 0 || a.Answer != null)
                            {

                                //DateTime? myBirthdate = null;


                                //if (a.ContactNo.Length > 12)
                                //{
                                //    data = "Phone number should be 10 to 12 disit";
                                //    ViewBag.Message = data;

                                //}

                                //myBirthdate = Convert.ToDateTime(a.DOB);


                                int resullt = PostExcelData(a.questionId, a.Question, a.LevelID, a.Explanation, a.Answer, a.Rightstatus);
                                rtqnstnid = resullt;
                                if (resullt <= 0)
                                {
                                    data = "Hello User, Found some duplicate values! Only unique employee number has inserted and duplicate values(s) are not inserted";
                                    ViewBag.Message = data;
                                    continue;

                                }
                                else
                                {
                                    data = "Successful upload records";
                                    ViewBag.Message = data;
                                }
                            }

                            else
                            {
                                data = "Some fields are null, Please check your excel sheet";
                                ViewBag.Message = data;
                                return View("QuestionExcelUpload");
                            }

                        }
                    }

                    else
                    {
                        data = "This file is not valid format";
                        ViewBag.Message = data;
                    }
                    return View("QuestionExcelUpload");
                }
                else
                {

                    data = "Only Excel file format is allowed";

                    ViewBag.Message = data;
                    return View("QuestionExcelUpload");

                }

            }
            else
            {

                if (FileUpload == null)
                {
                    data = "Please choose Excel file";
                }

                ViewBag.Message = data;
                return View("QuestionExcelUpload");
            }
        }

        public int PostExcelData(long questionId, string Question, long LevelID, string Explanation, string Answer, int rightstatus)
        {
            //int InsertExcelData = _Entities.sp_QuestionExcelUpload(questionId, Question, LevelID, Explanation, Answer,  rightstatus);
            //return  InsertExcelData;
            var data = _Entities.sp_QtnFileUpload(questionId, Question, LevelID, Explanation, Answer, rightstatus);
            int qid = Convert.ToInt32(data.FirstOrDefault().QuestionID);

            return qid;
        }

        #endregion

        #region Student Profile
        public ActionResult StudentProfile()
        {
            return View();
        }

        public PartialViewResult ViewStudentDetails(long userid)
        {
            var students = _Entities.tb_Login.Where(z => z.UserId == userid && z.IsActive == true).FirstOrDefault();
            var model = new Medacademy.Models.UserModel();
            model.UserId = userid;
            model.Address = students.Address;
            model.ContactNo = students.ContactNo;
            model.DisableStatus = students.DisableStatus;
            model.DOB = students.DOB;
            model.Email = students.Email;
            model.FirstName = students.FirstName;
            model.FilesName = students.FilesName;
            model.Gender = students.Gender;
            model.TimeStamp = Convert.ToDateTime(students.TimeStamp);

            return PartialView("~/Views/Admin/_pv_StudentProfile_View.cshtml", model);
        }

        public PartialViewResult EditStudentinfo(long userid)
        {
            var students = _Entities.tb_Login.Where(z => z.UserId == userid && z.IsActive == true).FirstOrDefault();
            var model = new Medacademy.Models.UserModel();
            model.UserId = userid;
            model.Address = students.Address;
            model.ContactNo = students.ContactNo;
            model.DisableStatus = students.DisableStatus;
            model.DOB = students.DOB;
            model.Email = students.Email;
            model.FirstName = students.FirstName;
            model.FilesName = students.FilesName;
            model.Gender = students.Gender;
            model.TimeStamp = Convert.ToDateTime(students.TimeStamp);

            //string groups = "";
            //var list = new Ma.EntityLibrary.Data.StudentsList().Getgroupname(userid).ToList();
            //if (list != null)
            //    groups = String.Join("~", from item in list select item.GroupID);
            //model.Groups = groups;

            //changed to set group selection from dropdown checklist to dropdown


            var list = new Ma.EntityLibrary.Data.StudentsList().Getgroupname(userid).FirstOrDefault();

            if (list != null)
            {
                model.GroupID = list.GroupID;
            }
            return PartialView("~/Views/Admin/_pv_studentProfile_Edit.cshtml", model);
        }

        public PartialViewResult RefreshStudentsinfo()
        {
            return PartialView("~/Views/Admin/_pv_StudentProfileGrid.cshtml", new Medacademy.Models.UserModel());
        }

        [HttpPost]
        public object EditStudent(UserModel model)
        {


            bool status = false;
            string msg = string.Empty;
            try
            {
                var studentinfo = _Entities.tb_Login.Where(z => z.UserId == model.UserId && z.IsActive == true).FirstOrDefault();
                studentinfo.FirstName = model.FirstName;
                studentinfo.LastName = model.LastName;



                studentinfo.Address = model.Address;
                studentinfo.ContactNo = model.ContactNo;
                //studentinfo.Email = model.Email;

                //Profile Pic
                if (model.Profileeditimage != null)
                {
                    string folderPath = Server.MapPath("~/Files/UserImg/");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);
                    var imageString = model.Profileeditimage.Substring(model.Profileeditimage.IndexOf(',') + 1);
                    byte[] imageByte = Convert.FromBase64String(imageString);
                    string imageName = Guid.NewGuid().ToString() + ".jpeg";
                    var imgFilePath = Server.MapPath("~/Files/UserImg/" + imageName);
                    var fileSave = "/Files/UserImg/" + imageName;

                    using (var imageFile = new FileStream(imgFilePath, FileMode.Create))
                    {
                        imageFile.Write(imageByte, 0, imageByte.Length);
                        imageFile.Flush();
                        studentinfo.FilesName = fileSave;
                    }
                }
                //Profile Pic



                status = _Entities.SaveChanges() > 0 ? true : false;
                {






                    var groupold = _Entities.tb_GroupStudent.Where(z => z.StudentID == model.UserId && z.IsActive == true).ToList();
                    foreach (var items in groupold)
                    {
                        _Entities.tb_GroupStudent.Remove(items);

                        //items.IsActive = false;
                        _Entities.SaveChanges();
                    }



                    var groupnew = _Entities.tb_GroupStudent.Create();
                    groupnew.StudentID = model.UserId;
                    groupnew.GroupID = model.GroupID;
                    groupnew.IsActive = true;

                    _Entities.tb_GroupStudent.Add(groupnew);
                    _Entities.SaveChanges();
                    msg = "Success";
                    status = true;


                    //if (model.GroupsEdit != "" && model.GroupsEdit != string.Empty && model.GroupsEdit != null)
                    //{
                    //    string[] splitData = model.GroupsEdit.Split('~');
                    //    foreach (var item in splitData)
                    //    {
                    //        long groupid = Convert.ToInt64(item);
                    //        var groupold = _Entities.tb_GroupStudent.Where(z => z.StudentID == model.UserId && z.IsActive == true).ToList();
                    //        foreach (var items in groupold)
                    //        {
                    //            items.IsActive = false;
                    //            _Entities.SaveChanges();
                    //        }


                    //    }
                    //    foreach (var item in splitData)
                    //    {
                    //        var groupnew = _Entities.tb_GroupStudent.Create();
                    //        groupnew.StudentID = model.UserId;
                    //        groupnew.GroupID = Convert.ToInt64(item);
                    //        groupnew.IsActive = true;

                    //        _Entities.tb_GroupStudent.Add(groupnew);
                    //        _Entities.SaveChanges();
                    //        msg = "Success";
                    //        status = true;
                    //    }

                    //}
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public object ActivateStudent(long userid)
        {
            bool status = false;
            string msg = string.Empty;
            var students = _Entities.tb_Login.Where(z => z.UserId == userid && z.IsActive == true).FirstOrDefault();
            if (students != null)
            {

                students.DisableStatus = true;
                status = _Entities.SaveChanges() > 0;
                msg = status ? "Activated!" : "Failed to activate!";
            }
            else
            {
                status = false;
                msg = "Failed to delete!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public object DeactivateStudent(long userid)
        {
            bool status = false;
            string msg = string.Empty;
            var students = _Entities.tb_Login.Where(z => z.UserId == userid && z.IsActive == true).FirstOrDefault();
            if (students != null)
            {
                students.DisableStatus = false;
                status = _Entities.SaveChanges() > 0;
                msg = status ? "Deactivated!" : "Failed to deactivate!";
            }
            else
            {
                status = false;
                msg = "Failed to deactivate!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public object StudentDelete(long userid)
        {
            bool status = false;
            string msg = string.Empty;
            var user = _Entities.tb_Login.Where(z => z.UserId == userid && z.IsActive == true).FirstOrDefault();
            if (user != null)
            {
                user.IsActive = false;
                status = _Entities.SaveChanges() > 0;
                msg = status ? " Student Deleted!" : "Failed to Delete!";
            }
            else
            {
                status = false;
                msg = "Failed to Deleted!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public object ConfirmPackage(string id)
        {
            string[] splitdata = id.Split('~');
            long userid = Convert.ToInt32(splitdata[0]);
            long packageid = Convert.ToInt32(splitdata[1]);
            bool status = false;
            string msg = string.Empty;
            var payment = _Entities.tb_Payment.Where(z => z.UserId == userid && z.IsActive == true && z.PackageID == packageid).FirstOrDefault();
            if (payment != null)
            {

                payment.PaidStatus = true;
                status = _Entities.SaveChanges() > 0;
                msg = status ? "Package Confirmed!" : "Package confirmation failed!";
            }
            else
            {
                status = false;
                msg = "Package confirmation failed!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region News Letter

        public ActionResult News()
        {
            return View();
        }

        public ActionResult AddNews()
        {
            return View();
        }

        [HttpPost]
        public object AddNews(NewsModel model)
        {
            bool status = false;
            string msg = string.Empty;
            //if (ModelState.IsValid)
            //{

            var cls = new tb_News();
            cls.Head = model.Head;
            cls.SubHead = model.Description;
            cls.News = model.DetailedDescription;

            try
            {
                if (model.DateofNews != string.Empty && model.DateofNews != null)
                {
                    string[] splitData = model.DateofNews.Split('-');
                    var dd = splitData[0];
                    var mm = splitData[1];
                    var yyyy = splitData[2];
                    var dob = mm + '-' + dd + '-' + yyyy;
                    cls.NewsDate = Convert.ToDateTime(dob);
                }
            }
            catch
            {

            }
            cls.IsActive = true;
            cls.TimeStamp = CurrentTime;

            if (model.Newsimage != null)
            {
                string folderPath = Server.MapPath("~/Files/NewsImage/");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                var imageString = model.Newsimage.Substring(model.Newsimage.IndexOf(',') + 1);
                byte[] imageByte = Convert.FromBase64String(imageString);
                string imageName = Guid.NewGuid().ToString() + ".jpeg";
                var imgFilePath = Server.MapPath("~/Files/NewsImage/" + imageName);
                var fileSave = "/Files/NewsImage/" + imageName;

                using (var imageFile = new FileStream(imgFilePath, FileMode.Create))
                {
                    imageFile.Write(imageByte, 0, imageByte.Length);
                    imageFile.Flush();
                    cls.Image = fileSave;
                }
            }

            _Entities.tb_News.Add(cls);
            status = _Entities.SaveChanges() > 0;
            msg = status ? "News added successfully!" : "Failed to add News!";




            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult RefreshNews()
        {
            return PartialView("~/Views/Admin/_pv_News_Grid.cshtml", new NewsModel());
        }

        public object NewsDelete(long newsid)
        {
            bool status = false;
            string msg = string.Empty;
            var packages = _Entities.tb_News.Where(z => z.NewsID == newsid && z.IsActive).FirstOrDefault();
            if (packages != null)
            {
                packages.IsActive = false;
                status = _Entities.SaveChanges() > 0;
                msg = status ? " News Removed!" : "Failed to Remove!";
            }
            else
            {
                status = false;
                msg = "Failed to Remove!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult EditNewsView(long newsid)
        {
            var packages = _Entities.tb_News.Where(z => z.NewsID == newsid && z.IsActive).FirstOrDefault();
            var model = new NewsModel();
            model.NewsId = packages.NewsID;
            model.Head = packages.Head;
            model.Description = packages.SubHead;
            model.DetailedDescription = packages.News;
            model.DateofNews = Convert.ToString(packages.NewsDate);
            model.Newsimage = packages.Image;

            return PartialView("~/Views/Admin/_pv_News_Edit.cshtml", model);
        }

        [HttpPost]
        public object EditNews(NewsModel model)
        {
            bool status = false;
            string msg = string.Empty;
            var cls = _Entities.tb_News.FirstOrDefault(z => z.NewsID == model.NewsId && z.IsActive);
            if (cls != null)
            {

                cls.Head = model.Head;
                cls.SubHead = model.Description;
                cls.News = model.DetailedDescription;
                try
                {
                    if (model.DateofNews != string.Empty && model.DateofNews != null)
                    {
                        string[] splitData = model.DateofNews.Split('-');
                        var dd = splitData[0];
                        var mm = splitData[1];
                        var yyyy = splitData[2];
                        var dob = mm + '-' + dd + '-' + yyyy;
                        cls.NewsDate = Convert.ToDateTime(dob);
                    }
                }
                catch
                {

                }

                if (model.Newseditimage != null)
                {
                    string folderPath = Server.MapPath("~/Files/NewsImage/");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);
                    var imageString = model.Newseditimage.Substring(model.Newseditimage.IndexOf(',') + 1);
                    byte[] imageByte = Convert.FromBase64String(imageString);
                    string imageName = Guid.NewGuid().ToString() + ".jpeg";
                    var imgFilePath = Server.MapPath("~/Files/NewsImage/" + imageName);
                    var fileSave = "/Files/NewsImage/" + imageName;

                    using (var imageFile = new FileStream(imgFilePath, FileMode.Create))
                    {
                        imageFile.Write(imageByte, 0, imageByte.Length);
                        imageFile.Flush();
                        cls.Image = fileSave;
                    }
                }

                status = _Entities.SaveChanges() > 0 ? true : false;
                msg = status ? "News Updated successfully!" : "Failed to Updated News!";
            }
            return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailedNews(long Newsid)
        {
            var model = new NewsModel();
            model.NewsId = Newsid;
            return View(model);
        }

        #endregion

    }
}