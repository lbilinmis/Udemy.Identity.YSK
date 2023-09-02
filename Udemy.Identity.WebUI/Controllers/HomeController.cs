using Microsoft.AspNetCore.Mvc;

namespace Udemy.Identity.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
