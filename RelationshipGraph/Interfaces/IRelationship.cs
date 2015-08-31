using System;

namespace RelationshipGraph.Interfaces
{
    /// <summary>
    /// Interface for defining a Relationship that will work with IGraph
    /// </summary>
    /// <typeparam name="TRelationship">
    /// Type of object used in the equality test
    /// </typeparam>
    /// <remarks>
    /// Must implement Equals() method of IEquatable<T> for valid queries to work
    /// </remarks>
    public interface IRelationship<TRelationship> : IEquatable<TRelationship> { }
}
