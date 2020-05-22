
using System;

namespace Match_game__logic
{
    class Class1
    {
        public static void Main()
        {
            Game test = new Game(4, 4, false, "TOMER");
            Console.WriteLine(test.PrintGameBoard());
            Console.ReadLine();
        }
    }
}
