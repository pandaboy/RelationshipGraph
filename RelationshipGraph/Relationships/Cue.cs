using System;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Relationships
{
    public class Cue : IRelationship<Cue>
    {
        public bool Equals(Cue other)
        {
            if (other == null)
                return false;

            if (this != other)
                return false;

            return true;
        }
    }
}
