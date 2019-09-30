using System;
using System.Linq;

namespace ACoreX.Extensions.Types
{
    public static class TypeExtensions
    {
        public static string GetFriendlyName(this Type type)
        {
            if (type == typeof(int))
            {
                return "int";
            }
            else if (type == typeof(short))
            {
                return "short";
            }
            else if (type == typeof(byte))
            {
                return "byte";
            }
            else if (type == typeof(bool))
            {
                return "bool";
            }
            else if (type == typeof(long))
            {
                return "long";
            }
            else if (type == typeof(float))
            {
                return "float";
            }
            else if (type == typeof(double))
            {
                return "double";
            }
            else if (type == typeof(decimal))
            {
                return "decimal";
            }
            else if (type == typeof(string))
            {
                return "string";
            }  else if (type == typeof(DateTime))
            {
                return "DateTime";
            }
            else if (type.IsGenericType)
            {

                return type.FullName.Split('`')[0] + "<" + string.Join(", ", type.GetGenericArguments().Select(x => GetFriendlyName(x)).ToArray()) + ">";
            }
            else
            {
                return type.FullName;
            }
        }

        public static string GetFriendlyNameTS(this Type type)
        {
            if (type == typeof(int))
            {
                return "number";
            }
            else if (type == typeof(short))
            {
                return "number";
            }
            else if (type == typeof(byte))
            {
                return "byte";
            }
            else if (type == typeof(bool))
            {
                return "boolean";
            }
            else if (type == typeof(long))
            {
                return "number";
            }
            else if (type == typeof(float))
            {
                return "number";
            }
            else if (type == typeof(double))
            {
                return "number";
            }
            else if (type == typeof(decimal))
            {
                return "number";
            }
            else if (type == typeof(string))
            {
                return "string";
            }
            else if (type == typeof(DateTime))
            {
                return "Date";
            }
            else if (type.IsGenericType)
            {

                return type.FullName.Split('`')[0] + "<" + string.Join(", ", type.GetGenericArguments().Select(x => GetFriendlyName(x)).ToArray()) + ">";
            }
            else
            {
                return type.FullName;
            }
        }

        public static string PrettyName(this Type t)
        {
            if (t.IsGenericType)
            {
                return string.Format(
                    "{0}<{1}>",
                    t.FullName.Substring(0, t.FullName.LastIndexOf("`", StringComparison.InvariantCulture)),
                    string.Join(", ", t.GetGenericArguments().Select(PrettyName)));
            }
            return t.FullName;
        }

        public static string GetTypeName(this Type t)
        {
            string arraytest = !t.FullName.Contains("[]") && t.FullName.Contains("IEnumerable") ? "[]" : "";
            if (t.IsGenericType)
            {

                return string.Join(", ", t.GetGenericArguments().Select(GetTypeName))+ arraytest;
            }
    
            if (t.FullName.LastIndexOf(".", StringComparison.InvariantCulture) > 1)
            {
                string finalString = t.FullName.Substring(t.FullName.LastIndexOf(".", StringComparison.InvariantCulture) + 1);
                return finalString == "String" ? "string" : finalString;
            };
            return t.FullName;
        }
        public static string GetTypeFileName(this Type t)
        {
            
            if (t.IsGenericType)
            {

                return string.Join(", ", t.GetGenericArguments().Select(GetTypeName));
            }
    
            if (t.FullName.LastIndexOf(".", StringComparison.InvariantCulture) > 1)
            {
                string finalString = t.FullName.Substring(t.FullName.LastIndexOf(".", StringComparison.InvariantCulture) + 1);
                return finalString == "String" ? "string" : finalString;
            };
            return t.FullName;
        }
    }
}
