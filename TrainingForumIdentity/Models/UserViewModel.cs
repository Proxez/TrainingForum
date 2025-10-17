using System.ComponentModel.DataAnnotations;

namespace TrainingForumIdentity.Models;

public class UserViewModel
{
    [Required, EmailAddress, MaxLength(320)]
    public string Email { get; set; } = default!;
    [Required, MaxLength(40)]
    public string UserName { get; set; } = default!;
    [Required, MaxLength(40)]
    public string FirstName { get; set; }
    [Required, MaxLength(100)]
    public string LastName { get; set; }
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = default!;
    [Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "Lösenorden stämmer inte överens med varandra.")]
    public string ConfirmPassword { get; set; } = default!;
}
