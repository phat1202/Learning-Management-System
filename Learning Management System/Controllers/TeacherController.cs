using CloudinaryDotNet.Actions;
using Learning_Management_System.Extensions;
using Learning_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Learning_Management_System.Controllers
{
    [Authorize(Roles = "Teacher")]
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
        //Edit Course
        public IActionResult EditCourse(int courseId)
        {
            var courseEdit = _context.Courses.First(c => c.CourseId == courseId);
            var categories = _context.CategoryCourses.ToList();
            categories.Insert(0, new CategoryCourse()
            {
                CategoryName = "Select Category",
                CategoryId = -1
            });
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", -1);
            return View(courseEdit);
        }
        [HttpPost]
        public IActionResult EditCourse(Course course)
        {
            var course_Exist = _context.Courses.First(c => c.CourseId == course.CourseId);
            course_Exist.CourseTitle = course.CourseTitle;
            course_Exist.CourseDescription = course.CourseDescription;
            course_Exist.CategoryId = course.CategoryId;
            _context.SaveChanges();
            return RedirectToAction("MyClasses");
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
                ChapterNumber = chapter.ChapterNumber,
                TotalNumberOfLesson = 0,
            };
            _context.Add(newChapter);
            _context.SaveChanges();
            return RedirectToAction("LessonManagement", new { chapterId = newChapter.ChapterId});
        }
        //EditChapter
        public IActionResult EditChapter(int chapterId)
        {
            var chapterEdit = _context.Chapters.First(c => c.ChapterId == chapterId);
            return View(chapterEdit);
        }
        [HttpPost]
        public IActionResult EditChapter(Chapter chapter)
        {
            var chapter_Exist = _context.Chapters.First(c => c.ChapterId == chapter.ChapterId);
            chapter_Exist.ChapterTitle = chapter.ChapterTitle;
            chapter_Exist.ChapterNumber = chapter.ChapterNumber;
            _context.SaveChanges();
            return RedirectToAction("MyClasses");
        }
        //CreateNewLesson
        public IActionResult CreateNewLesson(int chapterId)
        {
            TempData["ChapterId"] = chapterId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewLesson(Lesson lesson, IFormFile Video)
        {
            var chapterId = (int)TempData["ChapterId"];
            var chapter = _context.Chapters.First(c => c.ChapterId == chapterId);
            if (chapter == null)
            {
                return View();
            }
            var uploadVideo = new FileUpLoading();
            var uploaded = uploadVideo.UploadVideo(Video);
            var newLesson = new Lesson
            {
                ChapterId = chapterId,
                LessonName = lesson.LessonName,
                LessonNumber = lesson.LessonNumber,
                ContentUrl = uploaded.Item1,
                TimeDuration = uploaded.Item2,
            };           
            await _context.AddAsync(newLesson);
            chapter.TotalNumberOfLesson++;
            await _context.SaveChangesAsync();
            return View();
        }
        //EditLesson
        public IActionResult EditLesson(int lessonId)
        {
            var chapterEdit = _context.Lessons.First(c => c.LessonId == lessonId);
            return View(chapterEdit);
        }
        [HttpPost]
        public IActionResult EditLesson(Lesson lesson, IFormFile? Video)
        {
            var uploadVideo = new FileUpLoading();
            var lesson_Exist = _context.Lessons.First(c => c.LessonId == lesson.LessonId);
            if (Video != null)
            {
                var uploaded = uploadVideo.UploadVideo(Video);
                lesson_Exist.ContentUrl = uploaded.Item1;
                lesson_Exist.TimeDuration = uploaded.Item2;
            }
            lesson_Exist.LessonName = lesson.LessonName;
            lesson_Exist.LessonNumber = lesson.LessonNumber;
            _context.SaveChanges();
            return RedirectToAction("MyClasses");
        }
        //Index
        public IActionResult CourseManagement(int courseId)
        {
            ViewData["CourseId"] = courseId;
            var course = _context.Chapters.Where(c => c.course.CourseId == courseId)
                                          .Include(co => co.course)
                                          .Include(u => u.course.Teacher)
                                           .OrderBy(c => c.ChapterNumber).ToList();
            return View(course);
        }
        public IActionResult LessonManagement(int chapterId)
        {
            ViewData["ChapterId"] = chapterId;
            var lessonList = _context.Lessons.Where(l => l.ChapterId == chapterId).Include(c => c.chapter)
                                                .OrderBy(l => l.LessonNumber).ToList();
            return View(lessonList);
        }
        public IActionResult MyClasses()
        {
            var userId = HttpContext.User.Claims.First().Value;
            var myClasses = _context.Courses.Where(c => c.TeacherId == userId).Include(u => u.Teacher).ToList();
            return View(myClasses);
        }

    }
}
