using HtmlAgilityPack;
using LibrarySystem.Data;
using LibrarySystem.Models.Email;
using LibrarySystemModels;
using System;
using System.Net;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace LibrarySystem.Util
{
    public static class EmailUtil
    {
        public static async Task  CreateTextAndSend<T>(string path, string subject, string toEmail , T model)
        {

            string htmlData = File.ReadAllText(path);
            Type modelType = model.GetType();
            PropertyInfo[] properties = modelType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string placeholder = "{" + property.Name + "}";
                string value = property.GetValue(model)?.ToString();
                htmlData = htmlData.Replace(placeholder, value);
            }
            await SendEmailAsync(toEmail, subject, htmlData);
        }

        public static async Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient(MailSettings.Host,MailSettings.Port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(MailSettings.Mail, MailSettings.Password)
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(MailSettings.Mail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);
            mailMessage.To.Add(MailSettings.BccMail);
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}

