using ControleFrota.Domain;
using ControleFrota.Services;
using ControleFrota.State;
using ControleFrota.State.Navigators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ControleFrota.HostBuilders
{
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices((_, serviços) =>
            {
                serviços.AddSingleton<INavigator, Navigator>();
                serviços.AddSingleton<IDialogsStore, DialogsStore>();
                serviços.AddSingleton<IMessaging<int>, Messaging<int>>();
                serviços.AddSingleton<IMessaging<(string, string)>, Messaging<(string, string)>>();
                serviços.AddSingleton<IMessaging<bool>, Messaging<bool>>();
                serviços.AddSingleton<IMessaging<string>, Messaging<string>>();

                serviços.AddSingleton<BusyStateStore>();
                serviços.AddSingleton<CurrentScopeStore>();
                serviços.AddScoped<EntityStore<Veículo>>();
                serviços.AddScoped<EntityStore<EntityBase>>();

            });

            return host;
        }
    }
}
