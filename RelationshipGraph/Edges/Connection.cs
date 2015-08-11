using System;
using RelationshipGraph.Nodes;
using RelationshipGraph.Relationships;

namespace RelationshipGraph.Edges
{
    public class Connection : HistoryEdge<Entity, Relationship>
    {
        public Connection() : base() {}
        public Connection(Entity from, Entity to, Relationship relationship)
            : base(from, to, relationship) { }

        public override string ToString()
        {
            return PrintHistory();
        }

        public string PrintHistory()
        {
            string history = From + " - [";

            foreach(Relationship relationship in History)
                history += relationship + " ";

            history += "] - " + To;

            return history;
        }
    }
}
