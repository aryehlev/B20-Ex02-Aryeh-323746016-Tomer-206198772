using System.Collections.Generic;
using System;
using System.Linq;

namespace Match_game_logic
{ 
    public enum eMultiplayerModes
    {
        Off,
        Random,
        Easy,
        Normal,
        Genius
    }

    public class AiForComputerPlay
    {
        private readonly List<BoardCoordinates> r_Memory;
        private readonly int r_MemoryDepth;
        private readonly eMultiplayerModes r_MultiplayerMode;
        public const int k_EasyMemory = 2;
        public const int k_MediumMemory = 4;
        
        public AiForComputerPlay(eMultiplayerModes i_MultiplayerMode)
        {
            this.r_MultiplayerMode = i_MultiplayerMode;
            this.r_Memory = new List<BoardCoordinates>();
            switch (this.r_MultiplayerMode)
            {
                case eMultiplayerModes.Easy:
                    this.r_MemoryDepth = k_MediumMemory;
                    break;
                case eMultiplayerModes.Normal:
                    this.r_MemoryDepth = int.MaxValue;
                    break;
                default:
                    this.r_MemoryDepth = 0;
                    break;
            }
        }

        public BoardCoordinates[] GetCoordinatesForNextMove(GameBoard i_GameBoard)
        {
            List<BoardCoordinates> possibleBoardCoordinateses = new List<BoardCoordinates>();
            int heightOfBoard = i_GameBoard.GetHeightOfBoard();
            int lengthOfBoard = i_GameBoard.GetLengthOfBoard();
            for (int i = 0; i < heightOfBoard; i++)
            {
                for (int j = 0; j < lengthOfBoard; j++)
                {
                    BoardCoordinates currentBoardCoordinates= new BoardCoordinates(i,j);

                    if (!i_GameBoard.IsCardExposed(currentBoardCoordinates))
                    {
                        possibleBoardCoordinateses.Add(currentBoardCoordinates);
                    }
                }
            }

            Random rnd = new Random();
            int firstCardIdx = rnd.Next(0, possibleBoardCoordinateses.Count);
            BoardCoordinates firstChoiceBoardCoordinates = possibleBoardCoordinateses.ElementAt(firstCardIdx);
            possibleBoardCoordinateses.Remove(firstChoiceBoardCoordinates);
            int secondCardIdx = rnd.Next(0, possibleBoardCoordinateses.Count);
            BoardCoordinates secondChoiceBoardCoordinates = possibleBoardCoordinateses.ElementAt(secondCardIdx);
            this.r_Memory.Remove(firstChoiceBoardCoordinates);

            if (this.r_MultiplayerMode == eMultiplayerModes.Genius)
            {
                foreach (BoardCoordinates boardCoordinates1 in this.r_Memory)
                {
                    foreach (BoardCoordinates boardCoordinates2 in this.r_Memory)
                    {
                        if (i_GameBoard.GetCardByCoordinates(boardCoordinates1).Letter == i_GameBoard.GetCardByCoordinates(boardCoordinates2).Letter && 
                            boardCoordinates1.Column != boardCoordinates2.Column && boardCoordinates1.Row!= boardCoordinates2.Row)
                        {
                            secondChoiceBoardCoordinates = boardCoordinates2;
                            firstChoiceBoardCoordinates = boardCoordinates1;
                        }
                    }
                }
            }
            else if (this.r_MultiplayerMode != eMultiplayerModes.Random && this.r_Memory.Any())
            {
                int cardsCheckedForMatch = 0;
                foreach (BoardCoordinates boardCoordinates in this.r_Memory)
                {
                    if (cardsCheckedForMatch > this.r_MemoryDepth)
                    {
                        break;
                    }
                    
                    if (i_GameBoard.GetCardByCoordinates(firstChoiceBoardCoordinates).Letter == i_GameBoard.GetCardByCoordinates(boardCoordinates).Letter)
                    {
                        secondChoiceBoardCoordinates = boardCoordinates;
                    }

                    cardsCheckedForMatch++;
                }
            }

            this.r_Memory.Remove(secondChoiceBoardCoordinates);
            return new BoardCoordinates[] { firstChoiceBoardCoordinates , secondChoiceBoardCoordinates };
        }

        public void SaveToMemory(BoardCoordinates i_BoardCoordinates)
        {
            if(!r_Memory.Contains(i_BoardCoordinates))
            {
                this.r_Memory.Insert(0, i_BoardCoordinates);
            }
        }
    }
}
