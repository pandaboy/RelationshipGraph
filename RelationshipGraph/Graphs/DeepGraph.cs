using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Graphs;
using RelationshipGraph.Relationships;
using RelationshipGraph.Messages;

namespace RelationshipGraph.Graphs
{
    /// <summary>
    /// Extension of Basic Graph with methods for querying the data
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    /// <typeparam name="TRelationship"></typeparam>
    public class DeepGraph<TNode, TEdge, TRelationship> : Graph<TNode, IList<TEdge>>
        where TNode : INode<TNode>
        where TRelationship : IRelationship<TRelationship>
        where TEdge : IEdge<TNode, TRelationship>
    {
        /// <summary>
        /// Alias for ContainsKey(node)
        /// </summary>
        /// <param name="node"></param>
        /// <returns>true/false</returns>
        public bool IsGraphed(TNode node)
        {
            return ContainsKey(node);
        }

        /// <summary>
        /// Checks to see if the Node is in the graph and has more than 0 edge's
        /// </summary>
        /// <param name="node"></param>
        /// <returns>true/false</returns>
        /// <remarks>Node may be in the graph but have an empty Edge list.
        /// Also check IsGraphed/ContainsKey to test if in the graph</remarks>
        public bool HasEdges(TNode node)
        {
            return (ContainsKey(node) && this[node].Count > 0);
        }

        /// <summary>
        /// Checks to see if INode from has a Direct Edge to INode to
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public virtual bool HasEdge(TNode from, TNode to)
        {
            return NodeHasEdge(from, from, to);
        }

        /// <summary>
        /// Checks to see if an edge with matching Source and Destination INode's exists
        /// in INode node's edge's
        /// </summary>
        /// <param name="node"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public virtual bool NodeHasEdge(TNode node, TNode from, TNode to)
        {
            if (!ContainsKey(node))
            {
                return false;
            }

            foreach(TEdge edge in GetEdges(node))
            {
                if (edge.From.Equals(from) && edge.To.Equals(to))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Alternate for NodeHasEdge
        /// </summary>
        /// <param name="node"></param>
        /// <param name="edge"></param>
        /// <returns></returns>
        public virtual bool NodeHasEdge(TNode node, TEdge edge)
        {
            return NodeHasEdge(node, edge.From, edge.To);
        }

        /// <summary>
        /// Adds an Edge to INode node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="edge"></param>
        public virtual void AddEdge(TNode node, TEdge edge)
        {
            // if we aren't already storing edges for this node,
            // add a new key and list for the node
            if (!IsGraphed(node))
            {
                Add(node, new List<TEdge>());
            }

            // check if we already have this edge stored for this node
            // if we do, update the relationship - otherwise add it
            if (NodeHasEdge(node, edge))
            {
                GetNodeEdge(node, edge).Relationship = edge.Relationship;
            }
            else
            {
                this[node].Add(edge);
            }
        }

        /// <summary>
        /// Alternate for AddEdge, treats From attribute in Edge as INode node
        /// </summary>
        /// <param name="edge"></param>
        public virtual void AddDirectEdge(TEdge edge)
        {
            AddEdge(edge.From, edge);
        }

        /// <summary>
        /// Alternate for AddEdge that creates a bidirectional connection between two
        /// edge's by creating bothe Edge's at once.
        /// </summary>
        /// <param name="edge"></param>
        public virtual void AddCommonEdge(TEdge edge)
        {
            // This is sorcery, for ref: http://stackoverflow.com/a/731637/797709
            // Uses Activator class to create a new instance of the TEdge generic
            // Activator class: https://msdn.microsoft.com/en-us/library/wccyzw83(v=vs.110).aspx
            //TEdge copy = new TEdge();
            TEdge copy = (TEdge)Activator.CreateInstance(typeof(TEdge));

            copy.From = edge.To;
            copy.To = edge.From;
            copy.Relationship = edge.Relationship;

            AddDirectEdge(edge);
            AddDirectEdge(copy);
        }

        /// <summary>
        /// Returns INode node's Edges
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual ICollection<TEdge> GetEdges(TNode node)
        {
            return this[node];
        }

        /// <summary>
        /// Returns INode node's Direct Edges. Those that have a matching From attribute.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual ICollection<TEdge> GetDirectEdges(TNode node)
        {
            ICollection<TEdge> direct = new List<TEdge>();
            
            if(ContainsKey(node))
            {
                foreach (TEdge edge in this[node])
                {
                    if (edge.From.Equals(node))
                    {
                        direct.Add(edge);
                    }
                }
            }

            return direct;
        }

        /// <summary>
        /// Returns INode node's Indirect edge. Those that do not match with the From attribute.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual ICollection<TEdge> GetInDirectEdges(TNode node)
        {
            ICollection<TEdge> indirect = new List<TEdge>();

            foreach (TEdge edge in this[node])
            {
                if (!edge.From.Equals(node))
                {
                    indirect.Add(edge);
                }
            }

            return indirect;
        }

        /// <summary>
        /// Returns a Direction from INode from to Inode to
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public virtual TEdge GetEdge(TNode from, TNode to)
        {
            if (!ContainsKey(from))
            {
                return default(TEdge);
            }

            foreach(TEdge edge in GetDirectEdges(from))
            {
                if (edge.From.Equals(from) && edge.To.Equals(to))
                {
                    return edge;
                }
            }

            return default(TEdge);
        }

        /// <summary>
        /// Alias for GetNodeEdge
        /// </summary>
        /// <param name="node"></param>
        /// <param name="edge"></param>
        /// <returns></returns>
        public virtual TEdge GetNodeEdge(TNode node, TEdge edge)
        {
            return GetNodeEdge(node, edge.From, edge.To);
        }

        /// <summary>
        /// Returns the matching Edge from INode node's Edges
        /// </summary>
        /// <param name="node"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public virtual TEdge GetNodeEdge(TNode node, TNode from, TNode to)
        {
            if (!ContainsKey(node))
            {
                return default(TEdge);
            }

            foreach (TEdge nodeEdge in GetEdges(node))
            {
                if (nodeEdge.From.Equals(from) && nodeEdge.To.Equals(to))
                {
                    return nodeEdge;
                }
            }

            return default(TEdge);
        }

        /// <summary>
        /// Removes the matching Edge
        /// </summary>
        /// <param name="node"></param>
        /// <param name="edge"></param>
        /// <returns></returns>
        public virtual bool RemoveEdge(TNode node, TEdge edge)
        {
            if(ContainsKey(node))
            {
                // get the list of edges for this node
                IList<TEdge> edges = this[node];

                // if the edge exists remove it
                if (edges.Contains(edge))
                {
                    edges.Remove(edge);
                    return true;
                }
            }

            // it wasn't removed because it didn't exist
            return false;
        }

        /// <summary>
        /// Alias for RemoveEdge
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public virtual bool RemoveDirectEdge(TEdge edge)
        {
            return RemoveEdge(edge.From, edge);
        }

        /// <summary>
        /// Alias for RemoveEdge, removes matching bidirectional edge.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public virtual bool RemoveCommonEdge(TEdge edge)
        {
            // This is sorcery, ref: http://stackoverflow.com/a/731637/797709
            // Uses Activator class to create a new instance of the TEdge generic
            // Activator class: https://msdn.microsoft.com/en-us/library/wccyzw83(v=vs.110).aspx
            //TEdge copy = new TEdge();
            TEdge copy = (TEdge)Activator.CreateInstance(typeof(TEdge));

            // create a copy with the To and From entities swapped
            copy.From = edge.To;
            copy.To = edge.From;
            copy.Relationship = edge.Relationship;

            bool edge_result = RemoveDirectEdge(edge);
            bool copy_result = RemoveDirectEdge(copy);

            // will true if both were actually removed, false if either wasn't actually removed
            return (edge_result && copy_result);
        }

        /// <summary>
        /// Removes all Edge's for INode node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual bool ClearEdges(TNode node)
        {
            if (IsGraphed(node))
            {
                this[node].Clear();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes all Direct Edge's for INode node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual bool ClearDirectEdges(TNode node)
        {
            if(IsGraphed(node))
            {
                foreach(TEdge edge in GetDirectEdges(node))
                {
                    RemoveEdge(node, edge);
                }
            }

            return false;
        }

        /// <summary>
        /// Removes all IndirectEdges for INode node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual bool ClearIndirectEdges(TNode node)
        {
            if (IsGraphed(node))
            {
                foreach (TEdge edge in GetInDirectEdges(node))
                {
                    RemoveEdge(node, edge);
                }
            }

            return false;
        }

        /// <summary>
        /// Returns nodes with the given relationship to the given node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="relationship"></param>
        /// <returns>List of nodes</returns>
        /// <remarks>
        /// For example, if the given node represents a car and the relationship was 'drivers'
        /// of the car; this method will return nodes that *the car* tracks as drivers of the given car
        /// i.e. Will search from the node's relationships to other nodes.
        /// - Only works on DirectEdges
        /// </remarks>
        public IList<TNode> WithRelationship(TNode node, TRelationship relationship)
        {
            IList<TNode> nodes = new List<TNode>();

            // this is searching through ALL the connections
            // but should only search through the given nodes direct connections
            foreach(TEdge edge in GetDirectEdges(node))
            {
                if (edge.Relationship.Equals(relationship) && edge.From.Equals(node))
                {
                    nodes.Add(edge.To);
                }
            }

            return nodes;
        }

        /// <summary>
        /// Returns nodes with the given relationship TO the given node.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="relationship"></param>
        /// <returns>list of nodes that match the given parameters</returns>
        /// <remarks>
        /// For example, if the given node represents a car and the relationship was 'drivers'
        /// of the car; this method will return the nodes that track themselves as drivers of the car.
        /// i.e. will search from other nodes relationship's to this node.
        /// </remarks>
        public IList<TNode> WithRelationshipTo(TNode node, TRelationship relationship)
        {
            IList<TNode> nodes = new List<TNode>();

            foreach(IList<TEdge> edges in Values)
            {
                foreach(TEdge edge in edges)
                {
                    if (edge.Relationship.Equals(relationship) && edge.To.Equals(node))
                    {
                        nodes.Add(edge.From);
                    }
                }
            }

            return nodes;
        }

        /// <summary>
        /// Alias for WithRelationship
        /// </summary>
        /// <param name="node"></param>
        /// <param name="edge"></param>
        /// <returns></returns>
        public ICollection<TNode> WithEdge(TNode node, TEdge edge)
        {
            return WithRelationship(node, edge.Relationship);
        }

        /// <summary>
        /// alias for WithRelationshipTo
        /// </summary>
        /// <param name="node"></param>
        /// <param name="edge"></param>
        /// <returns></returns>
        public ICollection<TNode> WithEdgeTo(TNode node, TEdge edge)
        {
            return WithRelationshipTo(node, edge.Relationship);
        }

        /// <summary>
        /// Graph has a messenger for sending messages between nodes
        /// </summary>
        /// <remarks>
        /// NOTE: this member isn't necessary for sending messages. The methods
        /// could just as well pass the IMessage directly, but this demonstrates
        /// using a class to manage the messages for queuing.
        /// </remarks>
        private Messenger<TNode> _messenger;

        /// <summary>
        /// Sends an IMessage from INode from to INode. Message can be queued.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="msg"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public bool SendMessage(TNode from, TNode to, IMessage msg, double delay = 0.0)
        {
            if (_messenger == null)
            {
                _messenger = Messenger<TNode>.Instance;
            }

            _messenger.Send(from, to, msg, delay);

            return true;
        }

        /// <summary>
        /// Sends an IMessage to Nodes that INode from has IRelationship rel with.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="rel"></param>
        /// <param name="msg"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public bool SendMessage(TNode from, TRelationship rel, IMessage msg, double delay = 0.0)
        {
            ICollection<TNode> nodes = WithRelationship(from, rel);

            foreach(TNode node in nodes)
            {
                SendMessage(from, node, msg, delay);
            }

            return true;
        }

        /// <summary>
        /// Sends an IMessage to Nodes with IRelationship to INode
        /// </summary>
        /// <param name="from"></param>
        /// <param name="rel"></param>
        /// <param name="msg"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public bool SendMessageTo(TNode from, TRelationship rel, IMessage msg, double delay = 0.0)
        {
            ICollection<TNode> nodes = WithRelationshipTo(from, rel);

            foreach (TNode node in nodes)
            {
                SendMessage(from, node, msg, delay);
            }

            return true;
        }

        /// <summary>
        /// Forgets messages queue'd for the given node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public void ForgetMessages(TNode node)
        {
            _messenger.Forget(node);
        }
    }
}
