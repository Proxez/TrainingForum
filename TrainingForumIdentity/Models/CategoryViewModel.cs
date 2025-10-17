using Entities;

namespace TrainingForumIdentity.Models;

public class CategoryViewModel
{
    public Category Category { get; set; }
    public List<Category> Categories { get; set; } = new();
    public SubCategory SubCategory { get; set; }
    public List<SubCategory> SubCategories { get; set; } = new();

    public List<Post> AllPosts { get; set; } = new();
    public Post Post { get; set; }
    public User User { get; set; }

}
        