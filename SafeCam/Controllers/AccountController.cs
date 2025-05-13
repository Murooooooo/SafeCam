using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SafeCam.Helper;
using SafeCam.Models;
using SafeCam.ViewModels;
using System.Threading.Tasks;

namespace SafeCam.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new()
            {
                Name = registerVm.Name,
                UserName = registerVm.UserName,
                Email = registerVm.EmailAddress

            };
            var result = await _userManager.CreateAsync(user, registerVm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, RoleEnum.Member.ToString());

            return RedirectToAction("login", "Account");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByEmailAsync(loginVM.UserNameorEmailAddress);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(loginVM.UserNameorEmailAddress);
                if (user is null)
                {
                    ModelState.AddModelError("", "Bos qoyula bilmez");
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);

            return RedirectToAction("Index", "Home");

        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> CreateRole()
        {
            foreach (var role in Enum.GetValues(typeof(RoleEnum)))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
            }
            return Content("role yarandi");
        }
    }
}
