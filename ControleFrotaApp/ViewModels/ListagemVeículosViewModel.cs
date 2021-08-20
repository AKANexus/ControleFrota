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
        private Veículo _veículoSelecionado;
        public ObservableCollection<Veículo> Veículos { get; set; } = new();

        public Veículo VeículoSelecionado
        {
            get => _veículoSelecionado;
            set { _veículoSelecionado = value; OnPropertyChanged(nameof(VeículoSelecionado)); }
        }

        public bool HitTestVisible { get; set; } = true;
        public ICommand Cadastrar { get; }
        public ICommand Editar { get; }

        public ListagemVeículosViewModel(IServiceProvider serviceProvider)
        {
            _veículoDataService = serviceProvider.GetRequiredService<VeículoDataService>();

            Cadastrar = new CadastrarNovoVeículoCommand(this, serviceProvider);
            Editar = new EditarVeículoCommand(this, serviceProvider);
            PreencheDataGrid();
            Debug.WriteLine("");
        }

        public async Task PreencheDataGrid()
        {
            Veículos.Clear();

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

    public class EditarVeículoCommand : ICommand
    {
        private readonly ListagemVeículosViewModel _listagemVeículosViewModel;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;

        public EditarVeículoCommand(ListagemVeículosViewModel listagemVeículosViewModel, IServiceProvider serviceProvider)
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

    public class CadastrarNovoVeículoCommand : ICommand
    {
        private readonly ListagemVeículosViewModel _listagemVeículosViewModel;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;

        public CadastrarNovoVeículoCommand(ListagemVeículosViewModel listagemVeículosViewModel, IServiceProvider serviceProvider)
        {
            _listagemVeículosViewModel = listagemVeículosViewModel;
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _dialogVMFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
        }

        public bool CanExecute(object parameter)
        {
            //return _listagemVeículosViewModel.VeículoSelecionado is not null;
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
