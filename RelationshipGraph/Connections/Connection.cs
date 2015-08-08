using System;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Connections
{
    public class Connection<TRelationship> : IConnection<TRelationship>
        where TRelationship : IRelationship<TRelationship>
    {
        private IEntity _From;
        public IEntity From
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

        private IEntity _To;
        public IEntity To
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
