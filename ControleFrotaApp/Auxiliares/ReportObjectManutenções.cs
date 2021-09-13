using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleFrota.Domain;

namespace ControleFrota.Auxiliares
{
    public class ReportObjectManutenções
    {
        public List<Manutenção> Manutenções { get; }
        public DateTime DataInicial { get; }
        public DateTime DataFinal { get; }

        public ReportObjectManutenções(List<Manutenção> manutenções, DateTime dataInicial, DateTime dataFinal)
        {
            Manutenções = manutenções;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
        }
    }
}
