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

}
