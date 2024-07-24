using WebMVC2.Models;
using System.Text.Json;

namespace WebMVC2.Services
{
    public class ApiService
    {
        private readonly ApiServiceHttpClient _httpClient;

        public ApiService(ApiServiceHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private ResultData ProcessApiResponse(string apiResponse)
        {
            var result = new ResultData();
            try
            {
                var resultData = JsonSerializer.Deserialize<ResultData>(apiResponse,
                    GlobalJsonSerializerOptions.Default);
                
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
                var jsonData = JsonSerializer.Serialize(responseData);
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
