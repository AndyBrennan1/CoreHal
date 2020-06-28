using System;
using Validation;

namespace CoreHal.Graph
{
    public class CurieLink : Link
    {
        public string Name { get; }

        public CurieLink(string name, string href) : base(href)
        {
            Requires.NotNullOrEmpty(name, nameof(name));

            if (!ContainsValidPlaceholderUrl(href))
                throw new ArgumentException("A curie must always contain a single template placeholder at the end of the url.", nameof(href));

            Name = name;
        }
    }
}