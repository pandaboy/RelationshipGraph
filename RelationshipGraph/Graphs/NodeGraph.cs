using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Graphs;

namespace RelationshipGraph.Graphs
{
    public class NodeGraph<TRelationship> : Graph<INode, IList<TRelationship>>
        where TRelationship : IRelationship<TRelationship>
    {
    }
}
