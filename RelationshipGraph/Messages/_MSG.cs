using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Messages
{
    /// <summary>
    /// Used for queuing messages
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    struct _MSG<TNode> where TNode : INode<TNode>
    {
        /// <summary>
        /// INode that sent the message
        /// </summary>
        public TNode Sender;
        /// <summary>
        /// INode to receive the message
        /// </summary>
        public TNode Recipient;
        /// <summary>
        /// Message being sent
        /// </summary>
        public IMessage Message;
        /// <summary>
        /// When to send the message
        /// </summary>
        public double DispatchTime;

        /// <summary>
        /// Constructor to initialize the _MSG struct
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="msg"></param>
        /// <param name="dispatchTime"></param>
        public _MSG(TNode from, TNode to, IMessage msg, double dispatchTime)
        {
            Sender       = from;
            Recipient    = to;
            Message      = msg;
            DispatchTime = dispatchTime;
        }
    }
}
