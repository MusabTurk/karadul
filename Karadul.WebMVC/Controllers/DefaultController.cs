using Microsoft.AspNetCore.Mvc;

namespace Karadul.WebMVC.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
