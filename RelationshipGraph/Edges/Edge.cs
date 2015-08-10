using System;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Edges
{
    public class Edge<TNode, TRelationship> : IEdge<TNode, TRelationship>
        where TRelationship : IRelationship<TRelationship>
        where TNode : INode
    {
        public Edge()
        {
            From = default(TNode);
            To = default(TNode);
            Relationship = default(TRelationship);
        }

        public Edge(TNode from, TNode to, TRelationship relationship)
        {
            From = from;
            To = to;
            Relationship = relationship;
        }

        private TNode _From;
        public virtual TNode From
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
        public virtual TNode To
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

        private TRelationship _Relationship;
        public virtual TRelationship Relationship
        {
            get
            {
                return _Relationship;
            }

            set
            {
                _Relationship = value;
            }
        }

    }
}
