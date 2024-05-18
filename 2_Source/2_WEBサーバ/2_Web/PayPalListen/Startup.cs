using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PayPalListen.Data;
using Unikktle.Services;
using Unikktle.TokenProviders;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Logging;
using UnikktleCommon;

namespace PayPalListen
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        private readonly ILogger _logger;

        public const string CurrentDirectory = "/var/aspnet/PayPalListen/";

        public Startup(IConfiguration configuration,
            ILogger<Startup> logger,
            IHostingEnvironment env)
        {
            try
            {
                _logger = logger;

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
                _logger.LogInformation("Added TodoRepository to services");

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

                services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromHours(3));
                services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("SendGrid"));
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
                    sqlServerOptions => sqlServerOptions.CommandTimeout(300))   // 各SQLの実行を最大5分待つ
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

                if (os.IndexOf("Windows") > -1)
                {
                    // デバッグ実行モード。nonセキュア。

                    services.AddSession(options =>
                    {
                        options.Cookie.Name = ".PayPalListenWorks.Session";
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
                        options.Cookie.Name = "__Secure-PayPalListen";
                        options.Cookie.Path = "/";
                        //options.Cookie.Domain = "xxx";
                        options.Cookie.HttpOnly = true;
                        options.Cookie.SameSite = SameSiteMode.Lax;
                        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                        options.Cookie.IsEssential = true;
                        options.Cookie.MaxAge = TimeSpan.FromDays(30);
                        //options.Cookie.Expiration = TimeSpan.FromDays(30);  // エラーになる
                        // Set a short timeout for easy testing.
                        //options.IdleTimeout = TimeSpan.FromSeconds(10);
                        // Make the session cookie essential
                    });

                    services.AddAntiforgery(options =>
                    {
                        // Set Cookie properties using CookieBuilder properties†.
                        //options.CookieName = "__Secure-PayPalListen";
                        options.Cookie.Name = "__Secure-PayPalListen";
                        options.Cookie.Path = "/";
                        //options.Cookie.Domain = "xxx";
                        options.Cookie.HttpOnly = true;
                        options.Cookie.SameSite = SameSiteMode.Lax;
                        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                        options.Cookie.IsEssential = true;
                        options.Cookie.MaxAge = TimeSpan.FromDays(30);
                        options.FormFieldName = "__Secure-PayPalListenforgF";
                        options.HeaderName = "X-CSRF-TOKEN-HEAD";
                        options.SuppressXFrameOptionsHeader = false;
                    });
                }

                services.AddLocalization(options => options.ResourcesPath = "Resources");

                services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddSessionStateTempDataProvider()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
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
                _logger.LogInformation("In Development environment");

                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

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
                        pattern: "{controller=Home}/{action=Index}/{id?}");
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
