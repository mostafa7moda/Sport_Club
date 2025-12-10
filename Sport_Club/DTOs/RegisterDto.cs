using Sport_Club.Enum;
using System.ComponentModel.DataAnnotations;

public class RegisterDto
{
    public string UserName { get; set; }


    public string Email { get; set; }
    public string Password { get; set; }

    public string? EmergencyPhone { get; set; }
    public string? HealthNotes { get; set; }

    public Gender Gender { get; set; }
}
