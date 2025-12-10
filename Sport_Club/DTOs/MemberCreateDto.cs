using System;
using System.ComponentModel.DataAnnotations;

namespace Sport_Club.DTOs
{
    public class MemberCreateDto
    {
        [Required]
        public string UserId { get; set; }

        [Phone]
        public string? EmergencyPhone { get; set; }

        public string? HealthNotes { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }
    }
}
