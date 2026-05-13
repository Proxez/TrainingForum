using System.ComponentModel.DataAnnotations;

namespace TrainingForumIdentity.Models;

public class CreateSubCategoryViewModel
{
    [Required]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Måste fylla i underkategori namn!")]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }
}
