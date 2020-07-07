using System.Drawing;
using System.Windows.Forms;
using Match_game_logic;

namespace Match_game_UI
{
    class CardButton : Button
    {
        private Card m_ButtonCard;
        private Image m_ImageToShow;
        private bool m_showingImage;

        public CardButton(Card i_Card, Image i_ImageToShow)
        {
            m_ButtonCard = i_Card;
            m_ImageToShow = i_ImageToShow;
            m_showingImage = false;
        }

        internal Card ButtonCard
        {
            get
            {
                return m_ButtonCard;
            }
        }

        internal Image ImageToShow
        {
            get
            {
                return m_ImageToShow;
            }
        }

        internal bool ShowingImage
        {
            get
            {
                return m_showingImage;
            }
        }

        internal void ShowImageOnButton()
        {
            this.Image = m_ImageToShow;
            m_showingImage = true;
        }

        internal void HideImageOnButton()
        {
            this.Image = null;
            m_showingImage = false;
        }
    }
}
