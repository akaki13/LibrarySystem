﻿using LibrarySystemModels;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.View
{
    public class BookView
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public List<int> GenreId { get; set; }

        [Required]
        [Display(Name = "Publisher")]
        public List<int> PublisherId { get; set; }

        [Required]
        [Display(Name = "Author")]
        public List<int> AuthorId { get; set; }

        [Required]
        [Display(Name = "Language")]
        public List<int> LanguageId { get; set; }

        [Required]
        [Display(Name = "Storage")]
        public List<int> StorageId { get; set; }

    }
}
