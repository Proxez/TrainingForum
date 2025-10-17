using Entities;
using Microsoft.AspNetCore.Identity;

namespace TrainingForumIdentity.Models;

public class RoleAdminViewModel
{
    public List<User> Users { get; set; } = new();
    public List<IdentityRole<int>> Roles { get; set; } = new();
    public UserManager<User> UserManager { get; set; } = default!;
}
