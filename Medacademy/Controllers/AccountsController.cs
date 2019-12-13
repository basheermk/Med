using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Medacademy.Models;
using Ma.ClassLibrary;
using Ma.EntityLibrary;
using Medacademy.Repository;
using Medacademy.Repository.Paytm;
using System.Web.Security;

namespace Medacademy.Controllers
{
    public class AccountsController : Controller
    {
        public DateTime CurrentTime = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        public MAEntities _Entities = new MAEntities();
        UserRepository UserRepository = new UserRepository();
        // GET: Accounts
        public ActionResult Index()
        {
            PaginationRepository PaginationRepository = new PaginationRepository();
            PaginationModel PaginationModel = new PaginationModel();
            var datas1 = (UserModel)Session["UserLoginFirstTime"];
            var datas = datas1;
            try
            {
                if (datas != null)
                {
                    PaginationModel.CourseID = (long)datas.CourseId;
                    datas.PaginationModel = PaginationModel;
                    datas.Home_Pacages_List = PaginationRepository.Home_PakagesLists(PaginationModel);
                    //Session["UserLoginFirstTime"] = datas;
                    return View(datas);
                }

                List<PackageModel> LI_Pacages = new List<PackageModel>();
                UserModel models = new UserModel();

                LI_Pacages = PaginationRepository.Home_PakagesLists(PaginationModel);
                models.Home_Pacages_List = LI_Pacages;
                Session["Localsession"] = models;

                return View(models);

            }
            catch (Exception ex)
            {
                return RedirectToAction("SessionExpired", "User");
            }


        }
        public ActionResult Courses()
        {
            var datas = (UserModel)Session["UserLoginFirstTime"];
            try
            {
                if (datas != null)
                {
                    PaginationRepository PaginationRepository = new PaginationRepository();
                    var var_CourseModel = _Entities.tb_Course.Where(x => x.CourseId == datas.CourseId && x.IsActive == true).ToList();

                    PaginationModel PaginationModel = new PaginationModel();
                    var datas3 = _Entities.tb_Course.Where(x => x.CourseId == datas.CourseId && x.IsActive == true).Count();
                    PaginationModel.TotalItems = datas3;
                    CourseModel Co = new CourseModel();
                    Co.PaginationModel = PaginationModel;
                    PaginationModel.CourseID = datas.CourseId;
                    datas.CourseModel = Co;
                    datas.CourseModel_Lists = PaginationRepository.CoursesLists(PaginationModel);
                    Session["UserLoginFirstTime"] = datas;

                    return View(datas);
                }

                var models = (UserModel)Session["Localsession"];
                if (models == null || models.LocalModel.PaginationModel == null)
                {//Login
                    return RedirectToAction("SessionExpired", "User");
                }
                return View(models);

            }
            catch (Exception ex)
            {
                return RedirectToAction("SessionExpired", "User");
            }


        }
        public ActionResult Packages()
        {
            var datas = (UserModel)Session["UserLoginFirstTime"];
            try
            {
                if (datas != null)
                {
                    return View(datas);
                }

                var models = (UserModel)Session["Localsession"];
                if (models == null || models.LocalModel.PaginationModel == null)
                {//Login
                    return RedirectToAction("SessionExpired", "User");
                }
                if (models.LocalModel.LI_Pacages == null)
                {
                    return RedirectToAction("Local_Courses");
                }
                return View(models);

            }
            catch (Exception ex)
            {
                return RedirectToAction("SessionExpired", "User");
            }


        }
        public ActionResult Local_Courses(long? CourseId)
        {
            PaginationRepository PaginationRepository = new PaginationRepository();
            UserModel model = new UserModel();
            if (CourseId == null)
            {
                PaginationModel PaginationModel = new PaginationModel();
                LocalModel LocalModel = new LocalModel();
                var datas3 = _Entities.tb_Course.Where(x => x.IsActive == true).Count();

                PaginationModel.TotalItems = datas3;


                LocalModel.PaginationModel = PaginationModel;
                LocalModel.CourseModel_Lists = PaginationRepository.CoursesLists(PaginationModel);
                model.LocalModel = LocalModel;
            }
            else
            {
                PaginationModel PaginationModel = new PaginationModel();
                LocalModel LocalModel = new LocalModel();
                var datas3 = _Entities.tb_Course.Where(x => x.CourseId == CourseId && x.IsActive == true).Count();

                PaginationModel.TotalItems = datas3;
                PaginationModel.CourseID = (long)CourseId;

                LocalModel.PaginationModel = PaginationModel;

                LocalModel.CourseModel_Lists = PaginationRepository.CoursesLists(PaginationModel);
                model.LocalModel = LocalModel;

            }
            Session["Localsession"] = model;
            return RedirectToAction("Courses");
        }

        public ActionResult Local_Packages(long? CourseId)
        {
            PaginationRepository PaginationRepository = new PaginationRepository();
            UserModel model = new UserModel();
            if (CourseId == null)
            {
                PaginationModel PaginationModel = new PaginationModel();
                LocalModel LocalModel = new LocalModel();
                var datas3 = _Entities.tb_Package.Where(x => x.Isactive == true).Count();

                PaginationModel.TotalItems = datas3;


                LocalModel.PaginationModel = PaginationModel;
                LocalModel.LI_Pacages = PaginationRepository.PakagesLists(PaginationModel);
                model.LocalModel = LocalModel;
            }
            else
            {
                PaginationModel PaginationModel = new PaginationModel();
                LocalModel LocalModel = new LocalModel();
                var datas3 = _Entities.tb_Package.Where(x => x.CourseID == CourseId && x.Isactive == true).Count();

                PaginationModel.TotalItems = datas3;
                PaginationModel.CourseID = (long)CourseId;

                LocalModel.PaginationModel = PaginationModel;

                LocalModel.LI_Pacages = PaginationRepository.PakagesLists(PaginationModel);
                model.LocalModel = LocalModel;

            }
            Session["Localsession"] = model;
            return RedirectToAction("Packages");
        }

        public ActionResult Preparation()
        {
            return View();
        }
        public ActionResult Reports()
        {
            return View();
        }
        public ActionResult Groups()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult CreateAccount_User(UserModel model)
        {
            PaginationRepository PaginationRepository = new PaginationRepository();
            bool status = false;
            string msg = string.Empty;

            List<PackageModel> LI_PackageModel = new List<PackageModel>();

            string dirFullPath = HttpContext.Server.MapPath("~/Files/UserImg");
            var FilesName = UserRepository.UploadFiles(model.file, dirFullPath);
            string sessionID = SessionClass.SessionId();
            try
            {
                var create = _Entities.tb_Login.Create();
                if (FilesName != null)
                {
                    create.FilesName = "/Files/UserImg/" + FilesName.ToString();
                    model.FilesName = FilesName.ToString();
                }

                create.FirstName = model.FirstName;
                create.Email = model.Email;
                create.Password = model.Password;
                create.CourseId = model.CourseId;
                create.Districts = model.Districts;
                create.Pin = model.Pin;
                create.State = model.State;
                create.Address = model.Address;
                create.ContactNo = model.ContactNo;
                create.SessionId = sessionID;
                create.Gender = model.Gender;

                create.IsActive = true;
                create.TimeStamp = DateTime.Now;

                create.RoleId = (int?)SessionClass.Users.User;

                _Entities.tb_Login.Add(create);
                _Entities.SaveChanges();

                status = true;
                msg = "Success";

                model.SessionId = sessionID;
                model.IsActive = true;
                model.RoleId = (int)SessionClass.Users.User;

                PaginationModel PaginationModel = new PaginationModel();
                var datas3 = _Entities.tb_Package.Where(x => x.CourseID == model.CourseId && x.Isactive == true).Count();

                PaginationModel.TotalItems = datas3;
                PaginationModel.CourseID = model.CourseId;

                model.PaginationModel = PaginationModel;

                long userId = _Entities.tb_Login.Max(x => x.UserId);

                model.PaymentModel_Lists = getPayment(userId);

                model.LI_Pacages = PaginationRepository.PakagesLists(PaginationModel);

                //var a3 = _Entities.tb_Login.Where(x => x.Email == model.Email).FirstOrDefault();

                model.UserId = userId;


            }
            catch (Exception ex)
            {
                return Json(new { status = status, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            Session["UserLoginFirstTime"] = model;
            return RedirectToAction("Packages", "Accounts");
        }

        public ActionResult LoginCheck(Login model)
        {
            try
            {
                PaginationRepository PaginationRepository = new PaginationRepository();
                var user = _Entities.tb_Login.Where(x => x.Email.ToLower() == model.Email.ToLower() && x.Password == model.Password && x.IsActive == true).FirstOrDefault();
                if (user != null)
                {
                    if (user.DisableStatus == true)
                    {

                        if (user.RoleId == 1)
                        {

                            //return Json(new { status = true, msg = "Success", userType = 1 }, JsonRequestBehavior.AllowGet);
                            FormsAuthentication.SetAuthCookie(user.UserId.ToString(), true);
                            return RedirectToAction("Dashboard", "Admin");
                        }
                        else if (user.RoleId == 2)
                        {

                            /////////////////////////
                            var pakageExpair = _Entities.PR_expires_Payment(user.UserId);
                            List<PackageModel> LI_PackageModel = new List<PackageModel>();
                            string sessionId = SessionClass.SessionId();
                            user.SessionId = sessionId;
                            _Entities.SaveChanges();

                            UserModel datas = new UserModel();

                            datas.UserId = user.UserId;
                            datas.Password = user.Password;
                            datas.FirstName = user.FirstName;
                            datas.Gender = user.Gender;
                            datas.Email = user.Email;
                            datas.ContactNo = user.ContactNo;
                            datas.TimeStamp = (DateTime)user.TimeStamp;
                            datas.State = user.State;
                            datas.Address = user.Address;
                            datas.Pin = user.Pin;
                            datas.Districts = user.Districts;
                            datas.FilesName = user.FilesName;
                            datas.SessionId = sessionId;
                            var payment = _Entities.tb_Payment.Where(x => x.PaymentType == 1 && x.UserId == user.UserId).FirstOrDefault();
                            if (payment == null)
                            {
                                datas.Ispaid = false;
                            }
                            else
                            {
                                if (DateTime.Now <= payment.Expirydate)
                                {
                                    datas.Ispaid = payment.PaidStatus;
                                    datas.PackageID = payment.PackageID;
                                }
                                else
                                {
                                    TempData["expire"] = false;
                                    return RedirectToAction("Login", "Accounts");
                                }
                            }

                            PaginationModel PaginationModel = new PaginationModel();
                            var datas3 = _Entities.tb_Package.Where(x => x.CourseID == user.CourseId && x.Isactive == true).Count();

                            PaginationModel.TotalItems = datas3;
                            PaginationModel.CourseID = (long)user.CourseId;


                            datas.PaginationModel = PaginationModel;
                            datas.LI_Pacages = PaginationRepository.PakagesLists(PaginationModel);
                            datas.CourseId = (long)user.CourseId;
                            datas.PaymentModel_Lists = getPayment(user.UserId);
                            Session["UserLoginFirstTime"] = datas;
                            //return RedirectToAction("Packages", "Accounts");
                            if (user.DisableStatus != true)
                            {
                                return RedirectToAction("Packages", "Accounts");
                            }
                            else
                            {
                                return RedirectToAction("Worksheet", "Curriculum");
                            }

                           


                          

                            //return Json(new { status = true, msg = "Success", userType = 2 }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            TempData["status"] = false;
                            return RedirectToAction("Login", "Accounts");
                            //return Json(new { status = false, msg = "Username/Password does not match" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        TempData["approval"] = false;
                        return RedirectToAction("Login", "Accounts");

                    }

                }
                else
                {
                    TempData["status"] = false;
                    return RedirectToAction("Login", "Accounts");
                    //return Json(new { status = false, msg = "Username/Password incorrect" }, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
                TempData["status"] = false;
                return RedirectToAction("Login", "Accounts");
                //return Json(new { status = false, msg = ex.InnerException.InnerException }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<PaymentPaytmModel> getPayment(long Userid)
        {

            try
            {
                //calculate ExpaidDate cheking..

                List<PaymentPaytmModel> lists = new List<PaymentPaytmModel>();
                var values = _Entities.tb_Payment.Where(x => x.UserId == Userid && x.IsActive == true && x.PaidStatus == true).ToList();
                foreach (var a1 in values)
                {
                    PaymentPaytmModel mo = new PaymentPaytmModel();
                    mo.PackageID = a1.PackageID;
                    mo.UserId = a1.UserId;
                    //mo.PaidStatus = a1.PaidStatus;
                    mo.Amount = a1.Amount;
                    mo.PaymentType = a1.PaymentType;
                    mo.TimeStamp = a1.TimeStamp;
                    //mo.ParentGuid = a1.ParentGuid;
                    mo.PaymentMode = a1.PaymentMode;
                    //mo.IsActive = a1.IsActive;
                    mo.PaymentId = a1.PaymentId;
                    mo.Expirydate = a1.Expirydate;
                    mo.REQUEST_ID = a1.REQUEST_ID;
                    mo.RESPONSE_ID = a1.RESPONSE_ID;
                    lists.Add(mo);
                }


                return lists;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        public ActionResult Contact(ContactUsModel model)
        {
            bool status = false;
            string msg = string.Empty;  
                  
                var contact = new tb_ContactUs();
                contact.Name = model.name;
                contact.Email = model.email;
                contact.ContactNo = model.contactNo;
                contact.Message = model.messgae;
                contact.ReplyStatus = false;
                contact.IsActive = true;
                contact.ContactDate = CurrentTime;
                _Entities.tb_ContactUs.Add(contact);
                status = _Entities.SaveChanges() > 0;

           

                var state = false;
                var description = "failed";


                try
                {

                    var filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/Content/email/ContactAdmin.html");
                    var emailTemplate = System.IO.File.ReadAllText(filePath);
                    var mailBody = emailTemplate.Replace("{{user}}", model.name)
                        .Replace("{{messgae}}", model.messgae)
                        .Replace("{{email}}", model.email)
                        .Replace("{{contactNo}}", model.contactNo);
                    //.Replace("{{schoolName}}", model.schoolName);


                    Mail.ContactusSend("MedAcademy - Contact", mailBody, model.name, model.email);

                    state = true;
                    description = "success";

                }
                catch (Exception exx)
                {
                  
                    state = false;
                    description = "Something went wrong";
                }

                msg = status ? "Success!" : "Failed to send!";
                return Json(new { status = status, msg = msg }, JsonRequestBehavior.AllowGet);
          
        }

        public ActionResult Indexes(string id)
        {
            if(id!=null)
            {
                TempData["News"] = id;
            }
            else
            {
                TempData["News"] = 0;
            }
            

          return  RedirectToAction("Index", "Accounts");

        }


    }
}