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
    public class CadastroVeículoViewModel : DialogContentViewModelBase
    {
        private readonly CurrentScopeStore _currentScopeStore;
        private readonly VeículoDataService _veículoDataService;
        private readonly MarcaDataService _marcaDataService;
        private readonly ModeloDataService _modeloDataService;

        private readonly IMessaging<int> _intMessaging;
        //private readonly EntityStore<Veículo> _veículoEntityStore;

        public Veículo VeículoSelecionado { get; set; }


        public ObservableCollection<Marca> Marcas { get; set; } = new();

        public ObservableCollection<Modelo> Modelos { get; set; } = new();

        public ICommand CloseCurrentWindow { get; set; }

        public ICommand SalvaVeículo { get; set; }

        public Marca MarcaSelecionada
        {
            get
            {
                return VeículoSelecionado?.Marca;
            }
            set
            {
                VeículoSelecionado.Marca = value;
                OnPropertyChanged(nameof(MarcaSelecionada));
                PreencheModelos();
            }
        }
        public Modelo ModeloSelecionado
        {
            get
            {
                return VeículoSelecionado?.Modelo;
            }
            set
            {
                VeículoSelecionado.Modelo = value;
                OnPropertyChanged(nameof(ModeloSelecionado));
            }
        }
        public string PlacaVeículo
        {
            get
            {
                return VeículoSelecionado?.Placa;
            }
            set
            {
                string placaDigitada = value;
                placaDigitada = placaDigitada.Replace("-", "");
                if (placaDigitada.Length != 7 || VeículoSelecionado is null) return;
                VeículoSelecionado.Placa = $"{placaDigitada.Substring(0, 3)}-{placaDigitada.Substring(3, 4)}";
                OnPropertyChanged(nameof(PlacaVeículo));
            }
        }
        public string RENAVAM
        {
            get => VeículoSelecionado?.RENAVAM;
            set
            {
                VeículoSelecionado.RENAVAM = value;
                OnPropertyChanged(nameof(RENAVAM));
            }
        }

        public DateTime ÚltimoLicenciamento
        {
            get => VeículoSelecionado?.ÚltimoLicenciamento ?? DateTime.Now;
            set
            {
                VeículoSelecionado.ÚltimoLicenciamento = value;
                OnPropertyChanged(nameof(ÚltimoLicenciamento));
            }
        }


        public CadastroVeículoViewModel(IServiceProvider serviceProvider)
        {
            _currentScopeStore = serviceProvider.GetRequiredService<CurrentScopeStore>();
            _currentScopeStore.CriaNovoEscopo();

            CloseCurrentWindow = new CloseCurrentWindowCommand(serviceProvider);
            SalvaVeículo = new SalvaVeículoCommand(this, serviceProvider, (x) => MessageBox.Show(x.Message));

            _veículoDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<VeículoDataService>();
            _marcaDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<MarcaDataService>();
            _modeloDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<ModeloDataService>();

            //_veículoEntityStore = _currentScopeStore.PegaEscopoAtual().GetRequiredService<EntityStore<Veículo>>();
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();


            PreencheCampos();
        }

        private async Task PreencheCampos()
        {
            foreach (Marca marca in await _marcaDataService.GetAll())
            {
                Marcas.Add(marca);
            }

            if (_intMessaging.Mensagem == default)
            {
                VeículoSelecionado = new Veículo();
                return;
            }

            VeículoSelecionado = await _veículoDataService.GetViagemByID(_intMessaging.Mensagem);
        }

        private async Task PreencheModelos()
        {
            foreach (Modelo modelo in await _modeloDataService.GetAllByMarca(VeículoSelecionado.Marca))
            {
                Modelos.Add(modelo);
            }
        }
    }

    public class SalvaVeículoCommand : AsyncCommandBase
    {
        private readonly CadastroVeículoViewModel _cadastroVeículoViewModel;
        private readonly CurrentScopeStore _currentScope;
        private readonly VeículoDataService _veículoDataService;
        private readonly IDialogsStore _dialogStore;

        public SalvaVeículoCommand(CadastroVeículoViewModel cadastroVeículoViewModel, IServiceProvider serviceProvider, Action<Exception> onException) : base(onException)
        {
            _cadastroVeículoViewModel = cadastroVeículoViewModel;
            _cadastroVeículoViewModel.PropertyChanged += _cadastroVeículoViewModel_PropertyChanged;
            _currentScope = serviceProvider.GetRequiredService<CurrentScopeStore>();
            _veículoDataService = _currentScope.PegaEscopoAtual().GetRequiredService<VeículoDataService>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
        }

        private void _cadastroVeículoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public override bool CanExecute(object parameter)
        {
            if (String.IsNullOrWhiteSpace(_cadastroVeículoViewModel.VeículoSelecionado?.Placa)) return false;
            if (String.IsNullOrWhiteSpace(_cadastroVeículoViewModel.VeículoSelecionado?.RENAVAM)) return false;
            if (_cadastroVeículoViewModel.VeículoSelecionado?.Modelo is null) return false;
            if (_cadastroVeículoViewModel.VeículoSelecionado?.Marca is null) return false;
            return true;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await _veículoDataService.AddOrUpdate(_cadastroVeículoViewModel.VeículoSelecionado);
            _dialogStore.CloseDialog(DialogResult.OK);
        }

        public override event EventHandler CanExecuteChanged;
    }
}
