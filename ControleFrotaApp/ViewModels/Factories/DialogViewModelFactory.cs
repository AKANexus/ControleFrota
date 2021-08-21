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
        private readonly CriaDialogViewModel<SaídaDeVeículoViewModel> _createSaídaDeVeículoViewModel;
        private readonly CriaDialogViewModel<RetornoDeVeículoViewModel> _createRetornoDeVeículoViewModel;

        public DialogViewModelFactory(CriaDialogViewModel<WorkInProgressViewModel> createWorkInProgressViewModel, CriaDialogViewModel<CadastroVeículoViewModel> createCadastroVeículoViewModel, CriaDialogViewModel<CadastroMotoristaViewModel> createCadastroMotoristaViewModel, CriaDialogViewModel<CadastroViagemViewModel> createCadastroViagemViewModel, CriaDialogViewModel<SaídaDeVeículoViewModel> createSaídaDeVeículoViewModel, CriaDialogViewModel<RetornoDeVeículoViewModel> createRetornoDeVeículoViewModel)
        {
            _createWorkInProgressViewModel = createWorkInProgressViewModel;
            _createCadastroVeículoViewModel = createCadastroVeículoViewModel;
            _createCadastroMotoristaViewModel = createCadastroMotoristaViewModel;
            _createCadastroViagemViewModel = createCadastroViagemViewModel;
            _createSaídaDeVeículoViewModel = createSaídaDeVeículoViewModel;
            _createRetornoDeVeículoViewModel = createRetornoDeVeículoViewModel;
        }

        public DialogContentViewModelBase CreateDialogContentViewModel(TipoDialogue tipoView)
        {
            return tipoView switch
            {
                TipoDialogue.WIP => _createWorkInProgressViewModel(),
                TipoDialogue.CadastroDeVeículos => _createCadastroVeículoViewModel(),
                TipoDialogue.CadastroDeMotoristas => _createCadastroMotoristaViewModel(),
                TipoDialogue.CadastroDeViagens => _createCadastroViagemViewModel(),
                TipoDialogue.NovaViagem => _createSaídaDeVeículoViewModel(),
                TipoDialogue.RetornoDeViatura => _createRetornoDeVeículoViewModel(),
                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}