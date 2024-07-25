using WebMVC2.Models;

namespace WebMVC2.Interface
{
    public interface IProductService
    {
        Task<(List<ProductItem> Products, PageNum PageInfo)> GetProductsAsync(int productTypeId, int currentPage, int itemSize);
    }
}
