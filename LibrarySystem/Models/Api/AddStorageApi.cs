using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.Api
{
    public class AddStorageApi
    {

        [Required]
        public string Saction { get; set; }
        [Required]
        public string Row { get; set; }
        [Required]
        public string Shell { get; set; }
    }
}
