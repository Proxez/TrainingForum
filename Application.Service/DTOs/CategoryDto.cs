using Entities;

namespace Application.Service.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
}
