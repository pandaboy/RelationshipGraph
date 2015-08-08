using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Relationships
{
    public class Relationship : IRelationship<Relationship>
    {
        public bool Equals(Relationship other)
        {
            if(other == null)
                return false;

            if(this != other)
                return false;

            return true;
        }
    }
}
