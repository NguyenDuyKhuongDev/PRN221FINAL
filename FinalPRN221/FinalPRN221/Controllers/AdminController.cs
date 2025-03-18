using Microsoft.AspNetCore.Mvc;

namespace FinalPRN221.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}