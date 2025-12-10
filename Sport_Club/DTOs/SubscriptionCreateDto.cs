using System;
using System.ComponentModel.DataAnnotations;

namespace Sport_Club.DTOs
{
    public class SubscriptionCreateDto
    {
        [Required]
        public int MemberSectionId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string PaymentStatus { get; set; }
    }
}
