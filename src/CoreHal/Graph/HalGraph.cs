﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Validation;
using System.Collections.Specialized;
using CoreHal.Utilities;

namespace CoreHal.Graph
{
    public class HalGraph : OrderedDictionary, IHalGraph, IHalGraphType
    {
        private readonly object _model;

        private const string LinksKey = "_links";
        private const string EmbeddedItemKey = "_embedded";
        private const string CuriesKey = "curies";

        public Type ModelType => _model.GetType();

        public HalGraph(object model)
        {
            Requires.NotNull(model, nameof(model));

            if (model is IEnumerable)
            {
                throw new ArgumentException("The model should not be Enumerable. You should use an embedded collection instead", nameof(model));
            }

            _model = model;

            var modelDictionary = _model.ToDictionary();

            foreach (var keyValuePair in modelDictionary)
            {
                if (keyValuePair.Value == null)
                {
                    Add(keyValuePair.Key, null);
                }
                else if (keyValuePair.Value.GetType().IsPrimitive)
                {
                    Add(keyValuePair.Key, keyValuePair.Value);
                }
                else if (!PropertyIsSystemType(keyValuePair.Value.GetType()))
                {
                    Add(keyValuePair.Key, keyValuePair.Value.ToDictionary());
                }
                else
                {
                    Add(keyValuePair.Key, keyValuePair.Value);
                }

            }

        }

        private static bool PropertyIsSystemType(Type propertyType)
        {
            return
                propertyType == typeof(bool)
                || propertyType == typeof(string)
                || propertyType == typeof(DateTime);
        }

        public IHalGraph AddLink(string rel, Link link)
        {
            Requires.NotNullOrEmpty(rel, nameof(rel));
            Requires.NotNull(link, nameof(link));

            if (!Contains(LinksKey))
            {
                Insert(0, LinksKey, new Dictionary<string, object>());
            }

            var linksCollection = (Dictionary<string, object>)this[LinksKey];

            if (linksCollection.ContainsKey(rel))
            {
                if (linksCollection[rel] is IEnumerable)
                {
                    ((List<Link>)linksCollection[rel]).Add(link);
                }
                else
                {
                    RemoveOriginalLinkAndSwapForCollection(rel, (Link)linksCollection[rel], link);
                }
            }
            else
            {
                linksCollection.Add(rel, link);
            }

            return this;
        }

        public IHalGraph AddLinks(string rel, IEnumerable<Link> links)
        {
            Requires.NotNullOrEmpty(rel, nameof(rel));
            Requires.NotNullOrEmpty(links, nameof(links));

            foreach (var link in links)
            {
                AddLink(rel, link);
            }

            return this;
        }

        public IHalGraph AddCurie(Curie curie)
        {
            Requires.NotNull(curie, nameof(curie));

            if (!Contains(LinksKey))
            {
                Insert(0, LinksKey, new Dictionary<string, object>());
            }

            var linksCollection = (Dictionary<string, object>)this[LinksKey];

            if (!linksCollection.ContainsKey(CuriesKey))
            {
                linksCollection.Add(CuriesKey, new List<CurieLink> {
                    new CurieLink(curie.Key, curie.Href.ToString()) });
            }
            else
            {
                var curiesCollection = (List<CurieLink>)linksCollection[CuriesKey];
                curiesCollection.Add(new CurieLink(curie.Key, curie.Href.ToString()));
            }

            return this;
        }

        public IHalGraph AddCuries(IEnumerable<Curie> curies)
        {
            Requires.NotNullOrEmpty(curies, nameof(curies));

            foreach (var curie in curies)
            {
                AddCurie(curie);
            }

            return this;
        }

        public IHalGraph AddEmbeddedItem(string key, IHalGraph embeddedGraph)
        {
            Requires.NotNullOrWhiteSpace(key, nameof(key));
            Requires.NotNull(embeddedGraph, nameof(embeddedGraph));

            if (!Contains(EmbeddedItemKey))
            {
                Insert(Keys.Count, EmbeddedItemKey, new Dictionary<string, object>());
            }

            var embeddedCollection = (Dictionary<string, object>)this["_embedded"];
            embeddedCollection.Add(key, embeddedGraph);

            return this;
        }

        public IHalGraph AddEmbeddedItemCollection(string key, IEnumerable<IHalGraph> embeddedGraphs)
        {
            Requires.NotNullOrWhiteSpace(key, nameof(key));
            Requires.NotNull(embeddedGraphs, nameof(embeddedGraphs));

            if (!Contains(EmbeddedItemKey))
            {
                Insert(Keys.Count, EmbeddedItemKey, new Dictionary<string, object>());
            }

            if (embeddedGraphs.GroupBy(halGraph => ((IHalGraphType)halGraph).ModelType).Count() > 1)
                throw new ArgumentException("An embedded set must represent a collection of the same type.");

            var embeddedCollection = (Dictionary<string, object>)this["_embedded"];
            embeddedCollection.Add(key, embeddedGraphs.ToList());

            return this;
        }

        private void RemoveOriginalLinkAndSwapForCollection(string rel, Link originalLink, Link newLink)
        {
            var linksCollection = (Dictionary<string, object>)this[LinksKey];

            var relLinkkCollection = new List<Link> { originalLink };
            relLinkkCollection.Add(newLink);

            linksCollection.Remove(rel);
            linksCollection.Add(rel, relLinkkCollection);
        }
    }
}