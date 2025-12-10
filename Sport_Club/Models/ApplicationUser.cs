using AutoMapper.Configuration.Annotations;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Identity;
using Sport_Club.Enum;

namespace Sport_Club.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public Gender Gender { get; set; }

        public Member? Member { get; set; }
        public Trainer? Trainer { get; set; }
    }
}
