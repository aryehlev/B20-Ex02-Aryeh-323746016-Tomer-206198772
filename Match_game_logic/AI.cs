using System;
using System.Collections.Generic;


namespace Match_game_logic
{
    public enum MultiplayerModes
    {
        off,
        easy,
        normal,
        hard,
        impossible
    }

    public class AI
    {
        private readonly MultiplayerModes m_MultiplayerMode;
        private List<Card> m_Memory;
        public const int k_NormalMemory = 2;
        public const int k_HardMemory = 4;

        public AI(MultiplayerModes i_MultiplayerMode)
        {
            this.m_MultiplayerMode = i_MultiplayerMode;
            this.m_Memory = new List<Card>();
        }

        public BoardCoordinates[] GetCoordinatesForNextMove(GameBoard i_GameBoard)
        {
            BoardCoordinates[] nextMoveCoordinates = new BoardCoordinates[2];
            List<Card> possibleCards = new List<Card>();
            int heightOfBoard = i_GameBoard.GetHeightOfBoard();
            int lengthOfBoard = i_GameBoard.GetLengthOfBoard();
            for (int i = 0; i < heightOfBoard; i++)
            {
                for (int j = 0; j < lengthOfBoard; j++)
                {
                    Card currentCard = i_GameBoard.GetGameBoard()[i, j];
                    if (!currentCard.Exposed)
                    {
                        possibleCards.Add(currentCard);
                    }
                }
            }

            Random rnd = new Random();
            int firstCardIdx = rnd.Next(0, possibleCards.Count);
            Card firstChoiceCard = possibleCards[firstCardIdx];
            possibleCards.Remove(firstChoiceCard);
            int secondCardIdx = rnd.Next(0, possibleCards.Count);
            Card secondChoiceCard = possibleCards[secondCardIdx];

            if (this.m_MultiplayerMode != MultiplayerModes.easy && this.m_Memory != null)  // search for a match in memory
            {
                //this.m_Memory.Remove(firstChoiceCard);  // avoid choosing the same card as a second choice
                int memoryDepth = 0;
                switch (this.m_MultiplayerMode)
                {
                    case MultiplayerModes.normal:
                        memoryDepth = k_NormalMemory;
                        break;
                    case MultiplayerModes.hard:
                        memoryDepth = k_HardMemory;
                        break;
                    case MultiplayerModes.impossible:
                        memoryDepth = i_GameBoard.GetLengthOfBoard() * i_GameBoard.GetHeightOfBoard();
                        break;
                }
                int cardsCheckedForMatch = 0;
                foreach (Card cardFromMemory in this.m_Memory)
                {
                    if (cardsCheckedForMatch > memoryDepth)
                    {
                        break;
                    }
                    if (firstChoiceCard.Letter == cardFromMemory.Letter)
                    {
                        // There's a match
                        secondChoiceCard = cardFromMemory;
                    }

                    cardsCheckedForMatch++;
                }
                this.m_Memory.Remove(secondChoiceCard);

            }

            return new BoardCoordinates[2] { firstChoiceCard.CardCoordinates, secondChoiceCard.CardCoordinates };
        }

        public void SaveToMemory(Card i_Card)
        {
            this.m_Memory.Insert(0, i_Card);
        }

    }
}
