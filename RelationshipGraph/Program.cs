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

            ExtendedEntity brendan = new ExtendedEntity("Brendan", 28);
            ExtendedEntity kathy = new ExtendedEntity("Kathy", 26);
            ExtendedEntity brian = new ExtendedEntity("Brian", 29);
            ExtendedEntity moha = new ExtendedEntity("Moha", 28);
            brian.Name = "Ondati";
            IList<ExtendedEntity> entities = new List<ExtendedEntity>();

            for (int i = 0; i < 20; i++)
            {
                entities.Add(new ExtendedEntity("Stranger", i + 1));
            }

            // testing a connection graph
            ConnectionGraph graph = ConnectionGraph.Instance;

            // Relationships
            Relationship gf = new Relationship(RelationshipType.GIRLFRIEND);
            Relationship fr = new Relationship(RelationshipType.FRIEND);
            Relationship wf = new Relationship(RelationshipType.WIFE);

            // Adding a connection
            graph.AddConnection(brendan, new Connection(brendan, kathy, gf));
            graph.AddDirectConnection(new Connection(kathy, brendan, new Relationship(RelationshipType.BOYFRIEND)));
            graph.AddConnection(brendan, new Connection(kathy, brian, fr));
            graph.AddDirectConnection(new Connection(brendan, brian, fr));

            Connection wifeConnection = new Connection(brendan, kathy, wf);
            //wifeConnection.Relationship = gf;
            //Console.WriteLine(wifeConnection);
            //brendan.Learn(wifeConnection);
            //graph.AddDirectConnection(wifeConnection);

            Console.WriteLine("Direct Connections");
            foreach(Connection conn in graph.GetDirectConnections(brendan))
            {
                Console.WriteLine(conn);
            }

            graph.PrintConnections();

            // keep the console window open
            Console.WriteLine("Enter any key to quit");
            Console.Read();
        }
    }
}
