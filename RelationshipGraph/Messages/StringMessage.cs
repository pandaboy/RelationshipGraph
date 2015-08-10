using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Messages
{
    public class StringMessage : IMessage
    {
        public StringMessage(string text = "")
        {
            Text = text;
        }

        private string text;
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
