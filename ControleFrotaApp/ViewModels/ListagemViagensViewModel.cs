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
    public class ListagemViagensViewModel : ViewModelBase
    {
        private readonly ViagemDataService _viagemDataService;
        private Viagem _viagemSelecionada;
        private readonly IMessaging<string> _stringMessaging;
        public ObservableCollection<Viagem> Viagens { get; set; } = new();

        public Viagem ViagemSelecionada
        {
            get => _viagemSelecionada;
            set { _viagemSelecionada = value; OnPropertyChanged(nameof(ViagemSelecionada)); }
        }

        public bool HitTestVisible { get; set; } = true;
        public ICommand NovaViagem { get; }
        public ICommand RetornoViagem { get; }
        public ICommand Imprimir { get; set; }
        public ICommand Editar { get; set; }


        public ListagemViagensViewModel(IServiceProvider serviceProvider)
        {
            _viagemDataService = serviceProvider.GetRequiredService<ViagemDataService>();
            _stringMessaging = serviceProvider.GetRequiredService<IMessaging<string>>();
            NovaViagem = new NovaViagemCommand(this, serviceProvider);
            RetornoViagem = new RetornoViagemCommand(this, serviceProvider);
            Editar = new EditarViagemCommand(this, serviceProvider);
            PreencheDataGrid();
        }

        public async Task PreencheDataGrid()
        {
            Viagens.Clear();
            _stringMessaging.Mensagem = null;
            HitTestVisible = false;
            OnPropertyChanged(nameof(HitTestVisible));
            Mouse.OverrideCursor = Cursors.Wait;
            Application.Current.Dispatcher.Invoke(() => { Mouse.OverrideCursor = Cursors.Wait; });

            foreach (Viagem viagem in await _viagemDataService.GetAllAsNoTracking())
            {
                Viagens.Add(viagem);
            }


            HitTestVisible = true;
            OnPropertyChanged(nameof(HitTestVisible));
            Application.Current.Dispatcher.Invoke(() => { Mouse.OverrideCursor = null; });
        }
    }

    public class EditarViagemCommand : ICommand
    {
        private readonly ListagemViagensViewModel _listagemViagensViewModel;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;

        public EditarViagemCommand(ListagemViagensViewModel listagemViagensViewModel, IServiceProvider serviceProvider)
        {
            _listagemViagensViewModel = listagemViagensViewModel;
            _listagemViagensViewModel.PropertyChanged += _listagemViagensViewModel_PropertyChanged; ;
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _dialogVMFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
        }

        private void _listagemViagensViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        private void _ListagemViagensViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public bool CanExecute(object parameter)
        {
            return _listagemViagensViewModel.ViagemSelecionada is not null;
        }

        public void Execute(object parameter)
        {
            _intMessaging.Mensagem = _listagemViagensViewModel.ViagemSelecionada.ID;
            _dialogGenerator.ViewModelExibido =
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.CadastroDeViagens);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _listagemViagensViewModel.PreencheDataGrid();
        }

        public event EventHandler CanExecuteChanged;
    }

    public class NovaViagemCommand : ICommand
    {
        private readonly ListagemViagensViewModel _ListagemViagensViewModel;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;

        public NovaViagemCommand(ListagemViagensViewModel ListagemViagensViewModel, IServiceProvider serviceProvider)
        {
            _ListagemViagensViewModel = ListagemViagensViewModel;
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
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.NovaViagem);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _ListagemViagensViewModel.PreencheDataGrid();
        }

        public event EventHandler CanExecuteChanged;
    }

    public class RetornoViagemCommand : ICommand
    {
        private readonly ListagemViagensViewModel _listagemViagensViewModel;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;

        public RetornoViagemCommand(ListagemViagensViewModel listagemViagensViewModel, IServiceProvider serviceProvider)
        {
            _listagemViagensViewModel = listagemViagensViewModel;
            _listagemViagensViewModel.PropertyChanged += _listagemViagensViewModel_PropertyChanged;
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _dialogVMFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
        }

        private void _listagemViagensViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public bool CanExecute(object parameter)
        {
            if (_listagemViagensViewModel.ViagemSelecionada is null) return false;
            if (_listagemViagensViewModel.ViagemSelecionada.Retorno == default) return true;
            return false;
        }

        public void Execute(object parameter)
        {
            _intMessaging.Mensagem = _listagemViagensViewModel.ViagemSelecionada.ID;
            _dialogGenerator.ViewModelExibido =
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.RetornoDeViatura);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _listagemViagensViewModel.PreencheDataGrid();
        }

        public event EventHandler CanExecuteChanged;
    }
}
