using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Domain
{
    public class FilteringInfo : EntityBase
    {
        public PropertyInfo Property { get; set; }
        public string Description { get; set; }
        public string FilterInfo { get; set; }
    }
}
