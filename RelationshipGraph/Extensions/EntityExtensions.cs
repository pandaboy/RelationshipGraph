using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Edges;
using RelationshipGraph.Nodes;
using RelationshipGraph.Graphs;
using RelationshipGraph.Relationships;

namespace RelationshipGraph.Extensions
{
    /// <summary>
    /// Extensions to Entity class for easier use of Connections graph
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// Sends messages with the graph
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <param name="message"></param>
        /// <param name="delay"></param>
        public static void SendMessage(this Entity entity, Entity other, IMessage message, double delay = 0.0)
        {
            Connections.Instance.SendMessage(entity, other, message, delay);
        }

        /// <summary>
        /// Broadcasts messages to entities the entity has the relationship with
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="relationship"></param>
        /// <param name="message"></param>
        /// <param name="delay"></param>
        public static void SendMessage(this Entity entity, Relationship relationship, IMessage message, double delay = 0.0)
        {
            Connections.Instance.SendMessage(entity, relationship, message, delay);
        }

        /// <summary>
        /// Broadcasts messages to entities with the given relationship TO the Entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="relationship"></param>
        /// <param name="message"></param>
        /// <param name="delay"></param>
        public static void SendMessageTo(this Entity entity, Relationship relationship, IMessage message, double delay = 0.0)
        {
            Connections.Instance.SendMessageTo(entity, relationship, message, delay);
        }

        /// <summary>
        /// Adds a new connection for the entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        public static void AddConnection(this Entity entity, Connection connection)
        {
            Connections.Instance.AddConnection(entity, connection);
        }

        /// <summary>
        /// Removes a matching connection
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        public static void RemoveConnection(this Entity entity, Connection connection)
        {
            Connections.Instance.RemoveConnection(entity, connection);
        }

        /// <summary>
        /// Clears the Entity's connections
        /// </summary>
        /// <param name="entity"></param>
        public static void ForgetConnections(this Entity entity)
        {
            Connections.Instance.ForgetConnections(entity);
        }

        /// <summary>
        /// Removes connections to Entity other
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        public static void ForgetAbout(this Entity entity, Entity other)
        {
            Connections.Instance.ForgetAbout(entity, other);
        }

        /// <summary>
        /// Returns the connection to Entity other
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static Connection GetConnection(this Entity entity, Entity other)
        {
            return Connections.Instance.GetConnection(entity, other);
        }

        /// <summary>
        /// Checks if the Entity knows Entity other
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool KnowsOf(this Entity entity, Entity other)
        {
            return Connections.Instance.KnowsConnectionsOf(entity, other);
        }

        /// <summary>
        /// Returns list of Entities the Entity has the given relationship with (Direct Connections)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="relationship"></param>
        /// <returns></returns>
        public static IList<Entity> WithRelationship(this Entity entity, Relationship relationship)
        {
            return Connections.Instance.WithRelationship(entity, relationship);
        }

        /// <summary>
        /// Returns list of Entities that have the given relationship with the Entity (Indirect Connections)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="relationship"></param>
        /// <returns></returns>
        public static IList<Entity> WithRelationshipTo(this Entity entity, Relationship relationship)
        {
            return Connections.Instance.WithRelationshipTo(entity, relationship);
        }

        /// <summary>
        /// Returns Entities that the Entity has relationship history with. (Direct Connections)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="relationship"></param>
        /// <returns></returns>
        public static IList<Entity> WithRelationshipHistory(this Entity entity, Relationship relationship)
        {
            return Connections.Instance.WithRelationshipHistory(entity, relationship);
        }

        /// <summary>
        /// Returns Entities that have relationship history with the Entity. (Indirect Connections)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="relationship"></param>
        /// <returns></returns>
        public static IList<Entity> WithRelationshipHistoryTo(this Entity entity, Relationship relationship)
        {
            return Connections.Instance.WithRelationshipHistoryTo(entity, relationship);
        }

    }
}
