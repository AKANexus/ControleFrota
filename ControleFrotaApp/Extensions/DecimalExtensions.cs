using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Extensions
{
    public static class DecimalExtensions
    {
        public static bool IsBetween(this decimal value, decimal lowerValue, decimal higherValue, bool inclusive = true)
        {
            return inclusive ? value <= higherValue && value >= lowerValue : value < higherValue && value > lowerValue;
        }
    }
}
