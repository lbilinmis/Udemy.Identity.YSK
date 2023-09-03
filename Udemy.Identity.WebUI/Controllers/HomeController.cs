using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Claims;
using Udemy.Identity.WebUI.Entities;
using Udemy.Identity.WebUI.Models;

namespace Udemy.Identity.WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
                var memberRole = await _roleManager.FindByNameAsync("Member");

                if (memberRole == null)
                {
                    await _roleManager.CreateAsync(new()
                    {
                        Name = "Member",
                        CreatedTime = DateTime.Now,
                    });

                }



                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Member");
                    return RedirectToAction("Index", "Home");
                }

                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }


        public IActionResult SignIn(string returnUrl)
        {
            return View(new UserLoginModel() { ReturnUrl=returnUrl});
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    var user = await _userManager.FindByNameAsync(model.UserName);
                    var roles= await _userManager.GetRolesAsync(user);

                    if(roles.Contains("Admin"))
                    {
                        return RedirectToAction("AdminPanel");
                    }
                    else
                    {
                        return RedirectToAction("MemberPanel");

                    }
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


        [Authorize]
        public IActionResult GetUserInfo()
        {
            var user = User.Identity;
            var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            return View();
        }


        [Authorize(Roles ="Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }


        [Authorize(Roles = "Member")]
        public IActionResult Member()
        {
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
