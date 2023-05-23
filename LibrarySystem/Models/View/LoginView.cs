using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.View
{
    public class LoginView
    {
        //  (ErrorMessage = "You must provide a phone number")
        [Required]
        [Display(Name = "Login")]
        public string Usarname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
