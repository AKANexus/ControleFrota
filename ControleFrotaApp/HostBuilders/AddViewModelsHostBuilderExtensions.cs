using ControleFrota.ViewModels;
using ControleFrota.ViewModels.DialogWindows;
using ControleFrota.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ControleFrota.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices((_,
                serviços) =>
            {
                serviços.AddSingleton<IViewModelFactory, ViewModelFactory>();
                serviços.AddTransient<MainViewModel>();
                serviços.AddTransient<TelaInicialViewModel>();
                

                serviços.AddSingleton<IDialogViewModelFactory, DialogViewModelFactory>();
                serviços.AddTransient<DialogMainViewModel>();
                serviços.AddTransient<WorkInProgressViewModel>();

                serviços.AddSingleton<CriaViewModel<TelaInicialViewModel>>(serviceProvider =>
                    serviceProvider.GetRequiredService<TelaInicialViewModel>);

                serviços.AddSingleton<CriaDialogViewModel<WorkInProgressViewModel>>(serviceProvider =>
                    serviceProvider.GetRequiredService<WorkInProgressViewModel>);


            });
            return host;
        }
    }
}