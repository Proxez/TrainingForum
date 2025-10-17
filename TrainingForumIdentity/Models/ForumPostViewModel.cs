using Entities;
using System.ComponentModel.DataAnnotations;

namespace TrainingForumIdentity.Models;


public class ForumPostViewModel
{
    [Required, StringLength(160)]
    public string Title { get; set; } = "";

    [Required]
    public string Content { get; set; } = "";

    [Required]
    public int SubCategoryId { get; set; }

    public IFormFile? UploadedImage { get; set; }
}
