using System;
using ControleFrota.Services;
using ControleFrota.ViewModels.DialogWindows;

namespace ControleFrota.ViewModels.Factories
{
    public class DialogViewModelFactory : IDialogViewModelFactory
    {
        private readonly CriaDialogViewModel<WorkInProgressViewModel> _createWorkInProgressViewModel;
        private readonly CriaDialogViewModel<CadastroVeículoViewModel> _createCadastroVeículoViewModel;
        private readonly CriaDialogViewModel<CadastroMotoristaViewModel> _createCadastroMotoristaViewModel;
        private readonly CriaDialogViewModel<CadastroViagemViewModel> _createCadastroViagemViewModel;

        public DialogViewModelFactory(CriaDialogViewModel<WorkInProgressViewModel> createWorkInProgressViewModel, CriaDialogViewModel<CadastroVeículoViewModel> createCadastroVeículoViewModel, CriaDialogViewModel<CadastroMotoristaViewModel> createCadastroMotoristaViewModel, CriaDialogViewModel<CadastroViagemViewModel> createCadastroViagemViewModel)
        {
            _createWorkInProgressViewModel = createWorkInProgressViewModel;
            _createCadastroVeículoViewModel = createCadastroVeículoViewModel;
            _createCadastroMotoristaViewModel = createCadastroMotoristaViewModel;
            _createCadastroViagemViewModel = createCadastroViagemViewModel;
        }

        public DialogContentViewModelBase CreateDialogContentViewModel(TipoDialogue tipoView)
        {
            return tipoView switch
            {
                TipoDialogue.WIP => _createWorkInProgressViewModel(),
                TipoDialogue.CadastroDeVeículos => _createCadastroVeículoViewModel(),
                TipoDialogue.CadastroDeMotoristas => _createCadastroMotoristaViewModel(),
                TipoDialogue.CadastroDeViagens => _createCadastroViagemViewModel(),
                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}