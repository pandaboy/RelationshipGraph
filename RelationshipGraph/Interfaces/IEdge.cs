using System;

namespace RelationshipGraph.Interfaces
{
    public interface IEdge<TEntity, TRelationship> 
        where TRelationship : IRelationship<TRelationship>
        where TEntity : IEntity
    {
        TEntity From { get; set; }
        TEntity To { get; set; }
        TRelationship Relationship { get; set; }
    }
}
