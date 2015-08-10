using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Messages
{
    struct _MSG<TNode> where TNode : INode<TNode>
    {
        TNode Sender;
        TNode Recipient;
        IMessage Message;
        double DispatchTime;

        public _MSG(TNode from, TNode to, IMessage msg, double dispatchTime)
        {
            Sender = from;
            Recipient = to;
            Message = msg;
            DispatchTime = dispatchTime;
        }
    }
}
