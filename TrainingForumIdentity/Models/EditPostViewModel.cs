using System.ComponentModel.DataAnnotations;

namespace TrainingForumIdentity.Models;

public class EditPostViewModel
{
    [Required]
    public int Id { get; set; }

    [Required, StringLength(160)]
    public string Title { get; set; } = "";

    [Required]
    public string Content { get; set; } = "";

    [Required]
    public int SubCategoryId { get; set; }

    public string? ExistingImageUrl { get; set; }

    public IFormFile? UploadedImage { get; set; }

    public bool RemoveImage { get; set; }
}
