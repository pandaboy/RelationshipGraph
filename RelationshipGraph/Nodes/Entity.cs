using System;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Entities
{
    public class Entity : INode
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

        public int NodeId
        {
            get
            {
                return EntityId;
            }

            set
            {
                EntityId = value;
            }
        }

        public virtual bool HandleMessage(IMessage message)
        {
            // do something with the message

            // successfully did something
            return true;
        }
    }
}
