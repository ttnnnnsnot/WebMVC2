using WebMVC2.Models;

namespace WebMVC2.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public UserInfo userInfo { get; set; } = new UserInfo();
    }
}
