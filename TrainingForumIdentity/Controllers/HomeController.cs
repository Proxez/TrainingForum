using Application.Service.Interface;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using TrainingForumIdentity.Models;

namespace TrainingForumIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<User> _userManager;

        public HomeController(IPostService postService, RoleManager<IdentityRole<int>> roleManager, UserManager<User> userManager)
        {
            _postService = postService;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            var posts = await _postService.GetPagedPostsAsync(page, pageSize);
            return View(posts);
        }
        [HttpGet]
        public async Task<IActionResult> RoleAdmin()
        {
            RoleAdminViewModel vm = new RoleAdminViewModel();

            vm.Roles = await _roleManager.Roles.ToListAsync();
            vm.Users = await _userManager.Users.ToListAsync();
            vm.UserManager = _userManager;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifyRole(string addUserId, string removeUserId, string roleName)
        {
            if (addUserId != null)
            {
                var alterUser = await _userManager.FindByIdAsync(addUserId);
                if (alterUser != null)
                    await _userManager.AddToRoleAsync(alterUser, roleName);
            }

            if (removeUserId != null)
            {
                var alterUser = await _userManager.FindByIdAsync(removeUserId);
                if (alterUser != null)
                    await _userManager.RemoveFromRoleAsync(alterUser, roleName);
            }

            return RedirectToAction(nameof(RoleAdmin));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleAdmin(string roleName)
        {
            bool roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var role = new IdentityRole<int>
                { Name = roleName };
                await _roleManager.CreateAsync(role);
            }
            return RedirectToAction(nameof(RoleAdmin));
        }
        public IActionResult Admin()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
