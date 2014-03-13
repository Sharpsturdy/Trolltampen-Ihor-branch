using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace Trolltampen.App_Code
{
    public class MailService : ISendRecoveryLink
    {
        string token;
        string email;
        string userName;
        string app;
        public MailService(string token, string email, string userName, string app)
        {
            this.token = token;
            this.email = email;
            this.userName = userName;
            this.app = app;
        }

        public void SendLink()
        {

            SmtpClient smtpClient = new SmtpClient();
            //smtpClient.Credentials = new System.Net.NetworkCredential("escon@kplanner.no", "andriy2501");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpClient.EnableSsl = false;
            //smtpClient.UseDefaultCredentials = false;
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = false;
            mail.Subject = "Recovery pasword link";
            mail.Body = @"Dear user " + userName + @".

Your link for recovery password is:

http://" + app + @"/Account/MailLink?token=" + token + @"

WELCOME !";

            //Setting From , To and CC
            mail.From = new MailAddress("escon@kplanner.no", " Festival CMS administrator");
            mail.To.Add(new MailAddress(email));
            smtpClient.Send(mail);
        }
    }
}