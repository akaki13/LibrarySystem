using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.Api
{
    public class UpdatePublisherApi
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }
    }
}
