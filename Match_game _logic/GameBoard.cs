using System;
using System.Text;

namespace Match_game__logic
{
    public class GameBoard
    {
        private Card[,] m_GameBoard;
        private int m_numberOfExposedPairs;
        private Card m_cardExposedByPlayer;

        public GameBoard(int i_NumberOfRows, int i_NumberOfColumns)
        {
            this.m_numberOfExposedPairs = 0;
            this.m_cardExposedByPlayer = null;
            this.m_GameBoard = new Card[i_NumberOfRows, i_NumberOfColumns];
            initGameBoard();
        }

        public int NumberOfExposedPairs
        {
            get
            {
                return this.m_numberOfExposedPairs;
            }
            set
            {
                this.m_numberOfExposedPairs = value;
            }
        }

        public Card CardExposedByPlayer
        {
            get
            {
                return this.m_cardExposedByPlayer;
            }
            set
            {
                this.m_cardExposedByPlayer = value;
            }
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
                    //this.m_GameBoard[i, j] = new Card('A');
                }
            }
        }
        public void ExposeCard(BoardCoordinates i_boardCoordinates)
        { 
            this.m_cardExposedByPlayer = this.m_GameBoard[i_boardCoordinates.Row, i_boardCoordinates.Column];
            this.m_cardExposedByPlayer.Exposed = true;
        }

        public bool GuessCard(BoardCoordinates i_boardCoordinates)
        {
            bool guessWasCorrect = false;
            Card cardPickedByPlayer = this.m_GameBoard[i_boardCoordinates.Row, i_boardCoordinates.Column];
            if (m_cardExposedByPlayer.Letter == cardPickedByPlayer.Letter)
            {
                cardPickedByPlayer.Exposed = true;
                m_numberOfExposedPairs += 1;
                this.PrintGameBoard();
                guessWasCorrect = true;
            }
            else
            {
                cardPickedByPlayer.Exposed = true;
                this.PrintGameBoard();
                System.Threading.Thread.Sleep(2000);
                cardPickedByPlayer.Exposed = false;
                m_cardExposedByPlayer.Exposed = false;
                this.PrintGameBoard();
            }

            return guessWasCorrect;
        }

        public bool IsCardExposed(BoardCoordinates i_boardCoordinates)
        {
             return this.m_GameBoard[i_boardCoordinates.Row, i_boardCoordinates.Column].Exposed;
        }

        public bool IsBoardFullyExposed()
        {
            return this.NumberOfExposedPairs == (this.GetHeightOfBoard() * this.GetLengthOfBoard()) /  2;
        }

        public int GetLengthOfBoard()
        {
            return this.m_GameBoard.GetLength(1);
        }

        public int GetHeightOfBoard()
        {
            return this.m_GameBoard.GetLength(0);
        }

        public Card[,] GetGameBoard()
        {
            return this.m_GameBoard;
        }

        public void PrintGameBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            int heightOfBoard = this.GetHeightOfBoard();
            int lenghtOfBoard = this.GetLengthOfBoard();
            StringBuilder strToReturn = new StringBuilder(" ");
            char columnIndexChar = 'A';
            for (int i = 0; i < lenghtOfBoard; i++)
            {
                strToReturn.Append($"     {columnIndexChar}");
                columnIndexChar++;
            }

            strToReturn.Append(getSeperationRow(lenghtOfBoard));
            for (int i = 0; i < heightOfBoard; i++)
            {
                strToReturn.Append($" {i + 1} |");
                for (int j = 0; j < this.m_GameBoard.GetLength(1); j++)
                {
                    if (this.m_GameBoard[i, j].Exposed)
                    {
                        strToReturn.Append($"  {this.m_GameBoard[i, j].Letter}  |");
                    }
                    else
                    {
                        strToReturn.Append($"     |");
                    }
                }

                strToReturn.Append(getSeperationRow(lenghtOfBoard));
            }

            Console.Out.WriteLine(strToReturn);
        }

        private static string getSeperationRow(int i_columnsNumber)
        {
            StringBuilder strToReturn = new StringBuilder();
            strToReturn.Append("\n    ");
            strToReturn.Append('=', i_columnsNumber * 6);
            strToReturn.Append("\n");
            return strToReturn.ToString();
        }
    }


}
