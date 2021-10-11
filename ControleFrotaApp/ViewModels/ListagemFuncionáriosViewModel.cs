using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ControleFrota.Commands;
using ControleFrota.Domain;
using ControleFrota.Services;
using ControleFrota.Services.DataServices;
using ControleFrota.ViewModels.DialogWindows;
using ControleFrota.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.ViewModels
{
    public class ListagemFuncionáriosViewModel : DialogContentViewModelBase
    {
        private readonly MotoristaDataService _motoristaDataService;
        private Motorista _motoristaSelecionado;
        public ObservableCollection<Motorista> Motoristas { get; set; } = new();

        public Motorista MotoristaSelecionado
        {
            get => _motoristaSelecionado;
            set
            {
                _motoristaSelecionado = value;
                OnPropertyChanged(nameof(MotoristaSelecionado));
            }
        }

        public ICommand Cadastrar { get; }
        public ICommand Editar { get; }
        public ICommand Filtrar { get; set; }
        public ICommand Inativar { get; set; }
        public ICommand Imprimir { get; set; }
        public ListagemFuncionáriosViewModel(IServiceProvider serviceProvider)
        {
            _motoristaDataService = serviceProvider.GetRequiredService<MotoristaDataService>();

            Cadastrar = new CadastrarNovoMotoristaCommand(this, serviceProvider);
            Editar = new EditarMotoristaCommand(this, serviceProvider);
            Imprimir = new FakeCommand();
            PreencheDataGrid();
        }

        public async Task PreencheDataGrid()
        {
            Motoristas.Clear();
            foreach (Motorista motorista in await _motoristaDataService.GetAllAtivosAsNoTracking())
            {
                Motoristas.Add(motorista);
            }
        }
    }

    public class EditarMotoristaCommand : ICommand
    {
        private readonly ListagemFuncionáriosViewModel _listagemFuncionáriosViewModel;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;

        public EditarMotoristaCommand(ListagemFuncionáriosViewModel listagemFuncionáriosViewModel, IServiceProvider serviceProvider)
        {
            _listagemFuncionáriosViewModel = listagemFuncionáriosViewModel;
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _dialogVMFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _intMessaging.Mensagem = _listagemFuncionáriosViewModel.MotoristaSelecionado.ID;
            _dialogGenerator.ViewModelExibido =
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.CadastroDeMotoristas);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _listagemFuncionáriosViewModel.PreencheDataGrid();
        }

        public event EventHandler? CanExecuteChanged;
    }

    public class CadastrarNovoMotoristaCommand : ICommand
    {
        private readonly ListagemFuncionáriosViewModel _listagemFuncionáriosViewModel;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;

        public CadastrarNovoMotoristaCommand(ListagemFuncionáriosViewModel listagemFuncionáriosViewModel, IServiceProvider serviceProvider)
        {
            _listagemFuncionáriosViewModel = listagemFuncionáriosViewModel;
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _dialogVMFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _intMessaging.Mensagem = default;
            _dialogGenerator.ViewModelExibido =
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.CadastroDeMotoristas);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _listagemFuncionáriosViewModel.PreencheDataGrid();
        }

        public event EventHandler? CanExecuteChanged;
    }
}
