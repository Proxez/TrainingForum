using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites;
public class SubCategory
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Slug { get; set; }
    public Category Category { get; set; }
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
