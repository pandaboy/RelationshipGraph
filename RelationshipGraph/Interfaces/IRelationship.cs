using System;

namespace RelationshipGraph.Interfaces
{
    /// <summary>
    /// Must implement .Equals for valid queries to work
    /// </summary>
    /// <typeparam name="T">
    /// Type of object used in the equality test
    /// </typeparam>
    public interface IRelationship<T> : IEquatable<T> { }
}
