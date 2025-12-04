namespace Sport_Club.Models
{
    public class TeamMember
    {
        public int TeamMemberId { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }

        public DateTime JoinDate { get; set; }
    }
}
