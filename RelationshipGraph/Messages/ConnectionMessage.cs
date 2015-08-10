using System;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Edges;
using RelationshipGraph.Messages;

namespace RelationshipGraph.Messages
{
    public class ConnectionMessage : EdgeMessage<Connection>
    {
        public ConnectionMessage() : base() {}
        public ConnectionMessage(Connection connection) : base(connection) { }

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
