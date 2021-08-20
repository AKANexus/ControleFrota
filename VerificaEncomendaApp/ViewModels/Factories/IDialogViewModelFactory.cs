using ControleFrota.Services;
using ControleFrota.ViewModels.DialogWindows;

namespace ControleFrota.ViewModels.Factories
{
    public interface IDialogViewModelFactory
    {
        DialogContentViewModelBase CreateDialogContentViewModel(TipoDialogue tipoView);
    }
}