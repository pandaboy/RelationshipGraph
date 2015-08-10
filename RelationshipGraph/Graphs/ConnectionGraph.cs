using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Graphs;
using RelationshipGraph.Relationships;
using RelationshipGraph.Edges;
using RelationshipGraph.Nodes;

namespace RelationshipGraph.Graphs
{
    /// <summary>
    /// Specific implementation of DeepGraph for Entities and Relationships.
    /// </summary>
    /// <remarks>
    /// Essentially a wrapper for the Generic DeepGraph that operates on
    /// Entity, Connection and Relationship classes. Adds additional
    /// methods specific to these classes.
    /// </remarks>
    public class ConnectionGraph : DeepGraph<Entity, Connection, Relationship>
    {
        public ICollection<Connection> GetConnections(Entity entity)
        {
            return GetEdges(entity);
        }

        public void AddConnection(Entity entity, Connection connection)
        {
            AddEdge(entity, connection);
        }

        public void AddDirectConnection(Connection connection)
        {
            AddDirectEdge(connection);
        }

        public void PrintConnections()
        {
            Console.WriteLine(">> CONNECTIONS:");
            foreach(KeyValuePair<Entity, IList<Connection>> item in this._values)
            {
                Console.WriteLine(item.Key);

                foreach(Connection connection in item.Value)
                {
                    Console.WriteLine(connection);
                }
            }
            Console.WriteLine("--");
        }
    }
}
