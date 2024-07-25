using WebMVC2.Models;

namespace WebMVC2.Interface
{
    public interface IApiService
    {
        public Task<ResultData> CallApi(ResponseData responseData);
    }
}
