using Learning_Management_System.Models;
using Learning_Management_System.Repositories.IdentityRepo;
using Learning_Management_System.Repositories.ModelRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Learning_Management_System.Controllers
{
    public class IdentityController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAccountRepo _account;
        public IdentityController(SignInManager<ApplicationUser> signInManager, IAccountRepo account)
        {
            _signInManager = signInManager;
            _account = account;
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
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var result = await _account.RegisterAsync(registerModel);
            if (!result.Succeeded)
            {
                return View();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var result = await _account.LoginAsync(loginModel);
            if (string.IsNullOrEmpty(result))
            {
                return View();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
