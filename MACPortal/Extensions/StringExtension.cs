using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace WellaMates.Extensions
{
    public static class StringExtension
    {
        public static string ShortName(this string value, int maxLength = 31)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            var substs = value.Split(new[] { ' ' });
            var shortName = substs[0] + (substs.Length > 1 ? " " + substs[1] : "") + (substs.Length > 2 ? " " + substs[2] : "");
            return shortName.Length > maxLength ? shortName.Substring(0, maxLength) : shortName;
        }

        public static string Escape(this string value)
        {
            return value;
        }

        public static string MaxChars(this string value, int maxChars)
        {
            if (value.Length > maxChars)
            {
                return value.Substring(0, maxChars - 3) + "...";
            }
            return value;
        }

        public static T EscapeProperties<T>(this T value, List<object> processedObjects = null)
        {
            if (processedObjects == null) processedObjects = new List<object> { value };
            else processedObjects.Add(value);

            Type type = value.GetType();
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                if (propertyInfo.CanRead && propertyInfo.PropertyType == typeof(string))
                {
                    propertyInfo.SetValue(value, HttpUtility.JavaScriptStringEncode((string)propertyInfo.GetValue(value)));
                }
                else if (propertyInfo.CanRead && !propertyInfo.PropertyType.IsValueType)
                {
                    var recursionValue = propertyInfo.GetValue(value);
                    if (recursionValue == null) continue;
                    var items = recursionValue as IEnumerable;
                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            if (processedObjects.Contains(item)) continue;
                            item.EscapeProperties(processedObjects);
                        }
                    }
                    else
                    {
                        if (!processedObjects.Contains(recursionValue))
                            recursionValue.EscapeProperties(processedObjects);
                    }
                }
            }
            return value;
        }

        public static T Copy<T>(this T value, T target)
        {
            foreach (var propertyInfo in value.GetType().GetProperties())
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite && propertyInfo.PropertyType.IsValueType)
                {
                    propertyInfo.SetValue(target, propertyInfo.GetValue(value));
                }
                else if (propertyInfo.CanRead && !propertyInfo.PropertyType.IsValueType)
                {
                    var recursionValue = propertyInfo.GetValue(value);
                    if (recursionValue == null) continue;
                    var items = recursionValue as IEnumerable;
                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            item.EscapeProperties();
                        }
                    }
                    else
                    {
                        recursionValue.EscapeProperties();
                    }
                }
            }
            return value;
        }
    }
}