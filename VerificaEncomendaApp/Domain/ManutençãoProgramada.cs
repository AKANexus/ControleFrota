using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class ManutençãoProgramada
    {
        public Veículo Veículo { get; set; }
        public Manutenção? Manutenção { get; set; }
        public decimal KM { get; set; }
    }
}
