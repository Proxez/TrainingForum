using System.ComponentModel.DataAnnotations;

namespace TrainingForumIdentity.Models;

public class SubCategoryEditViewModel
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    [Required] 
    public string Title { get; set; } = "";
    public string? Description { get; set; }
}
