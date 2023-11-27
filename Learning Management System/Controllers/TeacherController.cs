using Learning_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        //Index trong lesson
        public IActionResult CourseManagement(int courseId)
        {
            var chapters = _context.Chapters.Where(c => c.CourseId == courseId).ToList();

            return View(chapters);
        }
        public IActionResult CreateChapter(int courseId)
        {
            var course = _context.Courses.ToList();
            course.Insert(0, new Course()
            {
                CourseTitle = "Select Your Course",
                CourseId = -1
            });
            ViewData["CourseId"] = new SelectList(course, "CourseId", "CourseTitle", -1);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateChapter(Chapter chapter)
        {
            var newChapter = new Chapter
            {
                ChapterNumber = chapter.ChapterNumber,
                ChapterTitle = chapter.ChapterTitle,
                CourseId = chapter.CourseId,
                TotalNumberOfLesson = chapter.TotalNumberOfLesson,
            };
            await _context.AddAsync(newChapter);
            await _context.SaveChangesAsync();
            return RedirectToAction("CourseManagement");
        }
    }
}
