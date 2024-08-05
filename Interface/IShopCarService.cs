using WebMVC2.Models;
using WebMVC2.ViewModels;

namespace WebMVC2.Interface
{
    public interface IShopCarService
    {
        public int Get();
        public Task<int> Add(int Id, int Num);
        public ShopCar Delete(int Id);
        public ShopCar GetShopCar();
        public ShopCar Upd(int Id, int Num);
        public Task<ResultMessage> ShoppingDone();
        public Task<OrderDetailViewModel> UserOrderDetail(int StatusType = 1);
    }
}
