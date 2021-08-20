using System;
using ControleFrota.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ControleFrota.HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((_, serviços) =>
            {
                string connString = $"server=192.168.10.250;userid=AmbiSuite;password=masterkey;database=AmbiStore;ConvertZeroDateTime=True";
                ServerVersion version = new MySqlServerVersion(new Version(8, 0, 26));
                void ConfigureDbContext(DbContextOptionsBuilder c)
                {
                    c.UseMySql(connString, version, x =>
                    {
                        x.CommandTimeout(600);
                        x.EnableRetryOnFailure(3);
                    });
                    c.EnableSensitiveDataLogging();
                }

                serviços.AddSingleton(new MainDbContextFactory(ConfigureDbContext));
                serviços.AddDbContext<MainDbContext>(ConfigureDbContext);
            });
            return host;
        }
    }
}
