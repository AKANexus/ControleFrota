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
        public string Chassis { get; set; }
        public DateTime ÚltimoLicenciamento { get; set; }

        [NotMapped]
        public DateTime ÚltimoAbastecimento
        {
            get
            {
                return Abastecimentos.OrderBy(x => x.KM).LastOrDefault()?.DataHora ?? default;
            }
        }

        [NotMapped]
        public DateTime ÚltimaManutenção
        {
            get
            {
                return Manutenções.OrderBy(x => x.KM).LastOrDefault()?.DataHora ?? default;

            }
        }

        [NotMapped]
        public string ÚltimoMotorista
        {
            get
            {
                return Viagens.OrderBy(x => x.Saída).LastOrDefault()?.Motorista?.Nome;
            }
        }

        public bool EmUso
        {
            get
            {
                return Viagens.Any(x => x.Retorno == default);
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
                var últimaManutenção = Manutenções.Count > 0 ? Manutenções.Max(x => x.KM) : 0;
                var últimoAbastecimento = Abastecimentos.Count > 0 ?  Abastecimentos.DefaultIfEmpty().Max(x=>x.KM) : 0;
                var últimaViagem = Viagens.Count > 0 ? Viagens.DefaultIfEmpty().Max(x => x.KMFinal) : 0;
                return Math.Max(Math.Max(últimoAbastecimento, últimaManutenção), últimaViagem);
            }
        }

        public bool Ativo { get; set; } = true;
        [NotMapped] public bool PróximoDoLicenciamento => ÚltimoLicenciamento <= DateTime.Now.AddDays(60);

        [NotMapped] public int IdViagemEmAberto => Viagens.FirstOrDefault(x => x.Retorno == default)?.ID ?? default;
    }
}
