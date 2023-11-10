using Learning_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learning_Management_System.Controllers
{
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
            return View();
        }
        public async Task<IActionResult> MyCourse(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var myCourses = await _context.Courses.Where(course => course.Id == userId)
                                                  .ToListAsync();

            return View(myCourses);
        }
    }
}