using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Entidades.Extensions
{
    public static class EnumExtension
    {
        public static string GetEnumDescription(this Enum value)
        {
            return GetDescription(value.ToString(), GetCustomAttributes(GetFieldInfo(value)));
        }

        public static List<string> GetEnumDescriptions(this Enum value)
        {
            return (from object itemValue in value.GetType().GetEnumValues()
                    select GetDescription(itemValue.ToString(), GetCustomAttributes(GetFieldInfo(itemValue)))).ToList();
        }

        private static string GetDescription(string enumValue, IReadOnlyList<DescriptionAttribute> attributes)
        {
            return attributes?.Count > 0 ? attributes[0]?.Description : enumValue;
        }

        private static DescriptionAttribute[] GetCustomAttributes(FieldInfo field)
        {
            return (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
        }

        private static FieldInfo GetFieldInfo(object item)
        {
            return item.GetType().GetField(item.ToString());
        }
    }
}