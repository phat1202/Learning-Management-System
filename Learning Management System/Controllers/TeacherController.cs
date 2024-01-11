using Learning_Management_System.Extensions;
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
                return RedirectToAction("Login", "User");
            }
            if(user.Role == 3)
            {
                ViewData["Message"] = "You did register";
                return RedirectToAction("MyClasses", "Teacher");
            }
            user.IsTeacher = true;
            user.Role = 3;
            _context.SaveChanges();
            ViewData["Message"] = "Successful";
            return RedirectToAction("MyClasses", "Teacher");
        }
        public IActionResult CreateNewCourse()
        {
            var categories = _context.CategoryCourses.ToList();
            categories.Insert(0, new CategoryCourse()
            {
                CategoryName = "Select Category",
                CategoryId = -1
            });
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", -1) ;
            return View();
        }
        [HttpPost]
        public IActionResult CreateNewCourse(Course course, IFormFile imageCover)
        {
            var uploadImage = new FileUpLoading();
            if (course.CategoryId < 1 || course.CategoryId == null)
            {
                ModelState.AddModelError("SelectCategory", "Please select the Category");
                return View(course);
            }
            var newCourse = new Course
            {
                CourseTitle = course.CourseTitle,
                CourseDescription = course.CourseDescription,
                Price = course.Price,
                CategoryId = course.CategoryId,
                TeacherId = course.TeacherId,
                ImageCover = uploadImage.UploadImage(imageCover),
            };
            _context.Add(newCourse); 
            _context.SaveChanges();
            return RedirectToAction("CreateNewChapter", new { courseId = newCourse.CourseId});
        }
        public IActionResult CreateNewChapter(int courseId)
        {
            TempData["CourseId"] = courseId;
            return View();
        }
        [HttpPost]
        public IActionResult CreateNewChapter(Chapter chapter)
        {
            var newChapter = new Chapter
            {
                CourseId = (int)TempData["CourseId"],
                ChapterTitle = chapter.ChapterTitle,
                TotalNumberOfLesson = chapter.TotalNumberOfLesson,
            };
            _context.Add(newChapter);
            _context.SaveChanges();
            return View();
        }
        public IActionResult CreateNewLesson()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNewLesson(Lesson lesson, IFormFile Video)
        {
            var uploadVideo = new FileUpLoading();
            var uploaded = uploadVideo.UploadVideo(Video);
            var newLesson = new Lesson
            {
                ChapterId = lesson.ChapterId,
                LessonName = lesson.LessonName,
            };
            _context.Add(newLesson);
            _context.SaveChanges();
            return View();
        }
        public IActionResult CourseManagement(int courseId)
        {
            var course = _context.Chapters.Where(c => c.course.CourseId == courseId)
                                          .Include(co => co.course)
                                          .Include(u => u.course.Teacher).ToList();
            return View(course);
        }
        public IActionResult AddChapter()
        {
            return View();
        }
        public IActionResult MyClasses(string userId)
        {
            var myClasses = _context.Courses.Where(c => c.TeacherId == userId).Include(u => u.Teacher).ToList();
            return View(myClasses);
        }
    }
}
