using System;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Nodes
{
    public class Node : INode<Node>
    {
        #region INode implementations
        public virtual bool HandleMessage(IMessage message)
        {
            // do something with the message

            // successfully did something
            return true;
        }

        public virtual bool Equals(Node other)
        {
            if (other == null)
                return false;

            if (!this.Equals(other))
                return false;

            return true;
        }
        #endregion
    }
}
