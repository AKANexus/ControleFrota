using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ControleFrota.Commands;
using ControleFrota.Domain;
using ControleFrota.Extensions;
using ControleFrota.Services;
using ControleFrota.Services.DataServices;
using ControleFrota.State;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.ViewModels.DialogWindows
{
    public class CadastroAbastecimentoViewModel : DialogContentViewModelBase
    {
        private readonly CurrentScopeStore _currentScopeStore;
        private readonly AbastecimentoDataService _abastecimentoDataService;
        private readonly IMessaging<int> _intMessaging;
        //private readonly CombustívelDataService _combustivelDataService;
        private readonly VeículoDataService _veículoDataService;
        private readonly MotoristaDataService _motoristaDataService;
        private readonly IMessaging<string> _stringMessaging;
        private Abastecimento _abastecimentoSelecionado;
        private readonly CultureInfo _ptBr = new("pt-BR");
        private readonly Regex _rgx = new("[^0-9 , .]");

        public Combustíveis CombustívelSelecionado
        {
            get => AbastecimentoSelecionado?.Combustível ?? Combustíveis.GasolinaComum;
            set
            {
                AbastecimentoSelecionado.Combustível = value;
                OnPropertyChanged(nameof(CombustívelSelecionado));
            }
        }

        //public ObservableCollection<Combustível> Combustívels { get; set; } = new();

        public Abastecimento AbastecimentoSelecionado
        {
            get => _abastecimentoSelecionado;
            set { _abastecimentoSelecionado = value; OnPropertyChanged(nameof(AbastecimentoSelecionado)); }
        }

        public Motorista MotoristaSelecionado
        {
            get => AbastecimentoSelecionado?.Motorista;
            set
            {
                AbastecimentoSelecionado.Motorista = value;
                OnPropertyChanged(nameof(MotoristaSelecionado));
            }
        }

        public ObservableCollection<Motorista> Motoristas { get; } = new();
        public Veículo VeículoSelecionado
        {
            get => AbastecimentoSelecionado?.Veículo;
            set
            {
                AbastecimentoSelecionado.Veículo = value;
                OnPropertyChanged(nameof(VeículoSelecionado));
            }
        }

        public ObservableCollection<Veículo> Veículos { get; } = new();
        public DateTime DataHora
        {
            get => AbastecimentoSelecionado?.DataHora ?? DateTime.Now;
            set
            {
                AbastecimentoSelecionado.DataHora = value;
                OnPropertyChanged(nameof(DataHora));
            }
        }
        public string KM
        {
            get => (AbastecimentoSelecionado?.KM ?? default).ToString("F2")+" km";
            set
            {
                string regexReplace = _rgx.Replace(value, "");
                AbastecimentoSelecionado.KM = regexReplace.Replace(".", ",").Safedecimal(_ptBr);
                OnPropertyChanged(nameof(KM));
            }
        }

        public string Posto
        {
            get => AbastecimentoSelecionado?.Posto;
            set
            {
                AbastecimentoSelecionado.Posto = value;
                OnPropertyChanged(nameof(Posto));
            }
        }

        public string ValorTotal
        {
            get => (AbastecimentoSelecionado?.ValorTotal ?? default).ToString("C2");
            set
            {
                string regexReplace = _rgx.Replace(value, "");
                AbastecimentoSelecionado.ValorTotal = regexReplace.Replace(".", ",").Safedecimal(_ptBr);
                OnPropertyChanged(nameof(ValorTotal));
            }
        }

        public string Litragem
        {
            get => (AbastecimentoSelecionado?.Litragem ?? default).ToString("F2") + " l";
            set
            {
                string regexReplace = _rgx.Replace(value, "");
                AbastecimentoSelecionado.Litragem = regexReplace.Replace(".", ",").Safedecimal(_ptBr);
                OnPropertyChanged(nameof(Litragem));
            }
        }

        public ICommand SalvaAbastecimento { get; set; }
        public ICommand CloseCurrentWindow { get; set; }

        public FormasPagamento FormaPagamento { get; set; }

        public CadastroAbastecimentoViewModel(IServiceProvider serviceProvider)
        {
            _currentScopeStore = serviceProvider.GetRequiredService<CurrentScopeStore>();
            _currentScopeStore.CriaNovoEscopo();

            CloseCurrentWindow = new CloseCurrentWindowCommand(serviceProvider);
            SalvaAbastecimento = new SalvaAbastecimento(this, serviceProvider, (x) => MessageBox.Show(x.Message));

            _abastecimentoDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<AbastecimentoDataService>();
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _stringMessaging = serviceProvider.GetRequiredService<IMessaging<string>>();
            //_combustivelDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<CombustívelDataService>();
            _veículoDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<VeículoDataService>();
            _motoristaDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<MotoristaDataService>();

            PreencheCampos();
        }

        private async Task PreencheCampos()
        {
            //foreach (Combustível combustível in await _combustivelDataService.GetAll())
            //{
            //    Combustívels.Add(combustível);
            //}
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
                AbastecimentoSelecionado = new Abastecimento()
                {
                    DataHora = DateTime.Now
                };
                if (!string.IsNullOrWhiteSpace(_stringMessaging.Mensagem))
                {
                    AbastecimentoSelecionado.Veículo = await _veículoDataService.GetVeículoByPlaca(_stringMessaging.Mensagem);
                    OnPropertyChanged(nameof(VeículoSelecionado));
                }

                return;
            }
            AbastecimentoSelecionado = await _abastecimentoDataService.GetByID(_intMessaging.Mensagem);
            OnPropertyChanged(null);
        }
    }

    public class SalvaAbastecimento : AsyncCommandBase
    {
        private readonly CadastroAbastecimentoViewModel _cadastroAbastecimentoViewModel;
        private readonly CurrentScopeStore _currentScope;
        private readonly IDialogsStore _dialogStore;
        private readonly AbastecimentoDataService _abastecimentoDataService;

        public SalvaAbastecimento(CadastroAbastecimentoViewModel cadastroAbastecimentoViewModel, IServiceProvider serviceProvider, Action<Exception> onException) : base(onException)
        {
            _cadastroAbastecimentoViewModel = cadastroAbastecimentoViewModel;
            _cadastroAbastecimentoViewModel.PropertyChanged += _cadastroAbastecimentoViewModel_PropertyChanged;
            _currentScope = serviceProvider.GetRequiredService<CurrentScopeStore>();
            _abastecimentoDataService = _currentScope.PegaEscopoAtual().GetRequiredService<AbastecimentoDataService>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
        }

        private void _cadastroAbastecimentoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await _abastecimentoDataService.AddOrUpdate(_cadastroAbastecimentoViewModel.AbastecimentoSelecionado);
            _dialogStore.CloseDialog(DialogResult.OK);
        }

        public override event EventHandler CanExecuteChanged;

        public override bool CanExecute(object parameter)
        {
            if (_cadastroAbastecimentoViewModel.MotoristaSelecionado is null) return false;
            if (_cadastroAbastecimentoViewModel.VeículoSelecionado is null) return false;
            if (_cadastroAbastecimentoViewModel.AbastecimentoSelecionado.KM == default) return false;
            if (string.IsNullOrWhiteSpace(_cadastroAbastecimentoViewModel.Posto)) return false;
            if (_cadastroAbastecimentoViewModel.AbastecimentoSelecionado.ValorPorLitro == default) return false;
            if (_cadastroAbastecimentoViewModel.Litragem == default) return false;
            return true;

        }
    }

}
