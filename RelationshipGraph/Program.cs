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

            // testing a connection graph
            Connections graph = Connections.Instance;

            ExtendedEntity brendan = new ExtendedEntity("Brendan", 28);
            ExtendedEntity kathy = new ExtendedEntity("Kathy", 26);
            ExtendedEntity brian = new ExtendedEntity("Brian", 29);
            ExtendedEntity moha = new ExtendedEntity("Moha", 30);
            ExtendedEntity group = new ExtendedEntity("MOB", 100);

            graph.AddConnection(brendan, new Connection(brendan, kathy, new Relationship(RelationshipType.GIRLFRIEND)));
            graph.AddConnection(brendan, new Connection(moha, brian, new Relationship(RelationshipType.FRIEND)));
            graph.AddConnection(brendan, new Connection(brian, kathy, new Relationship(RelationshipType.FRIEND)));
            graph.AddConnection(moha, new Connection(brendan, kathy, new Relationship(RelationshipType.GIRLFRIEND, 20)));

            brian.Name = "Ondati";
            IList<ExtendedEntity> entities = new List<ExtendedEntity>();

            for (int i = 0; i < 20; i++)
            {
                ExtendedEntity stranger = new ExtendedEntity("Stranger", i + 1);
                entities.Add(stranger);
                graph.AddDirectConnection(new Connection(stranger, group, new Relationship(RelationshipType.MEMBER)));
            }

            graph.AddDirectConnection(new Connection(group, brendan, new Relationship(RelationshipType.FOLLOWER)));
            graph.AddDirectConnection(new Connection(brendan, group, new Relationship(RelationshipType.LEADER)));

            graph.PrintConnections();

            // Relationships
            Relationship gf = new Relationship(RelationshipType.GIRLFRIEND, 10);
            Relationship fr = new Relationship(RelationshipType.FRIEND);
            Relationship wf = new Relationship(RelationshipType.WIFE);

            graph.AddConnection(brendan, new Connection(brendan, kathy, gf));
            Console.WriteLine("GET: " + graph.GetConnection(brendan, kathy));

            // Adding connections
            gf.Weight = 20;
            graph.AddConnection(brendan, new Connection(brendan, kathy, gf));
            graph.AddDirectConnection(new Connection(kathy, brendan, new Relationship(RelationshipType.BOYFRIEND)));
            graph.AddConnection(brendan, new Connection(kathy, brian, fr));
            graph.AddConnection(brendan, new Connection(moha, group, new Relationship(RelationshipType.ENEMY)));
            graph.AddConnection(brendan, new Connection(moha, brian, new Relationship(RelationshipType.RIVAL)));
            graph.AddConnection(brendan, new Connection(moha, kathy, fr));
            graph.AddDirectConnection(new Connection(brendan, brian, fr));
            graph.AddDirectConnection(new Connection(kathy, brian, fr));
            
            Console.WriteLine("KNOWN");
            foreach(Connection conn in graph.GetKnownConnectionsOf(brendan, kathy))
            {
                Console.WriteLine(conn);
            }

            Console.WriteLine("INDIRECT");
            foreach(Connection conn in graph.GetIndirectConnections(brendan))
            {
                Console.WriteLine(conn);
            }

            Console.WriteLine("ALL");
            foreach (Connection conn in graph.GetConnections(brendan))
            {
                Console.WriteLine(conn);
            }

            // keep the console window open
            Console.WriteLine("Enter any key to quit");
            Console.Read();
        }
    }
}
