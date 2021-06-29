using System;
using System.ComponentModel.DataAnnotations;

namespace CommerceBankOnlineBanking.Models
{
    public class Transaction
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }
        [Required]
        public string Action { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime ProcessingDate { get; set; }
        [Required]
        public Double Balance { get; set; }
        [Required]
        public Double Amount { get; set; }
    }
}