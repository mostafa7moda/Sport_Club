namespace Sport_Club.Models
{
    public class Trainer
    {
        public int Id { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }

        public string Gender { get; set; }
        public string Shift { get; set; }
        public int ExperienceYears { get; internal set; }
    }
}
