using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities;
public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public int SubCategoryId { get; set; }
    public SubCategory? SubCategory { get; set; }

    [Required, MaxLength(160)]
    public string Title { get; set; } = default!;

    [Required]
    public string Content { get; set; } = default!;

    [MaxLength(500)]
    public string? ImageUrl { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public bool IsDeleted { get; set; }
    public bool IsLocked { get; set; }
    [ValidateNever]
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    [ValidateNever]
    public ICollection<Media> Media { get; set; } = new List<Media>();
}
