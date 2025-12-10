using System;

namespace Sport_Club.DTOs
{
    public class AttendanceGetDto
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
