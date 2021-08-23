using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ControleFrota.Domain;
using ControleFrota.Services;
using ControleFrota.Services.DataServices;
using ControleFrota.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.ViewModels
{
    public class ListagemVeículosViewModel : ViewModelBase
    {
        private readonly VeículoDataService _veículoDataService;
        private Veículo _veículoSelecionada;
        private readonly IMessaging<string> _stringMessaging;
        public ObservableCollection<Veículo> Veículos { get; set; } = new();

        public Veículo VeículoSelecionado
        {
            get => _veículoSelecionada;
            set { _veículoSelecionada = value; OnPropertyChanged(nameof(VeículoSelecionado)); }
        }

        public bool HitTestVisible { get; set; } = true;
        public ICommand Cadastrar { get; }
        public ICommand Editar { get; }
        public ICommand SaídaDeViatura { get; set; }
        public ICommand RetornoDeViatura { get; set; }
        public ICommand AbastecerViatura { get; set; }
        public ICommand Filtrar { get; set; }
        public ICommand Inativar { get; set; }
        public ICommand Imprimir { get; set; }

        public ListagemVeículosViewModel(IServiceProvider serviceProvider)
        {
            _veículoDataService = serviceProvider.GetRequiredService<VeículoDataService>();
            _stringMessaging = serviceProvider.GetRequiredService<IMessaging<string>>();
            Cadastrar = new CadastrarNovaveículoCommand(this, serviceProvider);
            Editar = new EditarveículoCommand(this, serviceProvider);
            SaídaDeViatura = new SaídaDeViaturaCommand(this, serviceProvider);
            RetornoDeViatura = new RetornoDeViaturaCommand(this, serviceProvider);
            AbastecerViatura = new AbastecerViaturaCommand(this, serviceProvider);
            PreencheDataGrid();
            Debug.WriteLine("");
        }

        public async Task PreencheDataGrid()
        {
            Veículos.Clear();
            _stringMessaging.Mensagem = null;
            HitTestVisible = false;
            OnPropertyChanged(nameof(HitTestVisible));
            Mouse.OverrideCursor = Cursors.Wait;
            Application.Current.Dispatcher.Invoke(() => { Mouse.OverrideCursor = Cursors.Wait; });

            foreach (Veículo veículo in await _veículoDataService.GetAllAsNoTracking())
            {
                Veículos.Add(veículo);
            }

            
            HitTestVisible = true;
            OnPropertyChanged(nameof(HitTestVisible));
            Application.Current.Dispatcher.Invoke(() => { Mouse.OverrideCursor = null; });

        }
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
