using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class Abastecimento : EntityBase
    {
        [Description("Quilometragem")]
        public decimal KM { get; set; }
        [Description("Combustível")]
        public Combustíveis Combustível { get; set; } = Combustíveis.GasolinaAditivada;
        [NotMapped, Description("Motorista")] public string MotoristaString => Motorista?.Nome;
        public Motorista Motorista { get; set; }

        [NotMapped] 
        [Description("Preço por Litro")]
        public decimal ValorPorLitro => ValorTotal / Litragem;
        [Description("Litros Abastecidos")]
        public decimal Litragem { get; set; }
        [Description("Data")]
        public DateTime DataHora { get; set; }
        //[Description("Viatura")]
        public Veículo Veículo { get; set; }
        [Description("Posto")]
        public string Posto { get; set; }
        [Description("Valor Total")]
        public decimal ValorTotal { get; set; }
        //[Description("Forma de Pagamento")]
        public FormasPagamento FormasPagamento { get; set; } = FormasPagamento.Dinheiro;
    }
}
