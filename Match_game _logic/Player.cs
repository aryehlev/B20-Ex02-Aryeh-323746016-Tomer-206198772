
namespace Match_game_logic
{
    public class Player
    {
        private readonly string m_Name;
        private bool m_isMyTurn;
        private int m_Score;
        private readonly bool m_IsComputer;

        public Player(string i_Name, bool i_IsComputer, bool i_IsMyTurn)
        {
            this.m_Name = i_Name;
            this.m_Score = 0;
            this.m_IsComputer = i_IsComputer;
            this.m_isMyTurn = i_IsMyTurn;
        }

        public int Score
        {
            get
            {
                return this.m_Score;
            }
            set
            {
                this.m_Score = value;
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
        }

        public bool IsMyTurn
        {
            get
            {
                return this.m_isMyTurn;
            }
            set
            {
                this.m_isMyTurn = value;
            }

        }

        public bool IsComputer
        {
            get
            {
                return this.m_IsComputer;
            }
        }
    }
}
