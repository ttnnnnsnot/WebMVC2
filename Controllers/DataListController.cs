using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC2.Controllers
{
    public class DataListController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin2")]
        public IActionResult Index2()
        {
            return View();
        }
    }
}
