using System.Text;
using WebMVC2.Interface;

namespace WebMVC2.Services
{
    public class ApiServiceHttpClient : IApiServiceHttpClient
    {
        private readonly IHttpClientFactory _clientFactory;

        public ApiServiceHttpClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<HttpResponseMessage> SendPostRequest(string apiUrl, string jsonData)
        {
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return await _clientFactory.CreateClient().PostAsync(apiUrl, content);
        }
    }
}
