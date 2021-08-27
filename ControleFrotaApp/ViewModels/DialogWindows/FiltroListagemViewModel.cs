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
using System.Windows.Input;
using ControleFrota.Commands;
using ControleFrota.Domain;
using ControleFrota.State;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.ViewModels.DialogWindows
{
    public class FiltroListagemViewModel : DialogContentViewModelBase
    {
        private readonly EntityStore<EntityBase> _entityStore;
        private FilteringInfo _selectedPropDescPair;
        public ObservableCollection<FilteringInfo> Colunas { get; set; } = new();

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
                OnPropertyChanged(nameof(CampoTexto));
                OnPropertyChanged(nameof(CampoNumerico));
                OnPropertyChanged(nameof(CampoData));
            }

            else if (SelectedPropDescPair.Property.PropertyType == typeof(string))
            {
                CampoData = Visibility.Collapsed;
                CampoTexto = Visibility.Visible;
                CampoNumerico = Visibility.Collapsed;
                OnPropertyChanged(nameof(CampoNumerico));
                OnPropertyChanged(nameof(CampoData));
                OnPropertyChanged(nameof(CampoTexto));
            }

            else if (SelectedPropDescPair.Property.PropertyType == typeof(int) ||
                SelectedPropDescPair.Property.PropertyType == typeof(decimal) ||
                SelectedPropDescPair.Property.PropertyType == typeof(double))
            {
                CampoData = Visibility.Collapsed;
                CampoTexto = Visibility.Collapsed;
                CampoNumerico = Visibility.Visible;
                OnPropertyChanged(nameof(CampoData));
                OnPropertyChanged(nameof(CampoTexto));
                OnPropertyChanged(nameof(CampoNumerico));
            }
            else
            {
                CampoData = Visibility.Collapsed;
                CampoTexto = Visibility.Collapsed;
                CampoNumerico = Visibility.Collapsed;
                OnPropertyChanged(nameof(CampoData));
                OnPropertyChanged(nameof(CampoTexto));
                OnPropertyChanged(nameof(CampoNumerico));
            }
        }

        public ICommand Testar { get; set; }
        public ICommand CloseCurrentWindow { get; set; }
        public ICommand AplicaExceção { get; set; }
        public ICommand AplicaFiltro { get; set; }

        public Visibility CampoTexto { get; set; } = Visibility.Collapsed;
        public Visibility CampoNumerico { get; set; } = Visibility.Collapsed;
        public Visibility CampoData { get; set; } = Visibility.Collapsed;

        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }

        public string ValorInicial { get; set; }
        public string ValorFinal { get; set; }

        public string TextoDoFiltro { get; set; }
        public bool CampoInteiroChecked { get; set; }
        public bool ComeçandoComChecked { get; set; }
        public bool ContendoChecked { get; set; }

        public FiltroListagemViewModel(IServiceProvider serviceProvider)
        {
            _entityStore = serviceProvider.GetRequiredService<EntityStore<EntityBase>>();
            foreach (PropertyInfo propertyInfo in _entityStore.Entity.GetType().GetProperties())
            {
                var attrs = propertyInfo.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    if (attr is DescriptionAttribute descr)
                        Colunas.Add(new FilteringInfo() { Property = propertyInfo, Description = descr.Description }); ;
                }
            }

            CloseCurrentWindow = new CloseCurrentWindowCommand(serviceProvider);
            Testar = new TestarCommand(this, serviceProvider);
        }
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
