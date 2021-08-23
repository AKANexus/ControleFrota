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
using ControleFrota.Extensions;
using ControleFrota.Services;
using ControleFrota.Services.DataServices;
using ControleFrota.State;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.ViewModels.DialogWindows
{
    public class RetornoDeVeículoViewModel : DialogContentViewModelBase
    {
        private readonly CurrentScopeStore _currentScopeStore;
        private readonly IMessaging<int> _intMessaging;
        private readonly CultureInfo _ptBr = new("pt-BR");
        private readonly TipoGastoDataService _tipoGastoDataService;
        private readonly ViagemDataService _viagemDataService;

        public ICommand CloseCurrentWindow { get; set; }
        public ICommand SalvaViagem { get; set; }
        //public ICommand AcrescentaAbastecimento { get; set; }
        public Viagem ViagemSelecionada { get; set; }
        public List<TipoGasto> TipoGastos { get; set; } = new();
        public List<Gasto> Gastos { get; set; } = new();

        public string KMFinal
        {
            get => ViagemSelecionada?.KMFinal.ToString(_ptBr);
            set
            {
                ViagemSelecionada.KMFinal = value.Replace(".", ",").Safedecimal(_ptBr);
                OnPropertyChanged(nameof(KMFinal));
            }
        }


        public RetornoDeVeículoViewModel(IServiceProvider serviceProvider)
        {
            _currentScopeStore = serviceProvider.GetRequiredService<CurrentScopeStore>();
            _currentScopeStore.CriaNovoEscopo();

            CloseCurrentWindow = new CloseCurrentWindowCommand(serviceProvider);
            SalvaViagem = new SalvaRetornoDeViatura(this, serviceProvider, (x) => MessageBox.Show(x.Message));
            //AcrescentaAbastecimento = new AcrescentaAbastecimentoCommand(this, serviceProvider, (x) => MessageBox.Show(x.Message));

            _viagemDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<ViagemDataService>();
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _tipoGastoDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<TipoGastoDataService>();


            PreencheCampos();
        }

        private async Task PreencheCampos()
        {
            TipoGastos.Clear();
            foreach (TipoGasto tipoGasto in await _tipoGastoDataService.GetAll())
            {
                TipoGastos.Add(tipoGasto);
            }

            ViagemSelecionada = await _viagemDataService.GetViagemByID(_intMessaging.Mensagem);
        }
    }

    //public class AcrescentaAbastecimentoCommand : AsyncCommandBase
    //{
    //    private readonly RetornoDeVeículoViewModel _retornoDeVeículoViewModel;
    //    private CurrentScopeStore _currentScopeStore;

    //    public AcrescentaAbastecimentoCommand(RetornoDeVeículoViewModel retornoDeVeículoViewModel, IServiceProvider serviceProvider, Action<Exception> onException) : base(onException)
    //    {
    //        _retornoDeVeículoViewModel = retornoDeVeículoViewModel;
    //        _currentScopeStore = serviceProvider.GetRequiredService<CurrentScopeStore>();
    //        _currentScopeStore.CriaNovoEscopo();

    //    }

    //    protected override async Task ExecuteAsync(object parameter)
    //    {

    //    }
    //}

    public class SalvaRetornoDeViatura : AsyncCommandBase
    {
        private readonly RetornoDeVeículoViewModel _cadastroViagemViewModel;
        private readonly CurrentScopeStore _currentScope;
        private readonly ViagemDataService _viagemDataService;
        private readonly IDialogsStore _dialogStore;

        public SalvaRetornoDeViatura(RetornoDeVeículoViewModel cadastroViagemViewModel, IServiceProvider serviceProvider, Action<Exception> onException) : base(onException)
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
            if (string.IsNullOrWhiteSpace(_cadastroViagemViewModel.KMFinal)) return false;
            return true;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            _cadastroViagemViewModel.ViagemSelecionada.Retorno = DateTime.Now;
            await _viagemDataService.AddOrUpdate(_cadastroViagemViewModel.ViagemSelecionada);
            _dialogStore.CloseDialog(DialogResult.OK);
        }

        public override event EventHandler CanExecuteChanged;

    }
}
