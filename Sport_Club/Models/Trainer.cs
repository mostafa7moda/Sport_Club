using Sport_Club.Enum;

namespace Sport_Club.Models
{
    public class Trainer : BaseModel
    {
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int? SectionId { get; set; }
        public Section? Section { get; set; }

        public Gender? Gender { get; set; }
        public Shift? Shift { get; set; }
        public int? ExperienceYears { get; set; }

    }
}
