using System;
using System.Collections.Generic;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            SimulationLogic smLogic = new SimulationLogic();
            int numPacks = GetNumber("Enter the number of packs to use: ", 1, 50);//Max mumber can be any number, as of now set upto 50.

            MatchingConditions matchingCondition = ChooseMatchType();
            List<Card> cardPile = smLogic.CreateCardsPile(numPacks); 
            smLogic.ShufflingCards(cardPile);
            var result = smLogic.ExecutionOfGame(cardPile, matchingCondition);

           
            Console.WriteLine("\n Result of Game ************************** \n");
            foreach (var snapMsg in result.snapMessages)
            {
                Console.WriteLine(snapMsg);
            }

            Console.WriteLine($"\nCards won by the Player 1 :: {result.scoreForFirstPlayer}");
            Console.WriteLine($"Cards won by the Player 2 :: {result.scoreForSecondPlayer}");

            if (result.scoreForFirstPlayer > result.scoreForSecondPlayer)
                Console.WriteLine($"\nCongratulations to Player 1, you are the winner of the game.");
            else if (result.scoreForSecondPlayer > result.scoreForFirstPlayer)
                Console.WriteLine($"\nCongratulations to Player 2, you are the winner of the game.");
            else
                Console.WriteLine("No winner of this game.The game is draw.");

            Console.WriteLine("\n ************************** ");
        }

        private static int GetNumber(string message, int min, int max)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int value))
                {
                    if (value >= min && value <= max)
                        return value;
                    Console.WriteLine($"Please enter a number between {min} and {max}.");
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
        }

        private static MatchingConditions ChooseMatchType()
        {
            Console.WriteLine("\nChoose a matching condition:\n1. Face Value\n2. Suit\n3. Both");
            int choice = GetNumber("Please enter 1 to 3 : ", 1, 3);
            return (MatchingConditions)choice;
        }

    }
}



