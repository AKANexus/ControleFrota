using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class Motorista : EntityBase
    {
        public string Nome { get; set; }
        public string CNH { get; set; }
        public DateTime ValidadeCNH { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;
        public Setor Setor { get; set; }
        [NotMapped] public bool PróximoDoVencimento => ValidadeCNH <= DateTime.Now.AddDays(60);

    }
}
