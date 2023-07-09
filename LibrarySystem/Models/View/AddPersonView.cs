using LibrarySystemModels;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.View
{
    public class AddPersonView
    {
       

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

    }
}
