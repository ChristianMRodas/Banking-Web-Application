using System;
using System.ComponentModel.DataAnnotations;

namespace CommerceBankOnlineBanking.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&?]).{8,}$",
                            ErrorMessage = "Password must be at least 8 characters, and contain 1 upper case letter, 1 lower case letter, 1 digit and at least 1 special character.")]
        public string Password { get; set; }
    }
}