using System;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Relationships
{
    /// <summary>
    /// Weighted Relationship with a Relationship type to identify the relationship type
    /// </summary>
    public class Relationship : IRelationship<Relationship>
    {
        /// <summary>
        /// Constructor to initialize the Relationship with default values
        /// </summary>
        /// <param name="type"></param>
        /// <param name="weight"></param>
        public Relationship(RelationshipType type = RelationshipType.NONE, int weight = 0)
        {
            this.RelationshipType = type;
            this.Weight = weight;
        }

        /// <summary>
        /// Type of Relationship. Uses RelationshipType enum
        /// </summary>
        protected RelationshipType _RelationshipType;
        /// <summary>
        /// Accessor for the RelationshipType
        /// </summary>
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

        /// <summary>
        /// Weight used to indicate the strength of the Relationship
        /// </summary>
        private int _Weight;

        /// <summary>
        /// Accessor for the weight value
        /// </summary>
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

        /// <summary>
        /// Method required for functionality with the Graph
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <remarks>
        /// This method compares only the RelationshipType's and ignores the weight value.
        /// This should be replaced with a special member so that normal IEquatable overloading
        /// can still be used
        /// </remarks>
        public bool Equals(Relationship other)
        {
            if(other == null)
                return false;

            if(this.RelationshipType == other.RelationshipType)
                return true;

            return false;
        }

        /// <summary>
        /// Overriding Equality methods in the class. Passes the test to Equals(Relationship other)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Overloads == operator. Equality test passed to Equals(Relationship other)
        /// </summary>
        /// <param name="relationship"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool operator ==(Relationship relationship, Relationship other)
        {
            if ((object)relationship == null || (object)other == null)
                return Object.Equals(relationship, other);

            return relationship.Equals(other);
        }

        /// <summary>
        /// Overloads != operator. Equality test passed to Equals(Relationship other) and negated with !
        /// </summary>
        /// <param name="relationship"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool operator !=(Relationship relationship, Relationship other)
        {
            if (relationship == null || other == null)
                return !Object.Equals(relationship, other);

            return !relationship.Equals(other);
        }

        /// <summary>
        /// overrides GetHashCode method
        /// </summary>
        /// <returns></returns>
        /// <remarks> 
        /// ref: "Why is it important to override GetHashCode when Equals method is overridden?"
        /// http://stackoverflow.com/q/371328/797709
        /// </remarks>
        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + this.RelationshipType.GetHashCode();

            return hash;

            //return base.GetHashCode();
        }

        /// <summary>
        /// overrides ToString() method to print custom Relationship string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return RelationshipType + " (" + Weight + ")";
        }
    }
}
