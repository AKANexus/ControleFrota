using System;
using ControleFrota.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.ViewModels.DialogWindows
{
    public class WorkInProgressViewModel : DialogContentViewModelBase
    {
        public WorkInProgressViewModel(IServiceProvider provedor)
        {
            var messaging = provedor.GetRequiredService<IMessaging<(string, string)>>();
            Titulo = messaging.Mensagem.Item1;
            PrimeiraLinha = messaging.Mensagem.Item2;
        }

        public string Titulo { get; set; }
        public string PrimeiraLinha { get; set; }
    }
}