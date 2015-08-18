using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Messages
{
    struct _MSG<TNode> where TNode : INode<TNode>
    {
        public TNode Sender;
        public TNode Recipient;
        public IMessage Message;
        public double DispatchTime;

        public _MSG(TNode from, TNode to, IMessage msg, double dispatchTime)
        {
            Sender = from;
            Recipient = to;
            Message = msg;
            DispatchTime = dispatchTime;
        }
    }
}
