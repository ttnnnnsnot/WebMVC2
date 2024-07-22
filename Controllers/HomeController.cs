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
                // ���ƾڳq�L����
                // ����������ާ@�A�Ҧp�O�s��ƾڮw
                return Ok("Success");
            }
            else
            {
                // ���ƾڥ��q�L����
                // ��^�a�����~�H�������ϡA���Τ᭫�s��g���
                return View(model);
            }
        }

        // �i�Ω���Ʈw�i�����ҥΤ�W
        public IActionResult CheckUserID(string UserID)
        {
            if (UserID == "123")
            {
                return Json("�b���w�g�Q���U");
            }
            return Json(true);
        }
    }
}
