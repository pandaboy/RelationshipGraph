using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Edges;
using RelationshipGraph.Nodes;
using RelationshipGraph.Relationships;

namespace RelationshipGraph.Graphs
{
    public class DeepConnectionGraph<TRelationship>
        : DeepGraph<INode, IEdge<INode, TRelationship>, TRelationship>
        where TRelationship : IRelationship<TRelationship>
    {

    }
}
