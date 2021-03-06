﻿using System;
using System.Text;
using Match_game_logic;

namespace Match_game_UI
{
    public class mofo
    {
        public static eAiModes GetAndCheckMultiPlayerMode()
        {
            Console.WriteLine("Hi, welcome to the Matching game!\npress 1 if you would like to play against the Computer and 2 if you would like to play two players");
            eAiModes mode = eAiModes.Off;
            string inputFromUser = Console.ReadLine();

            while (inputFromUser != "1" && inputFromUser != "2")
            {
                Console.WriteLine("please enter either 1 or 2");
                inputFromUser = Console.ReadLine();
            }

            if (inputFromUser == "1")
            {
                Console.WriteLine("Choose Difficulty Level: Computer Chooses Randomly - r, Easy - e, normal - n ,Genius - g");
                inputFromUser = Console.ReadLine();
                while (inputFromUser != "e" && inputFromUser != "n" && inputFromUser != "r" && inputFromUser != "g")
                {
                    Console.WriteLine("Choose Difficulty Level: r, e , n or g, Please use EXACTLY the same letters as written");
                    inputFromUser = Console.ReadLine();
                }
            }

            switch (inputFromUser)
            {
                case "r":
                    mode = eAiModes.Random;
                    break;
                case "e":
                    mode = eAiModes.Easy;
                    break;
                case "n":
                    mode = eAiModes.Easy;
                    break;
                case "g":
                    mode = eAiModes.Genius;
                    break;
            }

            return mode;
        }

        public static int[] GetHeightAndLength()
        {
            Console.WriteLine("please enter desired Height of board  4, 5 or 6");
            string inputFromUserStr = Console.ReadLine();
            int[] lengthAndHeight = new int[2];

            while ((inputFromUserStr == null) || (inputFromUserStr != "4" && inputFromUserStr != "5" && inputFromUserStr != "6"))
            {
                Console.WriteLine("please enter either 4, 5 or 6");
                inputFromUserStr = Console.ReadLine();
            }

            lengthAndHeight[0] = int.Parse(inputFromUserStr);

            if (lengthAndHeight[0] == 5)
            {
                Console.WriteLine("please enter desired Length of board either 4, or 6");
                inputFromUserStr = Console.ReadLine();

                while (inputFromUserStr == null || (inputFromUserStr != "4" && inputFromUserStr != "6"))
                {
                    Console.WriteLine("please enter either 4 or 6");
                    inputFromUserStr = Console.ReadLine();
                }

                lengthAndHeight[1] = int.Parse(inputFromUserStr);
            }
            else
            {
                Console.WriteLine("please enter desired Length of board either 4, 5 or 6");
                inputFromUserStr = Console.ReadLine();

                while ((inputFromUserStr == null) || (inputFromUserStr != "4" && inputFromUserStr != "6" && inputFromUserStr != "5"))
                {
                    Console.WriteLine("please enter either 4, 5 or 6");
                    inputFromUserStr = Console.ReadLine();
                }

                lengthAndHeight[1] = int.Parse(inputFromUserStr);
            }

            return lengthAndHeight;
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

        public static BoardCoordinates GetAndCheckCoordinatesInput(Game i_CurrGame, Player i_CurrentPlayer)
        {
            Console.WriteLine($"{i_CurrentPlayer.Name}, you currently have {i_CurrentPlayer.Score} pairs. please choose next coordinates");
            BoardCoordinates coordinatesFromUser = new BoardCoordinates();
            bool isInputValid = false;

            while (!isInputValid)
            {
                string inputFromUser = Console.ReadLine();
                if (inputFromUser == "Q")
                {
                    Console.WriteLine("\nSee you next time!");
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(0);
                }

                if (inputFromUser == null || inputFromUser.Length != 2)
                {
                    Console.WriteLine("you need to put in 2 coordinates, for example 'A1'");
                    continue;
                }

                coordinatesFromUser = BoardCoordinates.TryParsePlacement(inputFromUser, out bool wasSuccess);
                if (!wasSuccess)
                {
                    Console.WriteLine("you need to put in a capital letter and a number bigger than 0, for example 'A1'");
                    continue;
                }

                GameBoard gameBoard = i_CurrGame.GameBoard;
                int row = coordinatesFromUser.Row;
                int column = coordinatesFromUser.Column;
                int lengthOfBoard = gameBoard.GetLengthOfBoard();
                int heightOfBoard = gameBoard.GetHeightOfBoard();
                if (column >= lengthOfBoard)
                {
                    Console.WriteLine($"{(char)('A' + column)} does not fit in board parameters");
                }
                else if (row >= heightOfBoard)
                {
                    Console.WriteLine($"{row + 1} does not fit in board parameters");
                }
                else if (gameBoard.IsCardExposed(coordinatesFromUser))
                {
                    Console.WriteLine("The card you picked is already exposed");
                }
                else
                {
                    isInputValid = true;
                }
            }

            return coordinatesFromUser;
        }

        public static bool EndGameAndCheckForRematch(Player i_LosingPlayer, Player i_WinningPlayer, bool i_WasTie)
        {
            if (i_WasTie)
            {
                Console.WriteLine("Good Game! It was a tie this time...");
            }
            else if (!i_WinningPlayer.IsComputer)
            {
                Console.WriteLine($"Congratulations Player {i_WinningPlayer.Name}! You won with {i_WinningPlayer.Score} pairs, {i_LosingPlayer.Name} you got {i_LosingPlayer.Score} pairs right");
            }
            else
            {
                Console.Out.WriteLine($"you lose! {i_WinningPlayer.Score}-{i_LosingPlayer.Score} sorry :(");
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
            Console.WriteLine("Good Game, See you Next time, press enter to exit...");
            Console.ReadLine();
        }

        public static void ShowGameBoard(GameBoard i_GameBoard, int i_MillisecondsToWait = 0)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            int heightOfBoard = i_GameBoard.GetHeightOfBoard();
            int lengthOfBoard = i_GameBoard.GetLengthOfBoard();
            StringBuilder strToReturn = new StringBuilder(" ");
            char columnIndexChar = 'A';

            for (int i = 0; i < lengthOfBoard; i++)
            {
                strToReturn.Append($"     {columnIndexChar}");
                columnIndexChar++;
            }

            strToReturn.Append(getSeparationRow(lengthOfBoard));
            for (int i = 0; i < heightOfBoard; i++)
            {
                strToReturn.Append($" {i + 1} |");
                for (int j = 0; j < lengthOfBoard; j++)
                {
                    Card currentCard = i_GameBoard.GetCardByCoordinates(new BoardCoordinates(i, j));
                    strToReturn.Append(currentCard.Exposed ? $"  {currentCard.Letter}  |" : "     |");
                }

                strToReturn.Append(getSeparationRow(lengthOfBoard));
            }

            Console.Out.WriteLine(strToReturn);
            System.Threading.Thread.Sleep(i_MillisecondsToWait);
        }

        private static string getSeparationRow(int i_ColumnsNumber)
        {
            StringBuilder strToReturn = new StringBuilder();
            strToReturn.Append("\n    ");
            strToReturn.Append('=', i_ColumnsNumber * 6);
            strToReturn.Append("\n");
            return strToReturn.ToString();
        }
    }
}