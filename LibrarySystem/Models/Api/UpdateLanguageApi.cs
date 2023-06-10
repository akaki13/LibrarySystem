using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.Api
{
    public class UpdateLanguageApi
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
