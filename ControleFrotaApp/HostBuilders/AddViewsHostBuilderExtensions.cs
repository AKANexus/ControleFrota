using System;
using ControleFrota.Services;
using ControleFrota.ViewModels;
using ControleFrota.ViewModels.DialogWindows;
using ControleFrota.Views;
using ControleFrota.Views.DialogWindows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ControleFrota.HostBuilders
{
    public static class AddViewsHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder host)
        {
            host.ConfigureServices((_, serviços) => {
                serviços.AddSingleton(CriaMainView);
                //serviços.AddTransient<DialogMainView>();
                serviços.AddSingleton(CriaDialogMainView);

                serviços.AddTransient<IDialogGenerator, DialogGenerator>();
            });
            return host;
        }
        private static MainView CriaMainView(IServiceProvider serviços)
        {
            return new(serviços.GetRequiredService<MainViewModel>());
        }

        private static DialogMainView CriaDialogMainView(IServiceProvider serviços)
        {
            return new(serviços.GetRequiredService<DialogMainViewModel>());
        }

    }
}
