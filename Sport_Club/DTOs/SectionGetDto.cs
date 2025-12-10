using Sport_Club.Enum;

namespace Sport_Club.DTOs
{
    public class SectionGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Shift Shift { get; set; }
        public string Gender { get; set; }
        public string? ManagerId { get; set; }
        public string? ManagerName { get; set; }
    }
}
