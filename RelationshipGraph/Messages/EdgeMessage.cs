using System;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Messages
{
    /// <summary>
    /// Implementation of IMessage that carries an IEdge value as payload
    /// </summary>
    /// <typeparam name="TEdge"></typeparam>
    public class EdgeMessage<TEdge> : IMessage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EdgeMessage()
        {
            // ..
        }

        /// <summary>
        /// Constructor with payload
        /// </summary>
        /// <param name="edge"></param>
        public EdgeMessage(TEdge edge)
        {
            _Edge = edge;
        }

        /// <summary>
        /// IEdge payload
        /// </summary>
        protected TEdge _Edge;
        /// <summary>
        /// Accessor for message payload
        /// </summary>
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
