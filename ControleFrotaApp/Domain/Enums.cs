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
        Numérico,
        Enum
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
        PneuPneus =0,
        [Description("Alinhamento")]
        PneuAlinhamento =1,
        [Description("Rodízio de Pneus")]
        PneuRodízio =2,
        [Description("Calots")]
        PneuCalotas =3,
        [Description("Estepe")]
        PneusEstepe =4,
        [Description("Suspensão")]
        SuspensãoSuspensão =5,
        [Description("Correias do Motor")]
        MotorCorreias =6,
        [Description("Mangueiras do Motor")]
        MotorMangueiras =7,
        [Description("Velas de Ignição")]
        MotorVelas =8,
        [Description("Injeção")]
        MotorInjeção =9,
        [Description("Radiador")]
        MotorRadiador =10,
        [Description("Pastilhas/Lonas de Freio")]
        FreioPastilha =11,
        [Description("Discos/Tambores dos Freios")]
        FreioDisco =12,
        [Description("Bateria")]
        BateriaBateria =13,
        [Description("Fluido de Transmissão")]
        FluidoTransmissão =14,
        [Description("Fluido de Freio")]
        FluidoFreio =15,
        [Description("Fluido do Limpador")]
        FluidoLimpador =16,
        [Description("Óleo")]
        FluidoÓleo =17,
        [Description("Filtro do Óleo")]
        FiltroÓleo =18,
        [Description("Filtro de Combustível")]
        FiltroCombustível =19,
        [Description("Filtro de Ar")]
        FiltroAr =20,
        [Description("Lâmpadas/Lanternas")]
        Lâmpadas =21,
        [Description("Limpadores")]
        Limpadores =22,
        [Description("Outros")]
        Outros =23,
        [Description("Pinças dos Freio (M)")]
        MotoFreiosPinças =24,
        [Description("Carburador")]
        MotoCarburador =25,
        [Description("Pinhão (M)")]
        MotoPinhão =26,
        [Description("Coroa (M)")]
        MotoCoroa =27,
        [Description("Correntes/Correias")]
        MotoCorrente =28,
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
