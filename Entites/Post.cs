using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites;
public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SubCategoryId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
    public bool IsDeleted { get; set; }
    public bool IsLocked { get; set; }
    public User User { get; set; }
    public SubCategory SubCategory { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Media> Media { get; set; } = new List<Media>();
}
