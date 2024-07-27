using Microsoft.AspNetCore.Mvc;
using WebMVC2.Filter;
using WebMVC2.Interface;
using WebMVC2.Models;

namespace WebMVC2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
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
        [ModelStateFilter]
        public async Task<IActionResult> Index(UserInfo userInfo)
        {
            // system wait is 3 seconds
            await Task.Delay(2000);

            ResultMessage result = await _userService.GetUserAddAsync(userInfo);

            TempData["ErrorMessage"] = result.MsgText;

            if (result.Msg)
            {
                // 清空所有模型狀態
                ModelState.Clear();
                // 或者，清空特定欄位的模型狀態
                //ModelState.Remove("UserInfo");
            }

            return View();
        }

        // 可用於對資料庫進行驗證用戶名
        public async Task<IActionResult> CheckUserID(string UserID)
        {
            var result = await _userService.GetUserAsync(UserID);
            if (result.Msg)
            {
                return Json("帳號已經被註冊");
            }
            return Json(true);
        }
    }
}
