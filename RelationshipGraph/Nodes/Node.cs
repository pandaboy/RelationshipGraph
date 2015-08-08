using System;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Entities
{
    public class Node : INode
    {
        private static int count = 0;

        public Node(int nodeId = -1)
        {
            // increase the number of entities
            count++;

            if (nodeId == -1)
                NodeId = count;
            else
                NodeId = nodeId;
        }

        // used to identify each Entity
        private int _NodeId;
        public int NodeId
        {
            get
            {
                return _NodeId;
            }

            set
            {
                _NodeId = value;
            }
        }

        public virtual bool HandleMessage(IMessage message)
        {
            // do something with the message

            // successfully did something
            return true;
        }
    }
}
