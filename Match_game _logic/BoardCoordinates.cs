using System;

namespace Match_game__logic
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

        public static BoardCoordinates ParsePlacement(string i_placement)
        {
            BoardCoordinates newBoardCoordinates = new BoardCoordinates();
            if (Char.IsUpper(i_placement[0]) && Char.IsDigit(i_placement[1]) && i_placement[1] != '0')
            {
                newBoardCoordinates = new BoardCoordinates(i_placement[1] - '0' - 1, i_placement[0] - 'A');
            }
            return newBoardCoordinates;
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
    }
}
