using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Nodes;
using RelationshipGraph.Graphs;

namespace RelationshipGraph.Extensions
{
    public static class EntityExtensions
    {
        public static void SendMessage(this Entity entity, Entity other, IMessage message, double delay = 0.0)
        {
            Connections.Instance.SendMessage(entity, other, message, delay);
        }
    }
}
