using System;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Edges
{
    public class Edge<TNode, TRelationship> : IEdge<TNode, TRelationship>
        where TRelationship : IRelationship<TRelationship>
        where TNode : INode
    {
        public Edge(TNode from, TNode to, TRelationship relationship)
        {
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

        private TRelationship _Relationship;
        public TRelationship Relationship
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
