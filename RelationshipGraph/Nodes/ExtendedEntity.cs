using System;
using System.Collections.Generic;

namespace RelationshipGraph.Nodes
{
    /// <summary>
    /// Demonstration of Extending an Entity to add extra functionality
    /// </summary>
    public class ExtendedEntity : Entity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Entity</param>
        /// <param name="id">Unique Identifier</param>
        /// <param name="type">EntityType</param>
        public ExtendedEntity(string name, int id = 0, EntityType type = EntityType.SINGLE)
            : base(id, type)
        {
            this._Name = name;
        }

        /// <summary>
        /// Name of the Entity
        /// </summary>
        protected string _Name;
        /// <summary>
        /// Accessor to set/get the _Name value;
        /// </summary>
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

        /// <summary>
        /// Displays the Name and Unique Identifier of the Entity
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name + "(" + EntityId + ")";
        }
    }
}
