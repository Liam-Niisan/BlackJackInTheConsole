using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJackConsoleApp
{
    public class Card
    {
        public string ID { get; set; }
        public string Suit { get; set; }
        public int Value { get; set; }

        public Card(string id, string suit, int value)
        {
            ID = id;
            Suit = suit;
            Value = value;
        }
    }
}
