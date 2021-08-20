using System;
using ControleFrota.ViewModels;

namespace ControleFrota.State.Navigators
{
    public enum TipoView
    {
        TelaInicial
    }

    public interface INavigator
    {
        public ViewModelBase ViewModelAtual { get; set; }
        event Action StateChanged;
        public int IDSelecionado { get; set; }
    }
}
