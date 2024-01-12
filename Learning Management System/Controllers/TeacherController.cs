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
                TotalNumberOfLesson = 0,
            };
            _context.Add(newChapter);
            _context.SaveChanges();
            return View();
        }
        public IActionResult CreateNewLesson(int chapterId)
        {
            TempData["ChapterId"] = chapterId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewLesson(Lesson lesson, IFormFile Video)
        {
            var uploadVideo = new FileUpLoading();
            var uploaded = uploadVideo.UploadVideo(Video);
            var chapterId = (int)TempData["ChapterId"];
            var chapter = _context.Chapters.First(c => c.ChapterId == chapterId);
            if (chapter == null)
            {
                return View();
            }
            var newLesson = new Lesson
            {
                ChapterId = chapterId,
                LessonName = lesson.LessonName,
                ContentUrl = uploaded
            };           
            await _context.AddAsync(newLesson);
            chapter.TotalNumberOfLesson++;
            await _context.SaveChangesAsync();
            return View();
        }
        public IActionResult CourseManagement(int courseId)
        {
            ViewData["CourseId"] = courseId;
            var course = _context.Chapters.Where(c => c.course.CourseId == courseId)
                                          .Include(co => co.course)
                                          .Include(u => u.course.Teacher).ToList();
            return View(course);
        }
        public IActionResult LessonManagement(int chapterId)
        {
            ViewData["ChapterId"] = chapterId;
            var lessonList = _context.Lessons.Where(l => l.ChapterId == chapterId).Include(c => c.chapter).ToList();
            return View(lessonList);
        }
        public IActionResult AddNewChapter()
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
