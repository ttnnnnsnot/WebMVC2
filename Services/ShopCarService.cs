using System.Dynamic;
using System.Security.Claims;
using System.Text.Json;
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

        public async Task<ResultMessage> ShoppingDone()
        {
            ResultMessage resultData = new ResultMessage();

            ShopCar shopCar = Load();

            var user = _httpContextAccessor?.HttpContext?.User;

            var userid = user?.FindFirst(ClaimTypes.Name)?.Value;

            if(string.IsNullOrEmpty(userid))
            {
                resultData.MsgText = "請先行登入";
                return resultData;
            }

            ResponseData responseData = new ResponseData()
            {
                ProcedureName = "ShoppingDone",
                Parameters = new Dictionary<string, string>() {
                        { "UserID" , userid.ToString() }
                },
                TableTypeName = "ShoppingDetailType",
                TableData = ConvertToDictionaryList.Convert(shopCar.productItem)
            };

            ResultData res = await _apiService.CallApi(responseData);

            if(!res.resultMessage.Msg)
            {
                return res.resultMessage;
            }

            List<ResultMessage> resultMessages = JsonSerializerService.Deserialize<List<ResultMessage>>(res.Data[0].GetRawText());

            if(resultMessages.First().Msg)
            {
                DeleteAll();
            }

            return resultMessages.First();
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

        public ShopCar Upd(int Id, int Num)
        {
            ShopCar shopCar = Load();

            var existingItem = shopCar.productItem.FirstOrDefault(item => item.Id == Id);
            if (existingItem != null)
            {
                if(Num == -1 && existingItem.Num == 1)
                {
                    return shopCar;
                }

                existingItem.Num += Num;
            }

            Save(shopCar);

            return shopCar;
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

        private void DeleteAll()
        {
            _httpContextAccessor?.HttpContext?.Response.Cookies.Delete(nameof(CookieName.ShopCar));
        }

        public ShopCar Delete(int Id)
        {
            ShopCar shopCar = Load();

            var itemToRemove = shopCar.productItem.FirstOrDefault(item => item.Id == Id);
            if (itemToRemove != null)
            {
                shopCar.productItem.Remove(itemToRemove);
            }

            Save(shopCar);
            return shopCar;
        }

        public int Get()
        {
            ShopCar shopCar = Load();
            return shopCar.productItem.Count;
        }

        public ShopCar GetShopCar()
        {
            return Load();
        }
    }
}
