using System;
using System.ComponentModel.DataAnnotations;

namespace Sport_Club.DTOs
{
    public class AttendanceLogDto
    {
        [Required]
        public int MemberId { get; set; }

        [Required]
        public int SectionId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
