using System;
using System.ComponentModel.DataAnnotations;

namespace Sport_Club.DTOs
{
    public class MemberUpdateDto
    {
        [Phone]
        public string? EmergencyPhone { get; set; }

        public string? HealthNotes { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }
    }
}
