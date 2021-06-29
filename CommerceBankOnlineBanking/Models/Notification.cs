using System;
using System.ComponentModel.DataAnnotations;

namespace CommerceBankOnlineBanking.Models
{
    public class Notification
    {
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime NotifiedDate { get; set; }
    }
}