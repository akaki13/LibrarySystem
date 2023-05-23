using LibrarySystem.Data;
using Microsoft.AspNetCore.Html;
using System.Net;
using System.Net.Mail;

namespace LibrarySystem.Util
{
    public static class EmailUtil
    {
        public static void PassworResetLink(string toEmail, string Body)
        {
          /*  MailAddress to = new MailAddress(toEmail);
            MailAddress from = new MailAddress(DataUtil.Email);

            MailMessage email = new MailMessage(from, to);
            email.Subject = "Testing out email sending";
            email.Body = "Hello all the way from the land of C#";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = DataUtil.smtpServer;
            smtp.Port = 25;
            smtp.Credentials = new NetworkCredential(DataUtil.Email, DataUtil.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            try
            {
                *//* Send method called below is what will send off our email 
                 * unless an exception is thrown.
                 *//*
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }*/

            //    MailMessage mailMessage = new MailMessage(DataUtil.Email, toEmail, subject, body);
        }
    }
}
