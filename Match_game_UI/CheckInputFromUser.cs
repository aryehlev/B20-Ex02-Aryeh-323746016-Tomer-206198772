using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Match_game__logic;

namespace Match_game_UI
{
    internal class CheckInputFromUser
    {
         internal static bool CheckMultiPlayer()
         { 
             string inputFromUser = Console.ReadLine();
            while (inputFromUser != "1" && inputFromUser != "2")
            {
                Console.WriteLine("please enter either 1 or 2");
                inputFromUser = Console.ReadLine();
            }

            bool isMultiPlayer = inputFromUser == "2" ? true : false;
            
            return isMultiPlayer;
        }

        internal static int CheckLengthOrHeight()
        {
            string inputFromUserStr = Console.ReadLine();
            while (inputFromUserStr == null || inputFromUserStr != "4" && inputFromUserStr != "6")
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
                
                if(inputFromUser != null && inputFromUser.Length == 2)
                {
                    int row = inputFromUser[1] - '0';
                    int column = inputFromUser[0] - 'A' - '0';
                    int lengthOfBoard = i_CurrGame.GetLengthOfBoard();
                    int heightOfBoard = i_CurrGame.GetHeightOfBoard();
                    bool isCardExposed = i_CurrGame.IsCardExposed(row, inputFromUser[0]);
                    if(CheckCoordanites(inputFromUser, row, column, lengthOfBoard, heightOfBoard, isCardExposed))
                        break;
                }
                else
                {
                    Console.WriteLine("you need to put in 2 coordinates");
                    inputFromUser = Console.ReadLine();
                }
            }

            return inputFromUser;

        }

        internal static bool CheckCoordanites(string i_InputFromUser, int i_Row, int i_Column, int i_LengthOfBoard, int i_HeightOfBoard, bool i_IsCardExposed)
        {
                bool isOkCoordinates = true;
                if(i_Column >= i_LengthOfBoard || i_Column < 0)
                {
                    Console.WriteLine($"{i_InputFromUser[0]} does not fit in board paramaters");
                    isOkCoordinates = false;
                }
                if (i_Row >= i_HeightOfBoard || i_Row < 0)
                {
                    Console.WriteLine($"{i_InputFromUser[1]} does not fit in board paramaters");
                    isOkCoordinates = false;
                }

                if(i_IsCardExposed)
                {
                    Console.WriteLine("The card you picked is already exposed");
                    isOkCoordinates = false;
                }

                return isOkCoordinates;

        }
    }
}
