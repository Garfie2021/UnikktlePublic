using System;
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
//using UnikktleMentor.Data;
//using UnikktleMentor.Services;
//using UnikktleMentor.TokenProviders;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using System.IO;
using UnikktleCommon;

namespace TestWeb
{
    public class MyMemoryCache
    {
        public MemoryCache Cache { get; set; }
        public MyMemoryCache()
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 100,            // �g�p�ʏ���iIMemoryCache�I�u�W�F�N�g���j
                CompactionPercentage = 0.30    // �g�p�ʏ���ɒB�������ɋ󂯂�e��
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
        public const string CurrentDirectory = "/var/aspnet/Test/";

        public Startup(IConfiguration configuration,
            IHostingEnvironment env)
        {
            try
            {
                var os = Environment.OSVersion.ToString();
                if (os.IndexOf("Windows") > -1)
                {
                    // �f�o�b�O���s
                    Configuration = configuration;
                    return;
                }

                // Windows����Ȃ�������{�Ԋ��Ƃ݂Ȃ��BDebug�V���{�����g���Ȃ��B
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
                    //.AddJsonFile("appsettings.json", false, true)
                    .Build();
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


                //services.AddTransient<CustomEmailConfirmationTokenProvider<IdentityUser>>();
                //services.AddTransient<CustomPasswordResetTokenProvider<IdentityUser>>();
                //services.AddTransient<IEmailSender, EmailSender>();
                //services.AddTransient<UserSettingService>();

                services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromHours(3));
                //services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("SendGrid"));
                services.Configure<RequestLocalizationOptions>(options =>
                {
                    options.DefaultRequestCulture = new RequestCulture("ja");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                });
                services.Configure<IdentityOptions>(options =>
                {
                    // Default Lockout settings.
                    options.Lockout.AllowedForNewUsers = true;      // �V�������[�U�[�����b�N�A�E�g�Ώۂɂ���B                
                    options.Lockout.MaxFailedAccessAttempts = 10;   // 10��A���Ń��O�C���Ɏ��s�����烍�b�N�A�E�g
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(2);   // ���b�N�A�E�g������������A���̌�2���ԃ��O�C���ł��Ȃ��B

                    // Default Password settings.
                    options.Password.RequireDigit = true;           // �p�X���[�h�ɂ́A0 ~ 9 �܂ł̐��l���K�v�ł��B
                    options.Password.RequireLowercase = true;       // �p�X���[�h�ɏ������̕������K�v�ł��B
                    options.Password.RequireUppercase = true;       // �p�X���[�h�ɑ啶�����K�v�ł��B
                    options.Password.RequireNonAlphanumeric = true; // �p�X���[�h�ɉp�����ȊO�̕������K�v�ł��B
                    options.Password.RequiredLength = 8;            // �p�X���[�h�̍ŏ����B
                    options.Password.RequiredUniqueChars = 1;       // �p�X���[�h�ɓ��������͊܂߂Ȃ��B1�͓���������1�܂Ŏg����Ƃ����ݒ�B
                });

                //// ���o�[�X�v���L�V�T�[�o���ʃ}�V���̏ꍇ�Ɏg�p����
                //services.Configure<ForwardedHeadersOptions>(options =>
                //{
                //    options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
                //});

                //services.AddDbContext<ApplicationDbContext>(options =>
                //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                //    sqlServerOptions => sqlServerOptions.CommandTimeout(60))   // �eSQL�̎��s���ő�1���҂B���[�U�[���ς�������E�B
                //    );

                services.AddDefaultIdentity<IdentityUser>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                    //config.Tokens.ProviderMap.Add("CustomEmailConfirmation",
                    //    new TokenProviderDescriptor(
                    //        typeof(CustomEmailConfirmationTokenProvider<IdentityUser>)));
                    config.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
                    //config.Tokens.ProviderMap.Add("CustomPasswordReset",
                    //    new TokenProviderDescriptor(
                    //        typeof(CustomPasswordResetTokenProvider<IdentityUser>)));
                    config.Tokens.PasswordResetTokenProvider = "CustomPasswordReset";
                })
                    .AddDefaultUI()
                    //.AddEntityFrameworkStores<ApplicationDbContext>()
                    ;

                //services.AddDistributedMemoryCache();
                services.AddMemoryCache();

                services.AddSingleton<MyMemoryCache>();

                if (os.IndexOf("Windows") > -1)
                {
                    // �f�o�b�O���s���[�h�Bnon�Z�L���A�B

                    services.AddSession(options =>
                    {
                        options.Cookie.Name = "UnikktleMentorWorks.Session";
                        options.Cookie.HttpOnly = true;
                        options.Cookie.IsEssential = true;
                    });
                }
                else
                {
                    // �{�ԃ��[�h�B�Z�L���A�B

                    services.AddSession(options =>
                    {
                        //options.CookieName = "__Secure-UnikktleMentor";
                        //options.CookieDomain = "Mentor.xxx";
                        //options.CookieHttpOnly = true;
                        //options.CookiePath = "/";
                        //options.CookieSecure = CookieSecurePolicy.Always;

                        //options.Cookie.Name = "__Secure-UnikktleMentor";
                        options.Cookie.Domain = "Mentor.xxx";
                        options.Cookie.Path = "/";
                        options.Cookie.HttpOnly = true;
                        options.Cookie.IsEssential = true;
                        options.Cookie.MaxAge = TimeSpan.FromDays(30);
                        options.Cookie.SameSite = SameSiteMode.Lax;
                        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    });
                }

                services.AddLocalization(options => options.ResourcesPath = "Resources");

                services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddSessionStateTempDataProvider()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                    //.AddDataAnnotationsLocalization(options =>
                    //{
                    //    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    //        factory.Create(typeof(SharedResource));
                    //})
                    ;
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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

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
    }
}
