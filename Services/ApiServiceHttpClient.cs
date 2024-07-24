using System.Text;

namespace WebMVC2.Services
{
    public class ApiServiceHttpClient
    {
        private readonly HttpClient _httpClient;

        public ApiServiceHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> SendPostRequest(string apiUrl, string jsonData)
        {
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(apiUrl, content);
        }
    }
}
