namespace Sport_Club.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public string? ManagerId { get; set; }
        public ApplicationUser? Manager { get; set; }

        public ICollection<Section>? Sections { get; set; }
        public ICollection<Team>? Teams { get; set; }
    }
}
