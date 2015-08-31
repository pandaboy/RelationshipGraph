using System;
using RelationshipGraph.Nodes;
using RelationshipGraph.Relationships;

namespace RelationshipGraph.Edges
{
    /// <summary>
    /// Extension of HistoryEdge that works with only Entity and Relationship types
    /// </summary>
    public class HistoryConnection : HistoryEdge<Entity, Relationship>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public HistoryConnection() : base() { }

        /// <summary>
        /// Initializing Constructor
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="relationship"></param>
        public HistoryConnection(Entity source, Entity destination, Relationship relationship)
            : base(source, destination, relationship) { }

        /// <summary>
        /// returns Connection Source, Destination and current Relationship a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return From + " - " + Relationship.RelationshipType + " - " + To;
        }
    }
}
