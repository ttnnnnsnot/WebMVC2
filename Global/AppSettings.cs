namespace WebMVC2.Global
{
    public static class AppSettings
    {
        public static IConfiguration Configuration { get; set; } =
            new ConfigurationBuilder().Build();
        public static string AllowedHosts =>
            Configuration["AllowedHosts"] ?? "*";
        public static string ApiUrl =>
            Configuration["ApiUrl"] ?? "http://127.0.0.1:5000/execute";
        public static string DefaultLogLevel =>
            Configuration["Logging:LogLevel:Default"] ?? "Information";
        public static string MicrosoftAspNetCoreLogLevel =>
            Configuration["Logging:LogLevel:Microsoft.AspNetCore"] ?? "Warning";

        //上傳檔案的路徑
        public const string FileUserUrl = "wwwroot/userfiles";
        //單個上傳檔案限制為2個
        public const int FileCount = 2;
        //單個上傳檔案限制為1MB (1024 * 1024 * 1
        public const long FileSizeLimit = 1024 * 1024 * 3;

        public const string FileExtensions = "jpg,jpeg,png,gif";
    }
}
