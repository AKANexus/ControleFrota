using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleFrota.Extensions;

namespace ControleFrota.Domain
{
    public class Manutenção : EntityBase
    {
        //[Description("Peça Reparada")]
        //public string Peça { get; set; }
        [Description("Reparo")]
        public ÁreaManutenção ÁreaManutenção { get; set; }
        [Description("Tipo de reparo")]
        public TipoManutenção TipoManutenção { get; set; }
        //[Description("Tipo do Reparo")]
        public TipoReparo TipoReparo { get; set; }
        [Description("Quilometragem")]
        public decimal KM { get; set; }
        [Description("Preço do Reparo")]
        public decimal Preço { get; set; }
        [Description("Local do Reparo")]
        public string Local { get; set; }
        [Description("Data do Reparo")]
        public DateTime DataHora { get; set; }
        //[Description("Veículo")]
        public Veículo Veículo { get; set; }
        //[Description("Motorista")]
        public Motorista Motorista { get; set; }

        [Description("Observações")] 
        public string Observações { get; set; }

        public string ReportÁreaManutenção => ÁreaManutenção.GetEnumDescription();
        public string ReportTipoReparo => TipoReparo.GetEnumDescription();
    }
}
