using System;
using System.ComponentModel;

namespace Entidades.Extensions
{
    public static class EnumExtension
    {
        public static string GetEnumDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes?.Length > 0 ? attributes[0]?.Description : value.ToString();
        }

    }
}