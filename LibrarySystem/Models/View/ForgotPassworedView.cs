using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LibrarySystem.Models.View
{
    public class ForgotPassworedView
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
