using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebMVC2.Interface;
using WebMVC2.Models;
using WebMVC2.ViewModels;

namespace WebMVC2.Controllers
{
    public class DataListController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IShopCarService _shopCarService;

        public DataListController(IProductService productService, IShopCarService shopCarService)
        {
            _productService = productService;
            _shopCarService = shopCarService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id, int productTypeId = 0)
        {
            int currentPage = id ?? 1;
            int itemSize = 12;

            var (products, pageInfo) = await _productService.GetProductsAsync(productTypeId, currentPage, itemSize);

            DataListViewData dataListViewData = new DataListViewData();
            dataListViewData.productItems = products;
            dataListViewData.pageNum = pageInfo;
            dataListViewData.productTypeId = productTypeId;

            return View(dataListViewData);
        }

        [HttpGet]
        public async Task<IActionResult> AddShopCar(int Id, int Num)
        {
            return Json(await _shopCarService.Add(Id, Num));
        }

        [HttpGet]
        public IActionResult GetShopCarSum()
        {
            return Json(_shopCarService.Get());
        }

        [HttpGet]
        public async Task<IActionResult> GetProductItem(int Id)
        {
            ProductItem productItem = await _productService.GetProductItemAsync(Id);
            return PartialView("_ProductDetail", productItem);
        }

        [Authorize(Roles = "Admin2")]
        public IActionResult Index2()
        {
            return View();
        }
    }
}
