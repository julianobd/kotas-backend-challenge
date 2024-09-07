using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonKotas.Infra.Helper
{
    public static class Extentions
    {
        public static string ToBase64(this string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        public static string ToBase64(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static void ResetIds(this object? obj)
        {
            if (obj is null) return;
            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.GetIndexParameters().Length > 0)
                    continue;

                //single property Id
                if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) &&
                    (property.PropertyType == typeof(int) || property.PropertyType == typeof(long)) &&
                    property.CanWrite)
                {
                    property.SetValue(obj, Convert.ChangeType(0, property.PropertyType));
                }

                //class
                else if (property.PropertyType.IsClass && property.PropertyType != typeof(string) && !typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    var propertyValue = property.GetValue(obj);
                    if (propertyValue != null)
                    {
                        propertyValue.ResetIds();
                    }
                }
                //list class
                else if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string))
                {
                    if (property.GetValue(obj) is IEnumerable collection)
                    {
                        foreach (var item in collection)
                        {
                            item?.ResetIds();
                        }
                    }
                }
            }
        }
    }
}
