using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Learning_Management_System.Repositories.AccountRepository;
using Learning_Management_System.Extensions;
using Learning_Management_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Learning_Management_System.Controllers
{
    public class UserController : Controller
    {

        private readonly LmsDbContext _context;
        public UserController(LmsDbContext context)
        {

            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel objLoginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == objLoginModel.Email && x.Password == objLoginModel.Password.Hash());
                if (user != null)
                {
                    var claims = new List<Claim>() {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("UserId", user.UserId),
                        new Claim(ClaimTypes.Role, ((EnumClass.Role)user.Role).ToString() )
                        //new Claim(ClaimTypes.Role, role.role.RoleName)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = objLoginModel.RememberLogin
                    });
                    var lastRequestURL = HttpContext.Session.GetString(TextConstant.LastRequestURL);
                    if (string.IsNullOrEmpty(lastRequestURL))
                    {
                        return Redirect("/");
                    }
                    else
                    {
                        return Redirect(lastRequestURL);
                    }

                }
                else
                {
                    ViewBag.Message = "Invalid Credential";
                    return View(user);
                }
            }
            return View(objLoginModel);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                if (model.Email != null)
                {
                    var error = new ErrorViewModel();
                    var userExisting = _context.Users.FirstOrDefault(x => x.Email!.Trim().ToLower() == model.Email.Trim().ToUpper() && !x.IsDeleted);
                    if (userExisting != null)
                    {
                        throw new Exception("Email này đã tồn tại!");
                    }
                    if (ModelState.IsValid)
                    {
                        var user = new User
                        {
                            UserName = model.UserName,
                            Email = model.Email.Trim().ToUpper(),
                            Password = model.Password.Hash(),
                            Role = (int)EnumClass.Role.User,
                            Gender = model.Gender,
                            IsActive = true,
                            IsDeleted = false,
                            IsTeacher = false,
                            IsStudent = false,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                        };
                        _context.Add(user);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        model.ErrorMessage = "Lỗi khi tạo tài khoản";
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Lỗi khi tạo tài khoản";
                return View(model);
            }
            return View();
        }
    }
}
