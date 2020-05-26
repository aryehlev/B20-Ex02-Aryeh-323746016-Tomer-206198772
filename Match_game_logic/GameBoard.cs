using System;
using System.Text;

namespace Match_game_logic
{
    public class GameBoard
    {
        private Card[,] m_GameBoard;
        private int m_numberOfExposedPairs;
        private Card m_cardTemporaryExposedByPlayer;

        public GameBoard(int i_NumberOfRows, int i_NumberOfColumns)
        {
            this.m_numberOfExposedPairs = 0;
            this.m_cardTemporaryExposedByPlayer = null;
            this.m_GameBoard = new Card[i_NumberOfRows, i_NumberOfColumns];
            initGameBoard();
        }

        private void initGameBoard()
        {
            int LengthOfBoard = this.GetLengthOfBoard();
            int heightOfBoard = this.GetHeightOfBoard();
            int numberOfPairs = LengthOfBoard * heightOfBoard / 2;
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

            for (int i = 0; i < heightOfBoard; i++)
            {
                for (int j = 0; j < LengthOfBoard; j++)
                {
                    this.m_GameBoard[i, j] = new Card(cardsPossibleLetters[i * LengthOfBoard + j], new BoardCoordinates(i, j));
                    //this.m_GameBoard[i, j] = new Card('A', new BoardCoordinates(i, j));
                }
            }
            //this.m_GameBoard[0, 0] = new Card('B', new BoardCoordinates(0, 0));
            //this.m_GameBoard[3, 3] = new Card('B', new BoardCoordinates(3, 3));
        }
        public void ExposeCard(BoardCoordinates i_boardCoordinates)
        { 
            this.m_cardTemporaryExposedByPlayer = this.m_GameBoard[i_boardCoordinates.Row, i_boardCoordinates.Column];
            this.m_cardTemporaryExposedByPlayer.Exposed = true;
        }

        public bool GuessCard(BoardCoordinates i_boardCoordinates)
        {
            bool guessWasCorrect = false;
            Card guessedCard = this.GetCardByCoordinates(i_boardCoordinates);
            guessedCard.Exposed = true;
            if (m_cardTemporaryExposedByPlayer.Letter == guessedCard.Letter)
            {
                m_numberOfExposedPairs += 1;
                guessWasCorrect = true;
            }

            return guessWasCorrect;
        }

        public void EraseLastMoveFromBoard(BoardCoordinates i_GuessedCardCoordinates)
        {
            Card wronglyGueesedCard = this.GetCardByCoordinates(i_GuessedCardCoordinates);
            wronglyGueesedCard.Exposed = false;
            this.m_cardTemporaryExposedByPlayer.Exposed = false;
        }

        public bool IsCardExposed(BoardCoordinates i_boardCoordinates)
        {
             return this.GetCardByCoordinates(i_boardCoordinates).Exposed;
        }

        public bool IsBoardFullyExposed()
        {
            return this.NumberOfExposedPairs == (this.GetHeightOfBoard() * this.GetLengthOfBoard()) /  2;
        }

        public Card GetCardByCoordinates(BoardCoordinates i_CardCoordinates)
        {
            return this.m_GameBoard[i_CardCoordinates.Row, i_CardCoordinates.Column];
        }

        public Card[,] GetGameBoard()
        {
            return this.m_GameBoard;
        }

        public int GetLengthOfBoard()
        {
            return this.m_GameBoard.GetLength(1);
        }

        public int GetHeightOfBoard()
        {
            return this.m_GameBoard.GetLength(0);
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
                return this.m_cardTemporaryExposedByPlayer;
            }
            set
            {
                this.m_cardTemporaryExposedByPlayer = value;
            }
        }
    }


}
