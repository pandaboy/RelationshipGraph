using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;
using RelationshipGraph.Nodes;
using RelationshipGraph.Messages;
using RelationshipGraph.Relationships;
using RelationshipGraph.Edges;
using RelationshipGraph.Graphs;

namespace RelationshipGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("|\t- RelationshipGraph -\t\t|");
            Console.WriteLine("=========================================");

            // Test basic Entities
            Node n = new Node();
            Entity a = new Entity();
            Entity b = new Entity();

            // Test basic Relationships
            Relationship r = new Relationship();

            // Test basic Message
            Message m = new Message();

            // Test basic Connection
            Edge<Entity, Relationship> c = new Edge<Entity, Relationship>(a, b, r);
            Connection c2 = new Connection(a, b, r); // this is the same thing as the one above
            Edge<Entity, Cue> e2 = new Edge<Entity, Cue>(a, b, new Cue());

            // Test Basic Graph
            /*
            Graph<Node, string> graph = new Graph<Node, string>();
            graph.Add(n, "Brendan");
            graph.Add(new Node(50), "Kathy");

            foreach(KeyValuePair<Node, string> p in graph)
            {
                Console.WriteLine(p.Key.NodeId + ": " + p.Value);
            }
            */
            IDictionary<INode, string> nodeStrings1 = new Dictionary<INode, string>();
            nodeStrings1.Add(n, "Brendan");

            IDictionary<INode, IList<string>> nodeStrings2 = new Dictionary<INode, IList<string>>();
            nodeStrings2.Add(n, new List<string>());
            nodeStrings2[n].Add("BOB");
            nodeStrings2[n].Add("JON");
            nodeStrings2[n].Add("KAT");
            nodeStrings2[n].Add("DAN");

            KeyValuePair<INode, string> p = new KeyValuePair<INode, string>(n, "PAT");

            Graph<INode, IList<Connection>> graph =
                new Graph<INode, IList<Connection>>();

            NodeGraph<Relationship> relationshipGraph = new NodeGraph<Relationship>();
            NodeGraph<Cue> actionGraph = new NodeGraph<Cue>();

            ConnectionGraph<Entity> connections = new ConnectionGraph<Entity>();
            connections.AddConnection(a, new Connection(a, b, new Relationship()));
            Console.WriteLine("CONNECTIONS: " + connections.Count);

            //DeepGraph<Node, Edge<Node, Cue>> dg = new DeepGraph<Node, Edge<Node, Cue>>();
            //DeepGraph<Node, Cue> dg = new DeepGraph<Node, Cue>();
            DeepGraph<Node, Edge<Node, Cue>, Cue> dg = new DeepGraph<Node, Edge<Node, Cue>, Cue>();
            DeepGraph<Entity, Connection, Relationship> dg2 = new DeepGraph<Entity, Connection, Relationship>();

            DeepConnectionGraph<Relationship> deepConnections = new DeepConnectionGraph<Relationship>();

            Console.WriteLine(p.Key.NodeId);
            Console.WriteLine(p.Value);

            // keep the console window open
            Console.WriteLine("Enter any key to quit");
            Console.Read();
        }
    }
}
