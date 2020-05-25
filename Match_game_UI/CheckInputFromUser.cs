using System;
using Match_game__logic;

namespace Match_game_UI
{
    internal class CheckInputFromUser

    {
        internal static MultiplayerModes CheckMultiPlayer()
        {
            string inputFromUser = Console.ReadLine();
            while(inputFromUser != "1" && inputFromUser != "2")
            {
                Console.WriteLine("please enter either 1 or 2");
                inputFromUser = Console.ReadLine();
            }

            return inputFromUser == "1" ? MultiplayerModes.easy : MultiplayerModes.off;
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

        internal static BoardCoordinates CheckCoordanitesInput(Game i_CurrGame)
        {
            string inputFromUser = Console.ReadLine();
            BoardCoordinates coordinatesFromUser = new BoardCoordinates();
            while (true)
            {
                if (inputFromUser == "Q")
                {
                    Console.WriteLine("\nSee you next time!");
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(0);
                }
                if(inputFromUser != null && inputFromUser.Length == 2)
                {
                    coordinatesFromUser = BoardCoordinates.ParsePlacement(inputFromUser);
                    int row = coordinatesFromUser.Row;
                    int column = coordinatesFromUser.Column;
                    int lengthOfBoard = i_CurrGame.GetLengthOfBoard();
                    int heightOfBoard = i_CurrGame.GetHeightOfBoard();
                    if(column >= lengthOfBoard)
                    {
                        Console.WriteLine($"{(char)('A' + column)} does not fit in board paramaters");
                    }
                    else if(row >= heightOfBoard)
                    {
                        Console.WriteLine($"{row + 1} does not fit in board paramaters");
                    }
                    else if(i_CurrGame.IsCardExposed(coordinatesFromUser))
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

            return coordinatesFromUser;
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
