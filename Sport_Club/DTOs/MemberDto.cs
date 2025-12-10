using Sport_Club.Enum;
using System;

namespace Sport_Club.DTOs
{
    public class MemberDto
    {
        public string? UserId { get; set; } // يجب أن يكون User موجود
        public string? EmergencyPhone { get; set; }
        public string? HealthNotes { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
