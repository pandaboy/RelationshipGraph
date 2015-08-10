﻿using System;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Messages;

namespace RelationshipGraph.Nodes
{
    public class Entity : INode<Entity>
    {
        private static int count = 0;

        public Entity(int entityId = -1)
        {
            // increase the number of entities
            count++;

            if (entityId == -1)
                EntityId = count;
            else
                EntityId = entityId;
        }

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

        public virtual bool HandleMessage(IMessage message)
        {
            // do something with the message
            if (message.GetType() == typeof(StringMessage))
            {
                StringMessage msg = message as StringMessage;
                Console.WriteLine(this + " \"" + msg.Text + "\"");
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

        public override string ToString()
        {
            return "ENTITY: " + EntityId;
        }
    }
}
