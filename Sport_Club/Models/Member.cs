namespace Sport_Club.Models
{
    public class Member
    {
        public int MemberId { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public string EmergencyPhone { get; set; }
        public string? HealthNotes { get; set; }
        public DateTime JoinDate { get; set; }

        public ICollection<MemberSection>? MemberSections { get; set; }
        public ICollection<Attendance>? Attendance { get; set; }
        public ICollection<CompetitionResult>? CompetitionResults { get; set; }
        public ICollection<TeamMember>? TeamMembers { get; set; }
    }
}
