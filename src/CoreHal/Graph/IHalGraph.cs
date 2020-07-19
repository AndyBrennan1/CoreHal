using System.Collections.Generic;

namespace CoreHal.Graph
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHalGraph
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rel"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        IHalGraph AddLink(string rel, Link link);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rel"></param>
        /// <param name="links"></param>
        /// <returns></returns>
        IHalGraph AddLinks(string rel, IEnumerable<Link> links);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curie"></param>
        /// <returns></returns>
        IHalGraph AddCurie(Curie curie);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curies"></param>
        /// <returns></returns>
        IHalGraph AddCuries(IEnumerable<Curie> curies);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="embeddedGraph"></param>
        /// <returns></returns>
        IHalGraph AddEmbeddedItem(string key, IHalGraph embeddedGraph);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="embeddedGraphs"></param>
        /// <returns></returns>
        IHalGraph AddEmbeddedItemCollection(string key, IEnumerable<IHalGraph> embeddedGraphs);
    }
}