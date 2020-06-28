using CoreHal.Annotation;
using System.Collections.Generic;
using System.ComponentModel;
using Validation;

namespace CoreHal.Utilities
{
    public static class ObjectToDictionary
    {
        public static IDictionary<string, object> ToDictionary(this object source)
        {
            Requires.NotNull(source, nameof(source));

            var dictionary = new Dictionary<string, object>();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
            {
                if (NotMarkedAsNoOutput(property))
                {
                    object value = property.GetValue(source);

                    dictionary.Add(property.Name, value);
                }
            }

            return dictionary;
        }

        private static bool NotMarkedAsNoOutput(PropertyDescriptor property)
        {
            return !property.Attributes.Contains(new NoOutput());
        }
    }
}