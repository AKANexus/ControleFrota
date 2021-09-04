using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ControleFrota.Domain;
using ControleFrota.Extensions;

namespace ControleFrota.Auxiliares
{
    public class ReportObject
    {
        private readonly Veículo _veículo;
        private readonly DateTime _dataInicial;
        private readonly DateTime _dataFinal;
        private readonly List<ManutençãoJson> _manutenções;
        private readonly CultureInfo ptBR = new("pt-BR");
        private readonly List<ViagemJson> _viagens;
        private readonly List<AbastecimentoJson> _abastecimentos;

        public ReportObject(Veículo veículo, DateTime dataInicial, DateTime dataFinal)
        {
            Thread.CurrentThread.CurrentCulture = ptBR;
            _veículo = veículo;
            _dataInicial = dataInicial;
            _dataFinal = dataFinal;
            _manutenções = new();
            _viagens = new();
            _abastecimentos = new();
            foreach (Manutenção manutenção in veículo.Manutenções.Where(x => x.DataHora.IsBetween(dataInicial, dataFinal, true)).ToList())
            {
                _manutenções.Add(new()
                {
                    KM = manutenção.KM.ToString("##.0 km"),
                    Custo = manutenção.Preço.ToString("C2"),
                    DataHora = manutenção.DataHora.ToShortDateString(),
                    Motorista = manutenção.Motorista.Nome,
                    TipoReparo = manutenção.TipoReparo switch
                    {
                        TipoReparo.Preventivo => "Preventivo",
                        TipoReparo.Reparativo => "Reparativo",
                        _ => throw new ArgumentOutOfRangeException()
                    },
                    TipoManutenção = manutenção.TipoManutenção switch
                    {
                        TipoManutenção.Revisão => "Revisão",
                        TipoManutenção.Troca => "Troca",
                        _ => throw new ArgumentOutOfRangeException()
                    },
                    ÁreaManutenção = manutenção.ÁreaManutenção.GetEnumDescription()
                }
                );
            }

            foreach (Viagem viagem in veículo.Viagens.Where(x => x.Saída.IsBetween(dataInicial, dataFinal, true)).ToList())
            {
                _viagens.Add(new()
                {
                    Motorista = viagem.Motorista.Nome,
                    KMInicial = viagem.KMInicial.ToString("##.0 km"),
                    KMFinal = viagem.KMFinal == default ? "Em viagem" : viagem.KMFinal.ToString("##.0 km"),
                    Saída = viagem.Saída.ToString("dd/MM/yy HH:mm"),
                    Entrada = viagem.Retorno == default ? "Em viagem" : viagem.Retorno.ToString("dd/MM/yy HH:mm")
                });
            }

            foreach (Abastecimento abastecimento in _veículo.Abastecimentos.Where(x=>x.DataHora.IsBetween(dataInicial, dataFinal)).ToList())
            {
                _abastecimentos.Add(new()
                {
                    KM = abastecimento.KM.ToString("##.0 km"),
                    Motorista = abastecimento.Motorista.Nome,
                    ValorPorLitro = abastecimento.ValorPorLitro.ToString("##.000 R$/l"),
                    Litragem = abastecimento.Litragem.ToString("##.0 l"),
                    ValorTotal = abastecimento.ValorTotal.ToString("C2"),
                    DataHora = abastecimento.DataHora.ToShortDateString(),
                    Posto = abastecimento.Posto,
                    Combustível = abastecimento.Combustível.GetEnumDescription(),
                    FormaPagamento = abastecimento.FormasPagamento.ToString()
                });
            }
        }

        [JsonIgnore] public Veículo Veículo => _veículo;

        public string DataInício => _dataInicial.ToShortDateString();
        public string DataFim => _dataFinal.ToShortDateString();
        public string Marca => _veículo.Marca.ToString();
        public string Modelo => _veículo.Modelo.Nome;
        public string Placa => _veículo.Placa;
        public string Quilometragem => $"{_veículo.ÚltimaQuilometragemInformada:#.00} km";
        public string PróximoLicenciamento => _veículo.PróximoLicenciamento.ToString("MM/yyyy");
        public string ÚltimoAbastecimento => _veículo.ÚltimoAbastecimento.ToShortDateString();
        public string ÚltimaManutenção => _veículo.ÚltimaManutenção.ToShortDateString();
        public List<ManutençãoJson> Manutencoes => _manutenções;
        public List<ViagemJson> Viagens => _viagens;
        public List<AbastecimentoJson> Abastecimentos => _abastecimentos;

        public string ConverteParaJson()
        {
            var options = new JsonSerializerOptions
            {

            };
            return JsonSerializer.Serialize(this);
        }
    }

    public class AbastecimentoJson
    {
        public string KM { get; set; }
        public string Motorista { get; set; }
        public string ValorTotal { get; set; }
        public string Litragem { get; set; }
        public string ValorPorLitro { get; set; }
        public string DataHora { get; set; }
        public string Posto { get; set; }
        public string Combustível { get; set; }
        public string FormaPagamento { get; set; }
    }

    public class ViagemJson
    {
        public string Motorista { get; set; }
        public string KMInicial { get; set; }
        public string KMFinal { get; set; }
        public string Saída { get; set; }
        public string Entrada { get; set; }
    }

    public class ManutençãoJson
    {
        public string KM { get; set; }
        public string Custo { get; set; }
        public string DataHora { get; set; }
        public string Motorista { get; set; }
        public string TipoReparo { get; set; }
        public string TipoManutenção { get; set; }
        public string ÁreaManutenção { get; set; }
    }
}
