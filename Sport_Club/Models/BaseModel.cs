namespace Sport_Club.Models
{
    public class BaseModel
    {
        public int ID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
