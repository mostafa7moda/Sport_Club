namespace Sport_Club.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int? CoachId { get; set; }
        public Trainer? Coach { get; set; }

        public ICollection<TeamMember>? TeamMembers { get; set; }
    }
}
