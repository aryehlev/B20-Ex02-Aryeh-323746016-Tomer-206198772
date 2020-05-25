using Match_game_logic;
using Match_game_UI;

namespace Match_game
{
    class Program
    {

        public static void Main()
        {
            Game newGame = createGame();
            startGame(newGame);
        }

        static Game createGame()
        {
            MultiplayerModes multiplayerMode = UI.GetAndCheckMultiPlayerMode();
            int lenOfBoard = UI.GetHeight();
            int heightOfBoard = UI.GetLength();
            string player1Name = UI.GetNameOfPlayer(1);
            string player2Name = "Computer";
            if (multiplayerMode == MultiplayerModes.off)
            {
                player2Name = UI.GetNameOfPlayer(2);
            }

            return new Game(heightOfBoard, lenOfBoard, multiplayerMode, player1Name, player2Name);
        }

        static void startGame(Game playGame)
        {
            AI computerAI = new AI(playGame.GetMultiPlayer());
            GameBoard gameBoard = playGame.GameBoard;
            UI.ShowGameBoard(gameBoard);
            Player winningPlayer, losingPlayer;
            while (!playGame.IsGameOver(out winningPlayer, out losingPlayer))
            {
                Player currentPlayer = playGame.WhosTurnIsIt();  // player that is now playing
                BoardCoordinates[] nextMovesCoordinates = new BoardCoordinates[2];  // The coordinates of the cards chosen by the player during the current turn
                if (!currentPlayer.IsComputer)
                {
                    nextMovesCoordinates[0] = UI.GetAndCheckCoordinatesInput(playGame, currentPlayer.Name);
                    playGame.ExposeCard(nextMovesCoordinates[0]);
                    UI.ShowGameBoard(gameBoard);
                    nextMovesCoordinates[1] = UI.GetAndCheckCoordinatesInput(playGame, currentPlayer.Name);
                    UI.ShowGameBoard(gameBoard);
                }
                else
                {
                    UI.ShowComputerIsPlaying();
                    nextMovesCoordinates = computerAI.GetCoordinatesForNextMove(gameBoard);
                    playGame.ExposeCard(nextMovesCoordinates[0]);
                    UI.ShowGameBoard(gameBoard);
                }
                computerAI.SaveToMemory(gameBoard.GetCardByCoordinates(nextMovesCoordinates[0]));
                bool wasGoodGuess;
                wasGoodGuess = playGame.GuessCardAndUpdateScores(nextMovesCoordinates[1]);
                computerAI.SaveToMemory(gameBoard.GetCardByCoordinates(nextMovesCoordinates[1]));
                if (wasGoodGuess)
                {
                    UI.ShowGameBoard(gameBoard);
                }
                else
                {
                    UI.ShowGameBoard(gameBoard, 2000);
                    gameBoard.EraseLastMoveFromBoard(nextMovesCoordinates[1]);
                    UI.ShowGameBoard(gameBoard);
                }
            }
            if (UI.EndGameAndCheckForRematch(losingPlayer, winningPlayer, winningPlayer == null))
            {
                Game newGame = new Game(gameBoard.GetHeightOfBoard(), gameBoard.GetLengthOfBoard(), playGame.GetMultiPlayer(), playGame.Player1.Name, playGame.Player2.Name);
                startGame(newGame);
            }
            else
            {
                UI.ExitGame();
            }
        }
    }
}

