using Ma.EntityLibrary;
using Medacademy.Models;
using Medacademy.Repository.Paytm;
using paytm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medacademy.Controllers
{
    public class PaytmController : Controller
    {
        MAEntities MAEntities = new MAEntities();
        // GET: Paytm
        public ActionResult Index()
        {
            return RedirectToAction("CreatePayment", "Paytm");
            // return View();
        }

       
        public ActionResult CreatePayment()
        {
            var datas = (UserModel)Session["UserLoginFirstTime"];
            var paidamount1 = Session["paidamount"];
            string paidamount = paidamount1.ToString();
            try
            {
                if (datas != null && paidamount != null)
                {

                    //string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
                    //datas.ReturnUrl = baseUrl + "/User/PaymentResponse";
                    //Session["UserLoginFirstTime"] = datas;
                    //return RedirectToAction("PaymentPost");
                    String merchantKey = PaytmRepository.MerchantKey;
                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    parameters.Add("MID", PaytmRepository.MerchantID);
                    parameters.Add("ORDER_ID", PaytmRepository.OrderID);
                    parameters.Add("CUST_ID", datas.UserId.ToString());
                    parameters.Add("TXN_AMOUNT", paidamount);
                    parameters.Add("CHANNEL_ID", "WEB");
                    parameters.Add("WEBSITE", PaytmRepository.Website);
                    //parameters.Add("CHECKSUMHASH", null);

                    parameters.Add("INDUSTRY_TYPE_ID", "industryvalue");
                    
                    parameters.Add("EMAIL", datas.Email);
                    parameters.Add("MOBILE_NO", datas.ContactNo);
                    
                    
                    
                    parameters.Add("CALLBACK_URL", PaytmRepository.CallBackUrl); //This parameter is not mandatory. Use this to pass the callback url dynamically.


                    string checksum = CheckSum.generateCheckSum(merchantKey, parameters);

                    string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + parameters.FirstOrDefault(x => x.Key == "ORDER_ID").Value;

                    string outputHTML = "<html>";
                    outputHTML += "<head>";
                    outputHTML += "<title>Merchant Check Out Page</title>";
                    outputHTML += "</head>";
                    outputHTML += "<body>";
                    outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
                    outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
                    outputHTML += "<table border='1'>";
                    outputHTML += "<tbody>";
                    foreach (string key in parameters.Keys)
                    {
                        outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
                    }
                    outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
                    outputHTML += "</tbody>";
                    outputHTML += "</table>";
                    outputHTML += "<script type='text/javascript'>";
                    outputHTML += "document.f1.submit();";
                    outputHTML += "</script>";
                    outputHTML += "</form>";
                    outputHTML += "</body>";
                    outputHTML += "</html>";

                    ViewBag.htmlData = outputHTML;

                    PaytmRequestSave(parameters);


                    return View("Paymentpage");

                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("SessionExpired", "User");
            }


            return RedirectToAction("SessionExpired", "User");


           
        }

        [HttpPost]
        public ActionResult paytmResponse(PaytmResponceModel model)
        {
            var datas = (UserModel)Session["UserLoginFirstTime"];
            datas.PaytmResponceModel = model;
            Session["UserLoginFirstTime"] = datas;
            var pakagename = MAEntities.tb_Package.Where(x => x.PackageID == datas.TempID1).FirstOrDefault();
            PaymentPaytmModel PaymentPaytmModel = new PaymentPaytmModel();

            PaytmResponseSave(model);
            try
            {
                if (model.STATUS == "TXN_SUCCESS")
                {

                    var requestId = Session["LastInsertedIdRequest"];
                    var responceId = Session["LastInsertedIdResponse"];
                    var paidamount1 = Session["paidamount"];
                    var ExpaidDays = MAEntities.tb_Package.Where(x => x.PackageID == datas.TempID1).FirstOrDefault();
                    TimeSpan tp = TimeSpan.FromDays(ExpaidDays.ExpiryDays);
                    DateTime CalculateDate = DateTime.Now.Add(tp);
                    var createPayment = MAEntities.tb_Payment.Create();
                    decimal Paidamount = Convert.ToDecimal(paidamount1);
                    createPayment.UserId = datas.UserId;
                    createPayment.PaidStatus = true; ////////
                    createPayment.Amount = Paidamount;
                    createPayment.PaymentType = 1;
                    createPayment.TimeStamp = DateTime.Now;
                    createPayment.ParentGuid = Guid.NewGuid();
                    createPayment.PaymentMode = 1;
                    createPayment.IsActive = true;
                    createPayment.PackageID = datas.TempID1;
                    createPayment.Expirydate = CalculateDate;
                    createPayment.REQUEST_ID = (decimal)requestId;
                    createPayment.RESPONSE_ID = (decimal)responceId;
                    MAEntities.tb_Payment.Add(createPayment);
                    MAEntities.SaveChanges();

                   


                    PaymentPaytmModel.Expirydate = CalculateDate;
                    PaymentPaytmModel.PakageName = pakagename.Name;
                    datas.PaymentPaytmModel = PaymentPaytmModel;

                    Session["UserLoginFirstTime"] = datas;

                }
                else
                {
                    PaymentPaytmModel.PakageName = pakagename.Name;
                    datas.PaymentPaytmModel = PaymentPaytmModel;
                    Session["UserLoginFirstTime"] = datas;

                }
            }
            catch (Exception ex)
            {

            }

            
            return RedirectToAction("PaymentMessage", "Paytm");
        }

       
        public ActionResult PaymentMessage()
        {
            var datas = (UserModel)Session["UserLoginFirstTime"];
            try
            {
                if (datas != null)
                {
                    Session.Abandon();
                    return View("PaymentMessage", datas);
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("SessionExpired", "User");
            }


            return RedirectToAction("LogOut", "User");
        }

        public bool PaytmRequestSave(Dictionary<string, string> parameters)
        {
            var datas = (UserModel)Session["UserLoginFirstTime"];
            try
            {
                var createRequest = MAEntities.tb_PAYTM_REQUEST.Create();
                createRequest.MID = parameters.FirstOrDefault(x => x.Key == "MID").Value;
                createRequest.ORDER_ID = parameters.FirstOrDefault(x => x.Key == "ORDER_ID").Value;
                createRequest.CUST_ID = parameters.FirstOrDefault(x => x.Key == "CUST_ID").Value;
                createRequest.TXN_AMOUNT = parameters.FirstOrDefault(x => x.Key == "TXN_AMOUNT").Value;
                createRequest.CHANNEL_ID = parameters.FirstOrDefault(x => x.Key == "CHANNEL_ID").Value;
                createRequest.WEBSITE = parameters.FirstOrDefault(x => x.Key == "WEBSITE").Value;
                createRequest.INDUSTRY_TYPE_ID = parameters.FirstOrDefault(x => x.Key == "INDUSTRY_TYPE_ID").Value;
                createRequest.EMAIL = parameters.FirstOrDefault(x => x.Key == "EMAIL").Value;
                createRequest.MOBILE_NO = parameters.FirstOrDefault(x => x.Key == "MOBILE_NO").Value;
                createRequest.CALLBACK_URL = parameters.FirstOrDefault(x => x.Key == "CALLBACK_URL").Value;
                createRequest.IsActive = true;
                createRequest.TimeStamp = DateTime.Now;

                createRequest.USER_ID = datas.UserId;
                createRequest.PAKAGE_ID = datas.TempID1;
                MAEntities.tb_PAYTM_REQUEST.Add(createRequest);
                MAEntities.SaveChanges();
                Session["LastInsertedIdRequest"] = MAEntities.tb_PAYTM_REQUEST.Max(x => x.REQUEST_ID);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public bool PaytmResponseSave(PaytmResponceModel model)
        {
            var datas = (UserModel)Session["UserLoginFirstTime"];
            try
            {
                var lastInsertedID = Session["LastInsertedIdRequest"];
                var createResponce = MAEntities.tb_PAYTM_RESPONSE.Create();
                createResponce.REQUEST_ID = (decimal) lastInsertedID;
                createResponce.USER_ID = datas.UserId;
                createResponce.PAKAGE_ID = datas.TempID1;
                createResponce.MID = model.MID;
                createResponce.TXNID = model.TXNID;
                createResponce.ORDERID = model.ORDERID;
                createResponce.BANKTXNID = model.BANKTXNID;
                createResponce.TXNAMOUNT = model.TXNAMOUNT;
                createResponce.CURRENCY = model.CURRENCY;
                createResponce.STATUS = model.STATUS;
                createResponce.RESPCODE = model.RESPCODE;
                createResponce.RESPMSG = model.RESPMSG;
                createResponce.TXNDATE = model.TXNDATE;
                createResponce.GATEWAYNAME = model.GATEWAYNAME;
                createResponce.BANKNAME = model.BANKNAME;
                createResponce.PAYMENTMODE = model.PAYMENTMODE;
                createResponce.CHECKSUMHASH = model.CHECKSUMHASH;
                createResponce.BIN_NUMBER = model.BIN_NUMBER;
                createResponce.CARD_LAST_NUMS = model.CARD_LAST_NUMS;
                createResponce.IsActive = true;
                createResponce.TimeStamp = DateTime.Now;
                MAEntities.tb_PAYTM_RESPONSE.Add(createResponce);
                MAEntities.SaveChanges();
                Session["LastInsertedIdResponse"] = MAEntities.tb_PAYTM_RESPONSE.Max(x => x.RESPONSE_ID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}