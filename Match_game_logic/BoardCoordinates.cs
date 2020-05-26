using System;

namespace Match_game_logic
{
    public struct BoardCoordinates
    {
        private int m_Row;
        private int m_Column;

        public BoardCoordinates(int i_Row = -1, int i_Column = -1)
        {
            this.m_Row = i_Row;
            this.m_Column = i_Column;
        } 
        
        public int Row
        {
            get
            {
                return this.m_Row;
            }

            set
            {
                this.m_Row = value;
            }
        }

        public int Column
        {
            get
            {
                return this.m_Column;
            }

            set
            {
                this.m_Column = value;
            }
        }

        public static BoardCoordinates TryParsePlacement(string i_placement, out bool o_WasSuccess)
        {
            BoardCoordinates newBoardCoordinates = new BoardCoordinates();
            o_WasSuccess = false;
            if (char.IsUpper(i_placement[0]) && char.IsDigit(i_placement[1]) && i_placement[1] != '0')
            {
                newBoardCoordinates = new BoardCoordinates(i_placement[1] - '0' - 1, i_placement[0] - 'A');
                o_WasSuccess = true;
            }

            return newBoardCoordinates;
        }
    }
}
