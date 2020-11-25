using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;


namespace CustomServerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseCustomServer(o => o.ListeningEndpoint = @"http://127.0.0.1:8091/")
                .UseStartup<Startup>()
                .Build();
    }
}
