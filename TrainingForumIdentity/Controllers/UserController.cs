using Microsoft.AspNetCore.Mvc;

namespace TrainingForum.Web.Controllers;
public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
