﻿

using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Match_game_logic
{
    public class Game
    {
        private GameBoard m_GameBoard;
        private Player m_player1;
        private Player m_player2;
        private MultiplayerModes m_MultiPlayerMode;


        public Game(
            int i_NumberOfRows,
            int i_NumberOfColumns,
            MultiplayerModes i_MultiPlayerMode,
            string i_NameOfPlayer1,
            string i_NameOfPlayer2 = "Computer")
        {

            this.m_GameBoard = new GameBoard(i_NumberOfRows, i_NumberOfColumns);
            this.m_player1 = new Player(i_NameOfPlayer1, false, true);
            this.m_player2 = new Player(i_NameOfPlayer2, i_MultiPlayerMode != MultiplayerModes.off, false);
            this.m_MultiPlayerMode = i_MultiPlayerMode;
        }



        public void FirstMove(BoardCoordinates i_boardCoordinates)
        {
            this.m_GameBoard.ExposeCard(i_boardCoordinates);
        }

        public bool SecondMove(BoardCoordinates i_boardCoordinates, out Card o_SecondGuessedCard)
        {
            bool wasSuccsesfulGuess = this.m_GameBoard.GuessCard(i_boardCoordinates, out o_SecondGuessedCard);
            if (wasSuccsesfulGuess)
            {
                if(m_player1.IsMyTurn)
                {
                    this.m_player1.Score++;
                }
                else
                {
                    this.m_player2.Score++;
                }
            }
            else
            {
                this.m_player1.IsMyTurn = !this.m_player1.IsMyTurn;
                this.m_player2.IsMyTurn = !this.m_player2.IsMyTurn;
            }

            return wasSuccsesfulGuess;
        }


        public bool IsGameOver()
        {
            return this.m_GameBoard.IsBoardFullyExposed();
        }

        public bool IsCardExposed(BoardCoordinates i_boardCoordinates)
        {
            return this.m_GameBoard.IsCardExposed(i_boardCoordinates);
        }
        public MultiplayerModes GetMultiPlayer()
        {
            return this.m_MultiPlayerMode;
        }

        public Player Player1
        {
            get
            {
                return this.m_player1;
            }
            set
            {
                this.m_player1 = value;
            }
        }

        public Player Player2
        {
            get
            {
                return this.m_player2;
            }
            set
            {
                this.m_player2 = value;
            }
        }

        public GameBoard GameBoard
        {
            get
            {
                return this.m_GameBoard;
            }
        }

        public Player WhosTurnIsIt()
        {
            return Player1.IsMyTurn ? Player1 : Player2;
        }

        public Player WhichPlayerWon(out Player o_LoosingPlayer, out bool o_WasTie)
        {
            o_WasTie = Player1.Score == Player2.Score;
            Player winningPlayer = null;
            if(Player1.Score >= Player2.Score)
            {
                winningPlayer = Player1;
                o_LoosingPlayer = Player2;
            }
            else
            {
                winningPlayer = Player2;
                o_LoosingPlayer = Player1;
            }

            return winningPlayer;
        }

    }

}
