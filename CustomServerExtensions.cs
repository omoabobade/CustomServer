using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace CustomServerExample
{

        public static class ServerExtensions
        {
            public static IWebHostBuilder UseCustomServer(this IWebHostBuilder hostBuilder, Action<CustomServerOptions> options)
            {
                return hostBuilder.ConfigureServices(services =>
                {
                    services.Configure(options);
                    services.AddSingleton<IServer, CustomServer>();
                });
            }
        }

}

