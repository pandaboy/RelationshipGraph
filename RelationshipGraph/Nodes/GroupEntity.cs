using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Graphs;
using RelationshipGraph.Messages;
using RelationshipGraph.Relationships;

namespace RelationshipGraph.Nodes
{
    /// <summary>
    /// This class is an extension of the Entity class that implements Group specific behaviour.
    /// </summary>
    public class GroupEntity : Entity
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>Specifies the EntityType to always be GROUP</remarks>
        public GroupEntity(int id = 0) : base(id, EntityType.GROUP){}

        /// <summary>
        /// Accepts IMessage's sent to the GRoup
        /// </summary>
        /// <param name="message"></param>
        /// <returns>true if successfully broadcasted to RelationshipType.MEMBER's</returns>
        /// <remarks>
        /// Overrides the default HandleMessage Behaviour for Group-like behaviour
        /// </remarks>
        public override bool HandleMessage(IMessage message)
        {
            // Check the message type
            if (message.GetType() == typeof(StringMessage))
            {
                // pass the message on
                Relationship members = new Relationship(RelationshipType.MEMBER);
                // broadcast the message using Graph methods
                Connections.Instance.SendMessageTo(this, members, message);
                return true;
            }

            // if it's a connection message, try to learn about the connection
            if (message.GetType() == typeof(ConnectionMessage))
            {
                // convert the message
                ConnectionMessage msg = message as ConnectionMessage;
                // pass the message
                Learn(msg.Connection);

                return true;
            }

            // didn't do anything.
            return false;
        }

        /// <summary>
        /// Displays the GROUP Unique Identifier
        /// </summary>
        /// <returns>Group ID as string</returns>
        public override string ToString()
        {
            return "GROUP(" + EntityId + ")";
        }
    }
}
