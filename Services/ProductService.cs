using WebMVC2.Global;
using WebMVC2.Interface;
using WebMVC2.Models;

namespace WebMVC2.Services
{
    public class ProductService : IProductService
    {
        private readonly IApiService _apiService;
        public ProductService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ProductItem> GetProductItemAsync(int Id)
        {
            ResponseData data = new ResponseData()
            {
                ProcedureName = "ProductDetail",
                Parameters = new Dictionary<string, string>()
                {
                    { "Id", Id.ToString() }
                }
            };

            ResultData resultData = await _apiService.CallApi(data);

            List<ProductItem> products = JsonSerializerService.Deserialize<List<ProductItem>>(resultData.Data[0].GetRawText());

            return (products.First());
        }

        public async Task<(List<ProductItem> Products, PageNum PageInfo)> GetProductsAsync(int productTypeId, int currentPage, int itemSize)
        {
            ResponseData data = new ResponseData()
            {
                ProcedureName = "ProductList",
                Parameters = new Dictionary<string, string>()
                {
                    { "ProductTypeID", productTypeId.ToString() },
                    { "CurrentPage", currentPage.ToString() },
                    { "ItemSize", itemSize.ToString() }
                }
            };

            ResultData resultData = await _apiService.CallApi(data);

            List<ProductItem> products = JsonSerializerService.Deserialize<List<ProductItem>>(resultData.Data[0].GetRawText());
            List<PageNum> pageNums = JsonSerializerService.Deserialize<List<PageNum>>(resultData.Data[1].GetRawText());

            PageNum pageInfo = pageNums[0];
            pageInfo.CurrentPage = currentPage;
            pageInfo.UrlParam = "?productTypeId=" + productTypeId.ToString();
            pageInfo.UrlActionName = "Index";
            pageInfo.UrlControllerName = "DataList";
            pageInfo.ItemSize = itemSize;

            return (products, pageInfo);
        }
    }
}
