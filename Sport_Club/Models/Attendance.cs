namespace Sport_Club.Models
{
    public class Attendance : BaseModel
    {
        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }

        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
