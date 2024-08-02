using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC2.Interface;
using WebMVC2.Models;
using WebMVC2.ViewModels;

namespace WebMVC2.Controllers
{
    public class DataListController : BaseController
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

            DataListViewModel dataListViewData = new DataListViewModel();
            dataListViewData.productItems = products;
            dataListViewData.pageNum = pageInfo;
            dataListViewData.productTypeId = productTypeId;

            return View(dataListViewData);
        }

        [HttpGet]
        public async Task<IActionResult> AddShopCar(int Id, int Num)
        {
            return Json(await ShopCarService.Add(Id, Num));
        }

        [HttpGet]
        public IActionResult UpdShopCar(int Id, int Num)
        {
            return Json(ShopCarService.Upd(Id, Num));
        }

        [HttpGet]
        public IActionResult GetShopCarSum()
        {
            return Json(ShopCarService.Get());
        }

        [HttpGet]
        public IActionResult GetShopCar()
        {
            return Json(ShopCarService.GetShopCar());
        }

        [HttpGet]
        public IActionResult DelShopCar(int Id)
        {
            return Json(ShopCarService.Delete(Id));
        }

        [HttpGet]
        public async Task<IActionResult> GetProductItem(int Id)
        {
            ProductItem productItem = await _productService.GetProductItemAsync(Id);
            return PartialView("_ProductDetail", productItem);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index2()
        {
            return View(ShopCarService.GetShopCar());
        }

        [Authorize(Roles = "Admin2")]
        public IActionResult Index3()
        {
            return View(ShopCarService.GetShopCar());
        }
    }
}
