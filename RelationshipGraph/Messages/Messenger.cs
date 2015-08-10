using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Messages
{
    public class Messenger<TNode> where TNode : INode<TNode>
    {
        private IList<_MSG<TNode>> _q;
        private static readonly Messenger<TNode> _instance = new Messenger<TNode>();

        private Messenger()
        {
            if (_q == null)
                _q = new List<_MSG<TNode>>();
        }

        public static Messenger<TNode> Instance
        {
            get
            {
                return _instance;
            }
        }

        public void Send(TNode sender, TNode recipient, IMessage message, double delay = 0.0)
        {
            if (delay <= 0.0)
                recipient.HandleMessage(message);
            else
                Queue(sender, recipient, message, delay);
        }

        public void Queue(TNode sender, TNode recipient, IMessage message, double delay = 0.0)
        {
            double dispatch_time = delay;
            //double dispatch_time = (int)Time.time + delay;
            _q.Add(new _MSG<TNode>(sender, recipient, message, dispatch_time));
        }

        public void SendDelayed()
        {
            for (int i = 0; i < _q.Count; i++)
            {
                /*
                 * NOTE: Time.time is from UnityEngine,
                 * so uncomment this when testing there
                 * ====================================
                if(_q[i].DispatchTime <= Time.time)
                {
                    Send(_q[i].Sender, _q[i].Recipient, _q[i].Message);
                    _q.RemoveAt(i);
                }
                */
            }
        }
    }
}
