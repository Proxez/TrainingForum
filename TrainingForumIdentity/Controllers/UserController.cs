//using Entities;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using TrainingForumIdentity.Models;

//namespace TrainingForumIdentity.Controllers;
//[Authorize]
//public class UserController : Controller
//{
//    private readonly UserManager<User> _userManager;
//    private readonly IWebHostEnvironment _env;
    
//    public UserController(UserManager<User> userManager, IWebHostEnvironment env)
//    {
//        _userManager = userManager;
//        _env = env;
//    }
//    public IActionResult Index()
//    {
//        return View();
//    }

//    [HttpGet]
//    public async Task<IActionResult> EditProfile()
//    {
//        var u = await _userManager.GetUserAsync(User);
//        return View(new ProfileEditViewModel
//        {
//            PhoneNumber = await _userManager.GetPhoneNumberAsync(u),
//            CurrentAvatarUrl = u.AvatarUrl
//        });
//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> EditProfile(ProfileEditViewModel vm)
//    {
//        var u = await _userManager.GetUserAsync(User);
//        if (!ModelState.IsValid) return View(vm);

//        if (vm.RemoveAvatar) { /* ta bort gammal + nolla */ }
//        else if (vm.AvatarFile is { Length: > 0 }) { /* validera, spara, sätt u.AvatarUrl */ }

//        await _userManager.UpdateAsync(u);
//        TempData["Message"] = "Profilen uppdaterades.";
//        return RedirectToAction(nameof(EditProfile));
//    }
//}
