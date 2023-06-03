﻿using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.Api
{
    public class PositionApi
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int? Salary { get; set; }
    }
}
