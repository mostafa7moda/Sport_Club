namespace Sport_Club.Models
{
    public class Member : BaseModel
    {
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public string? EmergencyPhone { get; set; }
        public string? HealthNotes { get; set; }
        public DateTime JoinDate { get; set; }

        public ICollection<MemberSection>? MemberSections { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
        public ICollection<TeamMember>? TeamMembers { get; set; }
    }
}
