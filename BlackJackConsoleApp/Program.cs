using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        //GAME STATES
        public enum GameResult { Win = 1, Lose = -1, Draw = 0, Pending = 2};

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
            //returns a shuffled deck
            public static Deck ShuffleDeck
            {
                get 
                {
                    return new Deck(NewDeck.OrderBy(Card => System.Guid.NewGuid()).ToArray());
                }
            }

            // calculate the value of a hand.
            // A Hand is just a few cards so we can represent as Deck<Card> again.
            // I compare two totals for aces and return the one closest to "less than or equal to 21".
            public static double HandValue(Deck deck)
            {
                //Ace = 1
                int val1 = deck.Sum(c => c.Value);

                //Ace = 11
                double aces = deck.Count(c => c.Suit == "A");
                double val2 = aces > 0 ? val1 + (10 * aces) : val1;

                return new double[] { val1, val2 }
                    .Select(handVal => new { handVal, weight = Math.Abs(handVal - 21) + (handVal > 21 ? 100 : 0)})
                    .OrderBy(n => n.weight)
                    .First().handVal;
            }

            // a few more rules
            // check if dealer can hit given current value of Hand. Assume Dealer will always stand on 17 and 
            // not hit on soft 17. Hence standLimit.
            public static bool CanDealerHit(Deck deck, int standLimit)
            {
                return deck.Value < standLimit;
            }

            // No point hitting above 21...
            public static bool CanPlayerHit(Deck deck)
            {
                return deck.Value < 21;
            }

            public static GameResult GetResult(Member player, Member dealer)
            { 
                
            }
        }
    }
}
