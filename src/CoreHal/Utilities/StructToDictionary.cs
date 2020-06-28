using System;
using System.Collections.Generic;
using System.Reflection;

namespace CoreHal.Utilities
{
    public static class StructToDictionary
    {
        public static IDictionary<string, object> ToDictionary(ValueType value) 
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
