using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.Api
{
    public class AddAuthorApi
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }
    }
}
