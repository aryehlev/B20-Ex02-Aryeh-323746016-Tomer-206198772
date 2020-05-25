using System;
using Match_game__logic;

namespace Match_game_UI
{
    class UI
    {
        public static void Main()
        {
            Game newGame = createGame();
            startGame(newGame);
        }

        static Game createGame()
        {
            Console.WriteLine("Hi, welcome to the Matching game!\npress 1 if you would like to play against the Computer and 2 if you would like to play two players");
            MultiplayerModes multiplayerMode = CheckInputFromUser.CheckMultiPlayer();
            Console.WriteLine("please enter desired Length of board either 4, or 6");
            int lenOfBoard = CheckInputFromUser.CheckLengthOrHeight();
            Console.WriteLine("please enter desired Height of board either 4, or 6");
            int heightOfBoard = CheckInputFromUser.CheckLengthOrHeight();
            Console.WriteLine("please enter player one name");
            string player1Name = Console.ReadLine();
            string player2Name = "Computer";
            if (multiplayerMode == MultiplayerModes.off)
            {
                Console.WriteLine("please enter second player name");
                player2Name = Console.ReadLine();
            }

            return new Game(heightOfBoard, lenOfBoard, multiplayerMode, player1Name, player2Name);
        }

        static void startGame(Game playGame)
        {
            BoardCoordinates coordinates;
            while (!playGame.IsGameOver())
            {
                Player currentPlayer = playGame.WhosTurnIsIt();
                if (!currentPlayer.IsComputer)
                {
                    Console.WriteLine($"{currentPlayer.Name}, please choose next coordinates");
                    coordinates = CheckInputFromUser.CheckCoordanitesInput(playGame);
                    playGame.FirstMove(coordinates);
                    Console.WriteLine($"{playGame.WhosTurnIsIt().Name}, please choose second coordinates");
                    coordinates = CheckInputFromUser.CheckCoordanitesInput(playGame);
                    playGame.SecondMove(coordinates);
                }
                else
                {
                    AI computerAI = new AI(MultiplayerModes.easy);
                    BoardCoordinates[] nextMovesCoordinates = computerAI.GetCoordinatesForNextMove(playGame.GameBoard.GetGameBoard());
                    playGame.FirstMove(nextMovesCoordinates[0]);
                    playGame.SecondMove(nextMovesCoordinates[1]);
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
            if (CheckInputFromUser.CheckIfWantRematch())
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