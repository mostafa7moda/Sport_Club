namespace Sport_Club.Models
{
    public class Team : BaseModel
    {
        public string TeamName { get; set; }

        public int DepartmentId { get; set; }

        public int? CoachId { get; set; }
        public Trainer? Coach { get; set; }

        public ICollection<TeamMember>? TeamMembers { get; set; }
    }
}
