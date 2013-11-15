using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FreeToolbarRemover
{
    /// <summary>
    /// Copied from http://mo.notono.us/2008/07/c-stringinject-format-strings-by-key.html
    /// </summary>
    public static class StringInjectExtension
    {
        /// <summary>
        /// Extension method that replaces keys in a string with the values of matching object properties.
        /// <remarks>Uses <see cref="string.Format()"/> internally; custom formats should match those used for that method.</remarks>
        /// </summary>
        /// <param name="formatString">The format string, containing keys like {foo} and {foo:SomeFormat}.</param>
        /// <param name="injectionObject">The object whose properties should be injected in the string</param>
        /// <returns>A version of the formatString string with keys replaced by (formatted) key values.</returns>
        public static string Inject(this string formatString, object injectionObject)
        {
            return formatString.Inject(GetPropertyHash(injectionObject));
        }

        /// <summary>
        /// Extension method that replaces keys in a string with the values of matching dictionary entries
        /// <remarks>Uses <see cref="String.Format()"/> internally; custom formats should match thouse used for that method</remarks>
        /// </summary>
        /// <param name="format">The format string, containing keys like {foo} and {foo:SomeFormat}.</param>
        /// <param name="dictionary">An <see cref="IDictionary"/> with keys and values to inject into the string</param>
        /// <returns>A version of the formatString string with hashtable keys replaced by {formatted} key values</returns>
        public static string Inject(this string format, IDictionary dictionary)
        {
            return format.Inject(new Hashtable(dictionary));
        }

        /// <summary>
        /// Extension method that replaces keys in a string with the values of matching hashtable entries
        /// <remarks>Uses <see cref="String.Format()"/> internally; custom formats should match thouse used for that method.</remarks>
        /// </summary>
        /// <param name="format">The format string, containing keys like {foo} and {foo:SomeFormat}.</param>
        /// <param name="attributes">A <see cref="Hastable"/> with keys and values to inject into the string</param>
        /// <returns>A version of the formatString string with hashtable keys replaced by {formatted} key values</returns>
        public static string Inject(this string format, Hashtable attributes)
        {
            string result = format;
            if (attributes == null || format == null)
                return result;

            foreach (string attributeKey in attributes.Keys)
            {
                result = result.InjectSingleValue(attributeKey, attributes[attributeKey]);
            }
            return result;
        }

        /// <summary>
        /// Replaces all instances of 'key' in a string with an optionally formatted value, and returns the result
        /// </summary>
        /// <param name="format">The string containing the name unformatted, or formatted </param>
        /// <param name="key">The key name</param>
        /// <param name="replacementValue">The replacement value</param>
        /// <returns>The input string with any instances of the key replacement value</returns>
        public static string InjectSingleValue(this string format, string key, object replacementValue)
        {
            string result = format;
            Regex attributeRegex = new Regex("{(" + key + ")(?:}|(?::(.[^}]*)}))");
            //loop through matches, since each key may be used more than once (and with a different format string)
            foreach (Match m in attributeRegex.Matches(format))
            {
                string replacement = m.ToString();
                if (m.Groups[2].Length > 0) //matched {foo:SomeFormat}
                {
                    //do a double string.Format - first to build the proper format string, and then to format the replacement value
                    string attributeFormatString = string.Format(CultureInfo.InvariantCulture, "{{0:{0}}}", m.Groups[2]);
                    replacement = string.Format(CultureInfo.CurrentCulture, attributeFormatString, replacementValue);
                }
                else //matched {foo}
                {
                    replacement = (replacementValue ?? string.Empty).ToString();
                }
                //perform replacements, one match at a time
                result = result.Replace(m.ToString(), replacement);
            }
            return result;
        }

        /// <summary>
        /// Creates a HashTable based on current object state.
        /// <remarks>Copied from the MVCToolkit HtmlExtensionUtility class</remarks>
        /// </summary>
        /// <param name="properties">The object from which to get the properties</param>
        /// <returns>A <see cref="Hashtable"/> containing the object instance's property names and their values</returns>
        static Hashtable GetPropertyHash(object properties)
        {
            Hashtable values = null;
            if (properties != null)
            {
                values = new Hashtable();
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(properties);
                foreach (PropertyDescriptor prop in props)
                {
                    values.Add(prop.Name, prop.GetValue(properties));
                }
            }
            return values;
        }
    }
}