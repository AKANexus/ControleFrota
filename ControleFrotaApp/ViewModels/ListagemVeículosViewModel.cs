using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleFrota.Domain;

namespace ControleFrota.ViewModels
{
    public class ListagemVeículosViewModel
    {
        public ObservableCollection<Veículo> Veículos { get; set; }

    }
}
