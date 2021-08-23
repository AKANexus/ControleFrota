using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class TipoGasto : EntityBase
    {
        public string Descrição { get; set; }

        public TipoGasto()
        {
            
        }

        public TipoGasto(int id, string descrição)
        {
            ID = id;
            Descrição = descrição;
        }
    }
}
