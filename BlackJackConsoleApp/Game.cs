using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJackConsoleApp
{
    class Game
    {
        static void Main(string[] args)
        {
            string input = "y";

            while (input == "y")
            {
                BlackJack bj = new BlackJack(17);
                BlackJack.ShowStats(bj);
                while (bj.Result == GameResult.Pending)
                {
                    input = Console.ReadLine();

                    if (input.ToLower() == "h")
                    {
                        bj.Hit();
                        BlackJack.ShowStats(bj);
                    }
                    else
                    {
                        bj.Stand();
                        BlackJack.ShowStats(bj);
                    }
                }

                Console.WriteLine(bj.Result);
                Console.WriteLine("Do you want to play again? y / n ?");
                input = Console.ReadLine();
            }
        }
    }
}
        //GAME STATES
        public enum GameResult { Win = 1, Lose = -1, Draw = 0, Pending = 2};