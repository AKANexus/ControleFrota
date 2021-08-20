using ControleFrota.State.Navigators;

namespace ControleFrota.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(TipoView tipoView);
    }
}