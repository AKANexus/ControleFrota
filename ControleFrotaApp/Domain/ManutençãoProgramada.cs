using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class ManutençãoProgramada : EntityBase
    {
        public decimal DeltaKM { get; set; }
        public int DeltaDias { get; set; }
        public ÁreaManutenção ÁreaManutenção { get; set; }
        public TipoManutenção TipoManutenção { get; set; }
        public TipoVeículo TipoVeículo { get; set; }
        public List<ModeloManutenção> ModeloManutenções { get; set; }

        public ManutençãoProgramada()
        {
            
        }

        public ManutençãoProgramada(int id, decimal deltaKM, int deltaDias, ÁreaManutenção áreaManutenção, TipoManutenção tipoManutenção, TipoVeículo tipoVeículo)
        {
            ID = id;
            DeltaKM = deltaKM;
            DeltaDias = deltaDias;
            ÁreaManutenção = áreaManutenção;
            TipoManutenção = tipoManutenção;
            TipoVeículo = tipoVeículo;
        }
    }
}
