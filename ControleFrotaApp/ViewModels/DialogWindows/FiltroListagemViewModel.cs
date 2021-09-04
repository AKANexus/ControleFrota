using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using ControleFrota.Commands;
using ControleFrota.Domain;
using ControleFrota.Services;
using ControleFrota.State;
using Microsoft.Extensions.DependencyInjection;
using DialogResult = ControleFrota.Services.DialogResult;
using MessageBox = System.Windows.MessageBox;

namespace ControleFrota.ViewModels.DialogWindows
{
    public class FiltroListagemViewModel : DialogContentViewModelBase
    {
        private readonly EntityStore<EntityBase> _entityStore;
        private FilteringInfo _selectedPropDescPair;
        private readonly IMessaging<Type> _typeMessaging;
        public ObservableCollection<FilteringInfo> Colunas { get; set; } = new();
        public TipoFiltro TipoFiltro { get; set; }

        public FilteringInfo SelectedPropDescPair
        {
            get => _selectedPropDescPair;
            set
            {
                _selectedPropDescPair = value;
                OnPropertyChanged(nameof(SelectedPropDescPair));
                TrocaInterface();
            }
        }

        private void TrocaInterface()
        {
            if (SelectedPropDescPair.Property.PropertyType == typeof(DateTime))
            {
                CampoData = Visibility.Visible;
                CampoTexto = Visibility.Collapsed;
                CampoNumerico = Visibility.Collapsed;
                CampoEnum = Visibility.Collapsed;
                TipoFiltro = TipoFiltro.Data;

            }

            else if (SelectedPropDescPair.Property.PropertyType == typeof(string))
            {
                CampoData = Visibility.Collapsed;
                CampoTexto = Visibility.Visible;
                CampoNumerico = Visibility.Collapsed;
                CampoEnum = Visibility.Collapsed;
                TipoFiltro = TipoFiltro.Texto;
            }

            else if (SelectedPropDescPair.Property.PropertyType == typeof(int) ||
                SelectedPropDescPair.Property.PropertyType == typeof(decimal) ||
                SelectedPropDescPair.Property.PropertyType == typeof(double))
            {
                CampoData = Visibility.Collapsed;
                CampoTexto = Visibility.Collapsed;
                CampoNumerico = Visibility.Visible;
                CampoEnum = Visibility.Collapsed;
                TipoFiltro = TipoFiltro.Numérico;
            }
            else if (SelectedPropDescPair.Property.PropertyType.IsEnum)
            {
                ListaDeEnums.Clear();
                foreach (var value in Enum.GetValues(SelectedPropDescPair.Property.PropertyType))
                {
                    ListaDeEnums.Add((Enum)value);
                }

                CampoData = Visibility.Collapsed;
                CampoTexto = Visibility.Collapsed;
                CampoNumerico = Visibility.Collapsed;
                CampoEnum = Visibility.Visible;
                TipoFiltro = TipoFiltro.Enum;
            }
            else
            {
                MessageBox.Show("Campo não tem um método de filtro definido");
                CampoData = Visibility.Collapsed;
                CampoTexto = Visibility.Collapsed;
                CampoNumerico = Visibility.Collapsed;
                CampoEnum = Visibility.Collapsed;
            }

            OnPropertyChanged(nameof(CampoTexto));
            OnPropertyChanged(nameof(CampoNumerico));
            OnPropertyChanged(nameof(CampoData));
            OnPropertyChanged(nameof(CampoEnum));
        }

        public ICommand Testar { get; set; }
        public ICommand CloseCurrentWindow { get; set; }
        public ICommand AplicaExceção { get; set; }
        public ICommand AplicaFiltro { get; set; }
        public ICommand LimparFiltros { get; set; }

        public Visibility CampoTexto { get; set; } = Visibility.Collapsed;
        public Visibility CampoNumerico { get; set; } = Visibility.Collapsed;
        public Visibility CampoData { get; set; } = Visibility.Collapsed;
        public Visibility CampoEnum { get; set; } = Visibility.Collapsed;

        public DateTime DataInicial { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime DataFinal { get; set; } = DateTime.Now;

        public string ValorInicial { get; set; }
        public string ValorFinal { get; set; }

        public string TextoDoFiltro { get; set; }
        public bool CampoInteiroChecked { get; set; }
        public bool ComeçandoComChecked { get; set; }
        public bool ContendoChecked { get; set; } = true;

        public ObservableCollection<Enum> ListaDeEnums { get; set; }
        public Enum EnumSelecionado { get; set; }

        public FiltroListagemViewModel(IServiceProvider serviceProvider)
        {
            _entityStore = serviceProvider.GetRequiredService<EntityStore<EntityBase>>();
            _typeMessaging = serviceProvider.GetRequiredService<IMessaging<Type>>();
            List<FilteringInfo> campos = new();
            ListaDeEnums = new();
            foreach (PropertyInfo propertyInfo in _typeMessaging.Mensagem.GetProperties())
            {
                var attrs = propertyInfo.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    if (attr is DescriptionAttribute descr)
                        campos.Add(new FilteringInfo() { Property = propertyInfo, Description = descr.Description }); ;
                }
            }

            foreach (FilteringInfo filteringInfo in campos.OrderBy(x=>x.Property.Name))
            {
                Colunas.Add(filteringInfo);
            }
            AplicaFiltro = new AplicaFiltroCommand(this, serviceProvider);
            AplicaExceção = new AplicarExceçãoCommand(this, serviceProvider);
            LimparFiltros = new LimparFiltrosCommand(serviceProvider);
            CloseCurrentWindow = new CloseCurrentWindowCommand(serviceProvider);
            Testar = new TestarCommand(this, serviceProvider);
        }
    }

    public class AplicarExceçãoCommand : ICommand
    {
        public AplicarExceçãoCommand(FiltroListagemViewModel filtroListagemViewModel, IServiceProvider serviceProvider)
        {
        }

        public bool CanExecute(object? parameter)
        {
            return false;
        }

        public void Execute(object? parameter)
        {
        }

        public event EventHandler? CanExecuteChanged;
    }

    public class LimparFiltrosCommand : ICommand
    {
        private readonly IDialogsStore _dialogStore;
        private readonly EntityStore<EntityBase> _entityStore;

        public LimparFiltrosCommand(IServiceProvider serviceProvider)
        {
            _entityStore = serviceProvider.GetRequiredService<EntityStore<EntityBase>>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _entityStore.Entity = new FilteringInfo() { FilterInfo = "clearfilters:" };
            _dialogStore.CloseDialog(DialogResult.OK);
        }

        public event EventHandler? CanExecuteChanged;
    }

    public class AplicaFiltroCommand : ICommand
    {
        private readonly FiltroListagemViewModel _filtroListagemViewModel;
        private readonly EntityStore<EntityBase> _entityStore;
        private readonly IDialogsStore _dialogStore;

        public AplicaFiltroCommand(FiltroListagemViewModel filtroListagemViewModel, IServiceProvider serviceProvider)
        {
            _filtroListagemViewModel = filtroListagemViewModel;
            _entityStore = serviceProvider.GetRequiredService<EntityStore<EntityBase>>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch (_filtroListagemViewModel.TipoFiltro)
            {
                case TipoFiltro.Texto:
                    if (_filtroListagemViewModel.CampoInteiroChecked)
                    {
                        _filtroListagemViewModel.SelectedPropDescPair.FilterInfo = $"wholefield:{_filtroListagemViewModel.TextoDoFiltro}";
                        break;
                    }
                    if (_filtroListagemViewModel.ContendoChecked)
                    {
                        _filtroListagemViewModel.SelectedPropDescPair.FilterInfo = $"contains:{_filtroListagemViewModel.TextoDoFiltro}";
                        break;
                    }
                    if (_filtroListagemViewModel.ComeçandoComChecked)
                    {
                        _filtroListagemViewModel.SelectedPropDescPair.FilterInfo = $"startswith:{_filtroListagemViewModel.TextoDoFiltro}";
                        break;
                    }
                    break;
                case TipoFiltro.Data:
                    _filtroListagemViewModel.SelectedPropDescPair.FilterInfo = $"datebetween:{_filtroListagemViewModel.DataInicial.ToShortDateString()};{_filtroListagemViewModel.DataFinal.ToShortDateString()}";
                    break;
                case TipoFiltro.Numérico:
                    _filtroListagemViewModel.SelectedPropDescPair.FilterInfo = $"valuebetween:{_filtroListagemViewModel.ValorInicial};{_filtroListagemViewModel.ValorFinal}";
                    break;
                case TipoFiltro.Enum:
                    _filtroListagemViewModel.SelectedPropDescPair.FilterInfo = $"enumvalue:{_filtroListagemViewModel.EnumSelecionado}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _entityStore.Entity = _filtroListagemViewModel.SelectedPropDescPair;
            _dialogStore.CloseDialog(DialogResult.OK);
        }

        public event EventHandler CanExecuteChanged;
    }

    public class TestarCommand : ICommand
    {
        private readonly FiltroListagemViewModel _filtroListagemViewModel;

        public TestarCommand(FiltroListagemViewModel filtroListagemViewModel, IServiceProvider serviceProvider)
        {
            _filtroListagemViewModel = filtroListagemViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Debug.WriteLine("");
        }

        public event EventHandler? CanExecuteChanged;
    }
}
