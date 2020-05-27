namespace Match_game_logic
{
    public class Card
    {
        private char m_Letter;
        private bool m_Exposed; 
        
        public Card(char i_Letter)
        {
            this.m_Letter = i_Letter;
            this.m_Exposed = false;
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
    }
}
