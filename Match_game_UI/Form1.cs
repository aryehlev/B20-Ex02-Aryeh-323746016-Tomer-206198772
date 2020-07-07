using System;
using System.Windows.Forms;
using Match_game_logic;

namespace Match_game_UI
{
    public partial class Form1 : Form
    {
        private int m_NumberOfRows = 4;
        private int m_NumberOfColumns = 4;
        private string m_Player1Name = "";
        private string m_Player2Name = "Computer";
        private eAiModes m_AiMode = eAiModes.Random;
        public Form1()
        {
            InitializeComponent();
        }

        public Game getGame()
        {
            return new Game(m_NumberOfRows, m_NumberOfColumns, m_AiMode, m_Player1Name, m_Player2Name);
        }

        private void friendOrComputerButton_Click(object sender, EventArgs e)
        {
            m_SecondPlayerNameTextBox.Enabled = !m_SecondPlayerNameTextBox.Enabled;
            m_FriendOrComputerButton.Text = m_SecondPlayerNameTextBox.Enabled ? "Against Computer" : "Against a Friend";
            m_AiMode = m_SecondPlayerNameTextBox.Enabled ? eAiModes.Off : eAiModes.Random;
            m_SecondPlayerNameTextBox.Text = m_SecondPlayerNameTextBox.Enabled ? "" : "-Computer-";
            m_Player2Name = m_SecondPlayerNameTextBox.Enabled ? "" : "Computer";
        }

        private void m_StartGameButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_SizeOfBoardButton_Click(object sender, EventArgs e)
        {
            if(m_NumberOfColumns != 6)
            {
                m_NumberOfColumns += m_NumberOfColumns == 4 && m_NumberOfRows == 5 ? 2 : 1;
            }
            else
            {
                m_NumberOfColumns = 4;
                if(m_NumberOfRows != 6)
                {
                    m_NumberOfRows++;
                }
                else
                {
                    m_NumberOfRows = 4;
                }
            }
            
            m_SizeOfBoardButton.Text = $"{m_NumberOfRows}x{m_NumberOfColumns}";
        }

        private void m_FirstPlayerNameTextBox_TextChanged(object sender, EventArgs e)
        {
            m_Player1Name = m_FirstPlayerNameTextBox.Text;
        }

        private void m_SecondPlayerNameTextBox_TextChanged(object sender, EventArgs e)
        {
            m_Player2Name = m_SecondPlayerNameTextBox.Text;
        }
    }
}
