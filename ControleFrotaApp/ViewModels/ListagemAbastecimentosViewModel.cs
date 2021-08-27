using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ControleFrota.Domain;
using ControleFrota.Extensions;
using ControleFrota.Services;
using ControleFrota.Services.DataServices;
using ControleFrota.State;
using ControleFrota.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.ViewModels
{
    public class ListagemAbastecimentosViewModel : ViewModelBase
    {
        private readonly AbastecimentoDataService _abastecimentoDataService;
        private Abastecimento _abastecimentoSelecionado;
        private readonly IMessaging<string> _stringMessaging;
        public ObservableCollection<Abastecimento> Abastecimentos { get; set; } = new();
        public ICollectionView AbastecimentosView { get; set; }

        public Abastecimento AbastecimentoSelecionado
        {
            get => _abastecimentoSelecionado;
            set
            {
                _abastecimentoSelecionado = value;
                OnPropertyChanged(nameof(AbastecimentoSelecionado));
            }
        }

        public ICommand Cadastrar { get; set; }
        public ICommand Editar { get; set; }
        public ICommand Filtrar { get; set; }
        public ICommand Inativar { get; set; }
        public ICommand Imprimir { get; set; }
        public bool HitTestVisible { get; set; } = true;

        public ListagemAbastecimentosViewModel(IServiceProvider serviceProvider)
        {
            _abastecimentoDataService = serviceProvider.GetRequiredService<AbastecimentoDataService>();
            _stringMessaging = serviceProvider.GetRequiredService<IMessaging<string>>();

            Cadastrar = new CadastrarNovoAbastecimentoCommand(this, serviceProvider);
            Editar = new EditarAbastecimentoCommand(this, serviceProvider);
            Filtrar = new FiltrarAbastecimentosCommand(this, serviceProvider);
            PreencheDataGrid();
        }

        public async Task PreencheDataGrid()
        {
            Abastecimentos.Clear();
            _stringMessaging.Mensagem = null;
            HitTestVisible = false;
            OnPropertyChanged(nameof(HitTestVisible));
            Mouse.OverrideCursor = Cursors.Wait;
            Application.Current.Dispatcher.Invoke(() => { Mouse.OverrideCursor = Cursors.Wait; });

            try
            {
                //foreach (Abastecimento abastecimento in await _abastecimentoDataService.GetAllAsNoTracking())
                //{
                //    Abastecimentos.Add(abastecimento);
                //}
                AbastecimentosView =
                    CollectionViewSource.GetDefaultView(await _abastecimentoDataService.GetAllAsNoTracking());
                OnPropertyChanged(nameof(AbastecimentosView));
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

    public class FiltrarAbastecimentosCommand : ICommand
    {
        private readonly ListagemAbastecimentosViewModel _listagemAbastecimentosViewModel;
        private readonly IDialogsStore _dialogStore;
        private readonly IDialogViewModelFactory _dialogViewModelFactory;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly EntityStore<EntityBase> _entityStore;
        private readonly FilterCollectionView _filterCollectionView;
        private readonly IMessaging<Type> _typeMessaging;

        public FiltrarAbastecimentosCommand(ListagemAbastecimentosViewModel listagemAbastecimentosViewModel, IServiceProvider serviceProvider)
        {
            _listagemAbastecimentosViewModel = listagemAbastecimentosViewModel;
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
            _dialogViewModelFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _entityStore = serviceProvider.GetRequiredService<EntityStore<EntityBase>>();
            _filterCollectionView = new();
            _typeMessaging = serviceProvider.GetRequiredService<IMessaging<Type>>();

        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _typeMessaging.Mensagem = typeof(Abastecimento);
            _dialogGenerator.ViewModelExibido =
                _dialogViewModelFactory.CreateDialogContentViewModel(TipoDialogue.Filtros);
            _dialogStore.RegisterDialog(_dialogGenerator);
            if (_dialogGenerator.Resultado == DialogResult.OK)
            {
                _listagemAbastecimentosViewModel.AbastecimentosView.Filter += _filterCollectionView.AddNewFilter(_entityStore.Entity);
                if (_entityStore.Entity is FilteringInfo fi)
                {
                    //_listagemAbastecimentosViewModel.AlteraFiltrosAplicadosString(fi);
                }
            }
        }

        public event EventHandler CanExecuteChanged;
    }


    public class EditarAbastecimentoCommand : ICommand
    {
        private readonly ListagemAbastecimentosViewModel _listagemAbastecimentosViewModel;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;

        public EditarAbastecimentoCommand(ListagemAbastecimentosViewModel listagemAbastecimentosViewModel, IServiceProvider serviceProvider)
        {
            _listagemAbastecimentosViewModel = listagemAbastecimentosViewModel;
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _dialogVMFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
            _listagemAbastecimentosViewModel.PropertyChanged += _listagemAbastecimentosViewModel_PropertyChanged;
        }

        private void _listagemAbastecimentosViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public bool CanExecute(object? parameter)
        {
            return _listagemAbastecimentosViewModel.AbastecimentoSelecionado is not null;
        }

        public void Execute(object? parameter)
        {
            _intMessaging.Mensagem = _listagemAbastecimentosViewModel.AbastecimentoSelecionado.ID;
            _dialogGenerator.ViewModelExibido =
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.CadastroDeAbastecimento);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _listagemAbastecimentosViewModel.PreencheDataGrid();

        }

        public event EventHandler? CanExecuteChanged;
    }

    public class CadastrarNovoAbastecimentoCommand : ICommand
    {
        private readonly ListagemAbastecimentosViewModel _listagemAbastecimentosViewModel;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;
        private readonly IMessaging<int> _intMessaging;

        public CadastrarNovoAbastecimentoCommand(ListagemAbastecimentosViewModel listagemAbastecimentosViewModel, IServiceProvider serviceProvider)
        {
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _listagemAbastecimentosViewModel = listagemAbastecimentosViewModel;
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
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.CadastroDeAbastecimento);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _listagemAbastecimentosViewModel.PreencheDataGrid();
        }

        public event EventHandler? CanExecuteChanged;
    }
}
