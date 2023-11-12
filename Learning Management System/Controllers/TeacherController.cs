using Learning_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learning_Management_System.Controllers
{
    public class TeacherController : Controller
    {
        private readonly LmsDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public TeacherController(LmsDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> MyClass(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var myCourses = await _context.Courses.Where(course => course.Id == userId)
                                                  .Include(category => category.Category)
                                                  .ToListAsync();
            return View(myCourses);
        }


    }
}
