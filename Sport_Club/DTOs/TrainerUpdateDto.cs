using Sport_Club.Enum;
using System.ComponentModel.DataAnnotations;

namespace Sport_Club.DTOs
{
    public class TrainerUpdateDto
    {
        [Required]
        public Shift Shift { get; set; }
        public int? SectionId { get; set; }
        public int ExperienceYears { get; set; }
    }
}
