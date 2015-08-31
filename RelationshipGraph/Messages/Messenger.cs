using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Messages
{
    /// <summary>
    /// Messenger used by Graph to manage submission of multiple message. Supports basic queuing.
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <remarks>This is a Singleton implementation</remarks>
    public class Messenger<TNode> where TNode : INode<TNode>
    {
        /// <summary>
        /// Number of Messages sent
        /// </summary>
        protected int _sent;

        /// <summary>
        /// Queue of pending messages
        /// </summary>
        protected IList<_MSG<TNode>> _q;

        /// <summary>
        /// private instance used in singleton - accessed using Instance property
        /// </summary>
        private static readonly Messenger<TNode> _instance = new Messenger<TNode>();

        /// <summary>
        /// Singleton constructor is a private instance.
        /// </summary>
        private Messenger()
        {
            _sent = 0;

            if (_q == null)
                _q = new List<_MSG<TNode>>();
        }

        /// <summary>
        /// Accessor to single class instance.
        /// </summary>
        public static Messenger<TNode> Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Number of messages sent.
        /// </summary>
        public int Sent
        {
            get
            {
                return _sent;
            }
        }

        /// <summary>
        /// Messages in the q
        /// </summary>
        public int Queued
        {
            get
            {
                return _q.Count;
            }
        }

        /// <summary>
        /// Sends a message to the recipient INode. Can be added to the queue for submission later.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <param name="message"></param>
        /// <param name="delay"></param>
        public void Send(TNode sender, TNode recipient, IMessage message, double delay = 0.0)
        {
            if (delay <= 0.0)
            {
                recipient.HandleMessage(message);
                ++_sent;
            }
            else
            {
                Queue(sender, recipient, message, delay);
            }
        }

        /// <summary>
        /// Adds a message to the queue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <param name="message"></param>
        /// <param name="delay"></param>
        protected void Queue(TNode sender, TNode recipient, IMessage message, double delay = 0.0)
        {
            double dispatch_time = delay;
            // Unity specific implementation can't be accessed from outside Unity runtime
            // double dispatch_time = (int)Time.time + delay;
            _q.Add(new _MSG<TNode>(sender, recipient, message, dispatch_time));
        }

        /// <summary>
        /// Processes delayed messages
        /// </summary>
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
                    ++_sent;
                    _q.RemoveAt(i);
                }
                */
            }
        }

        /// <summary>
        /// Empties the queue
        /// </summary>
        public void ClearQueue()
        {
            _q.Clear();
        }

        /// <summary>
        /// Removes all messages for the given INode
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>Removes any message that has the node as a recipient or sender</remarks>
        public void Forget(TNode node)
        {
            for(int i = 0; i < _q.Count; i++)
            {
                if (_q[i].Sender.Equals(node) || _q[i].Recipient.Equals(node))
                {
                    _q.RemoveAt(i);
                }
            }
        }
    }
}
