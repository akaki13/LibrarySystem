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
        public static async Task  CreateTextAndSend<T>(string path, string subject, string toEmail , IConfiguration configuration, T model)
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
            await SendEmailAsync(toEmail, subject, htmlData, configuration);
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

