using System;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Edges
{
    public class Edge<TEntity, TRelationship> : IEdge<TEntity, TRelationship>
        where TRelationship : IRelationship<TRelationship>
        where TEntity : IEntity
    {
        public Edge(TEntity from, TEntity to, TRelationship relationship)
        {
            From = from;
            To = to;
            Relationship = relationship;
        }

        private TEntity _From;
        public TEntity From
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

        private TEntity _To;
        public TEntity To
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
