using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJackConsoleApp
{
    public class BlackJack
    {
        public Member Dealer = new Member();
        public Member Player = new Member();
        public GameResult Result { get; set; }
        public Deck MainDeck;

        public int StandLimit { get; set; }

        public static void ShowStats(BlackJack bj)
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

            Console.WriteLine("Press \"h\" to hit, or any other key to stand.");

            Console.WriteLine(Environment.NewLine);
        }
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
}
