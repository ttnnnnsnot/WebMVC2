using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC2.Models;
using WebMVC2.Services;

namespace WebMVC2.Controllers
{
    public class DataListController : Controller
    {
        private readonly ApiService _apiService;

        public DataListController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            ResponseData data = new ResponseData()
            {
                ProcedureName = "ProductList",
                Parameters = new Dictionary<string, string>()
                {
                    { "ProductTypeID", "0" },
                    { "PageNum", "1" },
                }
            };

            ResultData resultData = await _apiService.CallApi(data);

            return View(resultData);
        }

        [Authorize(Roles = "Admin2")]
        public IActionResult Index2()
        {
            return View();
        }
    }
}
