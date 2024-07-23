using Microsoft.AspNetCore.Mvc;

namespace WebMVC2.Controllers
{
    public class DataListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
