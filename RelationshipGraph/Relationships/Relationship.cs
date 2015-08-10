using System;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Enums;

namespace RelationshipGraph.Relationships
{
    public class Relationship : IRelationship<Relationship>
    {
        public Relationship(RelationshipType type = RelationshipType.NONE, int weight = 0)
        {
            this.RelationshipType = type;
            this.Weight = weight;
        }

        private RelationshipType _RelationshipType;
        public RelationshipType RelationshipType
        {
            get
            {
                return _RelationshipType;
            }

            set
            {
                _RelationshipType = value;
            }
        }

        private int _Weight;
        public int Weight
        {
            get
            {
                return _Weight;
            }

            set
            {
                _Weight = value;
            }
        }

        public bool Equals(Relationship other)
        {
            if(other == null)
                return false;

            if(this.RelationshipType == other.RelationshipType)
                return true;

            return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Relationship relationship = obj as Relationship;

            if (relationship == null)
                return false;
            else
                return Equals(relationship);
        }

        public static bool operator ==(Relationship relationship, Relationship other)
        {
            if ((object)relationship == null || (object)other == null)
                return Object.Equals(relationship, other);

            return relationship.Equals(other);
        }

        public static bool operator !=(Relationship relationship, Relationship other)
        {
            if (relationship == null || other == null)
                return !Object.Equals(relationship, other);

            return !relationship.Equals(other);
        }

        // ref: "Why is it important to override GetHashCode when Equals method is overridden?"
        // http://stackoverflow.com/q/371328/797709
        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + this.RelationshipType.GetHashCode();

            return hash;
            //return base.GetHashCode();
        }
    }
}
