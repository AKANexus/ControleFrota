using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ObservableCollection<Veículo> Veículos { get; set; }
        public Veículo VeículoSelecionado { get; set; }
        public ICommand Cadastrar { get; set; }

        public ListagemVeículosViewModel(IServiceProvider serviceProvider)
        {
            _veículoDataService = serviceProvider.GetRequiredService<VeículoDataService>();

            Cadastrar = new CadastrarNovoVeículoCommand(this, serviceProvider);
            PreencheDataGrid();
        }

        public async Task PreencheDataGrid()
        {
            foreach (Veículo veículo in await _veículoDataService.GetAllAsNoTracking())
            {
                Veículos.Add(veículo);
            }
        }
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
            return _listagemVeículosViewModel.VeículoSelecionado is not null;
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
