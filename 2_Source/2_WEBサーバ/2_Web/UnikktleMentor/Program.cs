#define TEST    // 技術検証用ソースコード

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using UnikktleMentor.Data;
using UnikktleMentor.Models;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;
using System.Security.Authentication;
//using Microsoft.AspNetCore.Server.Kestrel.Https.Internal;
using System.Security.Cryptography.X509Certificates;
using UnikktleCommon;

namespace UnikktleMentor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = CreateWebHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<ApplicationDbContext>();
                }

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
                var host = WebHost.CreateDefaultBuilder(args)
                    //.UseLibuv()    // <- ↓Unix Sock で使う
                    .UseStartup<Startup>()
                    .ConfigureKestrel((context, options) =>
    {
                        //options.Limits.MaxConcurrentConnections = 100;
                        //options.Limits.MaxConcurrentUpgradedConnections = 100;
                        //options.Limits.MaxRequestBodySize = 10 * 1024;
                        //options.Limits.MinRequestBodyDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                        //options.Limits.MinResponseDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                        //options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
                        //options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
                        //options.Limits.Http2.MaxStreamsPerConnection = 100;
                        //options.Limits.Http2.HeaderTableSize = 4096;
                        //options.Limits.Http2.MaxFrameSize = 16384;
                        //options.Limits.Http2.MaxRequestHeaderFieldSize = 8192;
                        //options.Limits.Http2.InitialConnectionWindowSize = 131072;
                        //options.Limits.Http2.InitialStreamWindowSize = 98304;

                        //options.AllowSynchronousIO = false;

                        //options.ConfigureEndpointDefaults(opt =>
                        //{
                        //    opt.NoDelay = true;
                        //});

                        // Unix Sock 。Nginxとのパフォーマンス向上
                        //options.ListenUnixSocket("/var/sock/kestrel-UnikktleMentor.sock");

                    });

                return host;
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                throw;
            }
        }
    }
}
