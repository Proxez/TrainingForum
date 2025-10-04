using Enums;
using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class User : IdentityUser <int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public UserRole Role { get; set; } = UserRole.Member;
        public string AvatarUrl { get; set; }
        public bool IsBanned { get; set; }
        public DateTimeOffset? EmailVerifiedAt { get; set; }
        public DateTimeOffset? LastLoginAt { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();       
    }
}
