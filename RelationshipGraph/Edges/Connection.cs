using System;
using RelationshipGraph.Nodes;
using RelationshipGraph.Relationships;

namespace RelationshipGraph.Edges
{
    public class Connection : HistoryEdge<Entity, Relationship>
    {
        public Connection() : base() {}
        public Connection(Entity a, Entity b, Relationship rel) : base(a, b, rel) { }

        public override string ToString()
        {
            return PrintHistory();
            //return From + " - " + Relationship.RelationshipType + " - " + To;
        }

        public string PrintHistory()
        {
            string history = From + " - [";

            foreach(Relationship relationship in History)
                history += relationship.RelationshipType + " ";

            history += "] - " + To;

            return history;
        }
    }
}
