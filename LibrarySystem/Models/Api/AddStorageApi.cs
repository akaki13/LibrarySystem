using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.Api
{
    public class AddStorageApi
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int? Capacity { get; set; }
    }
}
