namespace LibrarySystem.Data
{
    public class MailSettings
    {
        public static string Mail { get; set; }
        public static string Password { get; set; }
        public static string Host { get; set; }
        public static int Port { get; set; }
        public static string BccMail { get; set; }
        static MailSettings()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json") // Specify the path to your JSON file
                .Build();

            Mail = configuration["MailSettings:Mail"];
            Password = configuration["MailSettings:Password"];
            Host = configuration["MailSettings:Host"];
            Port = int.Parse(configuration["MailSettings:Port"]);
            BccMail = configuration["MailSettings:BccMail"];
        }
    }
}
