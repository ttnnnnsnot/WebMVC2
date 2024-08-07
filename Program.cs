﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Serilog;
using WebMVC2.Global;
using WebMVC2.Interface;
using WebMVC2.Services;

var builder = WebApplication.CreateBuilder(args);

// 設定 Configuration
AppSettings.Configuration = builder.Configuration;

// 設定當地語系化
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews()
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        .AddDataAnnotationsLocalization();

RequestLocalizationOptions myOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("zh-TW"),
    SupportedCultures = AppSettings.CultureInfos,
    SupportedUICultures = AppSettings.CultureInfos
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options = myOptions;
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});

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

// 設定AddAntiforgery驗証 防止跨網站偽造要求 (XSRF/CSRF) 攻擊
builder.Services.AddAntiforgery(options =>
{
    // Set Cookie properties using CookieBuilder properties†.
    options.FormFieldName = "AntiforgeryFieldname";
    options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
    options.SuppressXFrameOptionsHeader = false;
});


// 全域設定
//使用 AutoValidateAntiforgeryToken 屬性，而非廣泛套用 ValidateAntiForgeryToken 屬性，然後用 IgnoreAntiforgeryToken 屬性將其覆寫。 此屬性的運作方式與 ValidateAntiForgeryToken 屬性相同，不同之處在於它在處理使用下列 HTTP 方法提出的要求時不需要權杖：GET、HEAD、OPTIONS、TRACE
// IgnoreAntiforgeryToken 篩選條件可用來消除指定動作的防偽權杖需求
// ValidateAntiForgeryToken 除非要求包含有效的防偽權杖，否則對套用此篩選動作的要求會遭到封鎖
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
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

// 設定當地語系化
app.UseRequestLocalization(myOptions);

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
