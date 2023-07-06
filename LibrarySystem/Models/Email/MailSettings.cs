namespace LibrarySystem.Models.Email
{
    public class MailSettings
    {
        public static string Mail { get; set; }
        public static string Password { get; set; }
        public static string Host { get; set; }
        public static int Port { get; set; }
        public static string BccMail { get; set; }

        public static void GetData()
        {
            var configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();
            var mailSettings = configuration.GetSection("MailSettings");

            Mail = mailSettings["Mail"];
            Password = mailSettings["Password"];
            Host = mailSettings["Host"];
            BccMail = mailSettings["Bccmail"];

        }
    }
}
