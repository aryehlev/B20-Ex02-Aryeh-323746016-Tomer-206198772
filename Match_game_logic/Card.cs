namespace Match_game_logic
{
    public class Card
    {
        private readonly BoardCoordinates r_CardCoordinates;
        private char m_Letter;
        private bool m_Exposed; 
        
        public Card(char i_Letter, BoardCoordinates i_CardCoordinates)
        {
            this.m_Letter = i_Letter;
            this.m_Exposed = false;
            this.r_CardCoordinates = i_CardCoordinates;
        }

        public bool Exposed
        {
            get
            {
                return this.m_Exposed;
            }

            set
            {
                this.m_Exposed = value;
            }
        }

        public char Letter
        {
            get
            {
                return this.m_Letter;
            }

            set
            {
                this.m_Letter = value;
            }
        }

        public BoardCoordinates CardCoordinates
        {
            get
            {
                return this.r_CardCoordinates;
            }
        }
    }
}
