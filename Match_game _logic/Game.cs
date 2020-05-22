using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_game__logic
{
    class Game
    {
        private char[,] m_LettersBehindCards;
        private GameBoard m_GameBoard;
        private string m_NameOfPlayer1;
        private string m_NameOfPlayer2;
        private bool m_MultiPlayerMode;
        private int m_Player1Score;
        private int m_Player2Score;

        public Game(int i_NumberOfRows, int i_NumberOfColumns, string i_NameOfPlayer1, bool i_MultiPlayerMode, string i_NameOfPlayer2 = "Computer")
        {
            this.m_GameBoard = new GameBoard(i_NumberOfRows, i_NumberOfColumns);
            this.m_Player1Score = 0;
            this.m_Player2Score = 0;
            this.m_NameOfPlayer1 = i_NameOfPlayer1;
            this.m_NameOfPlayer2 = i_NameOfPlayer2;
            this.m_MultiPlayerMode = i_MultiPlayerMode;

        }


        private char[,] initLettersBehindCards(int i_NumberOfRows, int i_NumberOfColumns)
        {

        }

        public int[] FirstMove(int i_PlayerNumber, int i_Row, char i_Column)
        {

        }

        public bool Second



    }
}
