using System;
using ControleFrota.ViewModels;

namespace ControleFrota.State.Navigators
{
    public class Navigator : INavigator
    {
        private ViewModelBase _viewModelAtual;

        public ViewModelBase ViewModelAtual
        {
            get => _viewModelAtual;
            set {
                _viewModelAtual = value;
                StateChanged?.Invoke();
            }
        }

        private int idSelecionado;

        public int IDSelecionado
        {
            get => idSelecionado;
            set => idSelecionado = value;
        }


        public event Action StateChanged;
    }
}
