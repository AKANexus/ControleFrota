using System;
using ControleFrota.State.Navigators;

namespace ControleFrota.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CriaViewModel<ListagemVeículosViewModel> _createListagemVeículosViewModel;
        private readonly CriaViewModel<ListagemFuncionáriosViewModel> _createListagemMotoristasViewModel;
        private readonly CriaViewModel<ListagemViagensViewModel> _createListagemViagensViewModel;

        public ViewModelFactory(CriaViewModel<ListagemVeículosViewModel> createListagemVeículosViewModel, CriaViewModel<ListagemFuncionáriosViewModel> createListagemMotoristasViewModel, CriaViewModel<ListagemViagensViewModel> createListagemViagensViewModel)
        {
            _createListagemVeículosViewModel = createListagemVeículosViewModel;
            _createListagemMotoristasViewModel = createListagemMotoristasViewModel;
            _createListagemViagensViewModel = createListagemViagensViewModel;
        }

        public ViewModelBase CreateViewModel(TipoView tipoView)
        {
            return tipoView switch
            {
                TipoView.ListagemVeículos => _createListagemVeículosViewModel(),
                TipoView.ListagemFuncionários => _createListagemMotoristasViewModel(),
                TipoView.ListagemViagens => _createListagemViagensViewModel(),
                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}