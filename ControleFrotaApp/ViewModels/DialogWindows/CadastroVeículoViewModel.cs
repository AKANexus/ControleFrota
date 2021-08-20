using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleFrota.Domain;
using ControleFrota.Services.DataServices;
using ControleFrota.State;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.ViewModels.DialogWindows
{
    public class CadastroVeículoViewModel : DialogContentViewModelBase
    {
        private readonly CurrentScopeStore _currentScopeStore;
        private readonly VeículoDataService _veículoDataService;
        private readonly MarcaDataService _marcaDataService;
        private readonly ModeloDataService _modeloDataService;
        private readonly EntityStore<Veículo> _veículoEntityStore;
        public Veículo VeículoSelecionado { get; set; }

        public string PlacaVeículo
        {
            get
            {
                return VeículoSelecionado?.Placa;
            }
            set
            {
                string placaDigitada = value;
                placaDigitada = placaDigitada.Replace("-", "");
                if (placaDigitada.Length != 7 || VeículoSelecionado is null) return;
                VeículoSelecionado.Placa = $"{placaDigitada.Substring(0, 3)}-{placaDigitada.Substring(3, 4)}";
            }
        }

        public ObservableCollection<Marca> Marcas { get; set; } = new();

        public ObservableCollection<Modelo> Modelos { get; set; } = new();

        public CadastroVeículoViewModel(IServiceProvider serviceProvider)
        {
            _currentScopeStore = serviceProvider.GetRequiredService<CurrentScopeStore>();
            _currentScopeStore.CriaNovoEscopo();

            _veículoDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<VeículoDataService>();
            _marcaDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<MarcaDataService>();
            _modeloDataService = _currentScopeStore.PegaEscopoAtual().GetRequiredService<ModeloDataService>();

            _veículoEntityStore = _currentScopeStore.PegaEscopoAtual().GetRequiredService<EntityStore<Veículo>>();
            PreencheCampos();
        }

        private async Task PreencheCampos()
        {
            foreach (Marca marca in await _marcaDataService.GetAll())
            {
                Marcas.Add(marca);
            }

            foreach (Modelo modelo in await _modeloDataService.GetAll())
            {
                Modelos.Add(modelo);
            }

            if (_veículoEntityStore.Entity is null)
            {
                VeículoSelecionado = new Veículo();
                return;
            }

            VeículoSelecionado = await _veículoDataService.GetVeículoByID(_veículoEntityStore.Entity.ID);
        }
    }
}
