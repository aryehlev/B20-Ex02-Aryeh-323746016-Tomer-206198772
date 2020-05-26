using System.Collections.Generic;
using System;

namespace Match_game_logic
{ 
    public enum MultiplayerModes
    {
        off,
        random,
        easy,
        normal,
        hard,
        genius
    }

    public class AI
    {
        public const int k_EasyMemory = 2;
        public const int k_MediumMemory = 4;
        private readonly MultiplayerModes m_MultiplayerMode;
        private List<Card> m_Memory;
        private int m_MemoryDepth;

        public AI(MultiplayerModes i_MultiplayerMode)
        {
            this.m_MultiplayerMode = i_MultiplayerMode;
            this.m_Memory = new List<Card>();
            switch (this.m_MultiplayerMode)
            {
                case MultiplayerModes.easy:
                    this.m_MemoryDepth = k_EasyMemory;
                    break;
                case MultiplayerModes.normal:
                    this.m_MemoryDepth = k_MediumMemory;
                    break;
                case MultiplayerModes.hard:
                    this.m_MemoryDepth = int.MaxValue;
                    break;
                default:
                    this.m_MemoryDepth = 0;
                    break;
            }
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
                    Card currentCard = i_GameBoard.GetCardByCoordinates(new BoardCoordinates(i, j));
                    
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

            if (this.m_MultiplayerMode == MultiplayerModes.genius)
            {
                foreach (Card cardFromPossibleCards in possibleCards)
                {
                    if (firstChoiceCard.Letter == cardFromPossibleCards.Letter)
                    {
                        // There's a match
                        secondChoiceCard = cardFromPossibleCards;
                    }
                }
            }
            else if (this.m_MultiplayerMode != MultiplayerModes.random && this.m_Memory != null)
            {
                this.m_Memory.Remove(firstChoiceCard);  // avoid choosing the same card as a second choice
                int cardsCheckedForMatch = 0;
                foreach (Card cardFromMemory in this.m_Memory)
                {
                    if (cardsCheckedForMatch > this.m_MemoryDepth)
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

        public void RemoveFromMemory(Card i_Card)
        {
            this.m_Memory.Remove(i_Card);
        }
    }
}
