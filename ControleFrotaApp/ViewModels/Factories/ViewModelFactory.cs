using System;
using ControleFrota.State.Navigators;

namespace ControleFrota.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CriaViewModel<ListagemVeículosViewModel> _createListagemVeículosViewModel;
        private readonly CriaViewModel<ListagemFuncionáriosViewModel> _createListagemMotoristasViewModel;
        private readonly CriaViewModel<ListagemViagensViewModel> _createListagemViagensViewModel;
        private readonly CriaViewModel<ListagemAbastecimentosViewModel> _createListagemAbastecimentosViewModel;
        private readonly CriaViewModel<ListagemManutençõesViewModel> _createListagemManuteçõesViewModel;


        public ViewModelFactory(CriaViewModel<ListagemVeículosViewModel> createListagemVeículosViewModel, CriaViewModel<ListagemFuncionáriosViewModel> createListagemMotoristasViewModel, CriaViewModel<ListagemViagensViewModel> createListagemViagensViewModel, CriaViewModel<ListagemAbastecimentosViewModel> createListagemAbastecimentosViewModel, CriaViewModel<ListagemManutençõesViewModel> createListagemManuteçõesViewModel)
        {
            _createListagemVeículosViewModel = createListagemVeículosViewModel;
            _createListagemMotoristasViewModel = createListagemMotoristasViewModel;
            _createListagemViagensViewModel = createListagemViagensViewModel;
            _createListagemAbastecimentosViewModel = createListagemAbastecimentosViewModel;
            _createListagemManuteçõesViewModel = createListagemManuteçõesViewModel;
        }

        public ViewModelBase CreateViewModel(TipoView tipoView)
        {
            return tipoView switch
            {
                TipoView.ListagemVeículos => _createListagemVeículosViewModel(),
                TipoView.ListagemFuncionários => _createListagemMotoristasViewModel(),
                TipoView.ListagemViagens => _createListagemViagensViewModel(),
                TipoView.ListagemAbastecimentos => _createListagemAbastecimentosViewModel(),
                TipoView.ListaManutenções => _createListagemManuteçõesViewModel(),
                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}