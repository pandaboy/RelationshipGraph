using System;

namespace RelationshipGraph.Interfaces
{
    /// <summary>
    /// Interface for defining an entity that can be used in the Graph
    /// </summary>
    /// <typeparam name="TNode">
    /// Type of object used in the equality test
    /// </typeparam>
    /// <remarks>
    /// An implementation of an IEntity MUST provide a unique EntityId.
    /// And a method for handling messages sent to it.
    /// </remarks>
    public interface INode<TNode> : IEquatable<TNode>
    {
        /// <summary>
        /// Endpoint for messages sent to this node
        /// </summary>
        /// <param name="message">An object that implements the IMessage interface</param>
        /// <returns>true if successfully handled, false otherwise</returns>
        bool HandleMessage(IMessage message);
    }
}
