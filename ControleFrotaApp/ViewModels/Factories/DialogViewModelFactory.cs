using System;
using ControleFrota.Services;
using ControleFrota.ViewModels.DialogWindows;

namespace ControleFrota.ViewModels.Factories
{
    public class DialogViewModelFactory : IDialogViewModelFactory
    {
        private readonly CriaDialogViewModel<WorkInProgressViewModel> _createWorkInProgressViewModel;
        private readonly CriaDialogViewModel<CadastroVeículoViewModel> _createCadastroVeículoViewModel;

        public DialogViewModelFactory(CriaDialogViewModel<WorkInProgressViewModel> createWorkInProgressViewModel, CriaDialogViewModel<CadastroVeículoViewModel> createCadastroVeículoViewModel)
        {
            _createWorkInProgressViewModel = createWorkInProgressViewModel;
            _createCadastroVeículoViewModel = createCadastroVeículoViewModel;
        }

        public DialogContentViewModelBase CreateDialogContentViewModel(TipoDialogue tipoView)
        {
            return tipoView switch
            {
                TipoDialogue.WIP => _createWorkInProgressViewModel(),
                TipoDialogue.CadastroDeVeículos => _createCadastroVeículoViewModel(),
                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}