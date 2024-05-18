﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Unikktle.Data;
using Unikktle.Services;
using Unikktle.TokenProviders;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using System.IO;
using UnikktleCommon;

namespace Unikktle
{
    public class MyMemoryCache
    {
        public MemoryCache Cache { get; set; }
        public MyMemoryCache()
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 10000,            // 使用量上限（IMemoryCacheオブジェクト数）
                CompactionPercentage = 0.30    // 使用量上限に達した時に空ける容量
                //ExpirationScanFrequency = TimeSpan.FromHours(1)
            });
        }
    }

    public class Startup
    {
        private CultureInfo[] supportedCultures = new[]
        {
            new CultureInfo("en"),
            new CultureInfo("ja"),
        };

        public IConfiguration Configuration { get; }
        public const string CurrentDirectory = "/var/aspnet/Unikktle/";

        public Startup(IConfiguration configuration,
            IHostingEnvironment env)
        {
            try
            {
                var os = Environment.OSVersion.ToString();
                if (os.IndexOf("Windows") > -1)
                {
                    // デバッグ実行
                    Configuration = configuration;
                    return;
                }

                // Windowsじゃなかったら本番環境とみなす。Debugシンボルが使えない。
                Environment.CurrentDirectory = CurrentDirectory;

                Console.WriteLine("----------------------------------------------------------------------------");
                Console.WriteLine($"OS : {os}");
                Console.WriteLine($"env.ContentRootPath : {env.ContentRootPath}");
                Console.WriteLine($"env.WebRootPath : {env.WebRootPath}");
                Console.WriteLine($"Environment.CurrentDirectory : {Environment.CurrentDirectory}");
                Console.WriteLine($"Directory.GetCurrentDirectory() : {Directory.GetCurrentDirectory()}");
                Console.WriteLine("----------------------------------------------------------------------------");

                //Configuration = configuration;
                Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddEnvironmentVariables()
                    .AddJsonFile("appsettings.json", false, true).Build();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                throw;
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                var os = Environment.OSVersion.ToString();

                //// クッキー　GDPR対応
                //services.Configure<CookiePolicyOptions>(options =>
                //{
                //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                //    options.CheckConsentNeeded = context => true;
                //    options.MinimumSameSitePolicy = SameSiteMode.None;
                //});

                services.AddTransient<CustomEmailConfirmationTokenProvider<IdentityUser>>();
                services.AddTransient<CustomPasswordResetTokenProvider<IdentityUser>>();
                services.AddTransient<IEmailSender, EmailSender>();
                services.AddTransient<UserSettingService>();

                services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromHours(3));
                services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("SendGrid"));
                services.Configure<RequestLocalizationOptions>(options =>
                {
                    options.DefaultRequestCulture = new RequestCulture("ja");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                });
                services.Configure<IdentityOptions>(options =>
                {
                    // Default Lockout settings.
                    options.Lockout.AllowedForNewUsers = true;      // 新しいユーザーもロックアウト対象にする。                
                    options.Lockout.MaxFailedAccessAttempts = 10;   // 10回連続でログインに失敗したらロックアウト
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(2);   // ロックアウトが発生したら、その後2時間ログインできない。

                    // Default Password settings.
                    options.Password.RequireDigit = true;           // パスワードには、0 ~ 9 までの数値が必要です。
                    options.Password.RequireLowercase = true;       // パスワードに小文字の文字が必要です。
                    options.Password.RequireUppercase = true;       // パスワードに大文字が必要です。
                    options.Password.RequireNonAlphanumeric = true; // パスワードに英数字以外の文字が必要です。
                    options.Password.RequiredLength = 8;            // パスワードの最小長。
                    options.Password.RequiredUniqueChars = 1;       // パスワードに同じ文字は含めない。1は同じ文字を1つまで使えるという設定。
                });

                //// リバースプロキシサーバが別マシンの場合に使用する
                //services.Configure<ForwardedHeadersOptions>(options =>
                //{
                //    options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
                //});

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions => sqlServerOptions.CommandTimeout(60))   // 各SQLの実行を最大1分待つ。ユーザーが耐えられる限界。
                    );

                services.AddDefaultIdentity<IdentityUser>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                    config.Tokens.ProviderMap.Add("CustomEmailConfirmation",
                        new TokenProviderDescriptor(
                            typeof(CustomEmailConfirmationTokenProvider<IdentityUser>)));
                    config.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
                    config.Tokens.ProviderMap.Add("CustomPasswordReset",
                        new TokenProviderDescriptor(
                            typeof(CustomPasswordResetTokenProvider<IdentityUser>)));
                    config.Tokens.PasswordResetTokenProvider = "CustomPasswordReset";
                })
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

                //services.AddDistributedMemoryCache();
                services.AddMemoryCache();

                services.AddSingleton<MyMemoryCache>();

                if (os.IndexOf("Windows") > -1)
                {
                    // デバッグ実行モード。nonセキュア。

                    services.AddSession(options =>
                    {
                        options.Cookie.Name = ".UnikktleWorks.Session";
                        // Set a short timeout for easy testing.
                        //options.IdleTimeout = TimeSpan.FromSeconds(10);
                        //options.Cookie.HttpOnly = true;
                        // Make the session cookie essential
                        options.Cookie.IsEssential = true;
                    });
                }
                else
                {
                    // 本番モード。セキュア。

                    services.AddSession(options =>
                    {
                        //options.CookieName = "__Secure-Unikktle";
                        //options.CookieDomain = "xxx";
                        //options.CookieHttpOnly = true;
                        //options.CookiePath = "/";
                        //options.CookieSecure = CookieSecurePolicy.Always;

                        options.Cookie.Name = "__Secure-Unikktle";
                        options.Cookie.Domain = "xxx";
                        options.Cookie.Path = "/";
                        options.Cookie.HttpOnly = true;
                        options.Cookie.IsEssential = true;
                        options.Cookie.MaxAge = TimeSpan.FromDays(30);
                        options.Cookie.SameSite = SameSiteMode.Lax;
                        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                        //options.Cookie.Expiration = TimeSpan.FromDays(30);  // エラーになる
                        // Set a short timeout for easy testing.
                        //options.IdleTimeout = TimeSpan.FromSeconds(10);
                        // Make the session cookie essential
                    });

                    // Session/Cookieのデシリアライズ処理が不安定になり、HTTP POSTが
                    // 「応答無し HTTP ERROR 400」を頻発に返して来るので、Cookieのセキュア化は諦めた。
                    // http://life-of-dev.sblo.jp/article/186622593.html
                    //
                    //services.AddAntiforgery(options =>
                    //{
                    //    // Set Cookie properties using CookieBuilder properties†.
                    //    options.CookieName = "__Secure-Unikktle";
                    //    options.Cookie.Name = "__Secure-Unikktle";
                    //    options.Cookie.Path = "/";
                    //    options.Cookie.Domain = "xxx";
                    //    options.Cookie.HttpOnly = true;
                    //    options.Cookie.SameSite = SameSiteMode.Lax;
                    //    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    //    options.Cookie.IsEssential = true;
                    //    options.Cookie.MaxAge = TimeSpan.FromDays(30);
                    //    options.FormFieldName = "__Secure-Unikktle";
                    //    options.HeaderName = "X-CSRF-TOKEN-HEAD";
                    //    options.SuppressXFrameOptionsHeader = false;
                    //});
                }

                services.AddLocalization(options => options.ResourcesPath = "Resources");

                services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddSessionStateTempDataProvider()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                    .AddDataAnnotationsLocalization(options => 
                    {
                        options.DataAnnotationLocalizerProvider = (type, factory) =>
                            factory.Create(typeof(SharedResource));
                    });
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                throw;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            try
            {
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

                app.UseRequestLocalization(new RequestLocalizationOptions
                {
                    DefaultRequestCulture = new RequestCulture("ja"),
                    // Formatting numbers, dates, etc.
                    SupportedCultures = supportedCultures,
                    // UI strings that we have localized.
                    SupportedUICultures = supportedCultures
                });

                //app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                app.UseSession();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{i?}");
                    endpoints.MapRazorPages();
                });
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                throw;
            }
        }


    }
}
