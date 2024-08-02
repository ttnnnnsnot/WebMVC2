using WebMVC2.Models;

namespace WebMVC2.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public UserLogin userLogin { get; set; } = new UserLogin();
    }
}
