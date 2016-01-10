using System;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Edges
{
    /// <summary>
    /// Standard implementation of IEdge interface
    /// </summary>
    /// <typeparam name="TNode">INode type for From and To Attributes</typeparam>
    /// <typeparam name="TRelationship">IRelationship type</typeparam>
    public class Edge<TNode, TRelationship> : IEdge<TNode, TRelationship>
        where TRelationship : IRelationship<TRelationship>
        where TNode : INode<TNode>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Edge()
        {
            _From = default(TNode);
            _To = default(TNode);
            _Relationship = default(TRelationship);
        }

        /// <summary>
        /// Initializing Constructor
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="relationship"></param>
        public Edge(TNode from, TNode to, TRelationship relationship)
        {
            _From = from;
            _To = to;
            _Relationship = relationship;
        }

        /// <summary>
        /// Source INode of the Edge
        /// </summary>
        protected TNode _From;
        /// <summary>
        /// Overrideable Source Node Accessor
        /// </summary>
        public virtual TNode From
        {
            get
            {
                return _From;
            }

            set
            {
                _From = value;
            }
        }

        /// <summary>
        /// Destination INode for the Edge
        /// </summary>
        protected TNode _To;
        /// <summary>
        /// Overrideable Destination Accessor
        /// </summary>
        public virtual TNode To
        {
            get
            {
                return _To;
            }

            set
            {
                _To = value;
            }
        }

        /// <summary>
        /// IRelationship for the Edge
        /// </summary>
        protected TRelationship _Relationship;
        /// <summary>
        /// Overrideable accessor for the Relationship attribute
        /// </summary>
        public virtual TRelationship Relationship
        {
            get
            {
                return _Relationship;
            }

            set
            {
                _Relationship = value;
            }
        }

    }
}
