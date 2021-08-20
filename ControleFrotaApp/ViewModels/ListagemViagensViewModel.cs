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
        public ObservableCollection<Viagem> Viagens { get; set; } = new();

        public Viagem ViagemSelecionada
        {
            get => _viagemSelecionada;
            set { _viagemSelecionada = value; OnPropertyChanged(nameof(ViagemSelecionada)); }
        }

        public bool HitTestVisible { get; set; } = true;
        public ICommand Cadastrar { get; }
        public ICommand Editar { get; }

        public ListagemViagensViewModel(IServiceProvider serviceProvider)
        {
            _viagemDataService = serviceProvider.GetRequiredService<ViagemDataService>();

            Cadastrar = new CadastrarNovoVeículoCommand(this, serviceProvider);
            Editar = new EditarVeículoCommand(this, serviceProvider);
            PreencheDataGrid();
            Debug.WriteLine("");
        }

        public async Task PreencheDataGrid()
        {
            Viagens.Clear();

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

    public class EditarVeículoCommand : ICommand
    {
        private readonly ListagemViagensViewModel _ListagemViagensViewModel;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;

        public EditarVeículoCommand(ListagemViagensViewModel ListagemViagensViewModel, IServiceProvider serviceProvider)
        {
            _ListagemViagensViewModel = ListagemViagensViewModel;
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _dialogVMFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
            _ListagemViagensViewModel.PropertyChanged += _ListagemViagensViewModel_PropertyChanged;
        }

        private void _ListagemViagensViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public bool CanExecute(object parameter)
        {
            return _ListagemViagensViewModel.ViagemSelecionada is not null;
        }

        public void Execute(object parameter)
        {
            _intMessaging.Mensagem = _ListagemViagensViewModel.ViagemSelecionada.ID;
            _dialogGenerator.ViewModelExibido =
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.CadastroDeViagens);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _ListagemViagensViewModel.PreencheDataGrid();
        }

        public event EventHandler CanExecuteChanged;
    }

    public class CadastrarNovoVeículoCommand : ICommand
    {
        private readonly ListagemViagensViewModel _ListagemViagensViewModel;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;

        public CadastrarNovoVeículoCommand(ListagemViagensViewModel ListagemViagensViewModel, IServiceProvider serviceProvider)
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
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.CadastroDeViagens);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _ListagemViagensViewModel.PreencheDataGrid();
        }

        public event EventHandler CanExecuteChanged;
    }
}
