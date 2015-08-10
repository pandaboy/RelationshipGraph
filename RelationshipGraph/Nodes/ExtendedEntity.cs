using System;
using System.Collections.Generic;

namespace RelationshipGraph.Nodes
{
    public class ExtendedEntity : Entity
    {
        public ExtendedEntity(string name, int id = 0, EntityType type = EntityType.SINGLE)
            : base(id, type)
        {
            Name = name;
        }

        protected string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        }

        public override string ToString()
        {
            return Name + "(" + EntityId + ")";
        }
    }
}
