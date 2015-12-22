using System;
using System.Collections.Generic;

namespace RelationshipGraph.Interfaces
{
    /// <summary>
    /// Interface for defining a Graph type
    /// </summary>
    /// <typeparam name="TNode">
    /// Node type used in the graph. Implements of the INode interface
    /// </typeparam>
    /// <typeparam name="TEdge">
    /// Edge type used in the graph. Implements of the IEdge interface
    /// </typeparam>
    public interface IGraph<TNode, TEdge> : IDictionary<TNode, TEdge>
    {
        // ..
    }
}
