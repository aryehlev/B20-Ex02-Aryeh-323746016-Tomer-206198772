using System.Collections.Generic;
using System;

namespace Match_game_logic
{ 
    public enum eMultiplayerModes
    {
        Off,
        Random,
        Easy,
        Normal,
        Hard,
        Genius
    }

    public class AiForComputerPlay
    {
        public const int k_EasyMemory = 2;
        public const int k_MediumMemory = 4;
        private readonly eMultiplayerModes r_MultiplayerMode;
        private readonly List<Card> r_Memory;
        private readonly int r_MemoryDepth;

        public AiForComputerPlay(eMultiplayerModes i_MultiplayerMode)
        {
            this.r_MultiplayerMode = i_MultiplayerMode;
            this.r_Memory = new List<Card>();
            switch (this.r_MultiplayerMode)
            {
                case eMultiplayerModes.Easy:
                    this.r_MemoryDepth = k_EasyMemory;
                    break;
                case eMultiplayerModes.Normal:
                    this.r_MemoryDepth = k_MediumMemory;
                    break;
                case eMultiplayerModes.Hard:
                    this.r_MemoryDepth = int.MaxValue;
                    break;
                default:
                    this.r_MemoryDepth = 0;
                    break;
            }
        }

        public BoardCoordinates[] GetCoordinatesForNextMove(GameBoard i_GameBoard)
        {
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

            if (this.r_MultiplayerMode == eMultiplayerModes.Genius)
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
            else if (this.r_MultiplayerMode != eMultiplayerModes.Random && this.r_Memory != null)
            {
                this.r_Memory.Remove(firstChoiceCard);  // avoid choosing the same card as a second choice
                int cardsCheckedForMatch = 0;
                foreach (Card cardFromMemory in this.r_Memory)
                {
                    if (cardsCheckedForMatch > this.r_MemoryDepth)
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

                this.r_Memory.Remove(secondChoiceCard);
            }

            return new BoardCoordinates[] { firstChoiceCard.CardCoordinates, secondChoiceCard.CardCoordinates };
        }

        public void SaveToMemory(Card i_Card)
        {
            this.r_Memory.Insert(0, i_Card);
        }

        public void RemoveFromMemory(Card i_Card)
        {
            this.r_Memory.Remove(i_Card);
        }
    }
}
