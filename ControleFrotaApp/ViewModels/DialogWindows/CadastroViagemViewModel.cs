using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using static ControleFrota.Extensions.StringExtensions;

namespace ControleFrota.ViewModels.DialogWindows
{
    public class CadastroViagemViewModel : DialogContentViewModelBase
    {
        private readonly CurrentScopeStore _currentScopeStore;
        private readonly VeículoDataService _viagemDataService;
        private readonly IMessaging<int> _intMessaging;
        private readonly CultureInfo _ptBr = new("pt-BR");
        private readonly TipoGastoDataService _tipoGastoDataService;
        private readonly VeículoDataService _veículoDataService;
        private readonly MotoristaDataService _motoristaDataService;

        public ICommand CloseCurrentWindow { get; set; }
        public ICommand SalvaViagem { get; set; }
        public Viagem ViagemSelecionada { get; set; }
        public ObservableCollection<Motorista> Motoristas { get; set; } = new();
        public ObservableCollection<Veículo> Veículos { get; set; } = new();
        public ObservableCollection<TipoGasto> TipoGastos { get; set; } = new();
        public Motorista MotoristaSelecionado { get; set; }
        public Veículo VeículoSelecionado { get; set; }
        public List<Gasto> Gastos { get; set; } = new();
        public DateTime Saída
        {
            get => ViagemSelecionada?.Saída ?? DateTime.Now;
            set
            {
                ViagemSelecionada.Saída = value;
                OnPropertyChanged(nameof(Saída));
            }
        }
        public DateTime Retorno
        {
            get => ViagemSelecionada?.Retorno ?? DateTime.Now;
            set
            {
                ViagemSelecionada.Retorno = value;
                OnPropertyChanged(nameof(Retorno));
            }
        }
        public string Motivo
        {
            get => ViagemSelecionada?.Motivo;
            set
            {
                ViagemSelecionada.Motivo = value;
                OnPropertyChanged(nameof(Motivo));
            }
        }

        public string KMInicial
        {
            get => ViagemSelecionada?.KMInicial.ToString(_ptBr);
            set
            {
                ViagemSelecionada.KMInicial = value.Replace(".", ",").Safedecimal(_ptBr);
                OnPropertyChanged(nameof(KMInicial));
            }
        }

        public string KMFinal
        {
            get => ViagemSelecionada?.KMFinal.ToString(_ptBr);
            set
            {
                ViagemSelecionada.KMFinal = value.Replace(".", ",").Safedecimal(_ptBr);
                OnPropertyChanged(nameof(KMFinal));
            }
        }


        public CadastroViagemViewModel(IServiceProvider serviceProvider)
        {
            _currentScopeStore = serviceProvider.GetRequiredService<CurrentScopeStore>();
            _currentScopeStore.CriaNovoEscopo();

            CloseCurrentWindow = new CloseCurrentWindowCommand(serviceProvider);
            SalvaViagem = new SalvaViagemCommand(this, serviceProvider, (x) => MessageBox.Show(x.Message));

            _viagemDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<VeículoDataService>();
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _tipoGastoDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<TipoGastoDataService>();
            _veículoDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<VeículoDataService>();
            _motoristaDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<MotoristaDataService>();


            PreencheCampos();
        }

        private async Task PreencheCampos()
        {
            foreach (TipoGasto tipoGasto in await _tipoGastoDataService.GetAll())
            {
                TipoGastos.Add(tipoGasto);
            }

            foreach (Motorista motorista in await _motoristaDataService.GetAll())
            {
                Motoristas.Add(motorista);
            }

            foreach (Veículo veículo in await _veículoDataService.GetAll())
            {
                Veículos.Add(veículo);
            }
            if (_intMessaging.Mensagem == default)
            {
                ViagemSelecionada = new Viagem
                {
                    Saída = DateTime.Now,
                    Retorno = DateTime.Now
                };
                //OnPropertyChanged(nameof(Saída));
                //OnPropertyChanged(nameof(Retorno));
                return;
            }

            VeículoSelecionado = await _viagemDataService.GetViagemByID(_intMessaging.Mensagem);
        }
    }

    public class SalvaViagemCommand : AsyncCommandBase
    {
        private readonly CadastroViagemViewModel _cadastroViagemViewModel;
        private readonly CurrentScopeStore _currentScope;
        private readonly ViagemDataService _viagemDataService;
        private readonly IDialogsStore _dialogStore;

        public SalvaViagemCommand(CadastroViagemViewModel cadastroViagemViewModel, IServiceProvider serviceProvider, Action<Exception> onException) : base(onException)
        {
            _cadastroViagemViewModel = cadastroViagemViewModel;
            _cadastroViagemViewModel.PropertyChanged += _cadastroViagemViewModel_PropertyChanged;
            _currentScope = serviceProvider.GetRequiredService<CurrentScopeStore>();
            _viagemDataService = _currentScope.PegaEscopoAtual().GetRequiredService<ViagemDataService>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
        }

        private void _cadastroViagemViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);

        }

        public override bool CanExecute(object parameter)
        {
            if (_cadastroViagemViewModel.VeículoSelecionado is null) return false;
            if (_cadastroViagemViewModel.MotoristaSelecionado is null) return false;
            if (String.IsNullOrWhiteSpace(_cadastroViagemViewModel.KMInicial)) return false;
            if (String.IsNullOrWhiteSpace(_cadastroViagemViewModel.KMFinal)) return false;
            if (String.IsNullOrWhiteSpace(_cadastroViagemViewModel.Motivo)) return false;

            return true;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await _viagemDataService.AddOrUpdate(_cadastroViagemViewModel.ViagemSelecionada);
            _dialogStore.CloseDialog(DialogResult.OK);
        }

        public override event EventHandler CanExecuteChanged;

    }
}
