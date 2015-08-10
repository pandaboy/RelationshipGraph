using System;
using RelationshipGraph.Nodes;
using RelationshipGraph.Relationships;

namespace RelationshipGraph.Edges
{
    public class Connection : Edge<Entity, Relationship>
    {
        public Connection() : base() {}
        public Connection(Entity a, Entity b, Relationship rel) : base(a, b, rel) { }

        public override string ToString()
        {
            return From + " - " + Relationship.RelationshipType + " - " + To;
        }
    }
}
