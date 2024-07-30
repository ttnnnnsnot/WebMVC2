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
    }
}
