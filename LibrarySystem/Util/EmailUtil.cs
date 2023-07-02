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
        public static async Task  PassworResetLink(string toEmail , IConfiguration configuration, EmailModel model)
        {

            string htmlData = File.ReadAllText(DataUtil.PasswordHtml);
            Type modelType = model.GetType();
            PropertyInfo[] properties = modelType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string placeholder = "{" + property.Name + "}";
                string value = property.GetValue(model)?.ToString();
                htmlData = htmlData.Replace(placeholder, value);
            }
            await SendEmailAsync(toEmail, DataUtil.PasswordEmailSubject, htmlData, configuration);
        }
        
        public static async Task EmailConfirmedLink(string toEmail, IConfiguration configuration, EmailModel model)
        {
           
            string htmlData = File.ReadAllText(DataUtil.EmailHtml);
            Type modelType = model.GetType();
            PropertyInfo[] properties = modelType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string placeholder = "{" + property.Name + "}";
                string value = property.GetValue(model)?.ToString();
                htmlData = htmlData.Replace(placeholder, value);
            }
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

