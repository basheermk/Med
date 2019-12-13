using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Medacademy.Repository
{
    public class Mail
    {

        internal static bool Send(string subject, string mailbody, string receiverName, System.Collections.ArrayList list_emails)
        {
            
            MailMessage msg = new MailMessage();
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            foreach (string mailList in list_emails)
            {
                string[] emailDomain = mailList.Split('@');
                string[] domain = emailDomain[1].Split('.');
                string dom = domain[0].ToString();
                if (dom == "sics") { }
                else
                {
                    try
                    {
                        msg.Subject = subject;
                        msg.Body = mailbody;
                        msg.From = new MailAddress("medacademy@srishtis.com");
                        msg.To.Add(new MailAddress(mailList, receiverName));
                        msg.IsBodyHtml = true;
                        client.Host = "k2smtpout.secureserver.net";
                        System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("medacademy@srishtis.com", "casics@123");
                        client.Port = int.Parse("25");
                        client.EnableSsl = false;
                        client.UseDefaultCredentials = false;
                        client.Credentials = basicauthenticationinfo;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Send(msg);
                    }
                    catch (Exception ex)
                    {
                        //ErrorLogger Logger = new ErrorLogger();
                        //Logger.LogException(ex);
                    }
                }
            }
            return true;
        }


        internal static bool ContactusSend(string subject, string mailbody, string receiverName,string reciever)
        {

            var status = false;

            SmtpClient client = new SmtpClient();
            string userName = "medacademy.co.in@gmail.com";
            string password = "ambili.S@01";
            string fromName = "Medacademy";
            MailAddress address = new MailAddress(userName, fromName);
            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress("medacademy.co.in@gmail.com", "Receiver"));
            message.From = address;
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = mailbody;
            client.Host = "smtp.gmail.com";//ConfigurationManager.AppSettings["smptpserver"];
            client.Port = 587;//Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
                              //client.Port = 465; 587
            client.EnableSsl = true;
            client.UseDefaultCredentials = true;
            //client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential(userName, password);
            try
            {
                client.Send(message);

            }
            catch (Exception e)
            {
                status = false;
            }

            return true;
        }

    }
}