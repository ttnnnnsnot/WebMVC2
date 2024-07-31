using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebMVC2.Filter;
using WebMVC2.Interface;
using WebMVC2.Models;

namespace WebMVC2.Controllers
{    
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            ViewData["ReturnUrl"] = HttpContext.Request.Query["ReturnUrl"].ToString();
            return View();
        }

        public IActionResult AccessDenied()
        {
            TempData["ErrorMessage"] = "您沒有權限存取此頁面";
            return LocalRedirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut(string? returnUrl)
        {
            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return LocalRedirect(returnUrl ?? "/");
        }

        [HttpPost]
        [Route("/Login/Index")]
        [ModelStateFilter]
        public async Task<IActionResult> Index(UserLogin userLogin, string? returnUrl)
        {
            bool res = await _userService.UserLoginAsync(userLogin);

            if (!res)
            {
                TempData["ErrorMessage"] = "帳號密碼有誤";
                ViewData["ReturnUrl"] = returnUrl;
                return View();
            }

            return LocalRedirect(returnUrl ?? "/");
        }


        [HttpGet]
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

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("/Login/Register")]
        [ModelStateFilter]
        public async Task<IActionResult> Register(UserInfo userInfo)
        {
            await Task.Delay(1000);

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
    }
}
