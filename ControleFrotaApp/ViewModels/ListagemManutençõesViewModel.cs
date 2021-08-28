using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class ListagemManutençõesViewModel : ViewModelBase
    {
        private StringBuilder _filtrosAplicados;
        private readonly ManutençãoDataService _manutençãoDataService;
        public ICollectionView ManutençõesView { get; set; }
        public Manutenção ManutençãoSelecionada { get; set; }

        public string FiltrosAplicados
        {
            get => _filtrosAplicados?.ToString();
        }
        public ICommand Cadastrar { get; }
        public ICommand Editar { get; }
        public ICommand Filtrar { get; set; }
        public bool HitTestVisible { get; set; } = true;


        public ListagemManutençõesViewModel(IServiceProvider serviceProvider)
        {
            _manutençãoDataService = serviceProvider.GetRequiredService<ManutençãoDataService>();

            Cadastrar = new CadastrarNovaManutençãoCommand(this, serviceProvider);
            Editar = new EditarManutençãoCommand(this, serviceProvider);
            Filtrar = new FiltrarManutençõesCommand(this, serviceProvider);

            PreencheGrid();
        }

        public async Task PreencheGrid()
        {
            HitTestVisible = false;
            OnPropertyChanged(nameof(HitTestVisible));
            Mouse.OverrideCursor = Cursors.Wait;
            Application.Current.Dispatcher.Invoke(() => { Mouse.OverrideCursor = Cursors.Wait; });

            try
            {
                ManutençõesView = new ListCollectionView(await _manutençãoDataService.GetAllAsNoTracking());
                OnPropertyChanged(nameof(ManutençõesView));
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

    }

    public class EditarManutençãoCommand : ICommand
    {
        private readonly ListagemManutençõesViewModel _listagemManutençõesViewModel;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;

        public EditarManutençãoCommand(ListagemManutençõesViewModel listagemManutençõesViewModel, IServiceProvider serviceProvider)
        {
            _listagemManutençõesViewModel = listagemManutençõesViewModel;
            _listagemManutençõesViewModel.PropertyChanged += _listagemManutençõesViewModel_PropertyChanged;
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _dialogVMFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
        }

        private void _listagemManutençõesViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public bool CanExecute(object? parameter)
        {
            return _listagemManutençõesViewModel.ManutençãoSelecionada is not null;
        }

        public void Execute(object? parameter)
        {
            _intMessaging.Mensagem = _listagemManutençõesViewModel.ManutençãoSelecionada.ID;
            _dialogGenerator.ViewModelExibido =
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.CadastroDeManutenção);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _listagemManutençõesViewModel.PreencheGrid();
        }

        public event EventHandler? CanExecuteChanged;
    }

    public class CadastrarNovaManutençãoCommand : ICommand
    {
        private readonly ListagemManutençõesViewModel _listagemManutençõesViewModel;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IDialogViewModelFactory _dialogVMFactory;
        private readonly IDialogsStore _dialogStore;

        public CadastrarNovaManutençãoCommand(ListagemManutençõesViewModel listagemManutençõesViewModel, IServiceProvider serviceProvider)
        {
            _listagemManutençõesViewModel = listagemManutençõesViewModel;
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
                _dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.CadastroDeManutenção);
            _dialogStore.RegisterDialog(_dialogGenerator);
            _listagemManutençõesViewModel.PreencheGrid();
        }

        public event EventHandler? CanExecuteChanged;
    }

    public class FiltrarManutençõesCommand : ICommand
    {
        private readonly ListagemManutençõesViewModel _listagemManutençõesViewModel;
        private readonly IDialogsStore _dialogStore;
        private readonly IDialogViewModelFactory _dialogViewModelFactory;
        private readonly IDialogGenerator _dialogGenerator;
        private readonly IMessaging<Type> _typeMessaging;
        private readonly FilterCollectionView _filterCollectionView;
        private readonly EntityStore<EntityBase> _entityStore;

        public FiltrarManutençõesCommand(ListagemManutençõesViewModel listagemManutençõesViewModel, IServiceProvider serviceProvider)
        {
            _listagemManutençõesViewModel = listagemManutençõesViewModel;
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
            _dialogViewModelFactory = serviceProvider.GetRequiredService<IDialogViewModelFactory>();
            _dialogGenerator = serviceProvider.GetRequiredService<IDialogGenerator>();
            _typeMessaging = serviceProvider.GetRequiredService<IMessaging<Type>>();
            _entityStore = serviceProvider.GetRequiredService<EntityStore<EntityBase>>();
            _filterCollectionView = new();
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _typeMessaging.Mensagem = typeof(Manutenção);
            _dialogGenerator.ViewModelExibido =
                _dialogViewModelFactory.CreateDialogContentViewModel(TipoDialogue.Filtros);
            _dialogStore.RegisterDialog(_dialogGenerator);
            if (_dialogGenerator.Resultado == DialogResult.OK)
            {
                _listagemManutençõesViewModel.ManutençõesView.Filter += _filterCollectionView.AddNewFilter(_entityStore.Entity);
                if (_entityStore.Entity is FilteringInfo fi)
                {
                    _listagemManutençõesViewModel.AlteraFiltrosAplicadosString(fi);
                }
            }
        }

        public event EventHandler? CanExecuteChanged;
    }
}
