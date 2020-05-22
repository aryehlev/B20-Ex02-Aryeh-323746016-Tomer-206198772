using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_game__logic
{
    class Game
    {
        private Card[,] m_GameBoard;
        private string m_NameOfPlayer1;
        private string m_NameOfPlayer2;
        private bool m_MultiPlayerMode;
        private int m_Player1Score;
        private int m_Player2Score;
        private Card m_
        public Game(int i_NumberOfRows, int i_NumberOfColumns, string i_NameOfPlayer1, bool i_MultiPlayerMode, string i_NameOfPlayer2 = "Computer")
        {
            this.m_GameBoard = new Card[i_NumberOfRows, i_NumberOfColumns];
            this.m_Player1Score = 0;
            this.m_Player2Score = 0;
            this.m_NameOfPlayer1 = i_NameOfPlayer1;
            this.m_NameOfPlayer2 = i_NameOfPlayer2;
            this.m_MultiPlayerMode = i_MultiPlayerMode;
            initCards();

        }


        private void initCards()
        {

        }

        public void ExposeCard(int i_PlayerNumber, int i_Row, char i_Column)
        {

        }

        public void GuessCard(int i_PlayerNumber, int i_Row, char i_Column)
        {

        }


        class Card
        {
            private char m_Letter;
            private bool m_Exposed;

            Card(char i_Letter)
            {
                this.m_Letter = i_Letter;
            }


        }



    }
}
