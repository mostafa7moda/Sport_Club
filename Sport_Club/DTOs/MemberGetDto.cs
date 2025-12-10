using System;

namespace Sport_Club.DTOs
{
    public class MemberGetDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? FullName { get; set; }
        public string? EmergencyPhone { get; set; }
        public string? HealthNotes { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
