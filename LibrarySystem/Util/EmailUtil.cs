using HtmlAgilityPack;
using LibrarySystem.Data;
using System.Net;
using System.Net.Mail;

namespace LibrarySystem.Util
{
    public static class EmailUtil
    {
        public static async Task  PassworResetLink(string toEmail , string link, IConfiguration configuration)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(DataUtil.PasswordHtml);
            HtmlNode linkNode = doc.DocumentNode.SelectSingleNode("//a");

            if (linkNode != null)
            {
                linkNode.SetAttributeValue("href", link);
            }
            var emailSubject = DataUtil.PasswordEmailSubject;
            var emailMessage = doc.DocumentNode.OuterHtml;
            await SendEmailAsync(toEmail, emailSubject, emailMessage , configuration);
        }

        public static async Task EmailConfirmedLink(string toEmail, string link, IConfiguration configuration)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(DataUtil.EmailHtml);
            HtmlNode linkNode = doc.DocumentNode.SelectSingleNode("//a");

            if (linkNode != null)
            {
                linkNode.SetAttributeValue("href", link);
            }
            var emailSubject = DataUtil.ConfirmEmailSubject;
            var emailMessage = doc.DocumentNode.OuterHtml;
            await SendEmailAsync(toEmail, emailSubject, emailMessage, configuration);
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

