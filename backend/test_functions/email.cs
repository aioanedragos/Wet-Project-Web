
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace wet_api.test_functions
{
    public class email
    {
        public void ceva(string email, string messageToBeSend)
        {
            string emailFor = "VerifyAccount";
            //string emailID = "aioane.costin@yahoo.com";
            string emailID = email;

            var vefifyUrl = "/User/" + emailFor + "/" + "1";
            //var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, vefifyUrl);
            //var link = message;
            //var link = WebRequest.CreateHttp("https://localhost:7117/api/Person/insertPeople");
            var fromEmail = new MailAddress("dragosaioane1997@gmail.com", "Validation User");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Colosal13245.";

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {

                subject = "Your account is succesfullycreated!";

                body = "<br/><br/> We are excited to tell you that your account is " +
                   "succesfully created. Your password is " + messageToBeSend;
            }

            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/><br/> We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + messageToBeSend + ">Reset Password link <a/>";

            }

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

                smtp.Send(message);
        }
    }
}
