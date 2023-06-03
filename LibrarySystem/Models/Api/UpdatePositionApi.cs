using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.Api
{
    public class UpdatePositionApi
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int? Salary { get; set; }
    }
}
