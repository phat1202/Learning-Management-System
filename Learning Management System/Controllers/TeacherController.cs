using Learning_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Learning_Management_System.Controllers
{
    [Authorize]
    public class TeacherController : Controller
    {
        private readonly LmsDbContext _context;
        public TeacherController(LmsDbContext context)
        {        
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BecomeTeacher()
        {
            return View();
        }
        [HttpPost]
        public IActionResult BecomeTeacher(string userId)
        {
            var user = _context.Users.First(u => u.UserId == userId);
            if(user == null)
            {
                return View();
            }
            user.IsTeacher = true;
            user.Role = 3;
            _context.SaveChanges();
            return View();
        }
        public IActionResult CreateNewCourse()
        {
            var categories = _context.CategoryCourses.ToList();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", -1) ;
            return View();
        }
        [HttpPost]
        public IActionResult CreateNewCourse(Course course)
        {
            var newCourse = new Course
            {
                CourseTitle = course.CourseTitle,
                CourseDescription = course.CourseDescription,
                CategoryId = course.CategoryId,
                TeacherId = course.TeacherId,
            };
            _context.Add(newCourse);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AddChapter()
        {
            return View();
        }
       
    }
}
