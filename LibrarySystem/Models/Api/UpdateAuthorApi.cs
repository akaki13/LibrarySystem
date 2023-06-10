using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.Api
{
    public class UpdateAuthorApi
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }
    }
}
