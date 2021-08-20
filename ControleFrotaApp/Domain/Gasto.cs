using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class Gasto : EntityBase
    {
        public TipoGasto TipoGasto { get; set; }
        public decimal Valor { get; set; }
    }
}
