using Sport_Club.Enum;
using System.ComponentModel.DataAnnotations;

namespace Sport_Club.DTOs
{
    // Used by Controller to receive full request
    public class TrainerRegistrationDto
    {
        [Required]
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public Shift Shift { get; set; }
        public int? SectionId { get; set; }
        public int ExperienceYears { get; set; }
    }

    // Used by Service to create Trainer entity
    public class TrainerCreateDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public Gender Gender { get; set; } // Enforced from User or separate? Trainer has Gender property too.
        [Required]
        public Shift Shift { get; set; }
        public int? SectionId { get; set; }
        public int ExperienceYears { get; set; }
    }
}
