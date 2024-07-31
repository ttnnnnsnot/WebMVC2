using WebMVC2.Models;

namespace WebMVC2.ViewModels
{
    public class DataListViewData : BaseViewModel
    {
        public List<ProductItem> productItems { get; set; } = new List<ProductItem>();
        public PageNum pageNum { get; set; } = new PageNum();
        public int productTypeId { get; set; }
        public ProductItem productItem { get; set; } = new ProductItem();
    }
}
