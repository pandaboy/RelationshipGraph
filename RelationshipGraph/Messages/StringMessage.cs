using System;
using System.Collections.Generic;
using RelationshipGraph.Interfaces;

namespace RelationshipGraph.Messages
{
    /// <summary>
    /// Message with a string payload.
    /// </summary>
    public class StringMessage : IMessage
    {
        public StringMessage(string text = "")
        {
            Text = text;
        }

        /// <summary>
        /// Message string payload
        /// </summary>
        protected string text;

        /// <summary>
        /// Public accessor for string payload
        /// </summary>
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

        /// <summary>
        /// Overrides default string representation to return text value
        /// </summary>
        /// <returns>value of Text payload</returns>
        public override string ToString()
        {
            return Text;
        }
    }
}
