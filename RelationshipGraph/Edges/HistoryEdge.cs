using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Edges
{
    public class HistoryEdge<TNode, TRelationship> : IEdge<TNode, TRelationship>
        where TRelationship : IRelationship<TRelationship>
        where TNode : INode
    {
        public HistoryEdge()
        {
            _Relationships = new List<TRelationship>();

            From = default(TNode);
            To = default(TNode);
            Relationship = default(TRelationship);
        }

        public HistoryEdge(TNode from, TNode to, TRelationship relationship)
        {
            _Relationships = new List<TRelationship>();

            From = from;
            To = to;
            Relationship = relationship;
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
        public IList<TRelationship> Relationships
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

        public ICollection<TRelationship> History
        {
            get
            {
                return _Relationships;
            }
        }

        public TRelationship Relationship
        {
            get
            {
                int last = Relationships.Count - 1;
                return Relationships[last];
            }

            set
            {
                Relationships.Add(value);
            }
        }
    }
}
