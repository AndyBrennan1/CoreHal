using System;
using Validation;

namespace CoreHal.Graph
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CurieLink : Link
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="href"></param>
        public CurieLink(string name, string href) : base(href)
        {
            Requires.NotNullOrEmpty(name, nameof(name));

            if (!ContainsValidPlaceholderUrl(href))
                throw new ArgumentException("A curie must always contain a single template placeholder at the end of the url.", nameof(href));

            Name = name;
        }
    }
}