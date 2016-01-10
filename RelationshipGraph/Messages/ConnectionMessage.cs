using System;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Edges;
using RelationshipGraph.Messages;

namespace RelationshipGraph.Messages
{
    /// <summary>
    /// EdgeMessage that only operates with Connection types
    /// </summary>
    public class ConnectionMessage : EdgeMessage<Connection>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionMessage() : base() {}
        /// <summary>
        /// Constructor with initializer
        /// </summary>
        /// <param name="connection"></param>
        public ConnectionMessage(Connection connection) : base(connection) { }

        /// <summary>
        /// Accessor alias for TEdge Edge { get; set; }
        /// </summary>
        public Connection Connection
        {
            get
            {
                return Edge;
            }

            set
            {
                Edge = value;
            }
        }
    }
}
