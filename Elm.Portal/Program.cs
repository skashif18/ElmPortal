using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Appo.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSetting("detailedErrors", "true");
                    webBuilder.UseStartup<Startup>();
                });
    }
} 
