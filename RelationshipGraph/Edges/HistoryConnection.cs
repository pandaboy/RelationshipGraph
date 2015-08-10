using System;
using RelationshipGraph.Nodes;
using RelationshipGraph.Relationships;

namespace RelationshipGraph.Edges
{
    public class HistoryConnection : HistoryEdge<Entity, Relationship>
    {
        public HistoryConnection() : base() { }
        public HistoryConnection(Entity a, Entity b, Relationship rel) : base(a, b, rel) { }

        public override string ToString()
        {
            return From + " - " + Relationship.RelationshipType + " - " + To;
        }
    }
}
