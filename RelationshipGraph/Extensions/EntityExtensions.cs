using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Edges;
using RelationshipGraph.Nodes;
using RelationshipGraph.Graphs;
using RelationshipGraph.Relationships;

namespace RelationshipGraph.Extensions
{
    public static class EntityExtensions
    {
        #region Messages
        public static void SendMessage(this Entity entity, Entity other, IMessage message, double delay = 0.0)
        {
            Connections.Instance.SendMessage(entity, other, message, delay);
        }

        public static void SendMessage(this Entity entity, Relationship relationship, IMessage message, double delay = 0.0)
        {
            Connections.Instance.SendMessage(entity, relationship, message, delay);
        }

        public static void SendMessageTo(this Entity entity, Relationship relationship, IMessage message, double delay = 0.0)
        {
            Connections.Instance.SendMessageTo(entity, relationship, message, delay);
        }
        #endregion

        #region Connections
        public static void AddConnection(this Entity entity, Connection connection)
        {
            Connections.Instance.AddConnection(entity, connection);
        }

        public static void RemoveConnection(this Entity entity, Connection connection)
        {
            Connections.Instance.RemoveConnection(entity, connection);
        }

        public static void ForgetConnections(this Entity entity)
        {
            Connections.Instance.ForgetConnections(entity);
        }

        public static void ForgetAbout(this Entity entity, Entity other)
        {
            Connections.Instance.ForgetAbout(entity, other);
        }

        public static Connection GetConnection(this Entity entity, Entity other)
        {
            return Connections.Instance.GetConnection(entity, other);
        }

        public static bool KnowsOf(this Entity entity, Entity other)
        {
            return Connections.Instance.KnowsConnectionsOf(entity, other);
        }
        #endregion

        #region Relationships
        public static IList<Entity> WithRelationship(this Entity entity, Relationship relationship)
        {
            return Connections.Instance.WithRelationship(entity, relationship);
        }

        public static IList<Entity> WithRelationshipTo(this Entity entity, Relationship relationship)
        {
            return Connections.Instance.WithRelationshipTo(entity, relationship);
        }

        public static IList<Entity> WithRelationshipHistory(this Entity entity, Relationship relationship)
        {
            return Connections.Instance.WithRelationshipHistory(entity, relationship);
        }

        public static IList<Entity> WithRelationshipHistoryTo(this Entity entity, Relationship relationship)
        {
            return Connections.Instance.WithRelationshipHistoryTo(entity, relationship);
        }
        #endregion

    }
}
