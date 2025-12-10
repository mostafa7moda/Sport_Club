using Sport_Club.Enum;
using System.ComponentModel.DataAnnotations;

namespace Sport_Club.DTOs
{
    public class SectionUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Shift Shift { get; set; }

        [Required]
        public string Gender { get; set; }

        public string? ManagerId { get; set; }
    }
}
