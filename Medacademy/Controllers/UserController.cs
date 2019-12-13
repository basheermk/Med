using Ma.EntityLibrary;
using Medacademy.Models;
using Medacademy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;
namespace Medacademy.Controllers
{

    public class UserController : Controller
    {
        public MAEntities _Entities = new MAEntities();
        public DateTime CurrentTime = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));


        [HttpGet]
        public JsonResult IsEmailIDExist(string Email)
        {
            bool isExist = true;
            var a1 = _Entities.tb_Login.Where(x => x.Email == Email && x.IsActive == true).FirstOrDefault();
            if (a1 == null)
            {
                isExist = false;
            }


            //isExist = _Entities.tb_Login.Where(x => x.FilesName.ToLowerInvariant().Equals(FirstName.ToLower())) != null;
            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UserLogin()
        {
            var datas = (UserModel)Session["UserLoginFirstTime"];
            try
            {
                if (datas != null)
                {
                    return View(datas);
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("SessionExpired", "User");
            }


            return RedirectToAction("SessionExpired", "User");

        }
        public ActionResult SessionExpired()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Accounts");
        }

        public PartialViewResult pv_packages(PackageModel models)
        {
            var datas = (UserModel)Session["UserLoginFirstTime"];
            try
            {
                if (datas != null)
                {
                    PaginationRepository PaginationRepository = new PaginationRepository();
                    PaginationModel PaginationModel = new PaginationModel();
                    PaginationModel.CourseID = datas.PaginationModel.CourseID;
                    PaginationModel.PageNumber = models.PaginationModel.PageNumber;
                    models.PaginationModel = PaginationModel;
                    datas.LI_Pacages = PaginationRepository.PakagesLists(models.PaginationModel);
                    Session["UserLoginFirstTime"] = datas;
                    return PartialView("~/Views/User/pv_packages.cshtml", datas);

                }

            }
            catch (Exception ex)
            {
                return PartialView("~/Views/User/SessionExpired.cshtml");
            }
            return PartialView("~/Views/User/SessionExpired.cshtml");


        }

        public PartialViewResult pv_courses(PackageModel models)
        {
            var datas = (UserModel)Session["UserLoginFirstTime"];
            try
            {
                if (datas != null)
                {
                    PaginationRepository PaginationRepository = new PaginationRepository();
                    PaginationModel PaginationModel = new PaginationModel();
                    PaginationModel.CourseID = datas.PaginationModel.CourseID;
                    PaginationModel.PageNumber = models.PaginationModel.PageNumber;
                    models.PaginationModel = PaginationModel;
                    datas.CourseModel_Lists = PaginationRepository.CoursesLists(models.PaginationModel);
                    Session["UserLoginFirstTime"] = datas;
                    return PartialView("~/Views/User/pv_courses.cshtml", datas);

                }

            }
            catch (Exception ex)
            {
                return PartialView("~/Views/User/SessionExpired.cshtml");
            }
            return PartialView("~/Views/User/SessionExpired.cshtml");


        }

        //pv_Latest_courses tostart............03-07-2019
        public PartialViewResult pv_Latest_courses()
        {
            var datas = (UserModel)Session["UserLoginFirstTime"];
            try
            {
                if (datas != null)
                {


                }
                return PartialView("~/Views/User/pv_Latest_courses.cshtml");

            }
            catch (Exception ex)
            {
                return PartialView("~/Views/User/SessionExpired.cshtml");
            }



        }

        #region AlertPayment

        public ActionResult AlertPayment(string page)
        {
            var datas = (UserModel)Session["UserLoginFirstTime"];
            try
            {
                if (datas != null)
                {
                    datas.TempID1 = Convert.ToInt64(page);
                    Session["UserLoginFirstTime"] = datas;
                    return View(datas);
                }

            }
            catch (Exception ex)
            {

                return RedirectToAction("SessionExpired", "User");
            }

            return RedirectToAction("Login", "Accounts");

        }

        public ActionResult Payment()
        {
            var datas = (UserModel)Session["UserLoginFirstTime"];
            try
            {
                if (datas != null)
                {
                    //Index 
                    return RedirectToAction("Index", "Paytm");
                    
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("SessionExpired", "User");
            }


            return RedirectToAction("SessionExpired", "User");

        }

        public ActionResult PaymentPost()
        {
            var _user = (UserModel)Session["UserLoginFirstTime"];

            try
            {
                if (_user != null)
                {
                    
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("SessionExpired", "User");
            }


            return RedirectToAction("SessionExpired", "User");


        }

        public ActionResult PaymentResponse()
        {
            var _user = (UserModel)Session["UserLoginFirstTime"];

            try
            {
                if (_user != null)
                {


                    string hashData = "81bdcb70e58922dbea99d4d11310caa5";
                    string secureHash = "";
                    string paymentStatus = "";
                    SortedDictionary<string, string> confirmData = new SortedDictionary<string, string>();

                    //PaymentModel model = new PaymentModel();

                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetExpires(CurrentTime.AddSeconds(-1));
                    Response.Cache.SetAllowResponseInBrowserHistory(false);
                    Response.Cache.SetNoStore();

                    if (!string.IsNullOrEmpty(Request["secretkey"]))
                        Session["SECRET_KEY"] = Request["secretkey"];
                    else
                        Session["SECRET_KEY"] = AlgorithmSelctor.GetAppConfig("SECRET_KEY", "");

                    hashData = (Session["SECRET_KEY"] + "").ToString();

                    //NameValueCollection nameValue = (Request.QueryString.Count > 0) ? Request.QueryString : Request.Form;

                    SortedDictionary<string, string> sortedDict = NameValueCreator.SortNameValueCollection(_user);

                    foreach (KeyValuePair<string, string> p in sortedDict)
                    {
                        if (p.Value.ToString() != null && p.Value.ToString().Length > 0 && p.Key.ToString().ToLower() != "secretkey" && p.Key.ToString() != "SecureHash" && p.Key.ToString() != "submitted" && !p.Key.ToString().ToLower().StartsWith("__"))
                        {
                            hashData += "|" + p.Value.ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(hashData) && hashData.Length > 0)
                    {
                        secureHash = Crypto.GenerateHashString(hashData, AlgorithmSelctor.GetConfigAlgorithm("Algorithm"), Crypto.EncodingType.HEX).ToUpper();
                        if (secureHash == Request["SecureHash"])
                        {
                            if (!string.IsNullOrEmpty(Request["ResponseCode"]))
                            {
                                int ResponseCode = -1;
                                int.TryParse(Request["ResponseCode"], out ResponseCode);
                                if (ResponseCode == 0)
                                {
                                    // update response and the order's payment status as SUCCESS in to database

                                    //for demo purpose, its stored in session
                                    paymentStatus = "SUCCESS";
                                    Session["paymentResponse"] = Request;
                                    bool status = false;

                                    var datas = _user.LI_Pacages.Where(x => x.PackageID == _user.TempID1).FirstOrDefault();
                                    var createPayment = _Entities.tb_Payment.Create();
                                    Guid guid = Guid.NewGuid();
                                    createPayment.UserId = _user.UserId;
                                    createPayment.PaidStatus = true;
                                    createPayment.IsActive = true;
                                    createPayment.PackageID = datas.PackageID;
                                    createPayment.ParentGuid = guid;
                                    //Pacage Id
                                    createPayment.TimeStamp = CurrentTime;
                                    createPayment.Amount = Convert.ToInt64(Convert.ToInt64(datas.Amount) - Convert.ToInt64(datas.DiscountAmount));

                                    _Entities.tb_Payment.Add(createPayment);
                                    status = _Entities.SaveChanges() > 0;

                                    var dateTime = CurrentTime.ToString("dd-MMM-yyyy");
                                    var state = false;
                                    var description = "failed";
                                    try
                                    {
                                        var paidAmount = Convert.ToInt64(Convert.ToInt64(datas.Amount) - Convert.ToInt64(datas.DiscountAmount));
                                        var filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/Content/email/RegistrationPayment.html");
                                        var emailTemplate = System.IO.File.ReadAllText(filePath);
                                        var mailBody = emailTemplate.Replace("{{user}}", _user.FirstName)
                                        .Replace("{{username}}", _user.Email)
                                        .Replace("{{password}}", _user.Password)
                                        .Replace("{{amount}}", paidAmount.ToString())
                                        .Replace("{{date}}", dateTime);
                                        Mail.Send("Med Academy - Registration Payment", mailBody, _user.FirstName, new System.Collections.ArrayList { _user.Email });

                                        var filePathAdmin = System.Web.Hosting.HostingEnvironment.MapPath(@"~/Content/email/RegistrationPaymentAdmin.html");
                                        var emailTemplateAdmin = System.IO.File.ReadAllText(filePathAdmin);
                                        var mailBodyAdmin = emailTemplateAdmin.Replace("{{user}}", _user.FirstName)
                                        .Replace("{{username}}", _user.Email)
                                        .Replace("{{password}}", _user.Password)
                                        .Replace("{{amount}}", paidAmount.ToString())
                                        .Replace("{{date}}", dateTime);
                                        Mail.Send("Child Academy - Registration Payment", mailBodyAdmin, _user.FirstName, new System.Collections.ArrayList { "medacademy@srishtis.com", "medacademyTest.srishti@gmail.com" });


                                        state = true;
                                        description = "success";

                                    }
                                    catch
                                    {
                                        state = false;
                                        description = "Something went wrong";
                                    }
                                }
                                else
                                {
                                    paymentStatus = "FAILED";
                                    Session["paymentResponse"] = Request;
                                }
                                //for demo purpose, its stored in session
                                Session["paymentStatus"] = paymentStatus;
                                Session["PaymentID"] = (Request["PaymentID"] + "").ToString();

                                // Redirect to confirm page with reference.
                                confirmData.Add("PaymentID", Request["PaymentID"]);
                                confirmData.Add("Status", paymentStatus);
                                confirmData.Add("Amount", Request["Amount"]);

                                hashData = (Session["SECRET_KEY"] + "").ToString();
                                foreach (KeyValuePair<string, string> p in confirmData)
                                {
                                    if (!string.IsNullOrEmpty(p.Value) && (p.Value + "").ToString().Length > 0)
                                    {
                                        hashData += "|" + (p.Value + "").ToString();
                                    }
                                }
                                if (hashData != null && hashData.Length > 0)
                                {
                                    _user.PaymentModel.SecureHash = Crypto.GenerateHashString(hashData, AlgorithmSelctor.GetConfigAlgorithm("Algorithm"), Crypto.EncodingType.HEX).ToUpper();
                                }
                            }
                        }
                        else
                        {
                            Response.Write("<h1>Error!!</h1>");
                            Response.Write("<p>Hash validation failed</p>");
                        }
                    }
                    else
                    {
                        Response.Write("<h1>Error!</h1>");
                        Response.Write("<p>Invalid response</p>");
                    }

                    _user.PaymentModel.HashValue = hashData;
                    _user.PaymentModel.PaymentStatus = paymentStatus;
                    _user.PaymentModel.ConfirmData = confirmData;

                    Session["UserLoginFirstTime"] = _user;

                    return View(_user);
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("SessionExpired", "User");
            }

            return RedirectToAction("SessionExpired", "User");
        }

        public ActionResult PaymentConfirm(PaymentModel model)
        {


            string hashData = "81bdcb70e58922dbea99d4d11310caa5";
            string secureHash = "";

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(CurrentTime.AddSeconds(-1));
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();

            if (!string.IsNullOrEmpty(Request["secretkey"]))
                Session["SECRET_KEY"] = Request["secretkey"];
            else
                Session["SECRET_KEY"] = AlgorithmSelctor.GetAppConfig("SECRET_KEY", "");

            hashData = (Session["SECRET_KEY"] + "").ToString();

            NameValueCollection nameValue = (Request.QueryString.Count > 0) ? Request.QueryString : Request.Form;

            SortedDictionary<string, string> sortedDict = NameValueCreator.SortNameValueCollection(nameValue);
            model.sortedDict = sortedDict;

            foreach (KeyValuePair<string, string> p in sortedDict)
            {
                if (p.Value.ToString() != null && p.Value.ToString().Length > 0 && p.Key.ToString() != "secretkey" && p.Key.ToString().ToLower() != "securehash" && p.Key.ToString() != "submitted" && !p.Key.ToString().ToLower().StartsWith("__"))
                {
                    hashData += "|" + p.Value.ToString();
                }
            }
            if (!string.IsNullOrEmpty(hashData) && hashData.Length > 0)
            {
                secureHash = Crypto.GenerateHashString(hashData, AlgorithmSelctor.GetConfigAlgorithm("Algorithm"), Crypto.EncodingType.HEX).ToUpper();
                if (secureHash != Request["SecureHash"])
                {
                    Response.Write("<h1>Error!!</h1>");
                    Response.Write("<p>Hash validation failed</p>");
                    Response.Write(Request["PaymentID"]);
                }
                else
                {
                    //req = (HttpRequest)Session["paymentResponse"];
                    //$response = $_SESSION['paymentResponse'][$_REQUEST['PaymentID']];
                }
            }
            else
            {
                Response.Write("<h1>Error!</h1>");
                Response.Write("<p>Invalid response</p>");
            }
            if (Session["paymentStatus"] == "SUCCESS")
            {
                //var userTable = Entities.tb_ReferFriend.Where(z => z.FriendId == _user.UserId).OrderByDescending(z => z.ReferId).FirstOrDefault();
                //if (userTable != null)
                //{

                //    userTable.IsActive = true;
                //    Entities.SaveChanges();
                //}
            }
            return View(model);
        }


        public ActionResult PaybyCash()
        {
            var datas = (UserModel)Session["UserLoginFirstTime"];
            var paidamount1 = Session["paidamount"];
            var ExpaidDays = _Entities.tb_Package.Where(x => x.PackageID == datas.TempID1).FirstOrDefault();
            TimeSpan tp = TimeSpan.FromDays(ExpaidDays.ExpiryDays);
            DateTime CalculateDate = DateTime.Now.Add(tp);
            var createPayment = _Entities.tb_Payment.Create();
            decimal Paidamount = Convert.ToDecimal(paidamount1);
            createPayment.UserId = datas.UserId;
            createPayment.PaidStatus = false; ////////
            createPayment.Amount = Paidamount;
            createPayment.PaymentType = 1;
            createPayment.TimeStamp = DateTime.Now;
            createPayment.ParentGuid = Guid.NewGuid();
            createPayment.PaymentMode = 2;
            createPayment.IsActive = true;
            createPayment.PackageID = datas.TempID1;
            createPayment.Expirydate = CalculateDate;
            _Entities.tb_Payment.Add(createPayment);
            _Entities.SaveChanges();

            Session.Abandon();
            return RedirectToAction("LogOut", "User");

        }

        #endregion

    }
}