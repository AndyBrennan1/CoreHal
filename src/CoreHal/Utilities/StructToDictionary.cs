using System;
using System.Collections.Generic;
using System.Reflection;

namespace CoreHal.Utilities
{
    internal static class StructToDictionary
    {
        internal static IDictionary<string, object> ToDictionary(ValueType value) 
        {
            var dictionary = new Dictionary<string, object>();

            foreach (var field in value.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                dictionary.Add(field.Name, field.GetValue(value));
            }

            return dictionary;
        }
    }
}
