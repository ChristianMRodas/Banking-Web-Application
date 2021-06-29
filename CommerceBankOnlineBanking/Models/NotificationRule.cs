using System;
using System.ComponentModel.DataAnnotations;

namespace CommerceBankOnlineBanking.Models
{
    public class NotificationRule
    {
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string RuleType { get; set; }
        [Required]
        public bool Enabled { get; set; }
    }
}