using System;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Nodes
{
    /// <summary>
    /// Example of minimum Node implementation
    /// </summary>
    public class Node : INode<Node>
    {
        /// <summary>
        /// HandleMessage receives IMessage's sent to the Node
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual bool HandleMessage(IMessage message)
        {
            // do something with the message...

            // successfully did nothing :)
            return false;
        }

        /// <summary>
        /// Equals is required to identify a unique Node in the graph
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual bool Equals(Node other)
        {
            if (other == null)
                return false;

            if (!this.Equals(other))
                return false;

            return true;
        }
    }
}
