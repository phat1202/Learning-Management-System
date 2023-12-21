using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Learning_Management_System.Repositories.AccountRepository;
using Learning_Management_System.Extensions;
using Learning_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Learning_Management_System.EndUserModels;

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
            HttpContext.Session.SetString("ReturnUrl", Request.Headers["Referer"].ToString());
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
                        new Claim("UserId", user.UserId),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, ((EnumClass.Role)user.Role).ToString())
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = objLoginModel.RememberLogin
                    });
                    var returnUrl = HttpContext.Session.GetString("ReturnUrl");
                    HttpContext.Session.Remove("ReturnUrl");
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect("/");
                    }
                    else
                    {
                        return Redirect(returnUrl);
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
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("ReturnUrl", Request.Headers["Referer"].ToString());
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            var returnUrl = HttpContext.Session.GetString("ReturnUrl");
            HttpContext.Session.Remove("ReturnUrl");
            if (string.IsNullOrEmpty(returnUrl))
            {
                return Redirect("/");
            }
            else
            {
                return Redirect(returnUrl);
            }
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
        public IActionResult UserProfile(string userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            return View(user);
        }
        [HttpPost]
        public IActionResult UserProfile(User userEditProfile)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userEditProfile.UserId);
            if (user == null)
            {
                //string errorMessage = "Thay đổi không thành công";
                //return Json(new { success = false, message = errorMessage });
                ModelState.AddModelError("EditError", "Thay đổi không thành công");
                return View(user);
            }
            //var newProfile = new User
            //{
            //    UserName = userEditProfile.UserName,
            //    DateOfBirth = userEditProfile.DateOfBirth,

            //};
            user.UserName = userEditProfile.UserName;
            user.DateOfBirth = userEditProfile.DateOfBirth;
            _context.SaveChanges();
            return View(user);
        }
        public IActionResult MyLearning(string userId)
        {
            var result = _context.Enrollments.Where(learnings => learnings.UserId == userId)
                .Include(c => c.course.Teacher)
                .Include(u => u.user)
                .ToList();

            return View(result);
        }
    }
}
