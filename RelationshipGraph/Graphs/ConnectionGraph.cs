using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Graphs;
using RelationshipGraph.Relationships;
using RelationshipGraph.Edges;
using RelationshipGraph.Nodes;

namespace RelationshipGraph.Graphs
{
    public class ConnectionGraph<TNode> : Graph<TNode, IList<Connection>>
        where TNode : INode
    {
        public ConnectionGraph() : base() { }
        
        public void AddConnection(TNode entity, Connection connection)
        {
            // check if we have a value
            if (!ContainsKey(entity))
                this.Add(entity, new List<Connection>());

            // add the new edge
            this[entity].Add(connection);
        }

        /*
        public void AddDirectConnection(TConnection connection)
        {
            AddConnection(connection.From, connection);
        }
        */
    }
}
