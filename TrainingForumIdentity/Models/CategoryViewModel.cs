
using Entities;

namespace TrainingForumIdentity.Models;

public class CategoryViewModel
{
    public Category Category { get; set; }
    public List<Category> Categories { get; set; }
    public SubCategory SubCategory { get; set; }
    public List<SubCategory> SubCategories { get; set; }
}
        