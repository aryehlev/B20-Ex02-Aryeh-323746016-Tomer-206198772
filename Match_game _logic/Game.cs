
using System;
using System.Text;

namespace Match_game__logic
{
    class Game
    {
        private Card[,] m_GameBoard;
        private Card m_cardExposedByPlayer;
        private string m_NameOfPlayer1;
        private string m_NameOfPlayer2;
        private bool m_MultiPlayerMode;
        private int m_Player1Score;
        private int m_Player2Score;

        public Game(int i_NumberOfRows, int i_NumberOfColumns, bool i_MultiPlayerMode, string i_NameOfPlayer1, string i_NameOfPlayer2 = "Computer")
        {
            this.m_GameBoard = new Card[i_NumberOfRows, i_NumberOfColumns];
            this.m_Player1Score = 0;
            this.m_Player2Score = 0;
            this.m_NameOfPlayer1 = i_NameOfPlayer1;
            this.m_NameOfPlayer2 = i_NameOfPlayer2;
            this.m_MultiPlayerMode = i_MultiPlayerMode;
            this.m_cardExposedByPlayer = null;
            initGameBoard();
        }


        private void initGameBoard()
        {
            int numberOfPairs = (this.m_GameBoard.GetLength(0) * this.m_GameBoard.GetLength(1)) / 2;
            char[] cardsPossibleLetters = new char[numberOfPairs * 2];
            char nextLetter = 'A';
            for (int i = 0; i < numberOfPairs; i++)
            {
                cardsPossibleLetters[i * 2] = nextLetter;
                cardsPossibleLetters[i * 2 + 1] = nextLetter;
                nextLetter++;
            }
            
            Random rnd = new Random();
            for (int i = cardsPossibleLetters.Length - 1; i >= 0; i--)
            {
                char currentLetter = cardsPossibleLetters[i];
                int randomNumber = rnd.Next(0, i + 1);
                cardsPossibleLetters[i] = cardsPossibleLetters[randomNumber];
                cardsPossibleLetters[randomNumber] = currentLetter;
            }

            for (int i = 0; i < this.m_GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < this.m_GameBoard.GetLength(1); j++)
                {
                    this.m_GameBoard[i, j] = new Card(cardsPossibleLetters[i * this.m_GameBoard.GetLength(0) + j]);
                }
            }
        }

        public void ExposeCard(int i_PlayerNumber, int i_Row, char i_Column)
        {

            m_cardExposedByPlayer = m_GameBoard[i_Row, i_Column];
            m_cardExposedByPlayer.Exposed = true;
            PrintGameBoard();
        }

        public void GuessCard(int i_PlayerNumber, int i_Row, char i_Column)
        {
            Card cardPickedByPlayer = m_GameBoard[i_Row, i_Column];
            if(m_cardExposedByPlayer.Letter == cardPickedByPlayer.Letter)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                cardPickedByPlayer.Exposed = true;
                PrintGameBoard();
            }
            else
            {
                cardPickedByPlayer.Exposed = true;
                Ex02.ConsoleUtils.Screen.Clear();
                PrintGameBoard();
                System.Threading.Thread.Sleep(2000);
                cardPickedByPlayer.Exposed = false;
                m_cardExposedByPlayer.Exposed = false;
            }

            
        }

        //must do
        public string PrintGameBoard()
        {
            StringBuilder strToReturn = new StringBuilder();
            for (int i = 0; i < this.m_GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < this.m_GameBoard.GetLength(1); j++)
                {
                    strToReturn.Append(this.m_GameBoard[i, j] + "\t");
                }
                strToReturn.Append("\n");
            }
            return strToReturn.ToString();
        }


        class Card
        {
            private char m_Letter;
            private bool _m_Exposed;

            public Card(char i_Letter)
            {
                this.m_Letter = i_Letter;
                this._m_Exposed = false;
            }

            public bool Exposed
            {
                get
                {
                    return this._m_Exposed;
                }
                set
                {
                    this._m_Exposed = value;
                }
            }

            public char Letter
            {
                get
                {
                    return this.m_Letter;
                }
                set
                {
                    this.m_Letter = value;
                }
            }

            public override string ToString() {
                return $"{this.m_Letter}";
            }
        }



    }
}
