using System.Text;
using WebMVC2.Global;
using WebMVC2.Interface;
using WebMVC2.Models;

namespace WebMVC2.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ApiService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private ResultData ApiResponse(string apiResponse)
        {
            var result = new ResultData();
            try
            {
                var resultData = JsonSerializerService.Deserialize<ResultData>(apiResponse);
                
                if (resultData != null)
                {
                    return resultData;
                }
                else
                {
                    return result;
                }
            }
            catch
            {
                // Handle JSON deserialization exception
                return result;
            }
        }

        public async Task<ResultData> CallApi(ResponseData responseData)
        {
            try
            {
                var jsonData = JsonSerializerService.Serialize(responseData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _clientFactory.CreateClient().PostAsync(AppSettings.ApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return ApiResponse(apiResponse);
                }

                return ApiResponse("");
            }
            catch
            {
                return ApiResponse("");
            }
        }
    }
}
