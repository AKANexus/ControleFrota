namespace ControleFrota.ViewModels.DialogWindows
{
    public delegate TViewModel CriaDialogViewModel<TViewModel>() where TViewModel : DialogContentViewModelBase;




    public class DialogContentViewModelBase : ViewModelBase
    {
    }
}