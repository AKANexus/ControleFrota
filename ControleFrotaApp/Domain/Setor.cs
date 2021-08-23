using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class Setor : EntityBase
    {
        public string Descrição { get; set; }

        public Setor()
        {
            
        }

        public Setor(int id, string descrição)
        {
            ID = id;
            Descrição = descrição;
        }
    }
}
