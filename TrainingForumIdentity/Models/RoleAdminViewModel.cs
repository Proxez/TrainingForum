using Microsoft.AspNetCore.Identity;

namespace TrainingForumIdentity.Models;

public class RoleAdminViewModel
{
    public List<IdentityUser> Users { get; set; }
    public List<IdentityRole> Roles { get; set; }
    public UserManager<IdentityUser> UserManager { get; set; }
}
