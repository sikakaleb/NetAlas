using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetAtlas.ViewModels
{
    public class RegisterVM
    {
        [Required, MinLength(5)]
        public string FullName { get; set; } = String.Empty;

        [Required, MinLength(5)]
        public string userName { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DatNaiss { get; set; }

        [Required]
        public string Country { get; set; } = String.Empty;

        [EmailAddress, Required]
        public string Email { get; set; }

        [Required, MinLength(5),  DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password"),DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Bio { get; set; } = String.Empty;


    }
}
