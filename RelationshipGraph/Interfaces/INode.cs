namespace RelationshipGraph.Interfaces
{
    /// <summary>
    /// Interface for defining an entity that can be used in the Graph
    /// </summary>
    /// <remarks>
    /// An implementation of an IEntity MUST provide a unique EntityId.
    /// And a method for handling messages sent to it.
    /// </remarks>
    public interface INode
    {
        /// <summary>
        /// Used to uniquely identify entities
        /// </summary>
        int NodeId { get; set; }

        /// <summary>
        /// Gets called by the Messenger class when a message is due
        /// </summary>
        /// <param name="message">An object that implements the IMessage interface</param>
        /// <returns>true if successfully handled, false otherwise</returns>
        bool HandleMessage(IMessage message);
    }
}
