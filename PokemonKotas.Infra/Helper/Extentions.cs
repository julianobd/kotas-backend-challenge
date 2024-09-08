using System.Collections;
using System.Text;

namespace PokemonKotas.Infra.Helper;

/// <summary>
///     Provides extension methods for various types, including string and object.
/// </summary>
public static class Extentions
{
    /// <summary>
    ///     Converts the specified string to its Base64 encoded representation.
    /// </summary>
    /// <param name="value">The string to be encoded.</param>
    /// <returns>A Base64 encoded string representation of the input value.</returns>
    public static string ToBase64(this string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    ///     Converts the specified byte array to its Base64 encoded representation.
    /// </summary>
    /// <param name="bytes">The byte array to be encoded.</param>
    /// <returns>A Base64 encoded string representation of the input byte array.</returns>
    public static string ToBase64(this byte[] bytes)
    {
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    ///     Resets the IDs of the specified object and its nested objects to zero.
    /// </summary>
    /// <param name="obj">The object whose IDs need to be reset.</param>
    /// <remarks>
    ///     This method recursively traverses the properties of the object. If a property named "Id" is found and it is of type
    ///     int or long, its value is set to zero.
    ///     The method also handles nested objects and collections, resetting their IDs as well.
    /// </remarks>
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
            else if (property.PropertyType.IsClass && property.PropertyType != typeof(string) &&
                     !typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                var propertyValue = property.GetValue(obj);
                if (propertyValue != null) propertyValue.ResetIds();
            }
            //list class
            else if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) &&
                     property.PropertyType != typeof(string))
            {
                if (property.GetValue(obj) is IEnumerable collection)
                    foreach (var item in collection)
                        item?.ResetIds();
            }
        }
    }
}