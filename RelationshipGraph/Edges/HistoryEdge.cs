using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Edges
{
    /// <summary>
    /// DeepEdge/HistoryEdge class. Maintains a list of previous Relationship values
    /// </summary>
    /// <typeparam name="TNode">INode used for From and To Attributes</typeparam>
    /// <typeparam name="TRelationship">IRelationship type</typeparam>
    public class HistoryEdge<TNode, TRelationship> : IEdge<TNode, TRelationship>
        where TRelationship : IRelationship<TRelationship>
        where TNode : INode<TNode>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public HistoryEdge()
        {
            _Relationships = new List<TRelationship>();
            _From = default(TNode);
            _To = default(TNode);
        }

        /// <summary>
        /// Initializing Constructor
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="relationship"></param>
        public HistoryEdge(TNode from, TNode to, TRelationship relationship)
        {
            _Relationships = new List<TRelationship>();

            _From = from;
            _To = to;
            _Relationships.Add(relationship);
        }

        /// <summary>
        /// Source INode
        /// </summary>
        protected TNode _From;
        /// <summary>
        /// Source INode accessor
        /// </summary>
        public TNode From
        {
            get
            {
                return _From;
            }

            set
            {
                _From = value;
            }
        }

        /// <summary>
        /// Destination INode
        /// </summary>
        protected TNode _To;
        /// <summary>
        /// Destination INode accessor
        /// </summary>
        public TNode To
        {
            get
            {
                return _To;
            }

            set
            {
                _To = value;
            }
        }
        
        /// <summary>
        /// List of previous relationship values
        /// </summary>
        protected IList<TRelationship> _Relationships;
        /// <summary>
        /// Accessor for previous relationship values.
        /// </summary>
        public virtual IList<TRelationship> Relationships
        {
            get
            {
                return _Relationships;
            }

            set
            {
                _Relationships = value;
            }
        }

        /// <summary>
        /// Alias accessor for previous relationship values
        /// </summary>
        public virtual ICollection<TRelationship> History
        {
            get
            {
                return _Relationships;
            }
        }

        /// <summary>
        /// Overrideable Accessor. Returns most recent relationship value
        /// </summary>
        public virtual TRelationship Relationship
        {
            get
            {
                if (Relationships.Count <= 0)
                    return default(TRelationship);

                int last = Relationships.Count - 1;
                return _Relationships[last];
            }

            set
            {
                // if we have relationships store, check the latest entity
                // before adding new ones
                if (Relationships.Count > 0)
                {
                    // check if the latest relationship is the same as the
                    // most recent. if it is don't add it, update the weight
                    int last = Relationships.Count - 1;
                    if (!_Relationships[last].Equals(value))
                        _Relationships.Add(value);
                    else
                        _Relationships[last] = value;
                }
                else
                    Relationships.Add(value);

                // or just add
                // Relationships.Add(value);
            }
        }
    }
}
