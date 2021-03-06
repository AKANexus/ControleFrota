using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class Modelo : EntityBase
    {
        public Marcas Marca { get; set; } = Marcas.Outros;
        public string Nome { get; set; }
        public TipoVeículo TipoVeículo { get; set; }
        public List<ModeloManutenção> ModeloManutenções { get; set; } = new();
    }
}
