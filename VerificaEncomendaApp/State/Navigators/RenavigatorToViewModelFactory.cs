using ControleFrota.ViewModels;

namespace ControleFrota.State.Navigators
{
    public class RenavigatorToViewModelFactory<TViewModel> : IReNavigator where TViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly CriaViewModel<TViewModel> _createViewModel;

        public RenavigatorToViewModelFactory(INavigator navigator, CriaViewModel<TViewModel> createViewModel)
        {
            _navigator = navigator;
            _createViewModel = createViewModel;
        }

        public void Renavigate()
        {
            _navigator.ViewModelAtual = _createViewModel();
        }
    }
}
