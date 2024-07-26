using WebMVC2.Models;

namespace WebMVC2.Interface
{
    public interface IUserService
    {
        Task<ResultMessage> GetUserAddAsync(UserInfo userInfo);

        Task<ResultMessage> GetUserAsync(string userId);
    }
}
