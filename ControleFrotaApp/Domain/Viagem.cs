using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class Viagem : EntityBase
    {
        public Motorista Motorista { get; set; }
        public Veículo Veículo { get; set; }
        public string Motivo { get; set; }
        public decimal KMInicial { get; set; }
        public decimal KMFinal { get; set; }
        public DateTime Saída { get; set; }
        public DateTime Retorno { get; set; }
        public ObservableCollection<Gasto> Gastos { get; set; } = new();
        [NotMapped]
        public bool ViagemAberta => Retorno == default;

        [NotMapped]
        public decimal TotalDeGastos => Gastos.Sum(x => x.Valor);

        [NotMapped] 
        public decimal TotalViagem
        {
            get
            {
                var distância = KMFinal - KMInicial;
                return distância < 0 ? 0 : distância;
            }
        }
    }
}
