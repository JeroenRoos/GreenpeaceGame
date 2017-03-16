using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectGreanLeader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                Game game = new Game();
                Console.ReadKey();
            }

            Console.ReadKey();
        }
    }
}
