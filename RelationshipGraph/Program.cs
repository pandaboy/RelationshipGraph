using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Nodes;
using RelationshipGraph.Messages;
using RelationshipGraph.Relationships;
using RelationshipGraph.Edges;
using RelationshipGraph.Graphs;
using RelationshipGraph.Enums;

namespace RelationshipGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("|\t- RelationshipGraph -\t\t|");
            Console.WriteLine("=========================================");

            ConnectionGraph graph = new ConnectionGraph();

            Entity a = new Entity();
            Entity b = new Entity();
            Entity g = new Entity();

            IList<Entity> entities = new List<Entity>();

            for(int i = 0; i < 10; i++)
            {
                Entity e = new Entity();
                entities.Add(e);
                graph.AddConnection(e, new Connection(e, g, new Relationship(RelationshipType.MEMBER)));
            }

            graph.AddConnection(g, new Connection(g, a, new Relationship(RelationshipType.FOLLOWER)));
            graph.AddConnection(a, new Connection(a, g, new Relationship(RelationshipType.LEADER)));

            Console.WriteLine("FOLLOWERS");
            Relationship rel = new Relationship(RelationshipType.MEMBER);
            
            foreach(Entity e in graph.WithRelationshipTo(g, rel))
            {
                Console.WriteLine(e);
            }

            StringMessage msg = new StringMessage("Brendan is crazy");

            Messenger<Entity>.Instance.Send(a, b, msg);
            msg.Text = "No he isn't!";
            Messenger<Entity>.Instance.Send(b, a, msg);
            msg.Text = "YES he is!";
            graph.SendMessage(b, a, msg);
            msg.Text = "I ACCEPT THIS MESSAGE FROM MY LEADER!";
            graph.SendMessage(g, rel, msg);

            // keep the console window open
            Console.WriteLine("Enter any key to quit");
            Console.Read();
        }
    }
}
