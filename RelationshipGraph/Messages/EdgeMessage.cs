using System;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Messages
{
    public class EdgeMessage<TEdge> : IMessage
    {
        public EdgeMessage() {}

        public EdgeMessage(TEdge edge)
        {
            Edge = edge;
        }

        protected TEdge _Edge;
        public TEdge Edge
        {
            get
            {
                return _Edge;
            }

            set
            {
                _Edge = value;
            }
        }
    }
}
