using Microsoft.AspNetCore.Mvc;

namespace WebMVC2.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
