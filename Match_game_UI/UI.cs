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
                Player currentPlayer = playGame.WhosTurnIsIt();
                if(!currentPlayer.IsComputer)
                {
                    Console.WriteLine($"{currentPlayer.Name}, please choose next coordinates");
                    coordinates = CheckInputFromUser.CheckCoordanitesInput(playGame);
                    playGame.FirstMove(coordinates[1] - '0' - 1, coordinates[0] - 'A');
                    Console.WriteLine($"{playGame.WhosTurnIsIt().Name}, please choose second coordinates");
                    coordinates = CheckInputFromUser.CheckCoordanitesInput(playGame);
                    playGame.SecondMove(coordinates[1] - '0' - 1, coordinates[0] - 'A');
                }
                else
                {
                    AI computerAI = new AI(Difficulty.easy);
                    AI.BoardCoordinates[] nextMovesCoordinates = computerAI.GetCoordinatesForNextMove(playGame.Ge);
                    playGame.FirstMove(AI)
                    playGame.SecondMove()
                }
            }

            bool wasTie;
            Player losingPlayer;
            Player winningPlayer = playGame.WhichPlayerWon(out losingPlayer, out wasTie);
            if (wasTie)
            {
                Console.WriteLine("Good Game! It was a tie this time...");
            }
            else
            {
                Console.WriteLine($"Congratulations Player {winningPlayer.Name}! You won with {winningPlayer.Score} pairs, {losingPlayer.Name} you got {losingPlayer.Score} pairs right");
            }
            Console.WriteLine("Rematch? (Y / N)");
            if(CheckInputFromUser.CheckIfWantRematch())
            {
                Game newGame = new Game(playGame.GetHeightOfBoard(), playGame.GetLengthOfBoard(), playGame.GetMultiPlayer(), playGame.Player1.Name, playGame.Player2.Name);
                startGame(newGame);
            }
            else
            {
                Console.WriteLine("Good Game, See you Next time, press any key to exit");
                Console.ReadLine();
            }

        }
    }
}
