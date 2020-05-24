using System;
using Match_game__logic;

namespace Match_game_UI
{
    class UI
    {
        public static void Main()
        {
            Console.WriteLine("Hi, welcome to the Matching game!\npress 1 if you would like to play against the Computer and 2 if you would like to play two players");

            bool multiplayer = CheckInputFromUser.CheckMultiPlayer();
            int heightOfBoard = 0;
            int lenOfBoard = 0;
            Console.WriteLine("please enter desired Length of board either 4, or 6");
            lenOfBoard = CheckInputFromUser.CheckLengthOrHeight();
            Console.WriteLine("please enter desired Height of board either 4, or 6");
            heightOfBoard = CheckInputFromUser.CheckLengthOrHeight();
            Console.WriteLine("please enter player one name");
            string player1Name = Console.ReadLine();
            string player2Name = "Computer";
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
            string coordinates = "";
            while(!playGame.IsGameOver())
            {
                    Console.WriteLine($"{playGame.WhosTurnIsIt().PlayerName}, please choose next coordinates");
                    coordinates = CheckInputFromUser.CheckCoordanitesInput(playGame);
                    playGame.FirstMove(coordinates[1] - '0' - 1, coordinates[0] - 'A');
                    Console.WriteLine($"{playGame.WhosTurnIsIt().PlayerName}, please choose second coordinates");
                    coordinates = CheckInputFromUser.CheckCoordanitesInput(playGame);
                    playGame.SecondMove(coordinates[1] - '0' - 1, coordinates[0] - 'A');
            }

        }
    }
}
