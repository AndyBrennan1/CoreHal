using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Validation;

namespace CoreHal.PropertyNaming
{
    public class CamelDashNameConvention : IPropertyNameConvention
    {
        public string Apply(string propertyName)
        {
            string result;

            Requires.NotNullOrEmpty(propertyName, nameof(propertyName));

            if (ProperyIsAbbreviation(propertyName))
            {
                result = propertyName.ToLower();
            }
            else
            {
                var words = SplitOnCapitals(propertyName);

                if (words.Count() > 1)
                {
                    result = CompileMultiWordProperty(words);
                }
                else
                {
                    result = propertyName.ToLower();
                }
            }

            return result;
        }

        private static bool ProperyIsAbbreviation(string propertyName)
        {
            return propertyName.Count(char.IsUpper) == propertyName.Length;
        }

        private static string CompileMultiWordProperty(IEnumerable<string> words)
        {
            int index = 0;
            var sb = new StringBuilder();

            foreach (var word in words)
            {
                if (index == 0)
                {
                    sb.Append(word.ToLower());
                }
                else
                {
                    sb.Append($"-{char.ToUpper(word[0]) + word.Substring(1)}");
                }
                index++;
            }

            return sb.ToString();
        }

        private static IEnumerable<string> SplitOnCapitals(string text)
        {
            Regex regex = new Regex(@"\p{Lu}\p{Ll}*");
            foreach (Match match in regex.Matches(text))
            {
                yield return match.Value;
            }
        }
    }
}