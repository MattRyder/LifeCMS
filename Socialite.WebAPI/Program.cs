using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Socialite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseUrls(new string[] { "http://localhost:5002"})
                .UseStartup<Startup>();
    }
}
