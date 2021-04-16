using System;
using System.Collections.Generic;

namespace BlackJackConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

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

        public class Deck : Stack<Card>
        { 
            public Deck(IEnumerable<Card> collection) : base(collection) { }
            public Deck() : base(52) { }

            //indexer
            public Card this[int index]
            {
                get 
                {
                    Card item;

                    if (index >= 0 && index <= this.Count - 1)
                    {
                        item = this.ToArray()[index];
                    }
                    else
                    {
                        item = null;
                    }

                    return item;
                }
            }

            //let's get value of the Deck
            public double Value
            {
                get
                {
                    //TODO: return value from Buisness rule
                    return 0;
                }
            }
        }

        public class Member
        {
            Deck Hand;

            public Member()
            {
                Hand = new Deck();
            }
        }

        public static class BlackJackRules
        {
            //card values
            public static string[] ids = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

            //card suits
            public static string[] suits = { "C", "D", "H", "S" };

            //returns a new deck
            public static Deck NewDeck
            {
                get
                {
                    Deck d = new Deck();
                    int value;

                    foreach (string suit in suits)
                    {
                        foreach (string id in ids)
                        {
                            value = Int32.TryParse(id, out value) ? value : id == "A" ? 1 : 10;
                            d.Push(new Card(id, suit, value));
                        }
                    }

                    return d;
                }
            }

        }
    }
}
