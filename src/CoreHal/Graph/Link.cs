using System;
using System.Text.RegularExpressions;
using Validation;

namespace CoreHal.Graph
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Link
    {
        /// <summary>
        /// 
        /// </summary>
        public Uri Href { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Templated { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        static readonly Regex placeholderRegex = new Regex(@"\{([^\}]+)\}", RegexOptions.Compiled);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="href"></param>
        public Link(string href) : this(href, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="href"></param>
        /// <param name="title"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="href"></param>
        /// <returns></returns>
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