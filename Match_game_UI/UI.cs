using System;
using System.Text;
using Match_game_logic;

namespace Match_game_UI
{
    public class UI

    {
        public static MultiplayerModes GetAndCheckMultiPlayerMode()
        {
            Console.WriteLine("Hi, welcome to the Matching game!\npress 1 if you would like to play against the Computer and 2 if you would like to play two players");
            MultiplayerModes mode = MultiplayerModes.off;
            string inputFromUser = Console.ReadLine();
            while(inputFromUser != "1" && inputFromUser != "2")
            {
                Console.WriteLine("please enter either 1 or 2");
                inputFromUser = Console.ReadLine();
            }
            if(inputFromUser == "1")
            {
                Console.WriteLine("Choose Difficaulty Level: easy , normal, hard or impossible");
                inputFromUser = Console.ReadLine();
                while(inputFromUser != "easy" && inputFromUser != "normal" && inputFromUser != "hard" && inputFromUser != "impossible")
                {
                    Console.WriteLine("Choose Difficaulty Level: easy , normal, hard or impossible, Please use EXACTLY the same words as written");
                    inputFromUser = Console.ReadLine();
                }
            }

            switch(inputFromUser)
            {
                case "easy":
                    mode = MultiplayerModes.easy;
                    break;
                case "normal":
                    mode = MultiplayerModes.normal;
                    break;
                case "hard":
                    mode = MultiplayerModes.hard;
                    break;
                case "impossible":
                    mode = MultiplayerModes.impossible;
                    break;
            }

            return mode;
        }

        public static int GetHeight()
        {
            Console.WriteLine("please enter desired Height of board either 4, or 6");
            return CheckLengthOrHeight();
        }

        public static int GetLength()
        {
            Console.WriteLine("please enter desired Length of board either 4, or 6");
            return CheckLengthOrHeight();
        }
        public static string GetNameOfPlayer(int i_PlayerNumber)
        {
            Console.WriteLine($"please enter player {i_PlayerNumber} name");
            return Console.ReadLine();
        }

        public static void ShowComputerIsPlaying()
        {
            Console.Out.WriteLine("Now it's the computer's turn to play");
            System.Threading.Thread.Sleep(3000);
        }

        public static int CheckLengthOrHeight()
        {

            string inputFromUserStr = Console.ReadLine();
            while(inputFromUserStr == null || inputFromUserStr != "4" && inputFromUserStr != "6")
            {
                Console.WriteLine("please enter either 4 or 6");
                inputFromUserStr = Console.ReadLine();
            }

            return int.Parse(inputFromUserStr);
        }

        public static BoardCoordinates GetAndCheckCoordinatesInput(Game i_CurrGame, string i_PlayerName)
        {
            Console.WriteLine($"{i_PlayerName}, please choose next coordinates");
            string inputFromUser = Console.ReadLine();
            BoardCoordinates coordinatesFromUser = new BoardCoordinates();
            while (true)
            {
                if (inputFromUser == "Q")
                {
                    Console.WriteLine("\nSee you next time!");
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(0);
                }
                if(inputFromUser != null && inputFromUser.Length == 2)
                {
                    coordinatesFromUser = BoardCoordinates.ParsePlacement(inputFromUser);
                    int row = coordinatesFromUser.Row;
                    int column = coordinatesFromUser.Column;
                    int lengthOfBoard = i_CurrGame.GameBoard.GetLengthOfBoard();
                    int heightOfBoard = i_CurrGame.GameBoard.GetHeightOfBoard();
                    if(column >= lengthOfBoard)
                    {
                        Console.WriteLine($"{(char)('A' + column)} does not fit in board paramaters");
                    }
                    else if(row >= heightOfBoard)
                    {
                        Console.WriteLine($"{row + 1} does not fit in board paramaters");
                    }
                    else if(i_CurrGame.IsCardExposed(coordinatesFromUser))
                    {
                        Console.WriteLine("The card you picked is already exposed");
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("you need to put in 2 coordinates, for example 'A1'");
                }

                inputFromUser = Console.ReadLine();
            }

            return coordinatesFromUser;
        }

        public static bool EndGameAndCheckForRematch(Player losingPlayer, Player winningPlayer, bool wasTie)
        {
            if (wasTie)
            {
                Console.WriteLine("Good Game! It was a tie this time...");
            }
            else if(!winningPlayer.IsComputer)
            {
                Console.WriteLine($"Congratulations Player {winningPlayer.Name}! You won with {winningPlayer.Score} pairs, {losingPlayer.Name} you got {losingPlayer.Score} pairs right");
            }
            else
            {
                Console.Out.WriteLine("you lose! sorry :(");
            }
            Console.WriteLine("Rematch? (Y / N)");
            string inputFromUser = Console.ReadLine();
            while (inputFromUser != "Y" && inputFromUser != "N")
            {
                Console.WriteLine("I didn't Understand, Would you like to play another game, press Y if you do and N if you don't");
                inputFromUser = Console.ReadLine();
            }

            return inputFromUser == "Y";
        }

        public static void ExitGame()
        {
            Console.WriteLine("Good Game, See you Next time, press any key to exit");
            Console.ReadLine();
        }


        public static void ShowGameBoard(GameBoard i_GameBoard, int i_MillisecondsToWait = 0)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            int heightOfBoard = i_GameBoard.GetHeightOfBoard();
            int lenghtOfBoard = i_GameBoard.GetLengthOfBoard();
            StringBuilder strToReturn = new StringBuilder(" ");
            char columnIndexChar = 'A';
            for (int i = 0; i < lenghtOfBoard; i++)
            {
                strToReturn.Append($"     {columnIndexChar}");
                columnIndexChar++;
            }

            strToReturn.Append(getSeperationRow(lenghtOfBoard));
            for (int i = 0; i < heightOfBoard; i++)
            {
                strToReturn.Append($" {i + 1} |");
                for (int j = 0; j < lenghtOfBoard; j++)
                {
                    if (i_GameBoard.GetGameBoard()[i, j].Exposed)
                    {
                        strToReturn.Append($"  {i_GameBoard.GetGameBoard()[i, j].Letter}  |");
                    }
                    else
                    {
                        strToReturn.Append($"     |");
                    }
                }

                strToReturn.Append(getSeperationRow(lenghtOfBoard));
            }

            Console.Out.WriteLine(strToReturn);
            System.Threading.Thread.Sleep(i_MillisecondsToWait);
        }

        private static string getSeperationRow(int i_columnsNumber)
        {
            StringBuilder strToReturn = new StringBuilder();
            strToReturn.Append("\n    ");
            strToReturn.Append('=', i_columnsNumber * 6);
            strToReturn.Append("\n");
            return strToReturn.ToString();
        }
    }

}

