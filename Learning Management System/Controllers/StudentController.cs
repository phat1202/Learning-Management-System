using Learning_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
