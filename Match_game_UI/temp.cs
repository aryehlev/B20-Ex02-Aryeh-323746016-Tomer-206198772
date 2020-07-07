using System;
using Match_game_logic;
using Match_game_UI;

namespace Match_game
{
    internal class temp
    {
    
        private static Game createGame()
        {
            eAiModes multiplayerMode = mofo.GetAndCheckMultiPlayerMode();
            int[] heightAndlenOfOfBoard = mofo.GetHeightAndLength();
            string player1Name = mofo.GetNameOfPlayer(1);
            string player2Name = "Computer";

            if (multiplayerMode == eAiModes.Off)
            {
                player2Name = mofo.GetNameOfPlayer(2);
            }

            return new Game(heightAndlenOfOfBoard[0], heightAndlenOfOfBoard[1], multiplayerMode, player1Name, player2Name);
        }

        private static void playGame(Game i_GameToPlay)
        {
            GameBoard gameBoard = i_GameToPlay.GameBoard;
            mofo.ShowGameBoard(gameBoard);
            Player winningPlayer, losingPlayer;

            while (!i_GameToPlay.IsGameOver(out winningPlayer, out losingPlayer))
            {
                Player currentPlayer = i_GameToPlay.WhosTurnIsIt();  // player that is now playing
                BoardCoordinates[] nextMovesCoordinates = new BoardCoordinates[2];  // The coordinates of the cards chosen by the player during the current turn

                if (!currentPlayer.IsComputer)
                {
                    nextMovesCoordinates[0] = mofo.GetAndCheckCoordinatesInput(i_GameToPlay, currentPlayer);
                    gameBoard.ExposeCard(nextMovesCoordinates[0]);
                    mofo.ShowGameBoard(gameBoard);
                    nextMovesCoordinates[1] = mofo.GetAndCheckCoordinatesInput(i_GameToPlay, currentPlayer);
                    mofo.ShowGameBoard(gameBoard);
                }
                else
                {
                    nextMovesCoordinates = i_GameToPlay.AiForComputerPlay.GetCoordinatesForNextMove(gameBoard);
                    mofo.ShowComputerIsPlaying();
                    gameBoard.ExposeCard(nextMovesCoordinates[0]);
                    mofo.ShowGameBoard(gameBoard, 1000);
                }

                if (i_GameToPlay.GuessCardAndUpdateScores(nextMovesCoordinates[1]))
                {
                    mofo.ShowGameBoard(gameBoard);
                }
                else
                {
                    mofo.ShowGameBoard(gameBoard, 2000);
                    gameBoard.EraseLastMoveFromBoard(nextMovesCoordinates[1]);
                    mofo.ShowGameBoard(gameBoard);
                    if (i_GameToPlay.AiMode != eAiModes.Off)
                    {
                        i_GameToPlay.AiForComputerPlay.SaveToMemory(nextMovesCoordinates[0]);
                        i_GameToPlay.AiForComputerPlay.SaveToMemory(nextMovesCoordinates[1]);
                    }
                }
            }

            if (mofo.EndGameAndCheckForRematch(losingPlayer, winningPlayer, winningPlayer == null))
            {
                Game newGame = new Game(gameBoard.GetHeightOfBoard(), gameBoard.GetLengthOfBoard(), i_GameToPlay.AiMode, i_GameToPlay.Player1.Name, i_GameToPlay.Player2.Name);
                playGame(newGame);
            }
            else
            {
                mofo.ExitGame();
            }
        }
    }
}