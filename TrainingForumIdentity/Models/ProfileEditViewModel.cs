namespace TrainingForumIdentity.Models;

public class ProfileEditViewModel
{
    public string? PhoneNumber { get; set; }
    public IFormFile? AvatarFile { get; set; }
    public bool RemoveAvatar { get; set; }
    public string? CurrentAvatarUrl { get; set; }
}
