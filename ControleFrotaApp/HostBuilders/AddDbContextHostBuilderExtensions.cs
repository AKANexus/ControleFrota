using System;
using System.Diagnostics;
using ControleFrota.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;


namespace ControleFrota.HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((_, serviços) =>
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
#if DEBUG
                    //.AddUserSecrets<App>();
                    .AddJsonFile("appsettings.json");

#else
                    .AddJsonFile("appsettings.json");

#endif
                var configuration = builder.Build();
                string connString = configuration.GetConnectionString("DefaultDB");
                ServerVersion version = new MySqlServerVersion(new Version(8, 0, 23));
                Action<DbContextOptionsBuilder> configureDbContext = c =>
                {
                    c.UseMySql(connString, version, x =>
                    {
                        x.CommandTimeout(600);
                        x.EnableRetryOnFailure(3);
                    });
                    c.EnableSensitiveDataLogging();
                };
                serviços.AddSingleton<MainDbContextFactory>(new MainDbContextFactory(configureDbContext));
                serviços.AddDbContext<MainDbContext>(configureDbContext);
            });
            return host;
        }
    }
}
