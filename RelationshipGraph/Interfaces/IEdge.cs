using System;

namespace RelationshipGraph.Interfaces
{
    public interface IEdge<TNode, TRelationship> 
        where TRelationship : IRelationship<TRelationship>
        where TNode : INode<TNode>
    {
        TNode From { get; set; }
        TNode To { get; set; }
        TRelationship Relationship { get; set; }
    }
}
