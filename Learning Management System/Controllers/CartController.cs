using Learning_Management_System.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Learning_Management_System.EndUserModels;

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
        public IActionResult CartIndex()
        {
            var userIdFromSession = HttpContext.User.Claims.First().Value;
            var user = _context.Users.First(u => u.UserId == userIdFromSession);
            var cartItems = _context.CartItems.Where(i => i.CartId == user.CartId)
                                                .Include(c => c.course).Include(u => u.course.Teacher).Include(c => c.cart)
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
            var item_Added = _context.CartItems.FirstOrDefault(i => i.CourseId == course.CourseId && i.CartId == user.CartId);
            if (item_Added == null)
            {
                var newItem = new CartItem()
                {
                    CourseId = course.CourseId,
                    Quantity = 1,
                    CartId = user.CartId,
                    ItemSelected = true,
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
        public IActionResult RemoveCartItem(int cartItem)
        {
            var item = _context.CartItems.First(i => i.CartItemId == cartItem);
            if (item.Quantity > 1)
            {
                item.Quantity--;
            }
            else
            {
                _context.Remove(item);
            }
            _context.SaveChanges();
            return RedirectToAction("CartIndex");
        }
        public IActionResult SelectItem(int itemId)
        {
            var result = _context.CartItems.FirstOrDefault(i => i.CartItemId == itemId);
            if(result.ItemSelected == true)
            {
                result.ItemSelected = false;
            }
            else
            {
                result.ItemSelected = true;
            }
            _context.SaveChanges();
            return RedirectToAction("CartIndex");
        }
    }
}
