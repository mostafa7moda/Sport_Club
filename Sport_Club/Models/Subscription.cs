namespace Sport_Club.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        public int MemberSectionId { get; set; }
        public MemberSection MemberSection { get; set; }

        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PaymentStatus { get; set; }
    }
}
