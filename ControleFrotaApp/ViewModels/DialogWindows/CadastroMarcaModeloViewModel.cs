using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ControleFrota.Commands;
using ControleFrota.Domain;
using ControleFrota.Services;
using ControleFrota.Services.DataServices;
using ControleFrota.State;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.ViewModels.DialogWindows
{
    public class CadastroMarcaModeloViewModel : DialogContentViewModelBase
    {
        private readonly CurrentScopeStore _currentScopeStore;
        private readonly ModeloDataService _modeloDataService;
        public ObservableCollection<Modelo> Modelos { get; set; } = new();

        public List<TipoVeículo> TipoVeículos { get; set; }
        public ICommand Salva { get; set; }
        public ICommand CloseCurrentWindow { get; set; }

        public CadastroMarcaModeloViewModel(IServiceProvider serviceProvider)
        {
            _currentScopeStore = serviceProvider.GetRequiredService<CurrentScopeStore>();
            _currentScopeStore.CriaNovoEscopo();

            Salva = new SalvaMarcasCommand(this, serviceProvider, x => MessageBox.Show(x.Message));
            CloseCurrentWindow = new CloseCurrentWindowCommand(serviceProvider);

            _modeloDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<ModeloDataService>();
            PreencheDataGrid();
        }

        private async Task PreencheDataGrid()
        {
            var x = await _modeloDataService.GetAll();
            foreach (Modelo modelo in x)
            {
                Modelos.Add(modelo);
            }
        }



    }

    public class SalvaMarcasCommand : AsyncCommandBase
    {
        private readonly CadastroMarcaModeloViewModel _cadastroMarcaModeloViewModel;
        private readonly CurrentScopeStore _currentScopeStore;
        private readonly ModeloDataService _modeloDataService;
        private readonly IDialogsStore _dialogStore;
        private readonly ManutençãoProgramadaDataService _manutençãoProgramada;

        public SalvaMarcasCommand(CadastroMarcaModeloViewModel cadastroMarcaModeloViewModel, IServiceProvider serviceProvider, Action<Exception> onException) : base(onException)
        {
            _cadastroMarcaModeloViewModel = cadastroMarcaModeloViewModel;
            _currentScopeStore = serviceProvider.GetRequiredService<CurrentScopeStore>();
            _modeloDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<ModeloDataService>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
            _manutençãoProgramada = _currentScopeStore.PegaEscopoAtual()
                .GetRequiredService<ManutençãoProgramadaDataService>();
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            foreach (Modelo modelo in _cadastroMarcaModeloViewModel.Modelos)
            {
                var y = await _manutençãoProgramada.GetAll();
                if (modelo.ModeloManutenções.All(x => x.ManutençãoProgramada.TipoVeículo != modelo.TipoVeículo))
                {
                    modelo.ModeloManutenções.Clear();
                    await _modeloDataService.AddOrUpdate(modelo);
                    foreach (ManutençãoProgramada manutençãoProgramada in await _manutençãoProgramada.GetAllByTipo(modelo.TipoVeículo))
                    {
                        modelo.ModeloManutenções.Add(new()
                        {
                            Modelo = modelo, 
                            ManutençãoProgramada = manutençãoProgramada
                        });
                    }
                }
                await _modeloDataService.AddOrUpdate(modelo);
            }
            _dialogStore.CloseDialog(DialogResult.OK);
        }
    }
}
