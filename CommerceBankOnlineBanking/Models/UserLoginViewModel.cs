using System.ComponentModel.DataAnnotations;

namespace CommerceBankOnlineBanking.Models
{
    public class UserLoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

}