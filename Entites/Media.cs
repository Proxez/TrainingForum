using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites;
public class Media
{
    public int Id { get; set; }
    public int? PostId { get; set; }
    public int? CommentId { get; set; }
    public string Url { get; set; }
    public string MimeType { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Post? Post { get; set; }
    public Comment? Comment { get; set; }
}
