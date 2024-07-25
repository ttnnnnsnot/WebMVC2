using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC2.Interface;

namespace WebMVC2.Controllers
{
    public class DataListController : Controller
    {
        private readonly IProductService _productService;

        public DataListController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id, int productTypeId = 0)
        {
            int currentPage = id ?? 1;
            int itemSize = 12;

            var (products, pageInfo) = await _productService.GetProductsAsync(productTypeId, currentPage, itemSize);

            ViewData["Products"] = products;
            ViewData["PageNums"] = pageInfo;
            ViewData["productTypeId"] = productTypeId.ToString();

            return View();
        }

        [Authorize(Roles = "Admin2")]
        public IActionResult Index2()
        {
            return View();
        }
    }
}
