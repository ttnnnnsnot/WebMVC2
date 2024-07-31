using WebMVC2.Global;
using WebMVC2.Interface;
using WebMVC2.Models;

namespace WebMVC2.Services
{
    public class ShopCarService : IShopCarService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApiService _apiService;

        public ShopCarService(IHttpContextAccessor httpContextAccessor, IApiService apiService)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiService = apiService;
        }

        private void Save(ShopCar shopCar)
        {
            var serializedData = JsonSerializerService.Serialize(shopCar);
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(30),
                HttpOnly = true,
                IsEssential = true
            };

            _httpContextAccessor?.HttpContext?.Response.Cookies.Append(nameof(CookieName.ShopCar), serializedData, cookieOptions);
        }
        private ShopCar Load()
        {
            string? serializedData = string.Empty;
            _httpContextAccessor?.HttpContext?.Request.Cookies.TryGetValue(nameof(CookieName.ShopCar), out serializedData);
            if (string.IsNullOrEmpty(serializedData))
            {
                return new ShopCar();
            }
            else
            {
                return JsonSerializerService.Deserialize<ShopCar>(serializedData);
            }
        }

        public async Task<int> Add(int Id, int Num)
        {
            ShopCar shopCar = Load();

            var existingItem = shopCar.productItem.FirstOrDefault(item => item.Id == Id);
            if (existingItem != null)
            {
                if (existingItem.Num != Num)
                {
                    existingItem.Num = Num;
                }
            }
            else
            {
                ResponseData responseData = new ResponseData()
                {
                    ProcedureName = "ProductDetail",
                    Parameters = new Dictionary<string, string>()
                    {
                        { "Id" , Id.ToString() }
                    }
                };

                ResultData resultData = await _apiService.CallApi(responseData);

                List<ProductItem> products = JsonSerializerService.Deserialize<List<ProductItem>>(resultData.Data[0].GetRawText());

                products.First().Num = Num;
                shopCar.productItem.Add(products.First());

            }

            Save(shopCar);

            return shopCar.Sum;
        }

        public void Delete(int Id)
        {
            ShopCar shopCar = Load();

            var itemToRemove = shopCar.productItem.FirstOrDefault(item => item.Id == Id);
            if (itemToRemove != null)
            {
                shopCar.productItem.Remove(itemToRemove);
            }

            Save(shopCar);
        }

        public int Get()
        {
            ShopCar shopCar = Load();
            return shopCar.productItem.Count;
        }
    }
}
