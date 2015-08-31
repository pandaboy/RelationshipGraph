using System;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Nodes;
using RelationshipGraph.Relationships;

namespace RelationshipGraph.Edges
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// NOTE: This Edge implements the IMessage interface, see the
    /// HandleMessage() method of the Entity class to see why we would do this.
    /// </remarks>
    public class Connection : HistoryEdge<Entity, Relationship>, IMessage
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
