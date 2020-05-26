using Match_game_logic;
using Match_game_UI;

namespace Match_game
{
    internal class Program
    { 
        internal static void Main()
        {
            Game newGame = createGame();
            startGame(newGame);
        }

        private static Game createGame()
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

        private static void startGame(Game playGame)
        {
            GameBoard gameBoard = playGame.GameBoard;
            UI.ShowGameBoard(gameBoard);
            Player winningPlayer, losingPlayer;
            while (!playGame.IsGameOver(out winningPlayer, out losingPlayer))
            {
                Player currentPlayer = playGame.WhosTurnIsIt();  // player that is now playing
                BoardCoordinates[] nextMovesCoordinates = new BoardCoordinates[2];  // The coordinates of the cards chosen by the player during the current turn
                if (!currentPlayer.IsComputer)
                {
                    nextMovesCoordinates[0] = UI.GetAndCheckCoordinatesInput(playGame, currentPlayer);
                    gameBoard.ExposeCard(nextMovesCoordinates[0]);
                    UI.ShowGameBoard(gameBoard);
                    nextMovesCoordinates[1] = UI.GetAndCheckCoordinatesInput(playGame, currentPlayer);
                    UI.ShowGameBoard(gameBoard);
                }
                else
                {
                    nextMovesCoordinates = playGame.ComputerAI.GetCoordinatesForNextMove(gameBoard);
                    UI.ShowComputerIsPlaying();
                    gameBoard.ExposeCard(nextMovesCoordinates[0]);
                    UI.ShowGameBoard(gameBoard, 1000);
                }

                if (playGame.MultiplayerMode != MultiplayerModes.off)
                {
                    playGame.ComputerAI.SaveToMemory(gameBoard.GetCardByCoordinates(nextMovesCoordinates[0]));
                    playGame.ComputerAI.SaveToMemory(gameBoard.GetCardByCoordinates(nextMovesCoordinates[1]));
                }

                if (playGame.GuessCardAndUpdateScores(nextMovesCoordinates[1]))
                {
                    UI.ShowGameBoard(gameBoard);
                    if (playGame.MultiplayerMode != MultiplayerModes.off)
                    {
                        playGame.ComputerAI.RemoveFromMemory(gameBoard.GetCardByCoordinates(nextMovesCoordinates[0]));
                        playGame.ComputerAI.RemoveFromMemory(gameBoard.GetCardByCoordinates(nextMovesCoordinates[1]));
                    }
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
                Game newGame = new Game(gameBoard.GetHeightOfBoard(), gameBoard.GetLengthOfBoard(), playGame.MultiplayerMode, playGame.Player1.Name, playGame.Player2.Name);
                startGame(newGame);
            }
            else
            {
                UI.ExitGame();
            }
        }
    }
}