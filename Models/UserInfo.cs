using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebMVC2.Services;
using WebMVC2.Validation;

namespace WebMVC2.Models
{
    public class UserInfo
    {
        [StringLength(20, ErrorMessage = "長度必須介於 {2} and {1}.", MinimumLength = 6)]
        [Remote(action: "CheckUserID", controller: "Home")]
        [Required(ErrorMessage = "帳號是必填的")]
        [RegularExpression("^[A-Za-z0-9_@]+$", ErrorMessage = "帳號只能包含英文數字")]
        public string UserID { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "密碼長度必須介於 {2} and {1}.", MinimumLength = 6)]
        [Required(ErrorMessage = "密碼是必填的")]
        public string Password { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "不得超過20個字元")]
        [Required(ErrorMessage = "密碼是必填的")]
        [Compare("Password", ErrorMessage = "確認密碼與密碼不匹配")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [DateAfter("2020/01/01", ErrorMessage = "生日必須在2020年1月1日之後")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "EMAIL是必填的")]
        [EmailAddress(ErrorMessage = "請輸入有效的EMAIL地址")]
        public string Email { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "不得超過{0}個字元")]
        [Required(ErrorMessage = "地址是必填的")]
        public string Address { get; set; } = string.Empty;

        [FileExtension(new string[] {".jpg",".jpeg",".png",".gif"}, ErrorMessage = "檔案格式錯誤")]
        [FileSize(3, ErrorMessage = "單一檔案大小必須小於3MB")]
        [MaxFileCount(2, ErrorMessage = "最多上傳2個檔案")]
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
        
        public List<string> FilesName { get; set; } = new List<string>();

        /*
         * 
        [MinLength(1, ErrorMessage = "至少上傳一個檔案")]
        [MaxLength(5, ErrorMessage = "最多上傳五個檔案")]
         * 
        [Required]: 確保屬性的值不為空。 
            用法：[Required]
        [Range]: 確保屬性的值在指定範圍內。 
            用法：[Range(1, 100)]
        [StringLength]: 確保字串屬性的長度在指定範圍內。 
            用法：[StringLength(50)]
        [RegularExpression]: 使用正則表達式驗證屬性的值。 
            用法：[RegularExpression(@"^[A-Za-z0-9]+$")] public string Username { get; set; }
        [Compare]: 比較兩個屬性的值是否相等。 
            用法：[Compare("PasswordConfirm")] public string Password { get; set; }
        [EmailAddress]: 確保屬性的值是有效的電子郵件地址。 
            用法：[EmailAddress] public string Email { get; set; }
        [Phone]: 確保屬性的值是有效的電話號碼。 
            用法：[Phone] public string Phone { get; set; }
        [CreditCard]: 確保屬性的值是有效的信用卡號碼。 
            用法：[CreditCard] public string CreditCardNumber { get; set; }
        [Url]: 確保屬性的值是有效的URL。 
            用法：[Url] public string Website { get; set; }
         */
    }
}
