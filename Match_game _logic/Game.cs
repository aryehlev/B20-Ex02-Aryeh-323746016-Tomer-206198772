
using System;
using System.Text;

namespace Match_game__logic
{
    class Game
    {
        private Card[,] m_GameBoard;
        private int m_numberOfUnexposedPairs;
        private Card m_cardExposedByPlayer;
        private Player m_player1;
        private Player m_player2;
        private bool m_MultiPlayerMode;

        public Game(int i_NumberOfRows, int i_NumberOfColumns, bool i_MultiPlayerMode, string i_NameOfPlayer1, string i_NameOfPlayer2 = "Computer")
        {
            this.m_GameBoard = new Card[i_NumberOfRows, i_NumberOfColumns];
            this.m_numberOfUnexposedPairs = (i_NumberOfRows * i_NumberOfColumns) / 2;
            this.m_player1 = new Player(i_NameOfPlayer1, false);
            this.m_player2 = new Player(i_NameOfPlayer2, i_MultiPlayerMode);
            this.m_MultiPlayerMode = i_MultiPlayerMode;
            this.m_cardExposedByPlayer = null;
            this.initGameBoard();
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

        public void ExposeCard(int i_Row, char i_Column)
        {
            m_cardExposedByPlayer = m_GameBoard[i_Row, i_Column];
            m_cardExposedByPlayer.Exposed = true;
            this.PrintGameBoard();
        }

        public void GuessCard(int i_PlayerNumber, int i_Row, char i_Column)
        {
            Card cardPickedByPlayer = m_GameBoard[i_Row, i_Column];
            if(m_cardExposedByPlayer.Letter == cardPickedByPlayer.Letter)
            {
                // Ex02.ConsoleUtils.Screen.Clear();
                cardPickedByPlayer.Exposed = true;
                m_numberOfUnexposedPairs -= 2;
                this.PrintGameBoard();
                if (i_PlayerNumber == 1)
                {
                    this.m_player1.PlayerScore++;
                } else
                {
                    this.m_player2.PlayerScore++;
                }
            }
            else
            {
                cardPickedByPlayer.Exposed = true;
                // Ex02.ConsoleUtils.Screen.Clear();
                this.PrintGameBoard();
                System.Threading.Thread.Sleep(2000);
                cardPickedByPlayer.Exposed = false;
                m_cardExposedByPlayer.Exposed = false;
            }
        }

        public bool IsGameOver()
        {
            return this.m_numberOfUnexposedPairs == 0;
        }

        public string PrintGameBoard()
        {
            int rowsNumber = this.m_GameBoard.GetLength(0);
            int columnsNumber = this.m_GameBoard.GetLength(1);
            StringBuilder strToReturn = new StringBuilder(" ");
            char columnIndexChar = 'A';
            for (int i = 0; i < columnsNumber; i++)
            {
                strToReturn.Append($"     {columnIndexChar}");
                columnIndexChar++;
            }
            strToReturn.Append(getSeperationRow(columnsNumber));
            for (int i = 0; i < rowsNumber; i++)
            {
                strToReturn.Append($" {i + 1} |");
                for (int j = 0; j < this.m_GameBoard.GetLength(1); j++)
                {
                    strToReturn.Append($"  {this.m_GameBoard[i, j]}  |");
                }
                strToReturn.Append(getSeperationRow(columnsNumber));
            }
            return strToReturn.ToString();
        }

        private static string getSeperationRow(int columnsNumber)
        {
            StringBuilder strToReturn = new StringBuilder();
            strToReturn.Append("\n    ");
            strToReturn.Append('=', columnsNumber * 6);
            strToReturn.Append("\n");
            return strToReturn.ToString();
        }


        class Card
        {
            private char m_Letter;
            private bool m_Exposed;

            public Card(char i_Letter)
            {
                this.m_Letter = i_Letter;
                this.m_Exposed = false;
            }

            public bool Exposed
            {
                get
                {
                    return this.m_Exposed;
                }
                set
                {
                    this.m_Exposed = value;
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
