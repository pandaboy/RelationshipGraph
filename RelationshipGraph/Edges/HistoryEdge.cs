using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Edges
{
    public class HistoryEdge<TNode, TRelationship> : IEdge<TNode, TRelationship>
        where TRelationship : IRelationship<TRelationship>
        where TNode : INode<TNode>
    {
        public HistoryEdge()
        {
            _Relationships = new List<TRelationship>();

            _From = default(TNode);
            _To = default(TNode);
        }

        public HistoryEdge(TNode from, TNode to, TRelationship relationship)
        {
            _Relationships = new List<TRelationship>();

            _From = from;
            _To = to;
            _Relationships.Add(relationship);
        }

        private TNode _From;
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

        private TNode _To;
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
        
        private IList<TRelationship> _Relationships;
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

        public virtual ICollection<TRelationship> History
        {
            get
            {
                return _Relationships;
            }
        }

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
