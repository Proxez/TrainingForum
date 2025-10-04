using Microsoft.AspNetCore.Mvc;

namespace TrainingForumIdentity.Controllers;
public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
