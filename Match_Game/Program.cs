using Match_game_logic;
using Match_game_UI;

namespace Match_game
{
    internal class Program
    { 
        internal static void Main()
        {
            Game newGame = createGame();
            playGame(newGame);
        }

        private static Game createGame()
        {
            eMultiplayerModes multiplayerMode = UserInterface.GetAndCheckMultiPlayerMode();
            int[] HeightAndlenOfOfBoard = UserInterface.GetHeightAndLength();
            string player1Name = UserInterface.GetNameOfPlayer(1);
            string player2Name = "Computer";
            if (multiplayerMode == eMultiplayerModes.Off)
            {
                player2Name = UserInterface.GetNameOfPlayer(2);
            }

            return new Game(HeightAndlenOfOfBoard[0], HeightAndlenOfOfBoard[1], multiplayerMode, player1Name, player2Name);
        }

        private static void playGame(Game i_GameToPlay)
        {
            GameBoard gameBoard = i_GameToPlay.GameBoard;
            UserInterface.ShowGameBoard(gameBoard);
            Player winningPlayer, losingPlayer;
            while (!i_GameToPlay.IsGameOver(out winningPlayer, out losingPlayer))
            {
                Player currentPlayer = i_GameToPlay.WhosTurnIsIt();  // player that is now playing
                BoardCoordinates[] nextMovesCoordinates = new BoardCoordinates[2];  // The coordinates of the cards chosen by the player during the current turn
                if (!currentPlayer.IsComputer)
                {
                    nextMovesCoordinates[0] = UserInterface.GetAndCheckCoordinatesInput(i_GameToPlay, currentPlayer);
                    gameBoard.ExposeCard(nextMovesCoordinates[0]);
                    UserInterface.ShowGameBoard(gameBoard);
                    nextMovesCoordinates[1] = UserInterface.GetAndCheckCoordinatesInput(i_GameToPlay, currentPlayer);
                    UserInterface.ShowGameBoard(gameBoard);
                }
                else
                {
                    nextMovesCoordinates = i_GameToPlay.AiForComputerPlay.GetCoordinatesForNextMove(gameBoard);
                    UserInterface.ShowComputerIsPlaying();
                    gameBoard.ExposeCard(nextMovesCoordinates[0]);
                    UserInterface.ShowGameBoard(gameBoard, 1000);
                }

                if (i_GameToPlay.GuessCardAndUpdateScores(nextMovesCoordinates[1]))
                {
                    UserInterface.ShowGameBoard(gameBoard);
                }
                else
                {
                    UserInterface.ShowGameBoard(gameBoard, 2000);
                    gameBoard.EraseLastMoveFromBoard(nextMovesCoordinates[1]);
                    UserInterface.ShowGameBoard(gameBoard);
                    if (i_GameToPlay.MultiplayerMode != eMultiplayerModes.Off)
                    {
                        i_GameToPlay.AiForComputerPlay.SaveToMemory(nextMovesCoordinates[0]);
                        i_GameToPlay.AiForComputerPlay.SaveToMemory(nextMovesCoordinates[1]);
                    }

                }
            }
             
            if (UserInterface.EndGameAndCheckForRematch(losingPlayer, winningPlayer, winningPlayer == null))
            {
                Game newGame = new Game(gameBoard.GetHeightOfBoard(), gameBoard.GetLengthOfBoard(), i_GameToPlay.MultiplayerMode, i_GameToPlay.Player1.Name, i_GameToPlay.Player2.Name);
                playGame(newGame);
            }
            else
            {
                UserInterface.ExitGame();
            }
        }
    }
}