using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class Combustível : EntityBase
    {
        public string Nome { get; set; }

        public Combustível()
        {
            
        }

        public Combustível(int id, string nome)
        {
            ID = id;
            Nome = nome;
        }
    }
}
