using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebMVC2.Models;

namespace WebMVC2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
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
        public IActionResult Index(UserInfo model)
        {
            if (ModelState.IsValid)
            {
                // 表單數據通過驗證
                // 執行相應的操作，例如保存到數據庫
                return Ok("Success");
            }
            else
            {
                // 表單數據未通過驗證
                // 返回帶有錯誤信息的視圖，讓用戶重新填寫表單
                return View(model);
            }
        }

        // 可用於對資料庫進行驗證用戶名
        public IActionResult CheckUserID(string UserID)
        {
            if (UserID == "123")
            {
                return Json("帳號已經被註冊");
            }
            return Json(true);
        }
    }
}
