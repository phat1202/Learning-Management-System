using CloudinaryDotNet;
using Learning_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            if (result == null)
            {
                return View();
            }
            return View(result);
        }
        public IActionResult Checkout(int courseId)
        {
            var userId = HttpContext.User.Claims.First().Value;
            var user = _context.Users.First(u => u.UserId == userId);
            if (courseId != 0)
            {
                //var listCourseIds = new List<int> { 0, courseId };
                //TempData["CheckoutCourseIds"] = Newtonsoft.Json.JsonConvert.SerializeObject(listCourseIds);
                TempData["CourseId"] = courseId;
                var courseItem = _context.Courses.First(c => c.CourseId == courseId);
                ViewData["ItemTitle"] = courseItem.CourseTitle;
                ViewData["ItemPrice"] = courseItem.Price;
                return View();
            }

            var items = _context.CartItems.Where(e => e.cart.CartId == user.CartId && e.ItemSelected == true)
                                            .Include(c => c.cart)
                                            .Include(c => c.course)
                                            .ToList();
            if (items.Count() == 0)
            {
                return RedirectToAction("CartIndex", "Cart");
            }
            var cartItemIds = items.Select(item => item.CartItemId).ToList();
            TempData["CheckoutCartIds"] = Newtonsoft.Json.JsonConvert.SerializeObject(cartItemIds);
            return View(items);
        }
        [HttpPost]
        public IActionResult Checkout(string userId)
        {
            var user = _context.Users.First(u => u.UserId == userId);
            if (TempData["CourseId"] is int)
            {
                var courseId = (int)TempData["CourseId"];
                var course = _context.Courses.First(c => c.CourseId == courseId);
                var exist_enroll = _context.Enrollments.FirstOrDefault(en => en.UserId == user.UserId && en.CourseId == course.CourseId);
                if (exist_enroll != null)
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
                _context.Add(NewEnrollments);
                user.Role = 2;
                user.IsStudent = true;
                _context.SaveChanges();
                TempData.Clear();
                return RedirectToAction("PaymentSuccess");
            }
            else
            {
                var listError = new List<string>();
                var cartItemIdsJson = TempData["CheckoutCartIds"] as string;
                var cartItemIds = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(cartItemIdsJson);
                foreach (int id in cartItemIds)
                {
                    var cartItem = _context.CartItems.Where(i => i.CartItemId == id).Include(c => c.course).First();
                    var exist_enroll = _context.Enrollments.FirstOrDefault(en => en.UserId == user.UserId && en.CourseId == cartItem.course.CourseId);
                    if (exist_enroll == null)
                    {
                        var NewEnrollments = new Enrollment
                        {
                            UserId = user.UserId,
                            CourseId = cartItem.CourseId,
                            EnrollmentDate = DateTime.Now,
                        };
                        _context.Add(NewEnrollments);
                        _context.Remove(cartItem);
                    }
                    else
                    {
                        string errorMess = $"{exist_enroll.course.CourseTitle}";
                        listError.Add(errorMess);
                        
                    }
                }
                user.Role = 2;
                user.IsStudent = true;
                _context.SaveChanges();
                TempData.Clear();
                return RedirectToAction("PaymentSuccess", new { listError = listError });
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
        public IActionResult PaymentSuccess(List<string>? listError)
        {
            return View(listError);
        }
        public IActionResult AddToWishList()
        {
            return View();
        }
    }
}
