namespace Match_game_UI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_FirstPlayerNameTextBox = new System.Windows.Forms.TextBox();
            this.m_FirstPlayerNameLabel = new System.Windows.Forms.Label();
            this.m_SecondPlayerNameLabel = new System.Windows.Forms.Label();
            this.m_SecondPlayerNameTextBox = new System.Windows.Forms.TextBox();
            this.m_FriendOrComputerButton = new System.Windows.Forms.Button();
            this.m_SizeOfBoardButton = new System.Windows.Forms.Button();
            this.m_StartGameButton = new System.Windows.Forms.Button();
            this.m_BoardSizeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_FirstPlayerNameTextBox
            // 
            this.m_FirstPlayerNameTextBox.Location = new System.Drawing.Point(157, 17);
            this.m_FirstPlayerNameTextBox.Name = "m_FirstPlayerNameTextBox";
            this.m_FirstPlayerNameTextBox.Size = new System.Drawing.Size(170, 22);
            this.m_FirstPlayerNameTextBox.TabIndex = 0;
            this.m_FirstPlayerNameTextBox.TextChanged += new System.EventHandler(this.m_FirstPlayerNameTextBox_TextChanged);
            // 
            // m_FirstPlayerNameLabel
            // 
            this.m_FirstPlayerNameLabel.AutoSize = true;
            this.m_FirstPlayerNameLabel.Location = new System.Drawing.Point(12, 23);
            this.m_FirstPlayerNameLabel.Name = "m_FirstPlayerNameLabel";
            this.m_FirstPlayerNameLabel.Size = new System.Drawing.Size(118, 16);
            this.m_FirstPlayerNameLabel.TabIndex = 1;
            this.m_FirstPlayerNameLabel.Text = "First Player Name:";
            
            // 
            // m_SecondPlayerNameLabel
            // 
            this.m_SecondPlayerNameLabel.AutoSize = true;
            this.m_SecondPlayerNameLabel.Location = new System.Drawing.Point(12, 88);
            this.m_SecondPlayerNameLabel.Name = "m_SecondPlayerNameLabel";
            this.m_SecondPlayerNameLabel.Size = new System.Drawing.Size(140, 16);
            this.m_SecondPlayerNameLabel.TabIndex = 2;
            this.m_SecondPlayerNameLabel.Text = "Second Player Name:";
            // 
            // m_SecondPlayerNameTextBox
            // 
            this.m_SecondPlayerNameTextBox.Enabled = false;
            this.m_SecondPlayerNameTextBox.Location = new System.Drawing.Point(157, 82);
            this.m_SecondPlayerNameTextBox.Name = "m_SecondPlayerNameTextBox";
            this.m_SecondPlayerNameTextBox.Size = new System.Drawing.Size(170, 22);
            this.m_SecondPlayerNameTextBox.TabIndex = 3;
            this.m_SecondPlayerNameTextBox.Text = "-Computer-";
            this.m_SecondPlayerNameTextBox.TextChanged += new System.EventHandler(this.m_SecondPlayerNameTextBox_TextChanged);
            // 
            // m_FriendOrComputerButton
            // 
            this.m_FriendOrComputerButton.Location = new System.Drawing.Point(333, 74);
            this.m_FriendOrComputerButton.Name = "m_FriendOrComputerButton";
            this.m_FriendOrComputerButton.Size = new System.Drawing.Size(159, 30);
            this.m_FriendOrComputerButton.TabIndex = 4;
            this.m_FriendOrComputerButton.Text = "Against a Friend";
            this.m_FriendOrComputerButton.UseVisualStyleBackColor = true;
            this.m_FriendOrComputerButton.Click += new System.EventHandler(this.friendOrComputerButton_Click);
            // 
            // m_SizeOfBoardButton
            // 
            this.m_SizeOfBoardButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_SizeOfBoardButton.Location = new System.Drawing.Point(33, 191);
            this.m_SizeOfBoardButton.Name = "m_SizeOfBoardButton";
            this.m_SizeOfBoardButton.Size = new System.Drawing.Size(105, 92);
            this.m_SizeOfBoardButton.TabIndex = 5;
            this.m_SizeOfBoardButton.Text = "4x4";
            this.m_SizeOfBoardButton.UseVisualStyleBackColor = false;
            this.m_SizeOfBoardButton.Click += new System.EventHandler(this.m_SizeOfBoardButton_Click);
            // 
            // m_StartGameButton
            // 
            this.m_StartGameButton.BackColor = System.Drawing.Color.LawnGreen;
            this.m_StartGameButton.Location = new System.Drawing.Point(353, 260);
            this.m_StartGameButton.Name = "m_StartGameButton";
            this.m_StartGameButton.Size = new System.Drawing.Size(117, 23);
            this.m_StartGameButton.TabIndex = 6;
            this.m_StartGameButton.Text = "Start!";
            this.m_StartGameButton.UseVisualStyleBackColor = false;
            this.m_StartGameButton.Click += new System.EventHandler(this.m_StartGameButton_Click);
            // 
            // m_BoardSizeLabel
            // 
            this.m_BoardSizeLabel.AutoSize = true;
            this.m_BoardSizeLabel.Location = new System.Drawing.Point(44, 159);
            this.m_BoardSizeLabel.Name = "m_BoardSizeLabel";
            this.m_BoardSizeLabel.Size = new System.Drawing.Size(77, 16);
            this.m_BoardSizeLabel.TabIndex = 7;
            this.m_BoardSizeLabel.Text = "Board Size:";
            
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(538, 317);
            this.Controls.Add(this.m_BoardSizeLabel);
            this.Controls.Add(this.m_StartGameButton);
            this.Controls.Add(this.m_SizeOfBoardButton);
            this.Controls.Add(this.m_FriendOrComputerButton);
            this.Controls.Add(this.m_SecondPlayerNameTextBox);
            this.Controls.Add(this.m_SecondPlayerNameLabel);
            this.Controls.Add(this.m_FirstPlayerNameLabel);
            this.Controls.Add(this.m_FirstPlayerNameTextBox);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_FirstPlayerNameTextBox;
        private System.Windows.Forms.Label m_FirstPlayerNameLabel;
        private System.Windows.Forms.Label m_SecondPlayerNameLabel;
        private System.Windows.Forms.TextBox m_SecondPlayerNameTextBox;
        private System.Windows.Forms.Button m_FriendOrComputerButton;
        private System.Windows.Forms.Button m_SizeOfBoardButton;
        private System.Windows.Forms.Button m_StartGameButton;
        private System.Windows.Forms.Label m_BoardSizeLabel;
    }
}