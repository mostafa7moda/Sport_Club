namespace Sport_Club.Models
{
    public class MemberSection : BaseModel
    {
        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        public Subscription? Subscription { get; set; }
    }
}
