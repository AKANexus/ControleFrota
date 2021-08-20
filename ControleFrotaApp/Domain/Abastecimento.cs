using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class Abastecimento : EntityBase
    {
        public decimal KM { get; set; }
        public Enums.Combustível Combustível { get; set; }
        public Motorista Motorista { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal Litragem { get; set; }
        public DateTime DataHora { get; set; }
    }
}
