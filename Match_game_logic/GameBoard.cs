using System;
using System.Text;

namespace Match_game_logic
{
    public class GameBoard
    {
        private Card[,] m_GameBoard;
        private int m_numberOfExposedPairs;
        private Card m_CardTemporaryTemporaryExposedByPlayer;

        public GameBoard(int i_NumberOfRows, int i_NumberOfColumns)
        {
            this.m_numberOfExposedPairs = 0;
            this.m_CardTemporaryTemporaryExposedByPlayer = null;
            this.m_GameBoard = new Card[i_NumberOfRows, i_NumberOfColumns];
            this.initGameBoard();
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
        
        public Card CardTemporaryExposedByPlayer
        {
            get
            {
                return this.m_CardTemporaryTemporaryExposedByPlayer;
            }

            set
            {
                this.m_CardTemporaryTemporaryExposedByPlayer = value;
            }
        } 
        
        public int GetLengthOfBoard()
        {
            return this.m_GameBoard.GetLength(1);
        }

        public int GetHeightOfBoard()
        {
            return this.m_GameBoard.GetLength(0);
        } 
        
        public Card GetCardByCoordinates(BoardCoordinates i_CardCoordinates)
        {
            return this.m_GameBoard[i_CardCoordinates.Row, i_CardCoordinates.Column];
        } 
        
        public bool IsCardExposed(BoardCoordinates i_boardCoordinates)
        {
            return this.GetCardByCoordinates(i_boardCoordinates).Exposed;
        } 
        
        public void ExposeCard(BoardCoordinates i_boardCoordinates)
        { 
            this.m_CardTemporaryTemporaryExposedByPlayer = this.m_GameBoard[i_boardCoordinates.Row, i_boardCoordinates.Column];
            this.m_CardTemporaryTemporaryExposedByPlayer.Exposed = true;
        }

        public bool GuessCard(BoardCoordinates i_boardCoordinates)
        {
            bool guessWasCorrect = false;
            Card guessedCard = this.GetCardByCoordinates(i_boardCoordinates);
            guessedCard.Exposed = true;
            if (this.m_CardTemporaryTemporaryExposedByPlayer.Letter == guessedCard.Letter)
            {
                this.m_numberOfExposedPairs += 1;
                guessWasCorrect = true;
            }

            return guessWasCorrect;
        }

        public void EraseLastMoveFromBoard(BoardCoordinates i_GuessedCardCoordinates)
        {
            Card wronglyGueesedCard = this.GetCardByCoordinates(i_GuessedCardCoordinates);
            wronglyGueesedCard.Exposed = false;
            this.m_CardTemporaryTemporaryExposedByPlayer.Exposed = false;
        } 
        
        public bool IsBoardFullyExposed()
        {
            return this.NumberOfExposedPairs == (this.GetHeightOfBoard() * this.GetLengthOfBoard()) / 2;
        } 
        
        private void initGameBoard()
        {
            int lengthOfBoard = this.GetLengthOfBoard();
            int heightOfBoard = this.GetHeightOfBoard();
            int numberOfPairs = lengthOfBoard * heightOfBoard / 2;
            char[] cardsPossibleLetters = new char[numberOfPairs * 2];
            char nextLetter = 'A';
            for (int i = 0; i < numberOfPairs; i++)
            {
                cardsPossibleLetters[i * 2] = nextLetter;
                cardsPossibleLetters[(i * 2) + 1] = nextLetter;
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
                for (int j = 0; j < lengthOfBoard; j++)
                {
                    this.m_GameBoard[i, j] = new Card(cardsPossibleLetters[(i * lengthOfBoard) + j], new BoardCoordinates(i, j));
                }
            }
        }
    }
}
