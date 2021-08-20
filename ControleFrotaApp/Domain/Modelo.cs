using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class Modelo : EntityBase
    {
        public Marca Marca { get; set; }
        public string Nome { get; set; }
    }
}
