using System;
using System.Runtime.CompilerServices;
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
        private readonly CriaDialogViewModel<CadastroAbastecimentoViewModel> _createCadastroAbastecimentoViewModel;
        private readonly CriaDialogViewModel<FiltroListagemViewModel> _createFiltroListagemViewModel;
        private readonly CriaDialogViewModel<CadastroManutençãoViewModel> _createCadastroManutençãoViewModel;
        private readonly CriaDialogViewModel<CadastroMarcaModeloViewModel> _createCadastroModelosViewModel;
        private readonly CriaDialogViewModel<FiltroRelatórioViewModel> _createFiltroRelatórioViewModel;




        public DialogViewModelFactory(CriaDialogViewModel<WorkInProgressViewModel> createWorkInProgressViewModel,
            CriaDialogViewModel<CadastroVeículoViewModel> createCadastroVeículoViewModel,
            CriaDialogViewModel<CadastroMotoristaViewModel> createCadastroMotoristaViewModel,
            CriaDialogViewModel<CadastroViagemViewModel> createCadastroViagemViewModel,
            CriaDialogViewModel<SaídaDeVeículoViewModel> createSaídaDeVeículoViewModel,
            CriaDialogViewModel<RetornoDeVeículoViewModel> createRetornoDeVeículoViewModel,
            CriaDialogViewModel<CadastroAbastecimentoViewModel> createCadastroAbastecimentoViewModel,
            CriaDialogViewModel<FiltroListagemViewModel> createFiltroListagemViewModel,
            CriaDialogViewModel<CadastroManutençãoViewModel> createCadastroManutençãoViewModel, 
            CriaDialogViewModel<CadastroMarcaModeloViewModel> createCadastroModelosViewModel, 
            CriaDialogViewModel<FiltroRelatórioViewModel> createFiltroRelatórioViewModel)
        {
            _createWorkInProgressViewModel = createWorkInProgressViewModel;
            _createCadastroVeículoViewModel = createCadastroVeículoViewModel;
            _createCadastroMotoristaViewModel = createCadastroMotoristaViewModel;
            _createCadastroViagemViewModel = createCadastroViagemViewModel;
            _createSaídaDeVeículoViewModel = createSaídaDeVeículoViewModel;
            _createRetornoDeVeículoViewModel = createRetornoDeVeículoViewModel;
            _createCadastroAbastecimentoViewModel = createCadastroAbastecimentoViewModel;
            _createFiltroListagemViewModel = createFiltroListagemViewModel;
            _createCadastroManutençãoViewModel = createCadastroManutençãoViewModel;
            _createCadastroModelosViewModel = createCadastroModelosViewModel;
            _createFiltroRelatórioViewModel = createFiltroRelatórioViewModel;
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
                TipoDialogue.CadastroDeAbastecimento => _createCadastroAbastecimentoViewModel(),
                TipoDialogue.Filtros => _createFiltroListagemViewModel(),
                TipoDialogue.CadastroDeManutenção => _createCadastroManutençãoViewModel(),
                TipoDialogue.CadastroDeModelos => _createCadastroModelosViewModel(),
                TipoDialogue.Relatório => _createFiltroRelatórioViewModel(),
                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}