using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using PayPalListen.Data;
using System.Text;
using System;
using UnikktleCommon;

namespace PayPalListen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // .Net Core で "Shift_JIS"変換するには、エンコードプロバイダーの登録が必要
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var host = CreateWebHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<ApplicationDbContext>();
                }

                var logger = host.Services.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Seeded the database.");

                host.Run();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                throw;
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            try
            {
                return WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                            //logging.AddDebug();
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
