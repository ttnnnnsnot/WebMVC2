using System.ComponentModel.DataAnnotations;

namespace WebMVC2.Models
{
    public class UserLogin
    {
        [RegularExpression("^[A-Za-z0-9_@]+$", ErrorMessage = "帳號只能包含英文數字")]
        [StringLength(20, ErrorMessage = "長度必須介於 {2} and {1}.", MinimumLength = 6)]
        [Required(ErrorMessage = "帳號是必填的")]
        public string UserID { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "密碼長度必須介於 {2} and {1}.", MinimumLength = 6)]
        [Required(ErrorMessage = "密碼是必填的")]
        public string Password { get; set; } = string.Empty;
    }
}
