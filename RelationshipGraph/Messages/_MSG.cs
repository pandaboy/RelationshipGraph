using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Messages
{
    struct _MSG
    {
        INode Sender;
        INode Recipient;
        IMessage Message;
        double DispatchTime;

        public _MSG(INode from, INode to, IMessage msg, double dispatchTime)
        {
            Sender = from;
            Recipient = to;
            Message = msg;
            DispatchTime = dispatchTime;
        }
    }
}
