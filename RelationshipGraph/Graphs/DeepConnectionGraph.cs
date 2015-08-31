using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Edges;
using RelationshipGraph.Nodes;
using RelationshipGraph.Relationships;

namespace RelationshipGraph.Graphs
{
    /// <summary>
    /// Simple Extension of DeepGraph that Hides IEdge
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <typeparam name="TRelationship"></typeparam>
    public class DeepConnectionGraph<TNode, TRelationship>
        : DeepGraph<TNode, IEdge<TNode, TRelationship>, TRelationship>
        where TRelationship : IRelationship<TRelationship>
        where TNode : INode<TNode>
    {

    }
}
