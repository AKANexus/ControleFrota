using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ControleFrota.Auxiliares;
using ControleFrota.Commands;
using ControleFrota.Domain;
using ControleFrota.Extensions;
using ControleFrota.Services;
using ControleFrota.Services.DataServices;
using ControleFrota.State;
using ControleFrota.ViewModels.DialogWindows;
using ControleFrota.ViewModels.Factories;
using FastReport;
using FastReport.Data;
using FastReport.Export.Html;
using FastReport.Export.PdfSimple;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.ViewModels
{
    public class ListagemVeículosViewModel : ViewModelBase
    {
        private readonly VeículoDataService _veículoDataService;
        private Veículo _veículoSelecionado;
        private readonly IMessaging<string> _stringMessaging;

        public FilterCollectionView FCV = new();
        public FilteringInfo CurrentFilteringInfo { get; set; }
        private StringBuilder _filtrosAplicados = new();
        //public ObservableCollection<Veículo> Veículos { get; set; } = new();
        //private CollectionViewSource _collectionViewSource;
        public Veículo VeículoSelecionado
        {
            get => _veículoSelecionado;
            set
            {
                _veículoSelecionado = value;
                OnPropertyChanged(nameof(VeículoSelecionado));
            }
        }

        public ICollectionView VeículosView { get; set; }

        public string FiltrosAplicados
        {
            get => _filtrosAplicados?.ToString();
        }

        public bool HitTestVisible { get; set; } = true;
        public ICommand Cadastrar { get; }
        public ICommand Editar { get; }
        public ICommand SaídaDeViatura { get; set; }
        public ICommand RetornoDeViatura { get; set; }
        public ICommand AbastecerViatura { get; set; }
        public ICommand ManutençãoDeViatura { get; set; }
        public ICommand Filtrar { get; set; }
        public ICommand Inativar { get; set; }
        public ICommand Imprimir { get; set; }
        public ICommand GerarRelatório { get; set; }

        public ListagemVeículosViewModel(IServiceProvider serviceProvider)
        {
            _veículoDataService = serviceProvider.GetRequiredService<VeículoDataService>();
            _stringMessaging = serviceProvider.GetRequiredService<IMessaging<string>>();
            Cadastrar = new CadastrarNovaveículoCommand(this, serviceProvider);
            Editar = new EditarveículoCommand(this, serviceProvider);
            SaídaDeViatura = new SaídaDeViaturaCommand(this, serviceProvider);
            RetornoDeViatura = new RetornoDeViaturaCommand(this, serviceProvider);
            AbastecerViatura = new AbastecerViaturaCommand(this, serviceProvider);
            ManutençãoDeViatura = new ManutençãoDeViaturaCommand(this, serviceProvider);
            Imprimir = new FakeCommand();
            Filtrar = new FiltrarVeículosCommand(this, serviceProvider);
            GerarRelatório = new GerarRelatórioVeículoCommand(this, serviceProvider, exception => { });
            PreencheDataGrid();

        }

        public void AlteraFiltrosAplicadosString(FilteringInfo fi)
        {
            if (fi.FilterInfo.StartsWith("clearfilters"))
            {
                _filtrosAplicados.Clear();
            }
            else
            {
                _filtrosAplicados.Append(fi.Property.Name);
                _filtrosAplicados.Append("=");
                _filtrosAplicados.Append(fi.FilterInfo);
                _filtrosAplicados.Append(";");
            }
            OnPropertyChanged(nameof(FiltrosAplicados));
        }

        public async Task PreencheDataGrid()
        {
            //Veículos.Clear();
            _stringMessaging.Mensagem = null;
            HitTestVisible = false;
            OnPropertyChanged(nameof(HitTestVisible));
            Mouse.OverrideCursor = Cursors.Wait;
            Application.Current.Dispatcher.Invoke(() => { Mouse.OverrideCursor = Cursors.Wait; });

            try
            {
                //foreach (Veículo veículo in await _veículoDataService.GetAllAsNoTracking())
                //{
                //    Veículos.Add(veículo);
                //}

                //VeículosView = CollectionViewSource.GetDefaultView(await _veículoDataService.GetAllAsNoTracking());
                //_collectionViewSource = new CollectionViewSource
                //    { Source = await _veículoDataService.GetAllAsNoTracking() };
                //VeículosView = _collectionViewSource.View;
                VeículosView = new ListCollectionView(await _veículoDataService.GetAllAsNoTracking());
                if (CurrentFilteringInfo is not null)
                    VeículosView.Filter += FCV.AddNewFilter(CurrentFilteringInfo);
                OnPropertyChanged(nameof(VeículosView));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                HitTestVisible = true;
                OnPropertyChanged(nameof(HitTestVisible));
                Application.Current.Dispatcher.Invoke(() => { Mouse.OverrideCursor = null; });
            }
        }
    }

    public class GerarRelatórioVeículoCommand : AsyncCommandBase
    {
        private readonly ListagemVeículosViewModel _listagemVeículosViewModel;
        private readonly VeículoDataService _veículoDataService;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogViewModelFactory;
        private readonly IDialogsStore _dialogStore;
        private readonly IMessaging<int> _intMessaging;


        public GerarRelatórioVeículoCommand(ListagemVeículosViewModel listagemVeículosViewModel, IServiceProvider serviceProvider, Action<Exception> onException) : base(onException)
        {
            _listagemVeículosViewModel = listagemVeículosViewModel;
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
            _dialogViewModelFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
        }

        public override bool CanExecute(object parameter)
        {
            return _listagemVeículosViewModel.VeículoSelecionado is not null;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            _intMessaging.Mensagem = _listagemVeículosViewModel.VeículoSelecionado.ID;
            _dialogGenerator.ViewModelExibido =
                _dialogViewModelFactory.CreateDialogContentViewModel(TipoDialogue.RelatórioVeículo);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _listagemVeículosViewModel.PreencheDataGrid();
        }


        public override event EventHandler CanExecuteChanged;
    }


    public class ManutençãoDeViaturaCommand : ICommand
    {
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;
        private readonly IMessaging<int> _intMessaging;
        private readonly ListagemVeículosViewModel _listagemVeículosViewModel;
        private readonly IMessaging<string> _stringMessaging;
        public ManutençãoDeViaturaCommand(ListagemVeículosViewModel listagemVeículosViewModel, IServiceProvider serviceProvider)
        {
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _stringMessaging = serviceProvider.GetRequiredService<IMessaging<string>>();
            _listagemVeículosViewModel = listagemVeículosViewModel;
            _listagemVeículosViewModel.PropertyChanged += _listagemVeículosViewModel_PropertyChanged; ;
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _dialogVMFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
        }

        private void _listagemVeículosViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _stringMessaging.Mensagem = _listagemVeículosViewModel.VeículoSelecionado.Placa;
            _intMessaging.Mensagem = default;
            _dialogGenerator.ViewModelExibido =
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.CadastroDeManutenção);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _listagemVeículosViewModel.PreencheDataGrid();
        }

        public event EventHandler? CanExecuteChanged;
    }

    public class FiltrarVeículosCommand : ICommand
    {
        private readonly ListagemVeículosViewModel _listagemVeículosViewModel;
        private readonly IDialogsStore _dialogStore;
        private readonly IDialogViewModelFactory _dialogViewModelFactory;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly EntityStore<EntityBase> _entityStore;
        private readonly IMessaging<Type> _typeMessaging;

        public FiltrarVeículosCommand(ListagemVeículosViewModel listagemVeículosViewModel, IServiceProvider serviceProvider)
        {
            _listagemVeículosViewModel = listagemVeículosViewModel;
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
            _dialogViewModelFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _typeMessaging = serviceProvider.GetRequiredService<IMessaging<Type>>();
            _entityStore = serviceProvider.GetRequiredService<EntityStore<EntityBase>>();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _typeMessaging.Mensagem = typeof(Veículo);
            _dialogGenerator.ViewModelExibido =
                _dialogViewModelFactory.CreateDialogContentViewModel(TipoDialogue.Filtros);
            _dialogStore.RegisterDialog(_dialogGenerator);
            if (_dialogGenerator.Resultado == DialogResult.OK)
            {
                _listagemVeículosViewModel.VeículosView.Filter += _listagemVeículosViewModel.FCV.AddNewFilter(_entityStore.Entity);
                _listagemVeículosViewModel.CurrentFilteringInfo = _entityStore.Entity as FilteringInfo;
                if (_entityStore.Entity is FilteringInfo fi)
                {
                    _listagemVeículosViewModel.AlteraFiltrosAplicadosString(fi);
                }
            }
        }

        public event EventHandler CanExecuteChanged;
    }

    public class AbastecerViaturaCommand : ICommand
    {
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;
        private readonly IMessaging<int> _intMessaging;
        private readonly ListagemVeículosViewModel _listagemVeículosViewModel;
        private readonly IMessaging<string> _stringMessaging;


        public AbastecerViaturaCommand(ListagemVeículosViewModel listagemVeículosViewModel, IServiceProvider serviceProvider)
        {
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _stringMessaging = serviceProvider.GetRequiredService<IMessaging<string>>();
            _listagemVeículosViewModel = listagemVeículosViewModel;
            _listagemVeículosViewModel.PropertyChanged += _listagemVeículosViewModel_PropertyChanged;
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _dialogVMFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
        }

        private void _listagemVeículosViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _stringMessaging.Mensagem = _listagemVeículosViewModel.VeículoSelecionado.Placa;
            _intMessaging.Mensagem = default;
            _dialogGenerator.ViewModelExibido =
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.CadastroDeAbastecimento);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _listagemVeículosViewModel.PreencheDataGrid();
        }

        public event EventHandler? CanExecuteChanged;
    }

    public class RetornoDeViaturaCommand : ICommand
    {
        private readonly ListagemVeículosViewModel _listagemVeículosViewModel;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;

        public RetornoDeViaturaCommand(ListagemVeículosViewModel listagemVeículosViewModel, IServiceProvider serviceProvider)
        {
            _listagemVeículosViewModel = listagemVeículosViewModel;
            _listagemVeículosViewModel.PropertyChanged += _listagemVeículosViewModel_PropertyChanged; ;
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _dialogVMFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();


        }

        private void _listagemVeículosViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public bool CanExecute(object? parameter)
        {
            if (_listagemVeículosViewModel.VeículoSelecionado is null) return false;
            if (!_listagemVeículosViewModel.VeículoSelecionado.EmUso) return false;
            if (!_listagemVeículosViewModel.VeículoSelecionado.Ativo) return false;

            return true;
        }

        public void Execute(object? parameter)
        {
            _intMessaging.Mensagem = _listagemVeículosViewModel.VeículoSelecionado.IdViagemEmAberto;
            _dialogGenerator.ViewModelExibido =
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.RetornoDeViatura);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _listagemVeículosViewModel.PreencheDataGrid();
        }

        public event EventHandler? CanExecuteChanged;
    }

    public class SaídaDeViaturaCommand : ICommand
    {
        private readonly ListagemVeículosViewModel _listagemVeículosViewModel;
        private readonly IMessaging<int> _intMessaging;
        private readonly IMessaging<string> _stringMessaging;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;

        public SaídaDeViaturaCommand(ListagemVeículosViewModel listagemVeículosViewModel, IServiceProvider serviceProvider)
        {
            _listagemVeículosViewModel = listagemVeículosViewModel;
            _listagemVeículosViewModel.PropertyChanged += _listagemVeículosViewModel_PropertyChanged;
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _stringMessaging = serviceProvider.GetRequiredService<IMessaging<string>>();
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _dialogVMFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();

        }

        private void _listagemVeículosViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public bool CanExecute(object? parameter)
        {
            if (_listagemVeículosViewModel.VeículoSelecionado is null) return false;
            if (_listagemVeículosViewModel.VeículoSelecionado.EmUso) return false;
            if (!_listagemVeículosViewModel.VeículoSelecionado.Ativo) return false;

            return true;
        }

        public void Execute(object? parameter)
        {
            _intMessaging.Mensagem = default;
            _stringMessaging.Mensagem = _listagemVeículosViewModel.VeículoSelecionado.Placa;
            _dialogGenerator.ViewModelExibido =
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.NovaViagem);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _listagemVeículosViewModel.PreencheDataGrid();

        }

        public event EventHandler? CanExecuteChanged;
    }

    public class EditarveículoCommand : ICommand
    {
        private readonly ListagemVeículosViewModel _listagemVeículosViewModel;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;

        public EditarveículoCommand(ListagemVeículosViewModel listagemVeículosViewModel, IServiceProvider serviceProvider)
        {
            _listagemVeículosViewModel = listagemVeículosViewModel;
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _dialogVMFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
            _listagemVeículosViewModel.PropertyChanged += _listagemVeículosViewModel_PropertyChanged;
        }

        private void _listagemVeículosViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public bool CanExecute(object parameter)
        {
            return _listagemVeículosViewModel.VeículoSelecionado is not null;
        }

        public void Execute(object parameter)
        {
            _intMessaging.Mensagem = _listagemVeículosViewModel.VeículoSelecionado.ID;
            _dialogGenerator.ViewModelExibido =
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.CadastroDeVeículos);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _listagemVeículosViewModel.PreencheDataGrid();
        }

        public event EventHandler CanExecuteChanged;
    }

    public class CadastrarNovaveículoCommand : ICommand
    {
        private readonly ListagemVeículosViewModel _listagemVeículosViewModel;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;

        public CadastrarNovaveículoCommand(ListagemVeículosViewModel listagemVeículosViewModel, IServiceProvider serviceProvider)
        {
            _listagemVeículosViewModel = listagemVeículosViewModel;
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _dialogVMFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _intMessaging.Mensagem = default;
            _dialogGenerator.ViewModelExibido =
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.CadastroDeVeículos);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _listagemVeículosViewModel.PreencheDataGrid();
        }

        public event EventHandler CanExecuteChanged;
    }
}
