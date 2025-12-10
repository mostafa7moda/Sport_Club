using System;

namespace Sport_Club.DTOs
{
    public class SubscriptionGetDto
    {
        public int Id { get; set; }
        public int MemberSectionId { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PaymentStatus { get; set; }
    }
}
