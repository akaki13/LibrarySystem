using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.Api
{
    public class UpdateGenreApi
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
