using WebMVC2.Models;

namespace WebMVC2.ViewModels
{
    public class OrderDetailViewModel
    {
        public List<OrderInfo> orderInfos = new List<OrderInfo>();
        public List<ProductItemForOrder> productItemForOrders = new List<ProductItemForOrder>();
    }
}
