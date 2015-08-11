using System;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Messages;
using RelationshipGraph.Edges;
using RelationshipGraph.Relationships;
using RelationshipGraph.Graphs;

namespace RelationshipGraph.Nodes
{
    public class Entity : INode<Entity>
    {
        #region Protected members
        /// <summary>
        /// Number of entities
        /// </summary>
        protected static int count = 0;
        #endregion

        #region Constructor
        public Entity(int entityId = 0, EntityType entityType = EntityType.SINGLE)
        {
            // increase the number of entities
            count++;

            // assign the properties
            EntityId = (entityId == 0) ? count : entityId;
            EntityType = entityType;
        }
        #endregion

        #region properties
        // used to identify each Entity
        private int _EntityId;
        public int EntityId
        {
            get
            {
                return _EntityId;
            }

            set
            {
                _EntityId = value;
            }
        }

        private EntityType _EntityType;
        public EntityType EntityType
        {
            get
            {
                return _EntityType;
            }

            set
            {
                _EntityType = value;
            }
        }
        #endregion

        #region INode implementations
        public virtual bool HandleMessage(IMessage message)
        {
            // do something with the message
            if (message.GetType() == typeof(StringMessage))
            {
                StringMessage msg = message as StringMessage;
                Console.WriteLine(this + " \"" + msg.Text + "\"");

                // if we are a GROUP, pass on the message to our MEMBERs
                if (EntityType == EntityType.GROUP)
                {
                    Relationship members = new Relationship(RelationshipType.MEMBER);
                    Connections.Instance.SendMessageTo(this, members, message);
                }
            }

            // if it's a connection message, try to learn about the connection
            if(message.GetType() == typeof(ConnectionMessage))
            {
                ConnectionMessage msg = message as ConnectionMessage;
                // pass the message
                Learn(msg.Connection);
            }

            // successfully did something
            return true;
        }

        public bool Equals(Entity other)
        {
            if (other == null)
                return false;

            if (this.EntityId != other.EntityId)
                return false;

            return true;
        }
        #endregion

        #region Equality methods
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Entity entity = obj as Entity;

            if (entity == null)
                return false;
            else
                return Equals(entity);
        }

        public override int GetHashCode()
        {
            return EntityId;
        }
        #endregion

        #region Utility
        public override string ToString()
        {
            return "ENTITY: " + EntityId;
        }
        #endregion

        #region Connection methods
        /// <summary>
        /// Handles obtaining newConnection information.
        /// </summary>
        /// <param name="newConnection"></param>
        /// <returns>true/false depending on successful implementation of learning</returns>
        public virtual bool Learn(Connection newConnection)
        {
            Connections graph = Connections.Instance;

            // check if the Entity has a similar connection already (matching FROM and TO properties)
            if(graph.HasConnection(newConnection.From, newConnection.To))
            {
                // if they do, pass data to MatchingConnection() or ConflictingConnection() methods
                // depending on result of relationship comparison
                Connection currentConnection = graph.GetConnection(newConnection.From, newConnection.To);

                if(currentConnection.Relationship.Equals(newConnection))
                {
                    MatchingConnection(newConnection, currentConnection);
                }
                else
                {
                    ConflictingConnection(newConnection, currentConnection);
                }
            }
            else
            {
                // otherwise "learn" new connection by adding it to the graph
                // Conflict will learn if currentConnection is different
                ConflictingConnection(newConnection);
            }

            // if we are a GROUP, pass on the message to our MEMBERs
            if (EntityType == EntityType.GROUP)
            {
                Relationship members = new Relationship(RelationshipType.MEMBER);
                ConnectionMessage message = new ConnectionMessage(newConnection);

                Connections.Instance.SendMessageTo(this, members, message);
            }

            return true;
        }

        /// <summary>
        /// Called when the new connection has the same relationship as the current one.
        /// </summary>
        /// <param name="newConnection"></param>
        /// <param name="currentConnection"></param>
        public virtual void MatchingConnection(Connection newConnection, Connection currentConnection)
        {
            Relationship newRelationship = newConnection.Relationship;
            Relationship currentRelationship = currentConnection.Relationship;

            if(currentRelationship.Weight < newRelationship.Weight)
            {
                currentRelationship.Weight = newRelationship.Weight;
            }
        }

        /// <summary>
        /// Called when the new connection and current connection are different.
        /// </summary>
        /// <param name="newConnection"></param>
        /// <param name="currentConnection"></param>
        /// <remarks>
        /// currentConnection can be null, this will force the newConnection to be added
        /// </remarks>
        public virtual void ConflictingConnection(Connection newConnection, Connection currentConnection = null)
        {
            Connections graph = Connections.Instance;
            if(currentConnection == null)
            {
                // learn newConnection
                graph.AddConnection(this, newConnection);
            }
            else
            {
                // compare connections by relationship to determine if the newConnection should be adopted
                Relationship newRelationship = newConnection.Relationship;
                Relationship currentRelationship = currentConnection.Relationship;

                if (currentRelationship.Weight <= newRelationship.Weight)
                {
                    // adopt the newConnection
                    graph.AddConnection(this, newConnection);
                }
            }
        }
        #endregion
    }
}
