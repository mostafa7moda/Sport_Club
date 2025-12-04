using Microsoft.AspNetCore.Identity;

namespace Sport_Club.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Gender { get; set; }

        public ICollection<Member>? Members { get; set; }
        public ICollection<Trainer>? Trainers { get; set; }
    }
}
