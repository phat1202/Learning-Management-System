using Learning_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Learning_Management_System.Extensions;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Learning_Management_System.Controllers
{
    [Authorize]
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
            var userId = User.Claims.First().Value;
            var enrolled = _context.Enrollments.Any(e => e.UserId == User.Claims.First().Value && e.CourseId == courseId);
            ViewData["CourseId"] = courseId;
            if (!enrolled)
            {
                return View();
            }
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
                var commentSection = new List<string>();

                var lesson = _context.Lessons.FirstOrDefault(l => l.LessonId == itemLessonId);
                var comments = _context.CommentLessons.Include(u => u.user).Where(c => c.LessonId == itemLessonId).ToList();
                if (lesson != null)
                {
                    HttpContext.Session.SetInt32("LessonId", lesson.LessonId ?? 0);
                    //Html Code
                    foreach (var cmt in comments)
                    {
                        int i = 1;

                        var commentReply = new List<string>();
                        var Replies = _context.RepliesComments.Include(u => u.user).Where(c => c.CommentId == cmt.CommentId);
                        foreach (var reply in Replies.OrderBy(t => t.CommentedAt))
                        {
                            var htmlReplyCode = $@"<div class=""d-flex flex-start mt-4"">
                                            <a class=""me-3"" href=""#"">
                                                <img class=""rounded-circle shadow-1-strong""
                                                     src=""{reply.user.Avatar}"" alt=""avatar""
                                                     width=""50"" height=""50"" />
                                            </a>
                                            <div class=""flex-grow-1 flex-shrink-1"">
                                                <div>
                                                    <div class=""d-flex justify-content-between align-items-center"">
                                                        <p class=""mb-1"">
                                                            {reply.user.UserName} <span class=""small"">{Extensions.Extensions.GetRelativeTime(reply.CommentedAt)}</span>
                                                        </p>
                                                    </div>
                                                    <p class=""small mb-0"">
                                                        {reply.CommentReply}
                                                    </p>
                                                </div>
                                            </div>
                                        </div>

                                        <hr/>";
                            commentReply.Add(htmlReplyCode);

                        }

                        //Comment 

                        var HtmlCode = $@"<div class=""d-flex flex-start"">
                               <img class=""rounded-circle shadow-1-strong me-3""
                                    src=""{cmt.user.Avatar}"" alt=""avatar"" width=""50""
                                    height=""50"" />
                               <div class=""flex-grow-1 flex-shrink-1"" id=""CommentParent"">
                                   <div>
                                       <div class=""d-flex justify-content-between align-items-center"">
                                           <p class=""mb-1"">
                                               {cmt.user.UserName} <span class=""small"">{Extensions.Extensions.GetRelativeTime(cmt.CommentedAt)}</span>
                                           </p>
                                           <a href=""#!"" class=""link-reply-comment"" id=""cmt-{i}""><i class=""fas fa-reply fa-xs""></i><span class=""small""> reply</span></a>
                                       </div>
                                       <p class=""small mb-0"">
                                           {cmt.StudentComment}
                                       </p>
                                   </div>
                                    {string.Join("", commentReply)}
                                    <form id=""reply-form-cmt-{i}"" class=""mt-3 reply-form"" style=""display: none;"">
                                           <div class=""input-group mb-3"">
                                                <textarea type=""text"" class=""form-control"" id=""newComment"" placeholder=""Reply this comment""></textarea>
                                            </div>
                                            <a href=""#"" class=""btn btn-sm btn-primary"">Submit</a>
                                    </form>
                               </div>
                            </div>

                            <hr/>
                            ";
                        commentSection.Add(HtmlCode);

                        //else
                        //{
                        //    var HtmlCode = $@"<div class=""d-flex flex-start"">
                        //       <img class=""rounded-circle shadow-1-strong me-3""
                        //            src=""{cmt.user.Avatar}"" alt=""avatar"" width=""50""
                        //            height=""50"" />
                        //       <div class=""flex-grow-1 flex-shrink-1"" id=""CommentParent"">
                        //           <div>
                        //               <div class=""d-flex justify-content-between align-items-center"">
                        //                   <p class=""mb-1"">
                        //                       {cmt.user.UserName} <span class=""small"">{Extensions.Extensions.GetRelativeTime(cmt.CommentedAt)}</span>
                        //                   </p>
                        //                   <a href=""#!"" class=""reply-link""><i class=""fas fa-reply fa-xs""></i><span class=""small""> reply</span></a>
                        //               </div>
                        //               <p class=""small mb-0"">
                        //                   {cmt.StudentComment}
                        //               </p>
                        //           </div>

                        //       </div>
                        //    </div>


                        //    ";
                        //    commentSection.Add(HtmlCode);
                        //}



                        i++;
                    }
                    //ViewBag.DynamicHtml = string.Join("", commentSection);
                    return Json(new { success = true, contentUrl = lesson.ContentUrl, listCmtSection = commentSection, durationTime =lesson.TimeDuration});
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
        [HttpPost]
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
            //return RedirectToAction("GetLessonId", new { itemLessonId = lessonId });
            //_context.Add(newComment);
            //_context.SaveChanges();
            return Json(new { success = true, contentUrl = lesson.ContentUrl, chapterId = lesson.ChapterId, lessonId = lesson.LessonId });
        }
        public IActionResult ProgressTracking(int itemId)
        {
            var userId = HttpContext.User.Claims.First().Value;
            var user = _context.Users.First(u => u.UserId == userId);
            var lesson = _context.Lessons.FirstOrDefault(l => l.LessonId == itemId);
            var ProgressExist = _context.StudentProgresses.FirstOrDefault(p => p.LessonId == itemId && p.UserId == userId);
            if(ProgressExist != null)
            {
                ProgressExist.LastAccessed = DateTime.Now;
                ProgressExist.CompletionStatus = "Revise";
            }
            var newProgress = new StudentProgress
            {
                UserId = userId,
                LessonId = lesson.LessonId,
                CompletionStatus = "OK",
                LastAccessed = DateTime.Now,
            };
            return View("LessonDetail");
        }
    }
}
