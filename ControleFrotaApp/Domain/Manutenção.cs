using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class Manutenção : EntityBase
    {
        public string Peça { get; set; }
        public string Motivo { get; set; }
        public decimal KM { get; set; }
        public decimal Preço { get; set; }
        public string Local { get; set; }
        public DateTime DataHora { get; set; }
    }
}
