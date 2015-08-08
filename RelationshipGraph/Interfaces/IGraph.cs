using System;
using System.Collections.Generic;

namespace RelationshipGraph.Interfaces
{
    public interface IGraph<TNode, TValue> : IDictionary<TNode, TValue>
    {
    }
}
