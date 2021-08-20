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
                serviços.AddSingleton<BusyStateStore>();
            });

            return host;
        }
    }
}
