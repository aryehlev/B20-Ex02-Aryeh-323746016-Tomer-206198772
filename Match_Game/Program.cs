using Match_game_UI;
using Match_game__logic;

namespace Match_Game
{
    class Program
    {
        public static void Main()
        {
            MultiplayerModes multiplayerMode;
            int lenOfBoard, heightOfBoard;
            string player1Name, player2Name;
            UI.GetGameParamaters(
                out multiplayerMode,
                out lenOfBoard,
                out heightOfBoard,
                out player1Name,
                out player2Name);
            Game newGame = new Game(heightOfBoard, lenOfBoard, multiplayerMode, player1Name, player2Name);
            startGame(newGame);

        }

        static void startGame(Game i_PlayGame)
        {
            BoardCoordinates coordinates;
            while (!i_PlayGame.GameBoard.IsBoardFullyExposed())
            {
                i_PlayGame.NextTurn();



                Player currentPlayer = i_PlayGame.WhosTurnIsIt();
                if (!currentPlayer.IsComputer)
                {
                    Console.WriteLine($"{currentPlayer.Name}, please choose next coordinates");
                    coordinates = CheckInputFromUser.CheckCoordanitesInput(i_PlayGame);
                    i_PlayGame.FirstMove(coordinates);
                    Console.WriteLine($"{i_PlayGame.WhosTurnIsIt().Name}, please choose second coordinates");
                    coordinates = CheckInputFromUser.CheckCoordanitesInput(i_PlayGame);
                    i_PlayGame.SecondMove(coordinates);
                }
                else
                {
                    AI computerAI = new AI(Difficulty.easy);
                    AI.BoardCoordinates[] nextMovesCoordinates = computerAI.GetCoordinatesForNextMove(i_PlayGame.Ge);
                    i_PlayGame.FirstMove(AI)
                    i_PlayGame.SecondMove()
                }
            }

            bool wasTie;
            Player losingPlayer;
            Player winningPlayer = i_PlayGame.WhichPlayerWon(out losingPlayer, out wasTie);
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
                Game newGame = new Game(i_PlayGame.GetHeightOfBoard(), i_PlayGame.GetLengthOfBoard(), i_PlayGame.GetMultiPlayer(), i_PlayGame.Player1.Name, i_PlayGame.Player2.Name);
                startGame(newGame);
            }
            else
            {
                Console.WriteLine("Good Game, See you Next time, press any key to exit");
                Console.ReadLine();
            }


        }
    }
