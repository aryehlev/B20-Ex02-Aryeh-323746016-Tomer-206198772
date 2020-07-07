using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Match_game_logic;

namespace Match_game_UI
{
    class Form2 : Form
    {
        private TableLayoutPanel m_TableLayoutPanel;
        private Label m_CurrentPlayerLabel;
        private Label m_Player1ScoreLabel;
        private Label m_Player2ScoreLabel;
        private Game m_GameToPlay;
        private GameBoard m_GameBoard;

        public Form2(Game i_GameToPlay)
        {
            m_GameToPlay = i_GameToPlay;
            m_GameBoard = m_GameToPlay.GameBoard;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            m_TableLayoutPanel = new TableLayoutPanel();
            m_CurrentPlayerLabel = new Label();
            m_Player1ScoreLabel = new Label();
            m_Player2ScoreLabel = new Label();

            this.SuspendLayout();
            // 
            // m_CurrentPlayerLabel
            // 
            m_CurrentPlayerLabel.AutoSize = true;
            m_CurrentPlayerLabel.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            updateCurrentPlayerLabel();
            // 
            // m_Player1ScoreLabel
            // 
            m_Player1ScoreLabel.AutoSize = true;
            m_Player1ScoreLabel.BackColor = Color.Aquamarine;
            updatePlayerScoreLabel(1);
            // 
            // m_Player2ScoreLabel
            // 
            m_Player2ScoreLabel.AutoSize = true;
            m_Player2ScoreLabel.BackColor = Color.LightPink;
            updatePlayerScoreLabel(2);
            // 
            // m_TableLayoutPanel
            // 
            InitializeTableLayoutPanel();
            
            this.Controls.Add(m_TableLayoutPanel);
            this.Width = 700;
            this.Height = 700;
            this.Name = "Form2";

            this.ResumeLayout(false);
        }

        private void InitializeTableLayoutPanel()
        {
            WebClient wc = new WebClient();
            Dictionary<char, Image> imagesByLetters = new Dictionary<char, Image>();
            int heightOfBoard = m_GameBoard.GetHeightOfBoard();
            int lengthOfBoard = m_GameBoard.GetLengthOfBoard();

            m_TableLayoutPanel.Dock = DockStyle.Fill;
            m_TableLayoutPanel.ColumnCount = lengthOfBoard;
            m_TableLayoutPanel.RowCount = heightOfBoard;

            for (int i = 0; i < lengthOfBoard; i++)
            {
                m_TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / lengthOfBoard));
            }

            for (int i = 0; i < heightOfBoard; i++)
            {
                m_TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / heightOfBoard));
            }

            for (int i = 0; i < heightOfBoard; i++)
            {
                for (int j = 0; j < lengthOfBoard; j++)
                {
                    Card card = m_GameBoard.GetCardByCoordinates(new BoardCoordinates(i, j));
                    CardButton button = null;
                    if (imagesByLetters.ContainsKey(card.Letter))
                    {
                        button = new CardButton(card, imagesByLetters[card.Letter]);
                    }
                    else
                    {
                        byte[] bytes = wc.DownloadData("https://picsum.photos/80");
                        MemoryStream ms = new MemoryStream(bytes);
                        Image imgFromWeb = Image.FromStream(ms);
                        imagesByLetters.Add(card.Letter, imgFromWeb);
                        button = new CardButton(card, imgFromWeb);
                    }

                    button.Name = $"card_{i}_{j}";
                    button.Dock = DockStyle.Fill;
                    button.Click += m_CardButton_Click;
                    m_TableLayoutPanel.Controls.Add(button, j, i);
                }
            }

            addLabelsToTableLayoutPanel();
        }

        private void addLabelsToTableLayoutPanel()
        {
            m_TableLayoutPanel.Controls.Add(m_CurrentPlayerLabel);
            m_TableLayoutPanel.SetColumnSpan(m_CurrentPlayerLabel, m_TableLayoutPanel.ColumnCount);

            m_TableLayoutPanel.Controls.Add(m_Player1ScoreLabel);
            m_TableLayoutPanel.SetColumnSpan(m_Player1ScoreLabel, m_TableLayoutPanel.ColumnCount);

            m_TableLayoutPanel.Controls.Add(m_Player2ScoreLabel);
            m_TableLayoutPanel.SetColumnSpan(m_Player2ScoreLabel, m_TableLayoutPanel.ColumnCount);
        }

        private void m_CardButton_Click(object sender, EventArgs e)
        {
            string[] nameSplit = (sender as CardButton).Name.Split('_');
            BoardCoordinates boardCoordinates = new BoardCoordinates(int.Parse(nameSplit[1]), int.Parse(nameSplit[2]));
            if(!(sender as CardButton).ShowingImage)
            {
                (sender as CardButton)?.ShowImageOnButton();
            }
            else
            {
                (sender as CardButton)?.HideImageOnButton();
            }
            //BoardCoordinates cardCoordinates = new BoardCoordinates(i, j);
            //Card card = m_GameToPlay.GameBoard.GetCardByCoordinates(cardCoordinates);
            //this.Text = (sender as CardButton)?.ButtonCard.Letter.ToString();
        }

        private void updateCurrentPlayerLabel()
        {
            Player currentPlayer = m_GameToPlay.WhosTurnIsIt();
            m_CurrentPlayerLabel.Text = $"Current Player: {currentPlayer.Name}";
            m_CurrentPlayerLabel.BackColor = currentPlayer == m_GameToPlay.Player1 ? Color.Aquamarine : Color.LightPink;
        }

        private void updatePlayerScoreLabel(int i_NumOfPlayer)
        {
            if(i_NumOfPlayer == 1)
            {
                this.m_Player1ScoreLabel.Text = $"{m_GameToPlay.Player1.Name}: {m_GameToPlay.Player1.Score} Pairs";
            }
            else
            {
                this.m_Player2ScoreLabel.Text = $"{m_GameToPlay.Player2.Name}: {m_GameToPlay.Player2.Score} Pairs";
            }
        }
    }
}
