using System.Collections.Generic;

namespace CoreHal.Graph
{
    public interface IHalGraph
    {
        IHalGraph AddLink(string rel, Link link);
        IHalGraph AddLinks(string rel, IEnumerable<Link> links);
        IHalGraph AddCurie(Curie curie);
        IHalGraph AddCuries(IEnumerable<Curie> curies);
        IHalGraph AddEmbeddedItem(string key, IHalGraph embeddedGraph);
        IHalGraph AddEmbeddedItemCollection(string key, IEnumerable<IHalGraph> embeddedGraphs);
    }
}