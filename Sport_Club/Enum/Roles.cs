using System.ComponentModel.DataAnnotations;

namespace Sport_Club.Enum
{
    public enum Roles
    {
        [Display(Name = "Member")]
        Member = 0,
        [Display(Name = "Trainer")]
        Trainer = 1,
        [Display(Name = "Admin")]
        Admin = 2
    }
}
