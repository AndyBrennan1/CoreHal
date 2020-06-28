using System;
using Validation;
using System.Text.RegularExpressions;

namespace CoreHal.Graph
{
    public class Curie
    {
        public string Key { get; set; }
        public Uri Href { get; set; }

        static readonly Regex placeholderRegex = new Regex(@"\{([^\}]+)\}", RegexOptions.Compiled);

        public Curie(string key, string href)
        {
            Requires.NotNullOrEmpty(key, nameof(key));
            Requires.NotNullOrEmpty(href, nameof(href));

            EnsureCurieIsTemplated(href);
            EnsureCurieOnlyContainsOneTemplatePlaceHolder(href);
            EnsureTemplatePlaceholderIsAtTheEnd(href);

            var uriWithoutTemplateForValidityCheck = placeholderRegex.Replace(href, delegate (Match match) { return string.Empty; }).TrimEnd('/');

            if (Uri.IsWellFormedUriString(uriWithoutTemplateForValidityCheck, UriKind.RelativeOrAbsolute))
            {
                Key = key;
                Href = new Uri(href, UriKind.RelativeOrAbsolute);
            }
            else
            {
                throw new ArgumentException("Your Href does not meet RFC 3986 and RFC 3987 standards");
            }
        }

        private static void EnsureCurieOnlyContainsOneTemplatePlaceHolder(string href)
        {
            if (placeholderRegex.Matches(href).Count > 1)
                throw new ArgumentException("Curies can only contain 1 template placeholder.", nameof(href));
        }

        private static void EnsureCurieIsTemplated(string href)
        {
            if (!placeholderRegex.IsMatch(href))
                throw new ArgumentException("Curies must be templated", nameof(href));
        }

        private void EnsureTemplatePlaceholderIsAtTheEnd(string href)
        {
            var tempGuid = Guid.NewGuid();
            var placeHolderCheck =
               placeholderRegex.Replace(href, delegate (Match match) { return tempGuid.ToString(); }).TrimEnd('/');

            if (!placeHolderCheck.EndsWith(tempGuid.ToString()))
                throw new ArgumentException("The template placeholder must be at the end of the Curie Href.", nameof(href));
        }
    }
}