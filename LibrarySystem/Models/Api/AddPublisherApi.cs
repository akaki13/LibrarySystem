using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.Api
{
    public class AddPublisherApi
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
