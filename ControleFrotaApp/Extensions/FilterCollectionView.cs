using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ControleFrota.Auxiliares;
using ControleFrota.Domain;

namespace ControleFrota.Extensions
{
    public class FilterCollectionView
    {
        private readonly GroupFilter _filtros = new();
        public Predicate<object> AddNewFilter(EntityBase entity)
        {
            FilteringInfo filteringInfo = entity as FilteringInfo;
            switch (filteringInfo.FilterInfo.Split(':')[0])
            {
                case "wholefield":
                    _filtros.AddFilter(x => x is EntityBase entityBase && (string)entityBase.GetPropValue(filteringInfo.Property.Name) == filteringInfo.FilterInfo.Split(':')[1]);
                    break;
                case "contains":
                    _filtros.AddFilter(x => x is EntityBase entityBase && ((string)entityBase.GetPropValue(filteringInfo.Property.Name)).Contains(filteringInfo.FilterInfo.Split(':')[1]));
                    break;
                case "startswith":
                    _filtros.AddFilter((x) => x is EntityBase entityBase && ((string)entityBase.GetPropValue(filteringInfo.Property.Name)).StartsWith(filteringInfo.FilterInfo.Split(':')[1]));
                    break;
                case "datebetween":
                    string[] dates = filteringInfo.FilterInfo.Split(':')[1].Split(';');
                    DateTime start = DateTime.Parse(dates[0]);
                    DateTime end = DateTime.Parse(dates[1]).AddDays(1).AddSeconds(-1);
                    _filtros.AddFilter((x) => x is EntityBase entityBase && ((DateTime)entityBase.GetPropValue(filteringInfo.Property.Name)).IsBetween(start, end));
                    break;
                case "valuebetween":
                    string[] values = filteringInfo.FilterInfo.Split(':')[1].Split(';');
                    decimal startValue = decimal.Parse(values[0]);
                    decimal endValue = decimal.Parse(values[1]);
                    _filtros.AddFilter((x) => x is EntityBase entityBase && ((decimal)entityBase.GetPropValue(filteringInfo.Property.Name)).IsBetween(startValue, endValue));
                    break;
                case "clearfilters":
                    _filtros.RemoveAllFilters();
                    return null;
            }
            return _filtros.Filter;
        }
    }
}
