using System;
using System.Windows;
using System.Windows.Input;
using ControleFrota.Commands;
using ControleFrota.State;
using ControleFrota.State.Navigators;
using ControleFrota.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;
using ControleFrota.Services;

namespace ControleFrota.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly BusyStateStore _busyStateStore;

        public MainViewModel(INavigator navigator, IViewModelFactory viewModelFactory, IServiceProvider serviceProvider)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;

            //_navigator.StateChanged += _navigator_StateChanged;
            _busyStateStore = serviceProvider.GetRequiredService<BusyStateStore>();
            _busyStateStore.PropertyChanged += _busyStateStore_PropertyChanged;
            //EditaFormasPagamento = new DialogoFormasDePagamentoCommand(serviceProvider);
            //EditaFuncionários = new UpdateViewModelAtualCommand(navigator, _viewModelFactory);
            UpdateViewModelAtual = new UpdateViewModelAtualCommand(navigator, _viewModelFactory);
            //EditaEmitente = new EditaEmitenteCommand(serviceProvider);
            //AlteraSenhaFuncionario = new AlterarSenhaFuncionárioCommand(this, serviceProvider);
            UpdateViewModelAtual.Execute(TipoView.TelaInicial);
        }

        private void _busyStateStore_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HitTestVisible));
        }

        public ViewModelBase ViewModelAtual => _navigator.ViewModelAtual;
        public Visibility MenuStripVisibility { get; set; } = Visibility.Collapsed;

        public ICommand UpdateViewModelAtual { get; set; }
        public ICommand EditaFormasPagamento { get; set; }
        public ICommand EditaFuncionários { get; set; }
        public ICommand EditaEmitente { get; set; }
        public bool HitTestVisible
        {
            get => !_busyStateStore.IsBusy;
        }

        public ICommand AlteraSenhaFuncionario { get; set; }

        //private void _navigator_StateChanged()
        //{
        //    if (ViewModelAtual is LoginViewModel)
        //    {
        //        MenuStripVisibility = Visibility.Collapsed;
        //        OnPropertyChanged(nameof(MenuStripVisibility));
        //    }
        //    else
        //    {
        //        MenuStripVisibility = Visibility.Visible;
        //        OnPropertyChanged(nameof(MenuStripVisibility));
        //    }

        //    OnPropertyChanged(nameof(ViewModelAtual));
        //}
    }

    //public class AlterarSenhaFuncionárioCommand : ICommand
    //{
    //    private readonly MainViewModel _parentViewModel;
    //    private readonly IMessaging<int> _intMessaging;
    //    private readonly IMessaging<bool> _boolMessaging;
    //    private readonly IFuncionarioDataService _funcionarioDataService;
    //    private readonly ConfigurationStore _configStore;
    //    private readonly IDialogViewModelFactory _dialogViewModelFactory;
    //    private readonly IDialogGenerator _dialogGenerator;
    //    private readonly IDialogsStore _dialogStore;

    //    public AlterarSenhaFuncionárioCommand(MainViewModel parentViewModel, IServiceProvider serviceProvider)
    //    {
    //        _parentViewModel = parentViewModel;
    //        _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
    //        _boolMessaging = serviceProvider.GetRequiredService<IMessaging<bool>>();
    //        _funcionarioDataService = serviceProvider.GetRequiredService<IFuncionarioDataService>();
    //        _configStore = serviceProvider.GetRequiredService<ConfigurationStore>();
    //        _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
    //        _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
    //        _dialogViewModelFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
    //    }

    //    public bool CanExecute(object? parameter)
    //    {
    //        return true;
    //    }

    //    public void Execute(object? parameter)
    //    {
    //        _intMessaging.Mensagem = _configStore.FuncionarioLogado.ID;
    //        _boolMessaging.Mensagem = false;

    //        _dialogGenerator.ViewModelExibido =
    //            _dialogViewModelFactory.CreateDialogContentViewModel(TipoDialogue.CadastraSenhaUsuário);
    //        _dialogStore.RegisterDialog(_dialogGenerator);
    //    }

    //    public event EventHandler? CanExecuteChanged;
    //}

    //public class EditaEmitenteCommand : ICommand
    //{
    //    private readonly IDialogGenerator _dialogGenerator;
    //    private readonly IDialogViewModelFactory _dialogViewModelFactory;
    //    private readonly IDialogsStore _dialogStore;

    //    public EditaEmitenteCommand(IServiceProvider serviceProvider)
    //    {
    //        _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
    //        _dialogViewModelFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
    //        _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public void Execute(object parameter)
    //    {
    //        _dialogGenerator.ViewModelExibido =
    //            _dialogViewModelFactory.CreateDialogContentViewModel(TipoDialogue.CadastroEmitente);
    //        _dialogStore.RegisterDialog(_dialogGenerator);
    //    }

    //    public event EventHandler CanExecuteChanged;
    //}

    //internal class DialogoFormasDePagamentoCommand : ICommand
    //{
    //    private readonly IDialogGenerator dialogGenerator;
    //    private readonly IDialogsStore dialogStore;
    //    private readonly IDialogViewModelFactory dialogVMFactory;

    //    public DialogoFormasDePagamentoCommand(IServiceProvider escopo)
    //    {
    //        dialogGenerator = escopo.GetRequiredService<IDialogGenerator>();
    //        dialogStore = escopo.GetRequiredService<IDialogsStore>();
    //        dialogVMFactory = escopo.GetRequiredService<IDialogViewModelFactory>();
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public void Execute(object parameter)
    //    {
    //        dialogGenerator.ViewModelExibido = dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.FormasPagto);
    //        dialogStore.RegisterDialog(dialogGenerator);
    //    }
    //}
}