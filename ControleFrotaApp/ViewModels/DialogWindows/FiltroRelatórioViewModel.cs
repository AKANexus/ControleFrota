using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ControleFrota.Commands;
using ControleFrota.Services;
using ControleFrota.Services.DataServices;
using ControleFrota.State;
//using DinkToPdf;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.ViewModels.DialogWindows
{
    public class FiltroRelatórioViewModel : DialogContentViewModelBase
    {
        public ICommand CloseCurrentWindow { get; set; }

        public ICommand ImprimirRelatório { get; set; }

        public FiltroRelatórioViewModel(IServiceProvider serviceProvider)
        {
            CloseCurrentWindow = new CloseCurrentWindowCommand(serviceProvider);
            ImprimirRelatório = new ImprimirRelatórioCommand(this, serviceProvider, x => MessageBox.Show(x.Message));
        }
    }

    public class ImprimirRelatórioCommand : AsyncCommandBase
    {
        private readonly FiltroRelatórioViewModel _filtroRelatórioViewModel;
        //private readonly SynchronizedConverter _HtmlToPdfConverter;
        private readonly VeículoDataService _veículoDataService;
        private readonly IMessaging<int> _intMessaging;

        public ImprimirRelatórioCommand(FiltroRelatórioViewModel filtroRelatórioViewModel, IServiceProvider serviceProvider, Action<Exception> onException) : base(onException)
        {
            _filtroRelatórioViewModel = filtroRelatórioViewModel;
            var currentScope = serviceProvider.GetRequiredService<CurrentScopeStore>();
            //_HtmlToPdfConverter = currentScope.PegaEscopoAtual().GetRequiredService<SynchronizedConverter>();
            _veículoDataService = currentScope.PegaEscopoAtual().GetRequiredService<VeículoDataService>();
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
        }

        protected override async Task ExecuteAsync(object parameter)
        {
           

        }
    }
}
