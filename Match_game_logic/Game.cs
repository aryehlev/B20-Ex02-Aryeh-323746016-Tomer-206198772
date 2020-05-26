using System;

namespace Match_game_logic
{ 
    public class Game
    {
        private GameBoard m_GameBoard;
        private Player m_player1;
        private Player m_player2;
        private MultiplayerModes m_MultiPlayerMode;
        private AI m_ComputerAI;

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
            this.m_ComputerAI = null;
            if (i_MultiPlayerMode != MultiplayerModes.off)
            {
                this.m_ComputerAI = new AI(i_MultiPlayerMode);
            }
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

        public MultiplayerModes MultiplayerMode
        {
            get
            {
                return this.m_MultiPlayerMode;
            }
        }

        public AI ComputerAI
        {
            get
            {
                return this.m_ComputerAI;
            }
        } 
        
        public int GetPlayerScore(int i_PlayerNumber)
        {
            return i_PlayerNumber == 1 ? this.Player1.Score : this.Player2.Score;
        }

        public Player WhosTurnIsIt()
        {
            return this.Player1.IsMyTurn ? this.Player1 : this.Player2;
        }

        public bool GuessCardAndUpdateScores(BoardCoordinates i_boardCoordinates)
        {
            bool wasSuccsesfulGuess = this.m_GameBoard.GuessCard(i_boardCoordinates);
            if (wasSuccsesfulGuess)
            {
                if (this.Player1.IsMyTurn)
                {
                    this.Player1.Score++;
                }
                else
                {
                    this.Player2.Score++;
                }
            }
            else
            {
                this.Player1.IsMyTurn = !this.Player1.IsMyTurn;
                this.Player2.IsMyTurn = !this.Player2.IsMyTurn;
            }

            return wasSuccsesfulGuess;
        }

        public bool IsGameOver(out Player o_WinningPlayer, out Player o_LosingPlayer)
        {
            bool isGameOver = false;
            o_WinningPlayer = null;
            o_LosingPlayer = null;
            if (this.m_GameBoard.IsBoardFullyExposed())
            {
                isGameOver = true;
                if (this.Player1.Score > this.Player2.Score)
                {
                    o_WinningPlayer = this.Player1;
                    o_LosingPlayer = this.Player2;
                }
                else if (this.Player1.Score < this.Player2.Score)
                {
                    o_WinningPlayer = this.Player2;
                    o_LosingPlayer = this.Player1;
                }
            }

            return isGameOver;
        }
    }
}
