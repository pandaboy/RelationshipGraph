using System;
using System.Collections;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Graphs
{
    /// <summary>
    /// Minimum IDictionary implementation for a Graph. Acts as a Base class for other Graphs
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <typeparam name="TEdges"></typeparam>
    /// <remarks>Internally, a graph is basically a Dictionary.</remarks>
    public class Graph<TNode, TEdges> : IGraph<TNode, TEdges>
    {
        /// <summary>
        /// Graph data.
        /// </summary>
        protected IDictionary<TNode, TEdges> _values;

        /// <summary>
        /// Constructor
        /// </summary>
        public Graph()
        {
            _values = new Dictionary<TNode, TEdges>();
        }

        /// <summary>
        /// Number of records
        /// </summary>
        public int Count
        {
            get
            {
                return _values.Count; 
            }
        }

        /// <summary>
        /// Graph can be accessed and updated so is NOT read only
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            } 
        }
        
        /// <summary>
        /// Returns a collection of Nodes
        /// </summary>
        public ICollection<TNode> Keys
        {
            get
            {
                return _values.Keys;
            }
        }
        
        /// <summary>
        /// Returns a collection of the Edge Sets
        /// </summary>
        public ICollection<TEdges> Values
        {
            get
            {
                return _values.Values; 
            } 
        }

        /// <summary>
        /// Returns the TEdges each node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public TEdges this[TNode node]
        {
            get
            {
                if (ContainsKey(node))
                {
                    return _values[node];
                }
                else
                {
                    return default(TEdges);
                }
            }

            set
            {
                if (ContainsKey(node))
                {
                    _values[node] = value;
                }
            }
        }

        /// <summary>
        /// Adds a new KeyValuePair
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<TNode, TEdges> item)
        {
            _values.Add(item);
        }

        /// <summary>
        /// Alternative add method
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        public void Add(TNode node, TEdges value)
        {
            _values.Add(node, value);
        }

        /// <summary>
        /// Clears the graph
        /// </summary>
        public void Clear()
        {
            _values.Clear();
        }
        
        /// <summary>
        /// Searches for a matching KeyValuePair in the graph
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TNode, TEdges> item)
        {
            return _values.Contains(item);
        }

        /// <summary>
        /// Searches for a matching INode
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool ContainsKey(TNode node)
        {
            return _values.ContainsKey(node);
        }

        /// <summary>
        /// Removes all Edges for the given INode
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Remove(TNode node)
        {
            if (ContainsKey(node))
            {
                return _values.Remove(node);
            }

            return false;
        }

        /// <summary>
        /// Removes a matching KeyValuePair
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TNode, TEdges> item)
        {
            return _values.Remove(item);
        }

        /// <summary>
        /// Attempts to retrieve the IEdge for the given INode
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TNode node, out TEdges value)
        {
            return _values.TryGetValue(node, out value);
        }

        /// <summary>
        /// Copies the data to an array
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<TNode, TEdges>[] array, int arrayIndex)
        {
            _values.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// IEnumerator required for IDictionary implementations
        /// </summary>
        /// <returns></returns>
        IEnumerator<KeyValuePair<TNode, TEdges>> IEnumerable<KeyValuePair<TNode, TEdges>>.GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        /// <summary>
        /// Required for IDictionary implementations
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<TNode, TEdges>>)this).GetEnumerator();
        }
    }
}
