using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsBetween(this DateTime dt, DateTime start, DateTime end, bool inclusive = true)
        {
            return inclusive ? dt >= start && dt <= end : dt > start && dt < end;
        }
    }
}
