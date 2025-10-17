using Entities;

namespace TrainingForumIdentity.Models;

public class PostCommentViewModel
{
    public List<Comment> Comments{ get; set; }
    public Post Post { get; set; }
}
    