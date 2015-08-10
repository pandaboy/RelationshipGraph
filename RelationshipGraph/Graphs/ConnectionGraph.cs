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
    /// Wrapper class of a DeepGraph for Entities, Connections and Relationships.
    /// </summary>
    /// <remarks>
    /// This implementation is done as a singleton wrapper around a DeepGraph.
    /// Essentially a wrapper for the Generic DeepGraph that operates on
    /// Entity, Connection and Relationship classes. Adds additional
    /// methods specific to these classes.
    /// </remarks>
    public class ConnectionGraph
    {
        private DeepGraph<Entity, Connection, Relationship> _graph;

        private static readonly ConnectionGraph _instance = new ConnectionGraph();

        private ConnectionGraph()
        {
            _graph = new DeepGraph<Entity, Connection, Relationship>();
        }

        public static ConnectionGraph Instance
        {
            get
            {
                return _instance;
            }
        }

        public ICollection<Connection> GetConnections(Entity entity)
        {
            return _graph.GetEdges(entity);
        }

        public ICollection<Connection> GetDirectConnections(Entity entity)
        {
            return _graph.GetDirectEdges(entity);
        }

        public ICollection<Connection> GetIndirectConnections(Entity entity)
        {
            return _graph.GetInDirectEdges(entity);
        }

        public bool HasConnection(Entity entity, Entity other)
        {
            return _graph.HasEdge(entity, other);
        }

        public bool EntityHasConnection(Entity entity, Connection connection)
        {
            return _graph.NodeHasEdge(entity, connection);
        }

        public Connection GetConnection(Entity entity, Entity other)
        {
            return _graph.GetEdge(entity, other);
        }

        public Connection GetEntityConnection(Entity entity, Connection connection)
        {
            return _graph.GetNodeEdge(entity, connection);
        }

        public void AddConnection(Entity entity, Connection connection)
        {
            _graph.AddEdge(entity, connection);
        }

        public void AddDirectConnection(Connection connection)
        {
            _graph.AddDirectEdge(connection);
        }

        public void SendMessage(Entity entity, Entity other, IMessage message, double delay = 0.0)
        {
            _graph.SendMessage(entity, other, message, delay);
        }

        // sends message to entities with [relationship] from entity
        // i.e to all entities that [entity] points to with [relationship] "Bob's -> brothers -> Entities"
        public void SendMessage(Entity entity, Relationship relationship, IMessage message, double delay = 0.0)
        {
            _graph.SendMessage(entity, relationship, message, delay);
        }

        // sends message to entities with [relationship] to entity
        // i.e. to all entities that point to [entity] with [relationship] "Entities -> [Member] -> Group"
        public void SendMessageTo(Entity entity, Relationship relationship, IMessage message, double delay = 0.0)
        {
            _graph.SendMessageTo(entity, relationship, message, delay);
        }

        /// <summary>
        /// This method will remove all presence of the entity from the graph.
        /// </summary>
        /// <param name="entity">Entity to forget</param>
        /// <param name="complete">Also remove the Entity from the graph</param>
        /// <returns>true if successfully forgotten, false otherwise</returns>
        /// <remarks>
        /// This method will remove the graph record and ALL connections that have
        /// a reference to the entity
        /// </remarks>
        public bool Forget(Entity entity, bool complete = false)
        {
            // remove all connections other entities have involving this entity
            foreach(KeyValuePair<Entity, IList<Connection>> item in this._graph)
            {
                // skip the entity passed in
                if (item.Key.Equals(entity))
                    continue;

                for(int i = 0; i < item.Value.Count; i++)
                {
                    if (item.Value[i].From.Equals(entity) || item.Value[i].To.Equals(entity))
                        item.Value.RemoveAt(i);
                }
            }

            // remove the connections stored for this entity
            if (complete)
                _graph.Remove(entity);

            return true;
        }

        /// <summary>
        /// Displays a list of known connections to the console
        /// </summary>
        public void PrintConnections()
        {
            if (_graph.Count > 0)
            {
                Console.WriteLine(">> CONNECTIONS:");

                foreach(KeyValuePair<Entity, IList<Connection>> item in _graph)
                {
                    Console.WriteLine(item.Key);

                    foreach(Connection connection in item.Value)
                    {
                        Console.WriteLine(connection);
                    }
                }
            }
            else
            {
                Console.WriteLine(">> NO CONNECTIONS");
            }
            Console.WriteLine("--");
        }
    }
}
