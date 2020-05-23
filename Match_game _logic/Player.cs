
namespace Match_game__logic
{
    class Player
    {
        private string m_PlayerName;
        private int m_PlayerScore;
        private bool m_IsComputer;

        public Player(string i_PlayerName, bool i_IsComputer)
        {
            this.m_PlayerName = i_PlayerName;
            this.m_PlayerScore = 0;
            this.m_IsComputer = i_IsComputer;
        }

        public int PlayerScore
        {
            get
            {
                return this.m_PlayerScore;
            }
            set
            {
                this.m_PlayerScore = value;
            }
        }

        public string PlayerName
        {
            get
            {
                return this.m_PlayerName;
            }
            set
            {
                this.m_PlayerName = value;
            }
        }
    }
}
