using System;
using ControleFrota.State.Navigators;

namespace ControleFrota.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CriaViewModel<ListagemVeículosViewModel> _createListagemVeículosViewModel;
        private readonly CriaViewModel<ListagemFuncionáriosViewModel> _createListagemMotoristasViewModel;

        public ViewModelFactory(CriaViewModel<ListagemVeículosViewModel> createListagemVeículosViewModel, CriaViewModel<ListagemFuncionáriosViewModel> createListagemMotoristasViewModel)
        {
            _createListagemVeículosViewModel = createListagemVeículosViewModel;
            _createListagemMotoristasViewModel = createListagemMotoristasViewModel;
        }

        public ViewModelBase CreateViewModel(TipoView tipoView)
        {
            return tipoView switch
            {
                TipoView.ListagemVeículos => _createListagemVeículosViewModel(),
                TipoView.ListagemFuncionários => _createListagemMotoristasViewModel(),
                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}