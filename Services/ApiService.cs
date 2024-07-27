using WebMVC2.Global;
using WebMVC2.Interface;
using WebMVC2.Models;

namespace WebMVC2.Services
{
    public class ApiService : IApiService
    {
        private readonly IApiServiceHttpClient _httpClient;

        public ApiService(IApiServiceHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private ResultData ProcessApiResponse(string apiResponse)
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
                HttpResponseMessage response = await _httpClient.SendPostRequest(AppSettings.ApiUrl, jsonData);

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return ProcessApiResponse(apiResponse);
                }
                else
                {
                    // Handle non-success status code
                    return ProcessApiResponse("");
                }
            }
            catch
            {
                // Handle HTTP request exception
                return ProcessApiResponse("");
            }
        }
    }
}
