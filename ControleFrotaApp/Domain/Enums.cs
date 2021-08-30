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
        Manutenção
    }

    public enum TipoVeículo
    {
        Carro,
        Moto
    }

    public enum ÁreaManutenção
    {
        PneuPneus,
        PneuAlinhamento,
        PneuRodízio,
        PneuCalotas,
        PneusEstepe,
        SuspensãoSuspensão,
        MotorCorreias,
        MotorMangueiras,
        MotorVelas,
        MotorInjeção,
        MotorRadiador,
        FreioPastilha,
        FreioDisco,
        BateriaBateria,
        FluidoTransmissão,
        FluidoFreio,
        FluidoLimpador,
        FluidoÓleo,
        FiltroÓleo,
        FiltroCombustível,
        FiltroAr,
        Lâmpadas,
        Limpadores,
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
