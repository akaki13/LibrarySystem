using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.Api
{
    public class AddPublisherApi
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
