using System.ComponentModel.DataAnnotations;

namespace Application.Service.DTOs;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "Du måste fylla i kategori namn!")]
    public string Title { get; set; }
    public string? Description { get; set; }
}
