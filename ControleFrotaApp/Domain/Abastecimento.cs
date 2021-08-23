using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class Abastecimento : EntityBase
    {
        public decimal KM { get; set; }
        public Combustível Combustível { get; set; }
        public Motorista Motorista { get; set; }

        [NotMapped] public decimal ValorPorLitro => ValorTotal / Litragem;
        public decimal Litragem { get; set; }
        public DateTime DataHora { get; set; }
        public Veículo Veículo { get; set; }
        public string Posto { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
