using HtmlAgilityPack;
using LibrarySystem.Data;
using LibrarySystemModels;
using System;
using System.Net;
using System.Net.Mail;

namespace LibrarySystem.Util
{
    public static class EmailUtil
    {
        public static async Task  PassworResetLink(string toEmail , string link, IConfiguration configuration, Person person)
        {

            string htmlData = File.ReadAllText(DataUtil.PasswordHtml);
            htmlData = htmlData.Replace("{FirstName}", person.Firstname);
            htmlData = htmlData.Replace("{LastName}", person.Lastname);
            htmlData = htmlData.Replace("{ResetPasswordLink}", link);
            await SendEmailAsync(toEmail, DataUtil.PasswordEmailSubject, htmlData, configuration);
        }
        
        public static async Task EmailConfirmedLink(string toEmail, string link, IConfiguration configuration, Person person)
        {
           
            string htmlData = File.ReadAllText(DataUtil.EmailHtml);
            htmlData = htmlData.Replace("{FirstName}", person.Firstname);
            htmlData = htmlData.Replace("{LastName}", person.Lastname);
            htmlData = htmlData.Replace("{EmailCinfirmLink}", link);
            await SendEmailAsync(toEmail, DataUtil.ConfirmEmailSubject, htmlData, configuration);
        }

        public static async Task SendEmailAsync(string email, string subject, string message, IConfiguration configuration)
        {
            var smtpClient = new SmtpClient(configuration.GetSection("MailSettings:Host").Value, configuration.GetValue<int>("MailSettings:Port"))
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(configuration.GetSection("MailSettings:Mail").Value, configuration.GetSection("MailSettings:Password").Value)
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(configuration.GetSection("MailSettings:Mail").Value),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);
            mailMessage.To.Add(configuration.GetSection("MailSettings:Bccmail").Value);
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}

