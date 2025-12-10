using Sport_Club.Enum;

namespace Sport_Club.DTOs
{
    public class TrainerGetDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; } // From User
        public string Email { get; set; } // From User
        public Gender Gender { get; set; }
        public Shift Shift { get; set; }
        public int? SectionId { get; set; }
        public string? SectionName { get; set; }
        public int ExperienceYears { get; set; }
    }
}
