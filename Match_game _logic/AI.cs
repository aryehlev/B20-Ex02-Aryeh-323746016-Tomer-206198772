using System;
using System.Collections.Generic;

namespace Match_game__logic
{
    public enum MultiplayerModes
    {
        off,
        easy,
        normal,
        hard
    }

    public class AI
    {
        private MultiplayerModes m_MultiplayerMode;
        private char[] memorey;

        public AI(MultiplayerModes i_MultiplayerMode)
        {
            this.m_MultiplayerMode = i_MultiplayerMode;
        }

        public BoardCoordinates[] GetCoordinatesForNextMove(Card[,] i_GameBoard)
        {
            List<BoardCoordinates> possibleCardsCoordinates = new List<BoardCoordinates>();
            int heightOfBoard = i_GameBoard.GetLength(0);
            int lengthOfBoard = i_GameBoard.GetLength(1);
            for (int i = 0; i < heightOfBoard; i++)
            {
                for(int j = 0; j < lengthOfBoard; j++)
                {
                    Card currentCard = i_GameBoard[i, j];
                    if (!currentCard.Exposed)
                    {
                        possibleCardsCoordinates.Add(new BoardCoordinates(i,j));
                    }
                }
            }

            Random rnd = new Random();

            int firstCardNumber = rnd.Next(0, possibleCardsCoordinates.Count);
            BoardCoordinates firstChoiceCardCoordinates = possibleCardsCoordinates[firstCardNumber];
            possibleCardsCoordinates.Remove(firstChoiceCardCoordinates);
            int secondCardNumber = rnd.Next(0, possibleCardsCoordinates.Count);
            BoardCoordinates secondChoiceCardCoordinates = possibleCardsCoordinates[secondCardNumber];
            BoardCoordinates[] nextMoveCoordinates = new BoardCoordinates[2];
            return new BoardCoordinates[2]{firstChoiceCardCoordinates, secondChoiceCardCoordinates};
        }
    }
}
