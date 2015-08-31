using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Graphs;
using RelationshipGraph.Messages;
using RelationshipGraph.Relationships;

namespace RelationshipGraph.Nodes
{
    public class GroupEntity : Entity
    {
        public GroupEntity(int id = 0)
            : base(id, EntityType.GROUP){}

        // override the default HandleMessage Behaviour for Group-like behaviour
        public override bool HandleMessage(IMessage message)
        {
            // do something with the message
            if (message.GetType() == typeof(StringMessage))
            {
                // pass the message on
                Relationship members = new Relationship(RelationshipType.MEMBER);
                Connections.Instance.SendMessageTo(this, members, message);
            }

            // if it's a connection message, try to learn about the connection
            if (message.GetType() == typeof(ConnectionMessage))
            {
                ConnectionMessage msg = message as ConnectionMessage;
                // pass the message
                Learn(msg.Connection);
            }

            // successfully did something
            return true;
        }

        public override string ToString()
        {
            return "GROUP(" + EntityId + ")";
        }
    }
}
