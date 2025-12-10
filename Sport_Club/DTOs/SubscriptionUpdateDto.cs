using System;
using System.ComponentModel.DataAnnotations;

namespace Sport_Club.DTOs
{
    public class SubscriptionUpdateDto
    {
        [Required]
        public string PaymentStatus { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
