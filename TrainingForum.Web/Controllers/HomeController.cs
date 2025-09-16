using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrainingForum.Web.Models;

namespace TrainingForum.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Training()
        {
            return View();
        }
        public IActionResult DietAndNutrition()
        {
            return View();
        }
        public IActionResult WeightManagement()
        {
            return View();
        }
        public IActionResult Motivation()
        {
            return View();
        }
        public IActionResult General()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
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
