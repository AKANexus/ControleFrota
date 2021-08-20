using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class Veículo : EntityBase
    {
        public string Placa { get; set; }
        public Marca Marca { get; set; }
        public Modelo Modelo { get; set; }
        public string RENAVAM { get; set; }
        public DateTime ÚltimoLicenciamento { get; set; }

        [NotMapped]
        public DateTime ÚltimoAbastecimento
        {
            get
            {
                return Abastecimentos.OrderBy(x => x.KM).Last().DataHora;
            }
        }

        [NotMapped]
        public DateTime ÚltimaManutenção
        {
            get
            {
                return Manutenções.OrderBy(x => x.KM).Last().DataHora;

            }
        }

        [NotMapped]
        public string ÚltimoMotorista
        {
            get
            {
                return Viagens.OrderBy(x => x.Saída).Last().Motorista.Nome;
            }
        }

        public ObservableCollection<Viagem> Viagens { get; set; }
        public ObservableCollection<Manutenção> Manutenções { get; set; }
        public ObservableCollection<Abastecimento> Abastecimentos { get; set; }
        public ObservableCollection<ManutençãoProgramada> ManutençõesProgramadas { get; set; }

        [NotMapped]
        public decimal ÚltimaQuilometragemInformada
        {
            get
            {
                var últimaManutenção = Manutenções.OrderBy(x => x.KM).Last().KM;
                var últimoAbastecimento = Abastecimentos.OrderBy(x => x.KM).Last().KM;
                var últimaViagem = Viagens.OrderBy(x => x.KMFinal).Last().KMFinal;
                return Math.Max(Math.Max(últimoAbastecimento, últimaManutenção), últimaViagem);
            }
        }

        public bool Ativo { get; set; } = true;
    }
}
