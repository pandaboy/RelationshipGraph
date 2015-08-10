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

        #region Connections Checkers
        public bool HasConnection(Entity entity, Entity other)
        {
            return _graph.HasEdge(entity, other);
        }

        public bool EntityHasConnection(Entity entity, Connection connection)
        {
            return _graph.NodeHasEdge(entity, connection);
        }

        /// <summary>
        /// Checks to see if the Entity knows of the other entity (is present in Indirect Connections)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool KnowsConnectionsOf(Entity entity, Entity other)
        {
            if(_graph.IsGraphed(entity))
            {
                foreach(Connection connection in GetIndirectConnections(entity))
                {
                    if (connection.To.Equals(other) || connection.From.Equals(other))
                        return true;
                }
            }

            return false;
        }
        #endregion

        #region Connection retrieval
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

        public Connection GetConnection(Entity entity, Entity other)
        {
            return _graph.GetEdge(entity, other);
        }

        public Connection GetEntityConnection(Entity entity, Connection connection)
        {
            return _graph.GetNodeEdge(entity, connection);
        }

        public ICollection<Connection> GetKnownConnectionsOf(Entity entity, Entity other)
        {
            IList<Connection> known = new List<Connection>();

            if(_graph.IsGraphed(entity))
            {
                foreach(Connection connection in GetIndirectConnections(entity))
                {
                    if (connection.From.Equals(other) || connection.To.Equals(other))
                        known.Add(connection);
                }
            }

            return known;
        }

        /// <summary>
        /// returns a collection of Entities from Direct Connections
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ICollection<Entity> GetDirectEntities(Entity entity)
        {
            ICollection<Entity> direct = new List<Entity>();

            if(_graph.IsGraphed(entity))
            {
                foreach(Connection connection in GetDirectConnections(entity))
                {
                    direct.Add(connection.To);
                }
            }

            return direct;
        }

        public ICollection<Entity> GetIndirectEntities(Entity entity)
        {
            ICollection<Entity> indirect = new List<Entity>();

            if(_graph.IsGraphed(entity))
            {
                foreach(Connection connection in GetIndirectConnections(entity))
                {
                    if (!indirect.Contains(connection.From))
                        indirect.Add(connection.From);

                    if (!indirect.Contains(connection.To))
                        indirect.Add(connection.To);
                }
            }

            return indirect;
        }

        public ICollection<Connection> GetCommonDirectConnections(Entity entity, Entity other)
        {
            ICollection<Connection> common = new List<Connection>();

            if(_graph.IsGraphed(entity) && _graph.IsGraphed(other))
            {
                foreach(Connection i in GetDirectConnections(entity))
                {
                    foreach(Connection j in GetDirectConnections(other))
                    {
                        // if they both point to the same entity, 
                        // they share connections to that that entity
                        if (i.To.Equals(j.To))
                        {
                            // add both connections
                            common.Add(i);
                            common.Add(j);
                        }
                    }
                }
            }

            return common;
        }
        #endregion

        #region Adding Connections
        public void AddConnection(Entity entity, Connection connection)
        {
            _graph.AddEdge(entity, connection);
        }

        public void AddDirectConnection(Connection connection)
        {
            _graph.AddDirectEdge(connection);
        }

        /// <summary>
        /// Adds a Direct Connection both ways
        /// </summary>
        /// <param name="connection"></param>
        public void AddCommonConnection(Connection connection)
        {
            _graph.AddCommonEdge(connection);
        }
        #endregion

        #region Messages
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

        public void ForgetMessages(Entity entity)
        {
            _graph.ForgetMessages(entity);
        }
        #endregion

        #region Removing Connections
        public void RemoveConnections(Entity entity)
        {
            _graph.Remove(entity);
        }

        public void RemoveConnection(Entity entity, Connection connection)
        {
            _graph.RemoveEdge(entity, connection);
        }

        public void RemoveDirectConnection(Connection connection)
        {
            _graph.RemoveDirectEdge(connection);
        }

        public void RemoveCommonConnection(Connection connection)
        {
            _graph.RemoveCommonEdge(connection);
        }

        public void ClearConnections(Entity entity)
        {
            _graph.ClearEdges(entity);
        }

        public void ForgetConnections(Entity entity)
        {
            _graph.ClearEdges(entity);
        }

        public void ForgetDirectConnections(Entity entity)
        {
            _graph.ClearDirectEdges(entity);
        }

        public void ForgetIndirectConnections(Entity entity)
        {
            _graph.ClearIndirectEdges(entity);
        }

        public bool Remove(Entity entity)
        {
            return Forget(entity, true);
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

        public void Clear()
        {
            _graph.Clear();
        }
        #endregion

        #region Relationships
        public IList<Entity> WithRelationship(Entity entity, Relationship relationship)
        {
            return _graph.WithRelationship(entity, relationship);
        }

        public IList<Entity> WithRelationshipTo(Entity entity, Relationship relationship)
        {
            return _graph.WithRelationshipTo(entity, relationship);
        }

        public IList<Entity> WithConnection(Entity entity, Connection connection)
        {
            return WithRelationship(entity, connection.Relationship);
        }

        public IList<Entity> WithConnectionTo(Entity entity, Connection connection)
        {
            return WithRelationshipTo(entity, connection.Relationship);
        }

        public IList<Entity> WithRelationshipHistory(Entity entity, Relationship relationship)
        {
            IList<Entity> matches = new List<Entity>();

            if(_graph.IsGraphed(entity))
            {
                foreach(Connection connection in GetDirectConnections(entity))
                {
                    foreach(Relationship rel in connection.History)
                    {
                        if (rel.Equals(relationship))
                            matches.Add(connection.To);
                    }
                }
            }

            return matches;
        }

        public IList<Entity> WithConnectionHistory(Entity entity, Connection connection)
        {
            return WithRelationshipHistory(entity, connection.Relationship);
        }

        public bool HaveRelationshipHistory(Entity entity, Entity other, Relationship relationship)
        {
            if(_graph.IsGraphed(entity) && _graph.IsGraphed(other))
            {
                foreach(Connection connection in GetDirectConnections(entity))
                {
                    if (connection.Relationship.Equals(relationship) && connection.To.Equals(other))
                        return false;
                }
            }

            return false;
        }

        public bool HaveConnectionHistory(Entity entity, Entity other, Connection connection)
        {
            return HaveRelationshipHistory(entity, other, connection.Relationship);
        }

        public bool HaveSharedRelationshipHistory(Entity entity, Entity other, Relationship relationship)
        {
            return HaveRelationshipHistory(entity, other, relationship)
                && HaveRelationshipHistory(other, entity, relationship);
        }

        public bool HaveSharedConnectionHistory(Entity entity, Entity other, Connection connection)
        {
            return HaveSharedRelationshipHistory(entity, other, connection.Relationship);
        }


        #endregion

        #region Utilities
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
        #endregion
    }
}
