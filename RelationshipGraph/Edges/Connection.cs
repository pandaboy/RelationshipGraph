using System;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Nodes;
using RelationshipGraph.Relationships;

namespace RelationshipGraph.Edges
{
    /// <summary>
    /// Implementation of History that works with Entity and Relationship types.
    /// </summary>
    /// <remarks>
    /// NOTE: This Edge implements the IMessage interface, see the
    /// HandleMessage() method of the Entity class to see why we would do this.
    /// </remarks>
    public class Connection : HistoryEdge<Entity, Relationship>, IMessage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Connection() : base() {}
        
        /// <summary>
        /// Initializing Constructor
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="relationship"></param>
        public Connection(Entity from, Entity to, Relationship relationship)
            : base(from, to, relationship) { }

        /// <summary>
        /// Returns string representation of Connection.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return PrintHistory();
        }

        /// <summary>
        /// returns string with Relationship history and source and destinations INode's
        /// </summary>
        /// <returns></returns>
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
