using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites;
public class Category
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Slug { get; set; }
    public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
