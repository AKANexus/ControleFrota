using System;
using ControleFrota.State.Navigators;

namespace ControleFrota.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CriaViewModel<TelaInicialViewModel> _createVendasViewModelFactory;

        public ViewModelFactory(CriaViewModel<TelaInicialViewModel> createVendasViewModelFactory)
        {
            _createVendasViewModelFactory = createVendasViewModelFactory;
        }

        public ViewModelBase CreateViewModel(TipoView tipoView)
        {
            return tipoView switch
            {
                TipoView.TelaInicial => _createVendasViewModelFactory(),
                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}