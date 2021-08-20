﻿using ControleFrota.ViewModels;
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
                serviços.AddTransient<ListagemVeículosViewModel>();
                serviços.AddTransient<ListagemFuncionáriosViewModel>();


                serviços.AddSingleton<IDialogViewModelFactory, DialogViewModelFactory>();
                serviços.AddTransient<DialogMainViewModel>();
                serviços.AddTransient<WorkInProgressViewModel>();
                serviços.AddTransient<CadastroVeículoViewModel>();
                serviços.AddTransient<CadastroMotoristaViewModel>();

                serviços.AddSingleton<CriaViewModel<ListagemVeículosViewModel>>(serviceProvider =>
                    serviceProvider.GetRequiredService<ListagemVeículosViewModel>); 
                serviços.AddSingleton<CriaViewModel<ListagemFuncionáriosViewModel>>(serviceProvider =>
                    serviceProvider.GetRequiredService<ListagemFuncionáriosViewModel>);

                serviços.AddSingleton<CriaDialogViewModel<WorkInProgressViewModel>>(serviceProvider =>
                    serviceProvider.GetRequiredService<WorkInProgressViewModel>);
                serviços.AddSingleton<CriaDialogViewModel<CadastroVeículoViewModel>>(serviceProvider =>
                    serviceProvider.GetRequiredService<CadastroVeículoViewModel>);
                serviços.AddSingleton<CriaDialogViewModel<CadastroMotoristaViewModel>>(serviceProvider =>
                    serviceProvider.GetRequiredService<CadastroMotoristaViewModel>);


            });
            return host;
        }
    }
}