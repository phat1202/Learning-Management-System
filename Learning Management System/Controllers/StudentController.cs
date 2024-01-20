using Learning_Management_System.Models;
using Microsoft.AspNetCore.Http;
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
                var comments = _context.CommentLessons.Include(u => u.user).Where(c => c.LessonId == itemLessonId).ToList();
                if (lesson != null)
                {
                    HttpContext.Session.SetInt32("LessonId", lesson.LessonId ?? 0);
                    //Html Code
                    foreach (var cmt in comments)
                    {
                        var HtmlCode = $@"<div class=""d-flex flex-start"">
                               <img class=""rounded-circle shadow-1-strong me-3""
                                    src=""{cmt.user.Avatar}"" alt=""avatar"" width=""50""
                                    height=""50"" />
                               <div class=""flex-grow-1 flex-shrink-1"">
                                   <div>
                                       <div class=""d-flex justify-content-between align-items-center"">
                                           <p class=""mb-1"">
                                               {cmt.user.UserName} <span class=""small"">- 2 hours ago</span>
                                           </p>
                                           <a href=""#!"" class=""reply-link""><i class=""fas fa-reply fa-xs""></i><span class=""small""> reply</span></a>
                                       </div>
                                       <p class=""small mb-0"">
                                           It is a long established fact that a reader will be distracted by
                                           the readable content of a page.
                                       </p>
                                   </div>
                               </div>
                           </div>";
                    }
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
        //AddComment
        public IActionResult AddComment(string comment)
        {
            var lessonId = HttpContext.Session.GetInt32("LessonId");
            var lesson = _context.Lessons.FirstOrDefault(l => l.LessonId == lessonId);
            if (lesson == null)
            {
                lessonId = 1;
            }
            var userId = HttpContext.User.Claims.First().Value;
            var user = _context.Users.First(u => u.UserId == userId);
            var newComment = new CommentLesson
            {
                UserId = user.UserId,
                StudentComment = comment,
                CommentedAt = DateTime.Now,
                LessonId = lessonId,
            };
            //_context.Add(newComment);
            //_context.SaveChanges();
            return Json(new { success = true, contentUrl = lesson.ContentUrl });
        }
    }
}
