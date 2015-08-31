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
    public class Connections
    {
        /// <summary>
        /// Internal graph object
        /// </summary>
        private DeepGraph<Entity, Connection, Relationship> _graph;

        /// <summary>
        /// Single has private instance
        /// </summary>
        private static readonly Connections _instance = new Connections();

        /// <summary>
        /// Private Constructor for Singleton to prevent duplicates
        /// </summary>
        private Connections()
        {
            _graph = new DeepGraph<Entity, Connection, Relationship>();
        }

        /// <summary>
        /// Public accessor for class instance
        /// </summary>
        public static Connections Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Checks if the Entity has Edge's in the graph
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsGraphed(Entity entity)
        {
            return _graph.IsGraphed(entity);
        }

        /// <summary>
        /// Wrapper for HasEdge
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool HasConnection(Entity entity, Entity other)
        {
            return _graph.HasEdge(entity, other);
        }

        /// <summary>
        /// Wrapper for NodeHasEdge
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public bool EntityHasConnection(Entity entity, Connection connection)
        {
            return _graph.NodeHasEdge(entity, connection);
        }

        /// <summary>
        /// Wrappper for NodeHasEdge
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public bool EntityHasConnection(Entity entity, Entity from, Entity to)
        {
            return _graph.NodeHasEdge(entity, from, to);
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
        
        /// <summary>
        /// Wrapper for GetEdges
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ICollection<Connection> GetConnections(Entity entity)
        {
            return _graph.GetEdges(entity);
        }

        /// <summary>
        /// Wrapper for GetDirectEdges
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ICollection<Connection> GetDirectConnections(Entity entity)
        {
            return _graph.GetDirectEdges(entity);
        }

        /// <summary>
        /// Wrapper for GetInDirectEdges
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ICollection<Connection> GetIndirectConnections(Entity entity)
        {
            return _graph.GetInDirectEdges(entity);
        }

        /// <summary>
        /// Wrapper for GetEdge
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public Connection GetConnection(Entity entity, Entity other)
        {
            return _graph.GetEdge(entity, other);
        }

        /// <summary>
        /// Wrapper for GetNodeEdge
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public Connection GetEntityConnection(Entity entity, Connection connection)
        {
            return _graph.GetNodeEdge(entity, connection);
        }

        /// <summary>
        /// Wrapper for GetNodeEdge
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public Connection GetEntityConnection(Entity entity, Entity from, Entity to)
        {
            return _graph.GetNodeEdge(entity, from, to);
        }

        /// <summary>
        /// Returns Connections other entity involving other Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public ICollection<Connection> GetConnectionsOf(Entity entity, Entity other)
        {
            IList<Connection> otherConnections = new List<Connection>();

            if (_graph.IsGraphed(entity))
            {
                foreach (Connection connection in GetConnections(entity))
                {
                    if (connection.From.Equals(other) || connection.To.Equals(other))
                        otherConnections.Add(connection);
                }
            }

            return otherConnections;
        }

        /// <summary>
        /// Returns connections involving other from entity's connections
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns All Entities from Indirect Connections
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns common connections shared by entity and other
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Wrapper for AddEdge
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        public void AddConnection(Entity entity, Connection connection)
        {
            _graph.AddEdge(entity, connection);
        }

        /// <summary>
        /// Wrapper for AddDirectEdge
        /// </summary>
        /// <param name="connection"></param>
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
        
        /// <summary>
        /// wrapper for SendMessage
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <param name="message"></param>
        /// <param name="delay"></param>
        public void SendMessage(Entity entity, Entity other, IMessage message, double delay = 0.0)
        {
            _graph.SendMessage(entity, other, message, delay);
        }

        
        /// <summary>
        /// Wrapper for SendMessaeg
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="relationship"></param>
        /// <param name="message"></param>
        /// <param name="delay"></param>
        /// <remarks>
        /// sends message to entities with [relationship] from entity
        /// i.e to all entities that [entity] points to with [relationship] "Bob's -> brothers -> Entities"
        /// </remarks>
        public void SendMessage(Entity entity, Relationship relationship, IMessage message, double delay = 0.0)
        {
            _graph.SendMessage(entity, relationship, message, delay);
        }

        /// <summary>
        /// Wrapper for SendMessageTo
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="relationship"></param>
        /// <param name="message"></param>
        /// <param name="delay"></param>
        /// <remarks>
        /// sends message to entities with [relationship] to entity
        /// i.e. to all entities that point to [entity] with [relationship] "Entities -> [Member] -> Group"
        /// </remarks>
        public void SendMessageTo(Entity entity, Relationship relationship, IMessage message, double delay = 0.0)
        {
            _graph.SendMessageTo(entity, relationship, message, delay);
        }

        /// <summary>
        /// Wrapper for ForgetMessages
        /// </summary>
        /// <param name="entity"></param>
        public void ForgetMessages(Entity entity)
        {
            _graph.ForgetMessages(entity);
        }
        
        /// <summary>
        /// Wrapper for Remove
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveConnections(Entity entity)
        {
            _graph.Remove(entity);
        }

        /// <summary>
        /// wrapper for RemoveEdge
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        public void RemoveConnection(Entity entity, Connection connection)
        {
            _graph.RemoveEdge(entity, connection);
        }

        /// <summary>
        /// Wrapper fro RemoveDirectEdge
        /// </summary>
        /// <param name="connection"></param>
        public void RemoveDirectConnection(Connection connection)
        {
            _graph.RemoveDirectEdge(connection);
        }

        /// <summary>
        /// Wrapper for RemoveCommonEdge
        /// </summary>
        /// <param name="connection"></param>
        public void RemoveCommonConnection(Connection connection)
        {
            _graph.RemoveCommonEdge(connection);
        }

        /// <summary>
        /// Wrapper for ClearEdges
        /// </summary>
        /// <param name="entity"></param>
        public void ClearConnections(Entity entity)
        {
            _graph.ClearEdges(entity);
        }

        /// <summary>
        /// Wrapper for ClearEdges
        /// </summary>
        /// <param name="entity"></param>
        public void ForgetConnections(Entity entity)
        {
            _graph.ClearEdges(entity);
        }

        /// <summary>
        /// Wrapper for ClearDirectEdges
        /// </summary>
        /// <param name="entity"></param>
        public void ForgetDirectConnections(Entity entity)
        {
            _graph.ClearDirectEdges(entity);
        }

        /// <summary>
        /// Wrapper for ClearIndirectEdges
        /// </summary>
        /// <param name="entity"></param>
        public void ForgetIndirectConnections(Entity entity)
        {
            _graph.ClearIndirectEdges(entity);
        }

        /// <summary>
        /// Wrapper for Forget
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Will remove all knowledge of the other Entity from entity's connections
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool ForgetAbout(Entity entity, Entity other)
        {
            ICollection<Connection> otherConnections = GetConnectionsOf(entity, other);

            foreach(Connection connection in otherConnections)
            {
                _graph.RemoveEdge(entity, connection);
            }

            return true;
        }

        /// <summary>
        /// Wrapper for Clear
        /// </summary>
        public void Clear()
        {
            _graph.Clear();
        }

        /// <summary>
        /// Wrapper for WithRelationship
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="relationship"></param>
        /// <returns></returns>
        public IList<Entity> WithRelationship(Entity entity, Relationship relationship)
        {
            return _graph.WithRelationship(entity, relationship);
        }

        /// <summary>
        /// Wrapper for WithRelationshipTo
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="relationship"></param>
        /// <returns></returns>
        public IList<Entity> WithRelationshipTo(Entity entity, Relationship relationship)
        {
            return _graph.WithRelationshipTo(entity, relationship);
        }

        /// <summary>
        /// Wrapper for WithConnection
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public IList<Entity> WithConnection(Entity entity, Connection connection)
        {
            return WithRelationship(entity, connection.Relationship);
        }

        /// <summary>
        /// Wrapper for WithConnectionTo
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public IList<Entity> WithConnectionTo(Entity entity, Connection connection)
        {
            return WithRelationshipTo(entity, connection.Relationship);
        }

        /// <summary>
        /// Checks the relationship history of entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="relationship"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Checks the relationship history of other entities
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="relationship"></param>
        /// <returns></returns>
        public IList<Entity> WithRelationshipHistoryTo(Entity entity, Relationship relationship)
        {
            IList<Entity> matches = new List<Entity>();

            // we have to check all the connections for each entity
            foreach (IList<Connection> connections in _graph.Values)
            {
                foreach (Connection connection in connections)
                {
                    // in each connection look through the relationship history
                    foreach (Relationship rel in connection.History)
                    {
                        // if the connection is TO the entity,
                        // and the relationship matches
                        // and it hasn't already been catalogued - add it.
                        if (rel.Equals(relationship) && connection.To.Equals(entity) && !matches.Contains(connection.From))
                            matches.Add(connection.From);
                    }
                }
            }

            return matches;
        }

        /// <summary>
        /// Wrapper for WithRelationshipHistory
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public IList<Entity> WithConnectionHistory(Entity entity, Connection connection)
        {
            return WithRelationshipHistory(entity, connection.Relationship);
        }

        /// <summary>
        /// Returns the Relationship History with Entity other or an empty list
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public IList<Relationship> GetRelationshipHistory(Entity entity, Entity other)
        {
            if(EntityHasConnection(entity, entity, other))
                return GetConnection(entity, other).Relationships;
            else
                return new List<Relationship>();
        }

        /// <summary>
        /// Checks if entity and other have "relationship" in their history
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <param name="relationship"></param>
        /// <returns></returns>
        public bool HaveRelationshipHistory(Entity entity, Entity other, Relationship relationship)
        {
            if (EntityHasConnection(entity, entity, other))
            {
                foreach (Relationship rel in GetConnection(entity, other).Relationships)
                {
                    if (rel.Equals(relationship))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Alias for HaveRelationshipHistory
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public bool HaveConnectionHistory(Entity entity, Entity other, Connection connection)
        {
            return HaveRelationshipHistory(entity, other, connection.Relationship);
        }

        /// <summary>
        /// Combines two HaveRelationshipHistory queries
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <param name="relationship"></param>
        /// <returns></returns>
        public bool HaveSharedRelationshipHistory(Entity entity, Entity other, Relationship relationship)
        {
            return HaveRelationshipHistory(entity, other, relationship)
                && HaveRelationshipHistory(other, entity, relationship);
        }

        /// <summary>
        /// Alias for HaveSharedRelationshipHistory
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public bool HaveSharedConnectionHistory(Entity entity, Entity other, Connection connection)
        {
            return HaveSharedRelationshipHistory(entity, other, connection.Relationship);
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
