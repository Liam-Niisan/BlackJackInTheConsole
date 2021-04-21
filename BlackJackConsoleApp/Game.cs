using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackConsoleApp
{
    class Game
    {
        static void Main(string[] args)
        {

            static void ShowStats(BlackJack bj)
            {
                // state info
                Console.WriteLine("Dealer");
                foreach (Card c in bj.Dealer.Hand)
                {
                    Console.WriteLine(string.Format("{0}{1}", c.ID, c.Suit));
                }

                Console.WriteLine(bj.Dealer.Hand.Value);

                Console.WriteLine(Environment.NewLine);

                Console.WriteLine("Player");

                foreach (Card c in bj.Player.Hand)
                {
                    Console.WriteLine(string.Format("{0}{1}", c.ID, c.Suit));
                }

                Console.WriteLine(bj.Player.Hand.Value);

                Console.WriteLine(Environment.NewLine);
            }

            string input = "y";

            while (input == "y")
            {
                BlackJack bj = new BlackJack(17);
                ShowStats(bj);
                while (bj.Result == GameResult.Pending)
                {
                    input = Console.ReadLine();

                    if (input.ToLower() == "h")
                    {
                        bj.Hit();
                        ShowStats(bj);
                    }
                    else
                    {
                        bj.Stand();
                        ShowStats(bj);
                    }
                }

                Console.WriteLine(bj.Result);
                Console.WriteLine("Do you want to play again? y / n ?");
                input = Console.ReadLine();
            }
        }
    }
}
        public static class BlackJackRules
        {
            //card values
            public static List<string> ids = new List<string>() { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

            //card suits
            public static List<string> suits = new List<string> { "C", "D", "H", "S" };

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
            public static Deck ShuffledDeck
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

            //return game state win, lose or draw given players' hands

            public static GameResult GetResult(Member player, Member dealer)
            {
                GameResult res = GameResult.Win; // why not?

                double playerValue = HandValue(player.Hand);
                double dealerValue = HandValue(dealer.Hand);

                // player could be winner if...
                if (playerValue <= 21)
                {
                    // and...
                    if (playerValue != dealerValue)
                    {
                        double closestValue = new double[] { playerValue, dealerValue }
                            .Select(handVal => new { handVal, weight = Math.Abs(handVal - 21) + (handVal > 21 ? 100 : 0) })
                            .OrderBy(n => n.weight).First().handVal;

                        res = playerValue == closestValue ? GameResult.Win : GameResult.Lose;
                    }
                    else
                    {
                        res = GameResult.Draw;
                    }
                }
                else
                {
                    res = GameResult.Lose;
                }
                
                    return res;
            }
         }

        public class BlackJack
        {
            public Member Dealer = new Member();
            public Member Player = new Member();
            public GameResult Result { get; set; }
            public Deck MainDeck;

            public int StandLimit { get; set; }
            public BlackJack(int dealerStandLimit)
            {
                // setup a blackjack game...

                Result = GameResult.Pending;

                StandLimit = dealerStandLimit;

                // throw a new shuffled deck on table
                MainDeck = BlackJackRules.ShuffledDeck;

                // clear Player & Dealer hands and sleeves ;-)
                Dealer.Hand.Clear();
                Player.Hand.Clear();

                //Deal the first two cards to Player & Dealer
                for (int i = 0; ++i < 3;)
                {
                    Dealer.Hand.Push(MainDeck.Pop());
                    Player.Hand.Push(MainDeck.Pop());
                }
            }

            // Allow Play to hit. Dealer automatically hits when user stands.
            public void Hit()
            {
                if (BlackJackRules.CanPlayerHit(Player.Hand) && Result == GameResult.Pending)
                {
                    Player.Hand.Push(MainDeck.Pop());
                }
            }

            // When user stands, allow the Dealer to continue hitting until standlimit or bust.
            // Then go ahead and set the game result.
            public void Stand()
            {
                if (Result == GameResult.Pending)
                {

                    while (BlackJackRules.CanDealerHit(Dealer.Hand, StandLimit))
                    {
                        Dealer.Hand.Push(MainDeck.Pop());
                    }

                    Result = BlackJackRules.GetResult(Player, Dealer);
                }
            }
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
                    return BlackJackRules.HandValue(this);
                }
            }
        }

        public class Member
        {
            public Deck Hand;

            public Member()
            {
                Hand = new Deck();
            }
        }

    

