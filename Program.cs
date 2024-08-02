using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.Features;
using Serilog;
using WebMVC2.Global;
using WebMVC2.Interface;
using WebMVC2.Services;

var builder = WebApplication.CreateBuilder(args);

// 設定 Configuration
AppSettings.Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

// 設定引用HttpContext的接口
builder.Services.AddHttpContextAccessor();

// 設置 Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(AppSettings.Configuration)
    .CreateLogger();

// 設置 Serilog
builder.Host.UseSerilog();

// Add HttpClient
builder.Services.AddHttpClient();

// 設定 MultipartBodyLengthLimit
builder.Services.Configure<FormOptions>(options =>
{
    // 單一次表單資料使用的預設是128MB，在此設定1MB
    options.MultipartBodyLengthLimit = 1024 * 1024 * 10;
    // 表單資料暫存到記憶體前的閾值，超過此大小就會使用暫存檔案
    options.MemoryBufferThreshold = 1024 * 1024 * 1;
});

// 設定數據保護的密鑰環持久化
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(AppSettings.CookieKey)) // 請根據實際情況設置目錄
    .SetApplicationName(AppSettings.ApplicationName);

// 設定session
builder.Services.AddRazorPages().AddSessionStateTempDataProvider();
builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
builder.Services.AddSession();

// 設置cookies登入驗証
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Login/AccessDenied";
        options.LoginPath = "/Login/Index";
    });

builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IShopCarService, ShopCarService>();

builder.Services.AddSingleton<ILoggerService, LoggerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 設置session
app.UseSession();

// 設置cookies登入驗証
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DataList}/{action=Index}/{id?}");

app.Run();
