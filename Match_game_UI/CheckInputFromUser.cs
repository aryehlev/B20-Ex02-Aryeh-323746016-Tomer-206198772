using System;
using Match_game__logic;

namespace Match_game_UI
{
    internal class CheckInputFromUser

    {
        internal static bool CheckMultiPlayer()
        {
            string inputFromUser = Console.ReadLine();
            while(inputFromUser != "1" && inputFromUser != "2")
            {
                Console.WriteLine("please enter either 1 or 2");
                inputFromUser = Console.ReadLine();
            }

            return inputFromUser == "2" ? true : false;
        }

        internal static int CheckLengthOrHeight()
        {
            string inputFromUserStr = Console.ReadLine();
            while(inputFromUserStr == null || inputFromUserStr != "4" && inputFromUserStr != "6")
            {
                Console.WriteLine("please enter either 4 or 6");
                inputFromUserStr = Console.ReadLine();
            }

            return int.Parse(inputFromUserStr);
        }

        internal static string CheckCoordanitesInput(Game i_CurrGame)
        {
            string inputFromUser = Console.ReadLine();
            while(true)
            {
                if(inputFromUser == "Q")
                {
                    Console.WriteLine("\nSee you next time!");
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(0);
                }
                if(inputFromUser != null && inputFromUser.Length == 2)
                {
                    int row = inputFromUser[1] - '0' - 1;
                    int column = inputFromUser[0] - 'A';
                    int lengthOfBoard = i_CurrGame.GetLengthOfBoard();
                    int heightOfBoard = i_CurrGame.GetHeightOfBoard();
                    if(column >= lengthOfBoard || column < 0)
                    {
                        Console.WriteLine($"{(char)('A' + column)} does not fit in board paramaters");
                    }
                    else if(row >= heightOfBoard || row < 0)
                    {
                        Console.WriteLine($"{row + 1} does not fit in board paramaters");
                    }
                    else if(i_CurrGame.IsCardExposed(row, column))
                    {
                        Console.WriteLine("The card you picked is already exposed");
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("you need to put in 2 coordinates, for example 'A1'");
                }

                inputFromUser = Console.ReadLine();
            }

            return inputFromUser;
        }

        internal static bool CheckIfWantRematch()
        {
            string inputFromUser = Console.ReadLine();
            while(inputFromUser != "Y" && inputFromUser != "N")
            {
                Console.WriteLine("I didn't Understand, Would you like to play another game, press Y if you do and N if you don't");
                inputFromUser = Console.ReadLine();
            }

            return inputFromUser == "Y";
        }
    }
}
