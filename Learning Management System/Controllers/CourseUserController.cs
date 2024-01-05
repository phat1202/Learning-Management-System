using Learning_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learning_Management_System.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
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
        public IActionResult Checkout(int courseId)
        {
            TempData["CourseId"] = courseId;
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(string userId)
        {
            var courseId = (int)TempData["CourseId"];
            var course = _context.Courses.First(c => c.CourseId == courseId);
            var user = _context.Users.First(u => u.UserId == userId);
            var exist_enroll = _context.Enrollments.FirstOrDefault(en => en.UserId == user.UserId && en.CourseId == course.CourseId);
            if(exist_enroll != null)
            {
                ViewData["ErrorMessage"] = "You have registered this course.";
                TempData["CourseId"] = courseId;
                return View();
            }
            var NewEnrollments = new Enrollment
            {
                UserId = user.UserId,
                CourseId = course.CourseId,
                EnrollmentDate = DateTime.Now,
            };
            user.Role = 2;
            user.IsStudent = true;
            _context.Add(NewEnrollments);
            _context.SaveChanges();
            return RedirectToAction("PaymentSuccess");
        }
        public IActionResult PaymentSuccess()
        {
            return View();
        }
        public IActionResult AddToWishLish()
        {
            return View();
        }
    }
}
