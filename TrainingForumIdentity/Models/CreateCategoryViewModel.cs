using System.ComponentModel.DataAnnotations;

namespace TrainingForumIdentity.Models;

public class CreateCategoryViewModel
{
    [Required(ErrorMessage = "Måste fylla i kategori namn!")]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }
}
