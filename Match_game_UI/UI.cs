using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Match_game__logic;

namespace Match_game_UI
{
    class UI
    {
        static void Main()
        {
            Console.WriteLine("Hi, welcome to the Matching game!\npress 1 if you would like to play against the Computer and 2 if you would like to play two players");

            bool multiplayer = CheckInputFromUser.checkMultiPlayer();
            int heightOfBoard = 0;
            int lenOfBoard = 0;
            Console.WriteLine("please enter desired Length of board either 4, or 6");
            lenOfBoard = CheckInputFromUser.checkLengthOrHeight();

            Console.WriteLine("please enter desired Height of board either 4, or 6");
            heightOfBoard = CheckInputFromUser.checkLengthOrHeight();

            Console.WriteLine("please enter your player name");
            string player1Name = Console.ReadLine();
            string player2Name = "Computer";3
            if (multiplayer)
            {
                Console.WriteLine("please enter your second player name");
                player2Name = Console.ReadLine();
            }

            Game newGame = new Game(heightOfBoard, lenOfBoard, multiplayer, player1Name, player2Name);
            startGame(newGame);

        }

        static void startGame(Game playGame)
        {

        }
    }
}
