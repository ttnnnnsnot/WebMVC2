using WebMVC2.Interface;
using WebMVC2.Models;

namespace WebMVC2.Services
{
    public class UserService : IUserService
    {
        private readonly IApiService _apiService;

        public UserService(IApiService apiService)
        {
            _apiService = apiService;
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
    }
}
