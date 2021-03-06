using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleFrota.Extensions;

namespace ControleFrota.Domain
{
    public class Veículo : EntityBase
    {
        [Description("Placa")]
        public string Placa { get; set; }

        [Description("Marca"), NotMapped] 
        public Marcas Marca
        {
            get => Modelo?.Marca ?? Marcas.Outros;
        }

        [NotMapped, Description("Modelo")] public string ModeloString => Modelo?.Nome;
        public Modelo Modelo { get; set; }
        [Description("Número de Registro")] 
        public int Registro { get; set; }
        [Description("RENAVAM")]
        public string RENAVAM { get; set; }
        [Description("Chassis")]
        public string Chassis { get; set; }
        [Description("Último Licenciamento")]
        public DateTime ÚltimoLicenciamento { get; set; }

        [NotMapped, Description("Último Abastecimento")] 
        public DateTime ÚltimoAbastecimento
        {
            get
            {
                return Abastecimentos.OrderBy(x => x.KM).LastOrDefault()?.DataHora ?? default;
            }
        }

        [NotMapped, Description("Última Manutenção")] 
        public DateTime ÚltimaManutenção
        {
            get
            {
                return Manutenções.OrderBy(x => x.KM).LastOrDefault()?.DataHora ?? default;

            }
        }

        [NotMapped, Description("Último Motorista")] 
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

        [NotMapped, Description("Quilometragem")] 
        public decimal ÚltimaQuilometragemInformada
        {
            get
            {
                var últimaManutenção = Manutenções.Count > 0 ? Manutenções.Max(x => x.KM) : 0;
                var últimoAbastecimento = Abastecimentos.Count > 0 ? Abastecimentos.DefaultIfEmpty().Max(x => x.KM) : 0;
                var últimaViagem = Viagens.Count > 0 ? Viagens.DefaultIfEmpty().Max(x => x.KMFinal) : 0;
                return Math.Max(Math.Max(últimoAbastecimento, últimaManutenção), últimaViagem);
            }
        }

        public bool Ativo { get; set; } = true;

        [NotMapped, Description("Próximo Licenciamento")] 
        public DateTime PróximoLicenciamento
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Placa)) return default;
                int year = ÚltimoLicenciamento.AddDays(1 - ÚltimoLicenciamento.Day).AddYears(1).Year;
                int month = Placa.Substring(7) switch
                {
                    "1" => 4,
                    "2" => 5,
                    "3" => 6,
                    "4" => 7,
                    "5" => 8,
                    "6" => 8,
                    "7" => 9,
                    "8" => 10,
                    "9" => 11,
                    "0" => 12,
                    _ => 1
                };
                return new DateTime(year, month, 1);
            }
        }
        [NotMapped]
        public bool PróximoDoLicenciamento => PróximoLicenciamento <= DateTime.Now.AddDays(60);

        public bool EmManutenção { get; set; } = false;
        [NotMapped] public int IdViagemEmAberto => Viagens.FirstOrDefault(x => x.Retorno == default)?.ID ?? default;

        [NotMapped, Description("Status do Veículo")] 
        public StatusVeículo Status
        {
            get
            {
                if (!Ativo)
                {
                    if (PróximoDoLicenciamento)
                    {
                        return StatusVeículo.Licenciamento;
                    }
                    else
                    {
                        return StatusVeículo.Inativo;
                    }
                }
                else
                {
                    if (EmUso)
                    {
                        return StatusVeículo.EmUso;
                    }
                    else
                    {
                        if (EmManutenção)
                        {
                            return StatusVeículo.Manutenção;
                        }

                        if (PróximaManutenção is not null)
                        {
                            return StatusVeículo.ManutençãoPendente;
                        }
                        if (PróximoDoLicenciamento)
                        {
                            return StatusVeículo.Licenciamento;
                        }
                        else
                        {
                            return StatusVeículo.Garagem;
                        }
                    }
                }
            }
        }

        [Description("Tipo do Veículo"), NotMapped] 
        public TipoVeículo TipoVeículo => Modelo.TipoVeículo;

        [NotMapped]
        public string TipoIcone => TipoVeículo switch
        {
            TipoVeículo.Carro => "🚗/C",
            TipoVeículo.Moto => "🏍/M",
            _ => throw new ArgumentOutOfRangeException()
        };

        [NotMapped]
        public ManutençãoProgramada PróximaManutenção
        {
            get
            {
                foreach (ModeloManutenção modeloModeloManutençõe in Modelo.ModeloManutenções)
                {
                    ManutençãoProgramada manutençãoTentativa = modeloModeloManutençõe.ManutençãoProgramada;
                    var últimaManutenção = Manutenções.LastOrDefault(x =>
                        x.TipoManutenção >= manutençãoTentativa.TipoManutenção &&
                        x.ÁreaManutenção == manutençãoTentativa.ÁreaManutenção);


                    if (últimaManutenção is null) return manutençãoTentativa;

                    if (manutençãoTentativa.DeltaKM != default)
                    {
                        if (ÚltimaQuilometragemInformada > últimaManutenção.KM + manutençãoTentativa.DeltaKM - 1000)
                            return manutençãoTentativa;
                    }

                    if (manutençãoTentativa.DeltaDias != default)
                    {
                        if (DateTime.Now > últimaManutenção.DataHora.AddDays(manutençãoTentativa.DeltaDias - 7))
                            return manutençãoTentativa;
                    }
                }

                return null;
            }
        }

        [NotMapped, Description("Próxima Manutenção")]
        public string DescriçãoPróximaManutenção => PróximaManutenção is not null
            ? $"{PróximaManutenção.TipoManutenção.GetEnumDescription()} de {PróximaManutenção.ÁreaManutenção.GetEnumDescription()}"
            : null;
    }
}
