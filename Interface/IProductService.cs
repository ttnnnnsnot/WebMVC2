using WebMVC2.Models;

namespace WebMVC2.Interface
{
    public interface IProductService
    {
        public Task<ProductItem> GetProductItemAsync(int Id);
        Task<(List<ProductItem> Products, PageNum PageInfo)> GetProductsAsync(int productTypeId, int currentPage, int itemSize);
    }
}
