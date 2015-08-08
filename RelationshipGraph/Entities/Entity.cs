using System;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Entities
{
    public class Entity : IEntity
    {
        private static int count = 0;

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

            // successfully did something
            return true;
        }
    }
}
