using Framework.Configuration.Models;
using Framework.Configuration;


namespace UserManagement;

/// <summary>
/// The Main function can be used to run the ASP.NET Core application locally using the Kestrel webserver.
/// </summary>
public class LocalEntryPoint
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .DefaultAppConfiguration(new[] { typeof(ApplicationOptions).Assembly }, args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}