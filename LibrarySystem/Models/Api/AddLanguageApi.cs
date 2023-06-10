using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.Api
{
    public class AddLanguageApi
    {

        [Required]
        public string Title { get; set; }
    }
}
