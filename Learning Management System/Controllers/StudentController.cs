using Learning_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learning_Management_System.Controllers
{
    public class StudentController : Controller
    {
        private readonly LmsDbContext _context;
        public StudentController(LmsDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MyLearning(string userId)
        {
            var result = _context.Enrollments
                .Where(learnings => learnings.UserId == userId)
                .Include(c => c.course.Teacher)
                .Include(c => c.course)
                .Include(u => u.user)
                .ToList();

            return View(result);
        }
        public IActionResult StudentCourse(int courseId)
        {
            ViewData["CourseId"] = courseId;
            var course = _context.Chapters.Where(c => c.course.CourseId == courseId)
                                          .Include(co => co.course)
                                          .Include(u => u.course.Teacher)
                                          .OrderBy(c => c.ChapterNumber).ToList();
            return View(course);
        }
        public IActionResult LessonDetail(int chapterId)
        {
            var lessons = _context.Lessons.Where(l => l.ChapterId == chapterId)
                                            .Include(c => c.chapter.course)
                                            .Include(c => c.chapter)
                                             .OrderBy(l => l.LessonNumber).ToList();
            return View(lessons);
        }
        [HttpPost]
        public IActionResult GetLessonId(int itemLessonId)
        {
            try
            {
                var lesson = _context.Lessons.FirstOrDefault(l => l.LessonId == itemLessonId);

                if (lesson != null)
                {
                    return Json(new { success = true, contentUrl = lesson.ContentUrl });
                }
                else
                {
                    return Json(new { success = false, errorMessage = "Lesson not found." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }
    }
}
