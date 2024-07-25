namespace WebMVC2.Interface
{
    public interface IApiServiceHttpClient
    {
        public Task<HttpResponseMessage> SendPostRequest(string apiUrl, string jsonData);
    }
}
