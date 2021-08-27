using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleFrota.Domain;

namespace ControleFrota.State
{
    public class EntityStore<T>
    {
        public T Entity { get; set; }
    }
}
