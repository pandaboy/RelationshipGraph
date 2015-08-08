using System;
using System.Collections.Generic;
using RelationshipGraph.Entities;
using RelationshipGraph.Messages;
using RelationshipGraph.Relationships;
using RelationshipGraph.Connections;

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
            Entity a = new Entity();

            // Test basic Relationships
            Relationship r = new Relationship();

            // Test basic Message
            Message m = new Message();

            // Test basic Connection
            Connection<Relationship> c = new Connection<Relationship>();

            // keep the console window open
            Console.WriteLine("Enter any key to quit");
            Console.Read();
        }
    }
}
