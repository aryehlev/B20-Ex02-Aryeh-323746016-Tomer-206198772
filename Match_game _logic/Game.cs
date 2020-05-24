
using System;
using System.Text;

namespace Match_game__logic
{
    public class Game
    {
        private GameBoard m_GameBoard;
        private Player m_player1;
        private Player m_player2;
        private bool m_MultiPlayerMode;

        public Game(
            int i_NumberOfRows,
            int i_NumberOfColumns,
            bool i_MultiPlayerMode,
            string i_NameOfPlayer1,
            string i_NameOfPlayer2 = "Computer")
        {
            this.m_GameBoard = new GameBoard(i_NumberOfRows, i_NumberOfColumns);
            this.m_player1 = new Player(i_NameOfPlayer1, false, true);
            this.m_player2 = new Player(i_NameOfPlayer2, i_MultiPlayerMode, false);
            this.m_MultiPlayerMode = i_MultiPlayerMode;
            this.m_GameBoard.PrintGameBoard();
        }

        public void FirstMove(int i_Row, int i_Column)
        {
            this.m_GameBoard.ExposeCard(i_Row, i_Column);
            this.m_GameBoard.PrintGameBoard();
        }

        public void SecondMove(int i_Row, int i_Column)
        {
            if(this.m_GameBoard.GuessCard(i_Row, i_Column))
            {
                if(m_player1.IsMyTurn)
                {
                    this.m_player1.PlayerScore++;
                }
                else
                {
                    this.m_player2.PlayerScore++;
                }
            }
            else
            {
                this.m_player1.IsMyTurn = !this.m_player1.IsMyTurn;
                this.m_player2.IsMyTurn = !this.m_player2.IsMyTurn;
            }
        }

        public bool IsGameOver()
        {
            return this.m_GameBoard.IsBoardFullyExposed();
        }

        public bool IsCardExposed(int i_row, int i_column)
        {
            return this.m_GameBoard.IsCardExposed(i_row, i_column);
        }


        public int GetLengthOfBoard()
        {
            return this.m_GameBoard.GetLengthOfBoard();
        }

        public int GetHeightOfBoard()
        {
            return this.m_GameBoard.GetHeightOfBoard();
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

        public Player WhosTurnIsIt()
        {
            return Player1.IsMyTurn ? Player1 : Player2;
        }

    }

}
