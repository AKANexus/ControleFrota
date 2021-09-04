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
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.ViewModels.DialogWindows
{
    public class CadastroManutençãoViewModel : DialogContentViewModelBase
    {
        private Manutenção _manutençãoSelecionada;
        private readonly CultureInfo _ptBr = new("pt-BR");
        private readonly Regex _rgx = new("[^0-9 , .]");
        private readonly CurrentScopeStore _currentScopeStore;
        private readonly ManutençãoDataService _manutençãoDataService;
        private readonly IMessaging<int> _intMessaging;
        private readonly IMessaging<string> _stringMessaging;
        private readonly VeículoDataService _veículoDataService;
        private readonly MotoristaDataService _motoristaDataService;

        public Manutenção ManutençãoSelecionada
        {
            get => _manutençãoSelecionada;
            set
            {
                _manutençãoSelecionada = value;
                OnPropertyChanged(nameof(ManutençãoSelecionada));
            }
        }
        public ObservableCollection<Motorista> Motoristas { get; } = new();
        public ObservableCollection<Veículo> Veículos { get; } = new();

        public Motorista MotoristaSelecionado
        {
            get => ManutençãoSelecionada?.Motorista;
            set
            {
                ManutençãoSelecionada.Motorista = value;
                OnPropertyChanged(nameof(MotoristaSelecionado));
            }
        }

        public Veículo VeículoSelecionado
        {
            get => ManutençãoSelecionada?.Veículo;
            set
            {
                ManutençãoSelecionada.Veículo = value;
                OnPropertyChanged(nameof(VeículoSelecionado));
            }
        }

        public TipoReparo TipoReparoSelecionado
        {
            get => ManutençãoSelecionada?.TipoReparo ?? TipoReparo.Reparativo;
            set
            {
                ManutençãoSelecionada.TipoReparo = value;
                OnPropertyChanged(nameof(TipoReparoSelecionado));
            }
        }
        public ÁreaManutenção AreaReparoSelecionada
        {
            get => ManutençãoSelecionada?.ÁreaManutenção ?? ÁreaManutenção.Outros;
            set
            {
                ManutençãoSelecionada.ÁreaManutenção = value;
                OnPropertyChanged(nameof(AreaReparoSelecionada));
            }
        }

        public TipoManutenção TipoManutençãoSelecionada
        {
            get => ManutençãoSelecionada?.TipoManutenção ?? TipoManutenção.Revisão;
            set
            {
                ManutençãoSelecionada.TipoManutenção = value;
                OnPropertyChanged(nameof(TipoManutençãoSelecionada));
            }
        }

        public DateTime DataHora
        {
            get => _manutençãoSelecionada?.DataHora ?? DateTime.Now;
            set
            {
                _manutençãoSelecionada.DataHora = value;
                OnPropertyChanged(nameof(DataHora));
            }
        }
        public string Local
        {
            get => ManutençãoSelecionada?.Local;
            set
            {
                ManutençãoSelecionada.Local = value;
                OnPropertyChanged(nameof(Local));
            }
        }

        public string Valor
        {
            get => (_manutençãoSelecionada?.Preço ?? default).ToString("C2");
            set
            {
                string regexReplace = _rgx.Replace(value, "");
                _manutençãoSelecionada.Preço = regexReplace.Replace(".", ",").Safedecimal(_ptBr);
                OnPropertyChanged(nameof(Valor));
            }
        }
        //public string Peça
        //{
        //    get => ManutençãoSelecionada?.Peça;
        //    set
        //    {
        //        ManutençãoSelecionada.Peça = value;
        //        OnPropertyChanged(nameof(Peça));
        //    }
        //}

        public string Observações
        {
            get => ManutençãoSelecionada?.Observações;
            set
            {
                ManutençãoSelecionada.Observações = value;
                OnPropertyChanged(nameof(Observações));
            }
        }

        public string KM
        {
            get => (_manutençãoSelecionada?.KM ?? default).ToString("F2") + " km";
            set
            {
                string regexReplace = _rgx.Replace(value, "");
                _manutençãoSelecionada.KM = regexReplace.Replace(".", ",").Safedecimal(_ptBr);
                OnPropertyChanged(nameof(KM));
            }
        }

        public ICommand Salvar { get; set; }
        public ICommand Cancelar { get; set; }


        public CadastroManutençãoViewModel(IServiceProvider serviceProvider)
        {
            _currentScopeStore = serviceProvider.GetRequiredService<CurrentScopeStore>();
            _currentScopeStore.CriaNovoEscopo();

            Salvar = new SalvarManutençãoCommand(this, serviceProvider, x => MessageBox.Show(x.Message));
            Cancelar = new CloseCurrentWindowCommand(serviceProvider);

            _manutençãoDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<ManutençãoDataService>();
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _stringMessaging = serviceProvider.GetRequiredService<IMessaging<string>>();
            //_combustivelDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<CombustívelDataService>();
            _veículoDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<VeículoDataService>();
            _motoristaDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<MotoristaDataService>();

            PreencheCampos();
        }

        private async Task PreencheCampos()
        {
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
                ManutençãoSelecionada = new Manutenção()
                {
                    DataHora = DateTime.Now
                };
                if (!string.IsNullOrWhiteSpace(_stringMessaging.Mensagem))
                {
                    ManutençãoSelecionada.Veículo = await _veículoDataService.GetVeículoByPlaca(_stringMessaging.Mensagem);
                    OnPropertyChanged(nameof(VeículoSelecionado));
                }

                return;
            }
            ManutençãoSelecionada = await _manutençãoDataService.GetManutençãoByID(_intMessaging.Mensagem);
            OnPropertyChanged(null);
        }

    }

    public class SalvarManutençãoCommand : AsyncCommandBase
    {
        private readonly CadastroManutençãoViewModel _cadastroManutençãoViewModel;
        private readonly CurrentScopeStore _currentScope;
        private readonly ManutençãoDataService _manutençãoDataService;
        private readonly IDialogsStore _dialogStore;

        public SalvarManutençãoCommand(CadastroManutençãoViewModel cadastroManutençãoViewModel, IServiceProvider serviceProvider, Action<Exception> onException) : base(onException)
        {
            _cadastroManutençãoViewModel = cadastroManutençãoViewModel;
            _cadastroManutençãoViewModel.PropertyChanged += _cadastroManutençãoViewModel_PropertyChanged;
            _currentScope = serviceProvider.GetRequiredService<CurrentScopeStore>();
            _manutençãoDataService = _currentScope.PegaEscopoAtual().GetRequiredService<ManutençãoDataService>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
        }

        private void _cadastroManutençãoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public override event EventHandler CanExecuteChanged;

        public override bool CanExecute(object parameter)
        {
            if (_cadastroManutençãoViewModel.MotoristaSelecionado is null) return false;
            if (_cadastroManutençãoViewModel.VeículoSelecionado is null) return false;
            if (_cadastroManutençãoViewModel.ManutençãoSelecionada.KM == default) return false;
            if (string.IsNullOrWhiteSpace(_cadastroManutençãoViewModel.Local)) return false;
            //if (string.IsNullOrWhiteSpace(_cadastroManutençãoViewModel.Peça)) return false;
            if (_cadastroManutençãoViewModel.ManutençãoSelecionada.Preço == default) return false;
            return true;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await _manutençãoDataService.AddOrUpdate(_cadastroManutençãoViewModel.ManutençãoSelecionada);
            _dialogStore.CloseDialog(DialogResult.OK);
        }
    }
}
