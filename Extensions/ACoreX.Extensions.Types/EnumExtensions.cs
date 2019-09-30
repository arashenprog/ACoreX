using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ACoreX.Extensions.Types
{
    public static class EnumExtensions
    {
        public static string GetDescription<TEnum>(this TEnum value) where TEnum : struct
        {
            return
            value
            .GetType()
            .GetMember(value.ToString())
            .FirstOrDefault()
            ?.GetCustomAttribute<DescriptionAttribute>()
            ?.Description;
        }
    }
}
