using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ControleFrota.Commands;
using ControleFrota.Domain;
using ControleFrota.Services;
using ControleFrota.Services.DataServices;
using ControleFrota.State;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.ViewModels.DialogWindows
{
    public class CadastroViagemViewModel : DialogContentViewModelBase
    {
        private readonly CurrentScopeStore _currentScopeStore;
        private readonly VeículoDataService _viagemDataService;
        private readonly IMessaging<int> _intMessaging;

        public ICommand CloseCurrentWindow { get; set; }
        public ICommand SalvaViagem { get; set; }
        public Viagem ViagemSelecionada { get; set; }
        public ObservableCollection<Motorista> Motoristas { get; set; }
        public ObservableCollection<Veículo> Veículos { get; set; }

        public List<TipoGasto> TipoGastos { get; set; }

        public Motorista MotoristaSelecionado { get; set; }
        public Veículo VeículoSelecionado { get; set; }

        public DateTime Saída { get; set; }

        public CadastroViagemViewModel(IServiceProvider serviceProvider)
        {
            _currentScopeStore = serviceProvider.GetRequiredService<CurrentScopeStore>();
            _currentScopeStore.CriaNovoEscopo();

            CloseCurrentWindow = new CloseCurrentWindowCommand(serviceProvider);
            SalvaViagem = new SalvaViagemCommand(this, serviceProvider, (x) => MessageBox.Show(x.Message));

            _viagemDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<VeículoDataService>();
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();


            PreencheCampos();
        }

        private async Task PreencheCampos()
        {
            if (_intMessaging.Mensagem == default)
            {
                ViagemSelecionada = new Viagem();
                return;
            }

            VeículoSelecionado = await _viagemDataService.GetViagemByID(_intMessaging.Mensagem);
        }
    }

    public class SalvaViagemCommand : AsyncCommandBase
    {
        public SalvaViagemCommand(CadastroViagemViewModel cadastroViagemViewModel, IServiceProvider serviceProvider, Action<Exception> onException) : base(onException)
        {
        }

        protected override async Task ExecuteAsync(object parameter)
        {
        }
    }
}
