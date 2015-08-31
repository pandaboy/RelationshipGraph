using System;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Relationships
{
    /// <summary>
    /// Cue as implementation of a Relationship
    /// </summary>
    public class Cue : IRelationship<Cue>
    {
        /// <summary>
        /// This method is required for indexing in the graph
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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
