using WebMVC2.Models;

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
    }
}
