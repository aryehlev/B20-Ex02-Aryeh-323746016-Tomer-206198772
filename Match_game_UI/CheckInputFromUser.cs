using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_game_UI
{
    internal class CheckInputFromUser
    {
        internal static bool checkMultiPlayer()
        {
            ConsoleKeyInfo checkKeyInfo = Console.ReadKey();
            string inputFromUser = checkKeyInfo.Key.ToString();
            while (inputFromUser != "1" && inputFromUser != "2")
            {
                Console.WriteLine("please enter either 1 or 2");
                inputFromUser = checkKeyInfo.Key.ToString();
            }

            bool isMultiPlayer = inputFromUser == "2" ? true : false;
            
            return isMultiPlayer;
        }

        internal static int checkLengthOrHeight()
        {
            ConsoleKeyInfo checkKeyInfo = Console.ReadKey();
            int inputFromUser = int.Parse(checkKeyInfo.Key.ToString());
            while (inputFromUser != 4 && inputFromUser != 6)
            {
                Console.WriteLine("please enter either 4 or 6");
                inputFromUser = int.Parse(checkKeyInfo.Key.ToString());
            }

            return inputFromUser;
        }
    }
}
