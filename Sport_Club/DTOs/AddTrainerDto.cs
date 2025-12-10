namespace Sport_Club.DTOs
{
    using Sport_Club.Enum;
    using System.ComponentModel.DataAnnotations;

    public class AddTrainerDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
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

}
