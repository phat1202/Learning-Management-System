using Learning_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Learning_Management_System.Controllers
{
    public class CourseController : Controller
    {
        private readonly LmsDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public CourseController(LmsDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var result = _context.Courses.Include(n => n.Teacher)
                                         .Include(c => c.Category)
                                         .ToList();
            return View(result);
        }
        [Authorize]
        public async Task<IActionResult> CreateCourse(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user.IsTeacher == false)
            {
                ModelState.AddModelError("", "Only teacher");
                return RedirectToAction("Privacy", "Home");
            }
            var categories = _context.CategoryCourses.ToList();
            categories.Insert(0, new CategoryCourse()
            {
                CategoryName = "Select Category",
                CategoryId = -1
            });
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", -1);
            

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(Course course) //Only Teacher
        {
            var newCourse = new Course
            {
                CourseTitle = course.CourseTitle,
                CourseDescription = course.CourseDescription,
                Teacher = course.Teacher,
                CategoryId = course.CategoryId,
                Id = course.Id,
                DateStarted = course.DateStarted,
                DateCompleted = null,
            };
            await _context.AddAsync(newCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        } 

    }
}