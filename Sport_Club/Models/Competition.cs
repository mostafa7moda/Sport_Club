namespace Sport_Club.Models
{
    public class Competition
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }

        public ICollection<CompetitionResult>? Results { get; set; }
    }
}
