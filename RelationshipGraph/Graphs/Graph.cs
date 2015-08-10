using System;
using System.Collections;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Graphs
{
    // basic graph implementation is an extension of A dictionary
    public class Graph<TKey, TValue> : IGraph<TKey, TValue>
    {
        #region Private Members
        protected IDictionary<TKey, TValue> _values;
        #endregion

        #region Constructors
        public Graph()
        {
            _values = new Dictionary<TKey, TValue>();
        }
        #endregion

        #region IDictionary members
        public int Count
        {
            get
            {
                return _values.Count; 
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            } 
        }
        
        public ICollection<TKey> Keys
        {
            get
            {
                return _values.Keys;
            }
        }
        
        public ICollection<TValue> Values
        {
            get
            {
                return _values.Values; 
            } 
        }

        public TValue this[TKey node]
        {
            get
            {
                return _values[node];
            }

            set
            {
                _values[node] = value;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _values.Add(item);
        }

        public void Add(TKey node, TValue value)
        {
            _values.Add(node, value);
        }

        public void Clear()
        {
            _values.Clear();
        }
        

        // search if we have a matching item in our graph.
        // however, our graph contains a list of TValues foreach
        // TKey - we check the LATEST/last value for a match
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _values.Contains(item);
        }

        // this is fine
        public bool ContainsKey(TKey node)
        {
            return _values.ContainsKey(node);
        }

        public bool Remove(TKey node)
        {
            return _values.Remove(node);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return _values.Remove(item);
        }

        public bool TryGetValue(TKey node, out TValue value)
        {
            return _values.TryGetValue(node, out value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _values.CopyTo(array, arrayIndex);
        }
        #endregion

        #region IEnumerable members
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<TKey, TValue>>)this).GetEnumerator();
        }
        #endregion
    }
}
