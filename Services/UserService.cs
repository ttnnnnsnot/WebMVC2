using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using WebMVC2.Global;
using WebMVC2.Interface;
using WebMVC2.Models;

namespace WebMVC2.Services
{
    public class UserService : IUserService
    {
        private readonly IApiService _apiService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IApiService apiService, IHttpContextAccessor httpContextAccessor)
        {
            _apiService = apiService;
            _httpContextAccessor = httpContextAccessor;
        }

        private void DeleteFile(List<string> filePaths)
        {
            foreach (var filePath in filePaths)
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        public async Task<ResultMessage> GetUserAddAsync(UserInfo userInfo)
        {
            var filePaths = new List<string>();

            foreach (var formFile in userInfo.Files)
            {
                var extension = Path.GetExtension(formFile.FileName).ToLowerInvariant();

                string fileName = $"{DateTime.Now:yyyyMMddHHmmssfff}_{Path.GetRandomFileName()}{extension}";

                var filePath = Path.Combine(AppSettings.FileUserUrl, fileName);

                filePaths.Add(filePath);

                using (var stream = File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                    userInfo.FilesName.Add(fileName);
                }
            }

            ResponseData responseData = new ResponseData()
            {
                ProcedureName = "UserAdd",
                Parameters = new Dictionary<string, string>
                {
                    {"UserID", userInfo.UserID},
                    {"Password", userInfo.Password},
                    {"Birthday", userInfo.Birthday.ToString("yyyy/MM/dd")},
                    {"Email", userInfo.Email},
                    {"Address", userInfo.Address},
                    {"FileName", string.Join(",",userInfo.FilesName)},
                }
            };

            ResultData resultData = await _apiService.CallApi(responseData);

            if(!resultData.resultMessage.Msg || resultData.Data == null || resultData.Data.Count == 0)
            {
                DeleteFile(filePaths);
                return new ResultMessage() { Msg = false , MsgText = "網路錯誤" };
            }

            List<ResultMessage> resultMessages = JsonSerializerService.Deserialize<List<ResultMessage>>(resultData.Data[0].GetRawText());

            if (!resultMessages[0].Msg)
            {
                DeleteFile(filePaths);
            }

            return resultMessages[0];
        }

        public async Task<ResultMessage> GetUserAsync(string userId)
        {
            ResponseData responseData = new ResponseData()
            {
                ProcedureName = "CheckUserID",
                Parameters = new Dictionary<string, string>
                {
                    {"UserID", userId},
                }
            };

            ResultData resultData = await _apiService.CallApi(responseData);

            if (!resultData.resultMessage.Msg || resultData.Data == null || resultData.Data.Count == 0)
            {
                return new ResultMessage() { Msg = false, MsgText = "網路錯誤" };
            }

            List<ResultMessage> resultMessages = JsonSerializerService.Deserialize<List<ResultMessage>>(resultData.Data[0].GetRawText());

            return resultMessages[0];
        }

        public async Task<bool> UserLoginAsync(UserLogin login)
        {
            ResponseData responseData = new ResponseData()
            {
                ProcedureName = "UserLogin",
                Parameters = new Dictionary<string, string>()
                {
                    {"UserID", login.UserID},
                    {"PassWord", login.Password}
                }
            };

            ResultData resultData = await _apiService.CallApi(responseData);

            if (!resultData.resultMessage.Msg || resultData.Data == null || resultData.Data.Count == 0)
            {
                return false;
            }

            List<UserLogin> resultMessages = JsonSerializerService.Deserialize<List<UserLogin>>(resultData.Data[0].GetRawText());

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, resultMessages[0].UserID),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                IssuedUtc = DateTimeOffset.UtcNow,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            if (_httpContextAccessor.HttpContext == null)
            {
                return false;
            }

            await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

            return true;
        }
    }
}
