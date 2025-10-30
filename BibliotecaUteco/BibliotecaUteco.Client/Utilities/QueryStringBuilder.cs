using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace BibliotecaUteco.Client.Utilities
{
    public class QueryStringBuilder
    {
        /// <summary>
        /// Convierte un objeto en un query string
        /// </summary>
        /// <param name="obj">Objeto a convertir</param>
        /// <param name="trimStrings">Si es true, aplica Trim() a las propiedades string</param>
        /// <returns>Query string sin el signo de interrogación inicial</returns>
        public static string ToQueryString(object obj, bool trimStrings = true)
        {
            if (obj == null)
                return string.Empty;

            var query = HttpUtility.ParseQueryString(string.Empty);
            var properties = obj.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj);

                if (value == null)
                {
                    query[prop.Name] = string.Empty;
                    continue;
                }

                if (value is string stringValue)
                {
                    query[prop.Name] = trimStrings ? stringValue.Trim() : stringValue;
                }
                else if (value is System.Collections.IEnumerable enumerable && !(value is string))
                {
                    var items = enumerable.Cast<object>().Where(item => item != null);
                    query[prop.Name] = string.Join(",", items);
                }
                else
                {
                    query[prop.Name] = value.ToString();
                }
            }

            return query.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Convierte un diccionario en un query string
        /// </summary>
        public static string ToQueryString(
            Dictionary<string, object> parameters,
            bool trimStrings = true
        )
        {
            if (parameters == null || !parameters.Any())
                return string.Empty;

            var query = HttpUtility.ParseQueryString(string.Empty);

            foreach (var kvp in parameters)
            {
                if (kvp.Value == null)
                {
                    query[kvp.Key] = string.Empty;
                    continue;
                }

                if (kvp.Value is string stringValue)
                {
                    query[kvp.Key] = trimStrings ? stringValue.Trim() : stringValue;
                }
                else
                {
                    query[kvp.Key] = kvp.Value.ToString();
                }
            }

            return query.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Convierte un objeto en un query string usando nombres personalizados
        /// </summary>
        public static string ToQueryString(
            object obj,
            Dictionary<string, string> propertyNameMap,
            bool trimStrings = true
        )
        {
            if (obj == null)
                return string.Empty;

            var query = HttpUtility.ParseQueryString(string.Empty);
            var properties = obj.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj);
                var queryParamName = propertyNameMap.ContainsKey(prop.Name)
                    ? propertyNameMap[prop.Name]
                    : prop.Name;

                if (value == null)
                {
                    query[queryParamName] = string.Empty;
                    continue;
                }

                if (value is string stringValue)
                {
                    query[queryParamName] = trimStrings ? stringValue.Trim() : stringValue;
                }
                else
                {
                    query[queryParamName] = value.ToString();
                }
            }

            return query.ToString() ?? string.Empty;
        }
    }
}
