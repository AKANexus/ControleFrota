using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class Motorista : EntityBase
    {
        public string Nome { get; set; }
        public string CNH { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
