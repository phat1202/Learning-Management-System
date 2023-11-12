using Learning_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learning_Management_System.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly LmsDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(LmsDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var list_user = _userManager.Users.ToList();
            return View(list_user);
        }
        public IActionResult RegisterTeacher()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterTeacher(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.IsTeacher = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult RegisterCourse(int courseId)
        {                       //By Student

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterCourse(Student user)
        {
            var student = new Student
            {
                Id = user.Id,
                CourseId = user.CourseId,
            };
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}