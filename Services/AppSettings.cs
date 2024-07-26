namespace WebMVC2.Services
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
        public static int FileSizeLimit => 
            Convert.ToInt32(Configuration["FileSizeLimit"]);
        public static string FileUserUrl => Configuration["FileUserUrl"] ?? "wwwroot/userfiles";

    }
}
