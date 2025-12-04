namespace Sport_Club.Models
{
    public class CompetitionResult
    {
        public int Id { get; set; }

        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int Position { get; set; }
        public int Score { get; set; }
        public string? Notes { get; set; }
    }
}
