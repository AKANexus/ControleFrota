using ControleFrota.Services;
using ControleFrota.Services.DataServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ControleFrota.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices((_, serviços) =>
            {
                serviços.AddScoped<EnvioDataService>();

                serviços.AddSingleton<IMessaging<(string, string)>, Messaging<(string, string)>>();
                serviços.AddSingleton<IMessaging<bool>, Messaging<bool>>();
                serviços.AddScoped<VeículoDataService>();
                serviços.AddScoped<MarcaDataService>();
                serviços.AddScoped<ModeloDataService>();



            });
            return host;
        }
    }
}
