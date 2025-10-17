using Application.Service.Interface;
using EFCore;
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
        private readonly MyDbContext _context;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<User> _userManager;

        public HomeController(MyDbContext context, RoleManager<IdentityRole<int>> roleManager, UserManager<User> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            var posts = await _context.Posts
                .AsNoTracking()
                .Include(p => p.User)
                .Include(p => p.SubCategory)
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return View(posts);
            //return View();
        }
        [HttpGet]
        public async Task<IActionResult> RoleAdmin(string RemoveUserId, string AddUserId, string RoleName)
        {
            if (AddUserId != null)
            {
                var alterUser = await _userManager.FindByIdAsync(AddUserId);
                await _userManager.AddToRoleAsync(alterUser, RoleName);
            }

            if (RemoveUserId != null)
            {
                var alterUser = await _userManager.FindByIdAsync(RemoveUserId);
                await _userManager.RemoveFromRoleAsync(alterUser, RoleName);
            }


            RoleAdminViewModel vm = new RoleAdminViewModel();

            vm.Roles = await _roleManager.Roles.ToListAsync();
            vm.Users = await _userManager.Users.ToListAsync();
            vm.UserManager = _userManager;

            return View(vm);
        }
        [HttpPost]
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
