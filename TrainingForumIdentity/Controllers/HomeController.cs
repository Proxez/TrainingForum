using EFCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TrainingForum.Web.Models;

namespace TrainingForum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(MyDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("CategoryAdmin")]
        public IActionResult CategoryAdmin()
        {
            return View();
        }
        //public IActionResult DietAndNutrition()
        //{
        //    return View();
        //}
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
                var role = new IdentityRole
                { Name = roleName };
                await _roleManager.CreateAsync(role);
            }
            return RedirectToAction(nameof(RoleAdmin));
        }
        //public IActionResult WeightManagement()
        //{
        //    return View();
        //}
        //public IActionResult Motivation()
        //{
        //    return View();
        //}
        //public IActionResult General()
        //{
        //    return View();
        //}
        //public IActionResult Privacy()
        //{
        //    return View();
        //}
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
