using Learning_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Learning_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LmsDbContext _context;

        public HomeController(ILogger<HomeController> logger, LmsDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        //EndUser Controller
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ViewCategoryCourseIndex()
        {
            var result = _context.Courses.Include(n => n.Teacher)
                                         .Include(c => c.Category)
                                         .ToList();
            return View(result);
        }
        public IActionResult ViewTeacherIndex()
        {
            var result = _context.Courses.Where(course => course.Teacher.IsTeacher == true)
                                        .Include(u => u.Teacher)
                                        .Include(c => c.Category)
                                        .ToList();
            return View(result);
        }
        public IActionResult ViewCourseIndex(int categoryCourse)
        {
            var result = _context.Courses.Where(c => c.Category.CategoryId == categoryCourse).Include(u => u.Teacher).ToList();
            return View(result);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}