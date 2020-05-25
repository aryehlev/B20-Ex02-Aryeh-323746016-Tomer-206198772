using System;
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
            UI.ShowGameBoard(playGame.GameBoard);
            while (!playGame.IsGameOver())
            {
                Player currentPlayer = playGame.WhosTurnIsIt();  // player that is now playing
                BoardCoordinates[] nextMovesCoordinates = new BoardCoordinates[2];  // The coordinates of the cards chosen by the player in this turn

                Card secondCard;
                bool wasGoodGuess = true;

                if (!currentPlayer.IsComputer)
                {
                    nextMovesCoordinates[0] = UI.GetAndCheckCoordinatesInput(playGame, currentPlayer.Name);
                    playGame.FirstMove(nextMovesCoordinates[0]);
                    UI.ShowGameBoard(playGame.GameBoard);
                    nextMovesCoordinates[1] = UI.GetAndCheckCoordinatesInput(playGame, currentPlayer.Name);
                    UI.ShowGameBoard(playGame.GameBoard);
                }
                else
                {
                    UI.ShowComputerIsPlaying();
                    nextMovesCoordinates = computerAI.GetCoordinatesForNextMove(playGame.GameBoard);
                    playGame.FirstMove(nextMovesCoordinates[0]);
                    UI.ShowGameBoard(playGame.GameBoard);
                }
                computerAI.SaveToMemory(playGame.GameBoard.GetCardByCoordinates(nextMovesCoordinates[0]));
                wasGoodGuess = playGame.SecondMove(nextMovesCoordinates[1], out secondCard);
                computerAI.SaveToMemory(playGame.GameBoard.GetCardByCoordinates(nextMovesCoordinates[1]));
                if (wasGoodGuess)
                {
                    UI.ShowGameBoard(playGame.GameBoard);
                }
                else
                {
                    UI.ShowGameBoard(playGame.GameBoard, 2000);
                    playGame.GameBoard.ReturnToPassedBoardAfterBadGuess(secondCard);
                    UI.ShowGameBoard(playGame.GameBoard);
                }
            }

            bool wasTie;
            Player losingPlayer;
            Player winningPlayer = playGame.WhichPlayerWon(out losingPlayer, out wasTie);
            if(UI.EndGameAndCheckForRematch(losingPlayer, winningPlayer, wasTie))
            {
                Game newGame = new Game(playGame.GameBoard.GetHeightOfBoard(), playGame.GameBoard.GetLengthOfBoard(), playGame.GetMultiPlayer(), playGame.Player1.Name, playGame.Player2.Name);
                startGame(newGame);
            }
            else
            {
                UI.ExitGame();
            }

        }
    }
}

