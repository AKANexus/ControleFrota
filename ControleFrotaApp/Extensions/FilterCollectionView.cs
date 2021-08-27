using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ControleFrota.Domain;

namespace ControleFrota.Extensions
{
    public static class FilterCollectionView
    {
        public static void FilterView(this ICollectionView collectionView, EntityBase entity)
        {
            FilteringInfo filteringInfo = entity as FilteringInfo;
            switch (filteringInfo.FilterInfo.Split(':')[0])
            {
                case "wholefield":
                    collectionView.Filter += (x) => x is EntityBase entityBase && (string)entityBase.GetPropValue(filteringInfo.Property.Name) == filteringInfo.FilterInfo.Split(':')[1];
                    break;
                case "contains":
                    collectionView.Filter += (x) => x is EntityBase entityBase && ((string)entityBase.GetPropValue(filteringInfo.Property.Name)).Contains(filteringInfo.FilterInfo.Split(':')[1]);
                    break;
                case "startswith":
                    collectionView.Filter += (x) => x is EntityBase entityBase && ((string)entityBase.GetPropValue(filteringInfo.Property.Name)).StartsWith(filteringInfo.FilterInfo.Split(':')[1]);
                    break;
                case "datebetween":
                    string[] dates = filteringInfo.FilterInfo.Split(':')[1].Split(';');
                    DateTime start = DateTime.Parse(dates[0]);
                    DateTime end = DateTime.Parse(dates[1]).AddDays(1).AddSeconds(-1);
                    collectionView.Filter += (x) => x is EntityBase entityBase && ((DateTime)entityBase.GetPropValue(filteringInfo.Property.Name)).IsBetween(start, end);
                    break;
                case "valuebetween":
                    string[] values = filteringInfo.FilterInfo.Split(':')[1].Split(';');
                    decimal startValue = decimal.Parse(values[0]);
                    decimal endValue = decimal.Parse(values[1]);
                    collectionView.Filter += (x) => x is EntityBase entityBase && ((decimal)entityBase.GetPropValue(filteringInfo.Property.Name)).IsBetween(startValue, endValue);
                    break;
            }
        }

    }
}
