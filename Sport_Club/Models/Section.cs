using System.ComponentModel.DataAnnotations.Schema;

namespace Sport_Club.Models
{
    public class Section
    {
        public int SectionId { get; set; }
        public string Name { get; set; }
        public string Shift { get; set; }
        public string Gender { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public string? ManagerId { get; set; }
        public ApplicationUser? Manager { get; set; }

        public ICollection<Trainer>? Trainers { get; set; }
        public ICollection<MemberSection>? MemberSections { get; set; }
        public ICollection<Attendance>? Attendance { get; set; }
        public ICollection<Competition>? Competitions { get; set; }
    }
}
