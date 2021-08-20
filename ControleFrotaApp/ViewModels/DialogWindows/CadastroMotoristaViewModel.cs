using System;
using System.Collections.Generic;
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
    public class CadastroMotoristaViewModel : DialogContentViewModelBase
    {
        private readonly CurrentScopeStore _currentScopeStore;
        private readonly MotoristaDataService _motoristaDataService;
        private readonly IMessaging<int> _intMessaging;
        public Motorista MotoristaSelecionado { get; set; }
        public ICommand CloseCurrentWindow { get; set; }
        public ICommand SalvaMotorista { get; set; }
        public string NomeMotorista
        {
            get => MotoristaSelecionado?.Nome;
            set
            {
                MotoristaSelecionado.Nome = value;
                OnPropertyChanged(nameof(NomeMotorista));
            }
        }

        public string CNHMotorista
        {
            get => MotoristaSelecionado?.CNH;
            set
            {
                MotoristaSelecionado.CNH = value;
                OnPropertyChanged(nameof(CNHMotorista));
            }
        }

        public CadastroMotoristaViewModel(IServiceProvider serviceProvider)
        {
            _currentScopeStore = serviceProvider.GetRequiredService<CurrentScopeStore>();
            _currentScopeStore.CriaNovoEscopo();

            CloseCurrentWindow = new CloseCurrentWindowCommand(serviceProvider);
            SalvaMotorista = new SalvaMotoristaCommand(this, serviceProvider, (x) => MessageBox.Show(x.Message));

            _motoristaDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<MotoristaDataService>();
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();


            PreencheCampos();

        }

        private async Task PreencheCampos()
        {
            if (_intMessaging.Mensagem == default)
            {
                MotoristaSelecionado = new Motorista();
                return;
            }

            MotoristaSelecionado = await _motoristaDataService.GetMotoristaByID(_intMessaging.Mensagem);
        }

        public class SalvaMotoristaCommand : AsyncCommandBase
        {
            private readonly CadastroMotoristaViewModel _cadastroMotoristaViewModel;
            private readonly CurrentScopeStore _currentScope;
            private readonly MotoristaDataService _motoristaDataService;
            private readonly IDialogsStore _dialogStore;

            public SalvaMotoristaCommand(CadastroMotoristaViewModel cadastroMotoristaViewModel, IServiceProvider serviceProvider, Action<Exception> onException) : base(onException)
            {
                _currentScope = serviceProvider.GetRequiredService<CurrentScopeStore>();

                _cadastroMotoristaViewModel = cadastroMotoristaViewModel;
                _cadastroMotoristaViewModel.PropertyChanged += _cadastroVeículoViewModel_PropertyChanged;
                _motoristaDataService = _currentScope.PegaEscopoAtual().GetRequiredService<MotoristaDataService>();
                _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
            }

            private void _cadastroVeículoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                CanExecuteChanged?.Invoke(this, e);
            }

            public override bool CanExecute(object parameter)
            {
                if (String.IsNullOrWhiteSpace(_cadastroMotoristaViewModel.MotoristaSelecionado?.Nome)) return false;
                if (String.IsNullOrWhiteSpace(_cadastroMotoristaViewModel.MotoristaSelecionado?.CNH)) return false;
                return true;
            }

            protected override async Task ExecuteAsync(object parameter)
            {
                await _motoristaDataService.AddOrUpdate(_cadastroMotoristaViewModel.MotoristaSelecionado);
                _dialogStore.CloseDialog(DialogResult.OK);
            }

            public override event EventHandler CanExecuteChanged;
        }

    }
}
