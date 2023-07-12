using LibrarySystem.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.View
{
    public class AddBorrowView
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public string Book { get; set; }

        [Required]
        public string Person { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        [Display(Name = "Return Time")]
        [CustomValidation(typeof(AddBorrowView), "ValidateReturnedTime")]
        public DateTime ReturnedTime { get; set; }

        public static ValidationResult ValidateReturnedTime(DateTime returnedTime)
        {
            if (returnedTime <= DateTime.Now)
            {
                return new ValidationResult(DataUtil.ReturnDataCheck);
            }

            return ValidationResult.Success;
        }
    }
}
