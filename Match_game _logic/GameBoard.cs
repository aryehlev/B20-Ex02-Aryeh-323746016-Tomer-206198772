
namespace Match_game__logic
{
    class GameBoard
    {
        private char[,] m_Board;
        private int m_PairsExposed;

        public GameBoard(int i_NumOfColumns, int i_NumOfRows)
        {
            this.m_Board = new char[i_NumOfRows, i_NumOfColumns];
            this.m_PairsExposed = 0;
        }


    }
}
