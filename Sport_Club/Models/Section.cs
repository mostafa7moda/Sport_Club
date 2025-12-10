using Sport_Club.Enum;

namespace Sport_Club.Models
{
    public class Section : BaseModel
    {
        public string Name { get; set; }
        public Shift Shift { get; set; }
        public string Gender { get; set; }

        public string? ManagerId { get; set; }
        public ApplicationUser? Manager { get; set; }

        public ICollection<Trainer>? Trainers { get; set; }
        public ICollection<MemberSection>? MemberSections { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
    }
}
