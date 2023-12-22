using Learning_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learning_Management_System.Controllers
{
    public class CourseUserController : Controller
    {
        private readonly LmsDbContext _context;
        public CourseUserController(LmsDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CourseDetail(int courseId)
        {
            if (courseId == 0)
            {
                return View();
            }
            var result = _context.Courses.Include(t => t.Teacher).Include(c => c.Category).First(c => c.CourseId == courseId);
            if(result == null)
            {
                return View();
            }
            return View(result);
        }
    }
}
