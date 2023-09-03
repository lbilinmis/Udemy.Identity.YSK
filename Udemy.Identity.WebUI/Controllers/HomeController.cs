using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Udemy.Identity.WebUI.Entities;
using Udemy.Identity.WebUI.Models;

namespace Udemy.Identity.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    Email = model.EMail,
                    UserName = model.UserName,
                    Gender = model.Gender,
                    ImagePath = "test",
                };
                var identityResult = await _userManager.CreateAsync(user, model.Password);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }


        public IActionResult SignIn()
        { return View(); }


        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);

                if (signInResult.Succeeded)
                {

                }
                else if (signInResult.IsLockedOut)
                {

                }
                else if (signInResult.IsNotAllowed)
                {

                }
            }
            return View(model);
        }
    }
}
