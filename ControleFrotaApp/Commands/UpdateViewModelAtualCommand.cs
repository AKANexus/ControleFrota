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

        public UpdateViewModelAtualCommand(INavigator navigator, IViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is TipoView)
            {
                TipoView tipoView = (TipoView)parameter;

                _navigator.ViewModelAtual = _viewModelFactory.CreateViewModel(tipoView);
            }
        }
    }
}
