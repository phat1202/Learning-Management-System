using Learning_Management_System.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Learning_Management_System.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly LmsDbContext _context;
        public CartController(LmsDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CartIndex(string userId)
        {
            var user = _context.Users.First(u => u.UserId == userId);
            var cartItems = _context.CartItems.Where(i => i.CartId == user.CartId)
                                                .Include(c => c.course).Include(u => u.course.Teacher)
                                                .ToList();
            return View(cartItems);
        }
        public async Task<IActionResult> AddItemToCart(int courseId, string userId)
        {
            var user = _context.Users.First(u => u.UserId == userId);
            var course = _context.Courses.FirstOrDefault(c => c.CourseId == courseId);
            if (course == null || user == null)
            {
                return View();
            }
            var item_Added = _context.CartItems.FirstOrDefault(i => i.CourseId == course.CourseId);
            if (item_Added == null)
            {
                var newItem = new CartItem()
                {
                    CourseId = course.CourseId,
                    Quantity = 1,
                    CartId = user.CartId,
                };
                _context.Add(newItem);
            }
            else
            {
                item_Added.Quantity++;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("CartIndex");
        }
    }
}
