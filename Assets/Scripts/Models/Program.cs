using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Game game = new Game();

            //game.DisplayRegion(game.regions[0]);

            Console.ReadKey();
        }
    }
}
