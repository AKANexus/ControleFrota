using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class ModeloManutenção
    {
        public int ModeloId { get; set; }
        public Modelo Modelo { get; set; }

        public int ManutençãoProgramadaId { get; set; }
        public ManutençãoProgramada ManutençãoProgramada { get; set; }

    }
}
