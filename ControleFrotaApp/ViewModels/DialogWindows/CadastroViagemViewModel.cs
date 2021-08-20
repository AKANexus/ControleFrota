using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleFrota.Domain;

namespace ControleFrota.ViewModels.DialogWindows
{
    public class CadastroViagemViewModel : DialogContentViewModelBase
    {
        public Viagem ViagemSelecionada { get; set; }
        public ObservableCollection<Motorista> Motoristas { get; set; }
        public ObservableCollection<Veículo> Veículos { get; set; }

        public List<TipoGasto> TipoGastos { get; set; }

        public Motorista MotoristaSelecionado { get; set; }
        public Veículo VeículoSelecionado { get; set; }

        public DateTime Saída { get; set; }
    }
}
