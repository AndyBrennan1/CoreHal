using System;
using System.Text.RegularExpressions;
using Validation;

namespace CoreHal.Graph
{
    [Serializable]
    public class Link
    {
        public Uri Href { get; private set; }

        public bool Templated { get; private set; }

        public string Title { get; set; }

        static readonly Regex placeholderRegex = new Regex(@"\{([^\}]+)\}", RegexOptions.Compiled);

        public Link(string href) : this(href, null)
        {
        }

        public Link(string href, string title)
        {
            Requires.NotNullOrEmpty(href, nameof(href));

            if (ContainsMoreThan1PlaceHolder(href))
                throw new ArgumentException("Only 1 template placeholder is allowed in a link.", nameof(href));

            var uriWithoutTemplateForValidityCheck =
                placeholderRegex.Replace(href, delegate (Match match) { return string.Empty; }).TrimEnd('/');

            if (Uri.IsWellFormedUriString(uriWithoutTemplateForValidityCheck, UriKind.RelativeOrAbsolute))
            {
                Href = new Uri(href.TrimEnd('/'), UriKind.RelativeOrAbsolute);

                if (ContainsPlaceholderInUrl(href))
                {
                    if (!TemplatePlaceholderIsAtTheEnd(href))
                    {
                        throw new ArgumentException("The template placeholder must be at the end of the link Href.", nameof(href));
                    }

                    Templated = true;
                }
            }
            else
            {
                throw new ArgumentException("Your Href does not meet RFC 3986 and RFC 3987 standards");
            }

            Title = title;
        }

        protected bool ContainsValidPlaceholderUrl(string href)
        {
            if (ContainsPlaceholderInUrl(href) && !ContainsMoreThan1PlaceHolder(href) && TemplatePlaceholderIsAtTheEnd(href))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool ContainsMoreThan1PlaceHolder(string href)
        {
            return placeholderRegex.Matches(href).Count > 1;
        }

        private static bool ContainsPlaceholderInUrl(string href)
        {
            return placeholderRegex.IsMatch(href);
        }

        private bool TemplatePlaceholderIsAtTheEnd(string href)
        {
            var tempGuid = Guid.NewGuid();
            var placeHolderCheck =
               placeholderRegex.Replace(href, delegate (Match match) { return tempGuid.ToString(); }).TrimEnd('/');

            if (!placeHolderCheck.EndsWith(tempGuid.ToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}