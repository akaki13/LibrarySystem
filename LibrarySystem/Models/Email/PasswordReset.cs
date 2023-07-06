namespace LibrarySystem.Models.Email
{
    public class PasswordReset
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string PasswordResetLink { get; set; }
    }
}
