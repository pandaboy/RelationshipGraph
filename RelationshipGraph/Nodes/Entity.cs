using System;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Messages;
using RelationshipGraph.Edges;
using RelationshipGraph.Relationships;
using RelationshipGraph.Graphs;

namespace RelationshipGraph.Nodes
{
    /// <summary>
    /// Standard Entity implementation of INode interface
    /// </summary>
    public class Entity : INode<Entity>
    {
        /// <summary>
        /// Number of entities
        /// </summary>
        protected static int count = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entityId">Unique Identifier</param>
        /// <param name="entityType">Type of Entity</param>
        public Entity(int entityId = 0, EntityType entityType = EntityType.SINGLE)
        {
            // increase the number of entities
            count++;

            // assign the properties
            EntityId = (entityId == 0) ? count : entityId;
            EntityType = entityType;
        }

        /// <summary>
        /// Unique Entity Identifier 
        /// </summary>
        private int _EntityId;
        /// <summary>
        /// Accessor for Unique Identifier
        /// </summary>
        public int EntityId
        {
            get
            {
                return _EntityId;
            }

            set
            {
                // probably not a good idea if these are supposed to Unique.
                _EntityId = value;
            }
        }

        /// <summary>
        /// EntityType of the Entity.
        /// </summary>
        private EntityType _EntityType;
        /// <summary>
        /// Accessor to set the EntityType
        /// </summary>
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
        
        /// <summary>
        /// Overridable implementation for handling IMessage's sent to Entity
        /// </summary>
        /// <param name="message"></param>
        /// <returns>true if IMessage successfully read</returns>
        /// <remarks>
        /// Accepts different types of message's and performs different tasks based on type.
        /// </remarks>
        public virtual bool HandleMessage(IMessage message)
        {
            // StringMessage's
            if (message.GetType() == typeof(StringMessage))
            {
                // convert to StringMessage object
                StringMessage msg = message as StringMessage;
                // output to console
                Console.WriteLine(this + " \"" + msg.Text + "\"");

                // if we are a GROUP, pass on the message to our MEMBERs
                // See: GroupEntity class for another way of implementing Group specific behaviour
                if (EntityType == EntityType.GROUP)
                {
                    Relationship members = new Relationship(RelationshipType.MEMBER);
                    Connections.Instance.SendMessageTo(this, members, message);
                }

                // Message processed.
                return true;
            }

            // if it's a connection message, try to learn about the connection
            // provided
            if(message.GetType() == typeof(ConnectionMessage))
            {
                // convert to ConnectionMessage
                ConnectionMessage msg = message as ConnectionMessage;
                // pass the message
                Learn(msg.Connection);

                return true;
            }

            // if the Connection implements the IMessage interface, it can be sent directly as
            // a message itself.
            if(message.GetType() == typeof(Connection))
            {
                // attempt to learn the connection
                Learn(message);

                return true;
            }

            // no message processed
            return false;
        }

        /// <summary>
        /// Implementation required for compatibility with Graph
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <remarks>Compares EntityId attributes - this assumes they are unique</remarks>
        public bool Equals(Entity other)
        {
            if (other == null)
                return false;

            if (this.EntityId != other.EntityId)
                return false;

            return true;
        }
        
        /// <summary>
        /// Overridden Equals method for comparison with objects. Passes test to Equals(Entity other).
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Overriden GetHasCode(). Returns unique EntityId
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return EntityId;
        }
        
        /// <summary>
        /// Overridden string representation of Entity. Displays EntityId.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "ENTITY: " + EntityId;
        }
        
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
            // Check GroupEntity for another approach to handling Group types
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

            // adopts the newRelationship if it has a higher weight value.
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

                // adopts the newRelationship if it has a higher weight value
                if (currentRelationship.Weight <= newRelationship.Weight)
                {
                    // adopt the newConnection
                    graph.AddConnection(this, newConnection);
                }
            }
        }
    }
}
