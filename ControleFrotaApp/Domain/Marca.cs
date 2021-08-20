using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class Marca : EntityBase
    {
        public Marca()
        {
            
        }

        public Marca(int id, string nome)
        {
            ID = id;
            Nome = nome;
        }
        public string Nome { get; set; }
    }
}
