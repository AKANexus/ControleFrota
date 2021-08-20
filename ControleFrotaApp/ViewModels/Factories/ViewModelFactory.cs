using System;
using ControleFrota.State.Navigators;

namespace ControleFrota.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CriaViewModel<ListagemVeículosViewModel> _createListagemVeículosViewModel;

        public ViewModelFactory(CriaViewModel<ListagemVeículosViewModel> createListagemVeículosViewModel)
        {
            _createListagemVeículosViewModel = createListagemVeículosViewModel;
        }

        public ViewModelBase CreateViewModel(TipoView tipoView)
        {
            return tipoView switch
            {
                TipoView.ListagemVeículos => _createListagemVeículosViewModel(),
                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}