using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.Features;
using Serilog;
using WebMVC2.Global;
using WebMVC2.Interface;
using WebMVC2.Services;

var builder = WebApplication.CreateBuilder(args);

// �]�w Configuration
AppSettings.Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

// �]�w�ޥ�HttpContext�����f
builder.Services.AddHttpContextAccessor();

// �]�m Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(AppSettings.Configuration)
    .CreateLogger();

// �]�m Serilog
builder.Host.UseSerilog();

// Add HttpClient
builder.Services.AddHttpClient();

// �]�w MultipartBodyLengthLimit
builder.Services.Configure<FormOptions>(options =>
{
    // ��@������ƨϥΪ��w�]�O128MB�A�b���]�w1MB
    options.MultipartBodyLengthLimit = 1024 * 1024 * 10;
    // ����ƼȦs��O����e���H�ȡA�W�L���j�p�N�|�ϥμȦs�ɮ�
    options.MemoryBufferThreshold = 1024 * 1024 * 1;
});

// �]�w�ƾګO�@���K�_�����[��
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(AppSettings.CookieKey)) // �Юھڹ�ڱ��p�]�m�ؿ�
    .SetApplicationName(AppSettings.ApplicationName);

// �]�wsession
builder.Services.AddRazorPages().AddSessionStateTempDataProvider();
builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
builder.Services.AddSession();

// �]�mcookies�n�J���
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

// �]�msession
app.UseSession();

// �]�mcookies�n�J���
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DataList}/{action=Index}/{id?}");

app.Run();
