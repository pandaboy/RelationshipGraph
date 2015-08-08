using System;

namespace RelationshipGraph.Interfaces
{
    public interface IConnection<TRelationship> where TRelationship : IRelationship<TRelationship>
    {
        IEntity From { get; set; }
        IEntity To { get; set; }
        TRelationship Relationship { get; set; }
    }
}
