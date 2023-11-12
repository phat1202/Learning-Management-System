using Microsoft.AspNetCore.Mvc;

namespace Learning_Management_System.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
