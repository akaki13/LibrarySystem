using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.Api
{
    public class AddGenreApi
    {
        [Required]
        public string Name { get; set; }
    }
}
