using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebMVC2.Models;

namespace WebMVC2.Controllers
{
    
    public class LoginController : Controller
    {
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
        public async Task<IActionResult> Index(Login login,string? returnUrl)
        {
            
            if (login.UserName != "123" && login.Password != "123")
            {
                return LocalRedirect("/");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "123"),
                new Claim(ClaimTypes.Name, "test@test.com"),
                new Claim("FullName", login.UserName),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                IssuedUtc = DateTimeOffset.UtcNow,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

            return LocalRedirect(returnUrl ?? "/");
            
        }
    }
}
