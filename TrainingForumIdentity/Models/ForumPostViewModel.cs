using Entities;

namespace TrainingForumIdentity.Models;

public class ForumPostViewModel
{
    public Post Post { get; set; }
    public List<Post> Posts { get; set; }
    public Comment Comment { get; set; }
    public List<Comment> Comments { get; set; }
    public IFormFile? UploadedImage { get; set; }
}   
