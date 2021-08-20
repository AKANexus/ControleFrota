using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class Viagem : EntityBase
    {
        public Motorista Motorista { get; set; }
        public string Motivo { get; set; }
        public decimal KMInicial { get; set; }
        public decimal KMFinal { get; set; }
        public DateTime Saída { get; set; }
        public DateTime Retorno { get; set; }
    }
}
