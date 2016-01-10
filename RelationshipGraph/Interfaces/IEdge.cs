using System;

namespace RelationshipGraph.Interfaces
{
    /// <summary>
    /// Interface for defining an edge type that can be used in the Graph
    /// </summary>
    /// <typeparam name="TNode">
    /// Node Type used to indiciate source and destination of the edge. Implements INode interface
    /// </typeparam>
    /// <typeparam name="TRelationship">
    /// Relationship Type used to indiciate relationship of the edge. Implements IRelationship interface
    /// </typeparam>
    /// <remarks>
    /// This method relies on valid implementations of the IRelationship and INode interfaces
    /// </remarks>
    public interface IEdge<TNode, TRelationship> 
        where TRelationship : IRelationship<TRelationship>
        where TNode : INode<TNode>
    {
        /// <summary>
        /// INode source of the edge
        /// </summary>
        TNode From { get; set; }

        /// <summary>
        /// INode destination of the edge
        /// </summary>
        TNode To { get; set; }

        /// <summary>
        /// IRelationship attribute
        /// </summary>
        TRelationship Relationship { get; set; }
    }
}
