using Microsoft.AspNetCore.Mvc;
using WebMVC2.Interface;
using WebMVC2.Models;

namespace WebMVC2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [Route("/Home/Index")]
        public async Task<IActionResult> Index(UserInfo userInfo)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("123213");
                TempData["CloseLoading"] = true;
                return View();
            }

            ResultMessage result = await _userService.GetUserAddAsync(userInfo);

            TempData["ErrorMessage"] = result.MsgText;
            return View();
        }

        // �i�Ω���Ʈw�i�����ҥΤ�W
        public async Task<IActionResult> CheckUserID(string UserID)
        {
            var result = await _userService.GetUserAsync(UserID);
            if (result.Msg)
            {
                return Json("�b���w�g�Q���U");
            }
            return Json(true);
        }
    }
}
