using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{

    public enum FormasPagamento
    {
        Dinheiro,
        Débito,
        Crédito,
        PIX
    }

    public enum Combustíveis
    {
        [Description("Gasolina Comum")]
        GasolinaComum = 1,
        [Description("Gasolina Aditivada")]
        GasolinaAditivada = 2,
        [Description("Etanol Comum")]
        EtanolComum = 3,
        [Description("Etanol Aditivado")]
        EtanolAditivado = 4
    }

    public enum TipoFiltro
    {
        Texto,
        Data,
        Numérico
    }

    public enum TipoReparo
    {
        [Description("Preventivo")]
        Preventivo,
        [Description("Reparativo")]
        Reparativo
    }

    public enum StatusVeículo
    {
        [Description("Garagem")]
        Garagem,
        [Description("Em uso")]
        EmUso,
        [Description("Inativo")]
        Inativo,
        [Description("Próximo do Licenciamento")]
        Licenciamento,
        [Description("Em Manutenção")]
        Manutenção,
        [Description("Manutenção Pendente")]
        ManutençãoPendente
    }

    public enum TipoVeículo
    {
        Carro,
        Moto
    }

    public enum ÁreaManutenção
    {
        [Description("Pneus")]
        PneuPneus,
        [Description("Alinhamento")]
        PneuAlinhamento,
        [Description("Rodízio de Pneus")]
        PneuRodízio,
        [Description("Calots")]
        PneuCalotas,
        [Description("Estepe")]
        PneusEstepe,
        [Description("Suspensão")]
        SuspensãoSuspensão,
        [Description("Correias do Motor")]
        MotorCorreias,
        [Description("Mangueiras do Motor")]
        MotorMangueiras,
        [Description("Velas de Ignição")]
        MotorVelas,
        [Description("Injeção")]
        MotorInjeção,
        [Description("Radiador")]
        MotorRadiador,
        [Description("Pastilhas de Freio")]
        FreioPastilha,
        [Description("Discos dos Freios")]
        FreioDisco,
        [Description("Bateria")]
        BateriaBateria,
        [Description("Fluido de Transmissão")]
        FluidoTransmissão,
        [Description("Fluido de Freio")]
        FluidoFreio,
        [Description("Fluido do Limpador")]
        FluidoLimpador,
        [Description("Óleo")]
        FluidoÓleo,
        [Description("Filtro do Óleo")]
        FiltroÓleo,
        [Description("Filtro de Combustível")]
        FiltroCombustível,
        [Description("Filtro de Ar")]
        FiltroAr,
        [Description("Lâmpadas")]
        Lâmpadas,
        [Description("Limpadores")]
        Limpadores,
        [Description("Outros")]
        Outros
    }

    public enum TipoManutenção
    {
        Revisão,
        Troca
    }

    public enum Marcas
    {
        Outros = -1,
        Fiat = 1,
        Ford,
        Jeep,
        Renault,
        Volkswagen,
        Yamaha,
        Honda,
        Suzuki,
        Chevrolet,
        Hyundai,
        JAC
    }

}
