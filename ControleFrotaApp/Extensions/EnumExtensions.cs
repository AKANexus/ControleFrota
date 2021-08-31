using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrota.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum enumObj)
        {
            if (enumObj is null) return String.Empty;
            if (Enum.IsDefined(enumObj.GetType(), enumObj) is false) return String.Empty;

            FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

            object[] attribArray = fieldInfo.GetCustomAttributes(false);

            if (attribArray.Length == 0)
            {
                return enumObj.ToString();
            }
            else
            {
                DescriptionAttribute attrib = attribArray[0] as DescriptionAttribute;
                return attrib.Description;
            }
        }
    }
}
