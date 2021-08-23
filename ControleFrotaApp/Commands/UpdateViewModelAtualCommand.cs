using System;
using System.Windows.Input;
using ControleFrota.State.Navigators;
using ControleFrota.ViewModels.Factories;

namespace ControleFrota.Commands
{
    public class UpdateViewModelAtualCommand : ICommand
    {
        private readonly INavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;
        private TipoView _tipoView;

        public UpdateViewModelAtualCommand(INavigator navigator, IViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _navigator.StateChanged += _navigator_StateChanged;
        }

        private void _navigator_StateChanged()
        {
            CanExecuteChanged?.Invoke(this, new());
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (parameter is TipoView tipoView)
            {
                if (tipoView == TipoView.ListaManutenções) return false;
                return tipoView != _tipoView;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is TipoView tipoView)
            {
                _tipoView = tipoView;
                _navigator.ViewModelAtual = _viewModelFactory.CreateViewModel(tipoView);
            }
        }
    }
}
