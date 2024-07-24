using System.ComponentModel.DataAnnotations;

namespace WebMVC2.Models
{
    public class Login
    {
        [Required(ErrorMessage = "帳號是必填的")]
        [RegularExpression("^[A-Za-z0-9_@]+$", ErrorMessage = "帳號只能包含英文數字")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "密碼是必填的")]
        public string Password { get; set; } = string.Empty;
    }
}
