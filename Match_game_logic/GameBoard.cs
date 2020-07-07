using System;

namespace Match_game_logic
{
    public class GameBoard
    {
        private readonly Card[,] r_GameBoard;
        private int m_NumberOfExposedPairs;
        private Card m_CardTemporaryExposedByPlayer;

        public GameBoard(int i_NumberOfRows, int i_NumberOfColumns)
        {
            this.m_NumberOfExposedPairs = 0;
            this.m_CardTemporaryExposedByPlayer = null;
            this.r_GameBoard = new Card[i_NumberOfRows, i_NumberOfColumns];
            this.initGameBoard();
        }

        public int NumberOfExposedPairs
        {
            get
            {
                return this.m_NumberOfExposedPairs;
            }

            set
            {
                this.m_NumberOfExposedPairs = value;
            }
        }
        
        public int GetLengthOfBoard()
        {
            return this.r_GameBoard.GetLength(1);
        }

        public int GetHeightOfBoard()
        {
            return this.r_GameBoard.GetLength(0);
        } 
        
        public Card GetCardByCoordinates(BoardCoordinates i_CardCoordinates)
        {
            return this.r_GameBoard[i_CardCoordinates.Row, i_CardCoordinates.Column];
        } 
        
        public bool IsCardExposed(BoardCoordinates i_BoardCoordinates)
        {
            return this.GetCardByCoordinates(i_BoardCoordinates).Exposed;
        } 
        
        public void ExposeCard(BoardCoordinates i_BoardCoordinates)
        { 
            this.m_CardTemporaryExposedByPlayer = this.r_GameBoard[i_BoardCoordinates.Row, i_BoardCoordinates.Column];
            this.m_CardTemporaryExposedByPlayer.Exposed = true;
        }

        public bool GuessCard(BoardCoordinates i_BoardCoordinates)
        {
            bool guessWasCorrect = false;
            Card guessedCard = this.GetCardByCoordinates(i_BoardCoordinates);
            guessedCard.Exposed = true;
           
            if (this.m_CardTemporaryExposedByPlayer.Letter == guessedCard.Letter)
            {
                this.m_NumberOfExposedPairs += 1;
                guessWasCorrect = true;
            }

            return guessWasCorrect;
        }

        public void EraseLastMoveFromBoard(BoardCoordinates i_GuessedCardCoordinates)
        {
            Card wronglyGuessedCard = this.GetCardByCoordinates(i_GuessedCardCoordinates);
            wronglyGuessedCard.Exposed = false;
            this.m_CardTemporaryExposedByPlayer.Exposed = false;
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
                    this.r_GameBoard[i, j] = new Card(cardsPossibleLetters[(i * lengthOfBoard) + j]);
                }
            }
        }
    }
}
