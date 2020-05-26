namespace Match_game_logic
{ 
    public class Game
    {
        private readonly GameBoard r_GameBoard;
        private readonly eMultiplayerModes r_MultiPlayerMode;
        private readonly AiForComputerPlay r_ComputerAiForComputerPlay;
        private Player m_Player1;
        private Player m_Player2; 
        
        public Game(
            int i_NumberOfRows,
            int i_NumberOfColumns,
            eMultiplayerModes i_MultiPlayerMode,
            string i_NameOfPlayer1,
            string i_NameOfPlayer2 = "Computer")
        {
            this.r_GameBoard = new GameBoard(i_NumberOfRows, i_NumberOfColumns);
            this.m_Player1 = new Player(i_NameOfPlayer1, false, true);
            this.m_Player2 = new Player(i_NameOfPlayer2, i_MultiPlayerMode != eMultiplayerModes.Off, false);
            this.r_MultiPlayerMode = i_MultiPlayerMode;
            this.r_ComputerAiForComputerPlay = null;
            if (i_MultiPlayerMode != eMultiplayerModes.Off)
            {
                this.r_ComputerAiForComputerPlay = new AiForComputerPlay(i_MultiPlayerMode);
            }
        } 
        
        public Player Player1
        {
            get
            {
                return this.m_Player1;
            }

            set
            {
                this.m_Player1 = value;
            }
        }

        public Player Player2
        {
            get
            {
                return this.m_Player2;
            }

            set
            {
                this.m_Player2 = value;
            }
        } 
        
        public GameBoard GameBoard
        {
            get
            {
                return this.r_GameBoard;
            }
        }

        public eMultiplayerModes MultiplayerMode
        {
            get
            {
                return this.r_MultiPlayerMode;
            }
        }

        public AiForComputerPlay ComputerAiForComputerPlay
        {
            get
            {
                return this.r_ComputerAiForComputerPlay;
            }
        } 
        
        public Player WhosTurnIsIt()
        {
            return this.Player1.IsMyTurn ? this.Player1 : this.Player2;
        }

        public bool GuessCardAndUpdateScores(BoardCoordinates i_BoardCoordinates)
        {
            bool wasSuccsesfulGuess = this.r_GameBoard.GuessCard(i_BoardCoordinates);
            if (wasSuccsesfulGuess)
            {
                if (this.Player1.IsMyTurn)
                {
                    this.Player1.Score++;
                }
                else
                {
                    this.Player2.Score++;
                }
            }
            else
            {
                this.Player1.IsMyTurn = !this.Player1.IsMyTurn;
                this.Player2.IsMyTurn = !this.Player2.IsMyTurn;
            }

            return wasSuccsesfulGuess;
        }

        public bool IsGameOver(out Player o_WinningPlayer, out Player o_LosingPlayer)
        {
            bool isGameOver = false;
            o_WinningPlayer = null;
            o_LosingPlayer = null;
            if (this.r_GameBoard.IsBoardFullyExposed())
            {
                isGameOver = true;
                if (this.Player1.Score > this.Player2.Score)
                {
                    o_WinningPlayer = this.Player1;
                    o_LosingPlayer = this.Player2;
                }
                else if (this.Player1.Score < this.Player2.Score)
                {
                    o_WinningPlayer = this.Player2;
                    o_LosingPlayer = this.Player1;
                }
            }

            return isGameOver;
        }
    }
}
