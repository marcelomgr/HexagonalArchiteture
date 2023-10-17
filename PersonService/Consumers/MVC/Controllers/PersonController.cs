using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
