using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

class Program
{
    static Game game;
    static Timer timeflowTimer;

    static void Main(string[] args)
    {
        //Console.ForegroundColor = ConsoleColor.White;

        game = new Game();
        StartTimeflowTimer();

        //Console.ReadKey();
    }
    static void StartTimeflowTimer()
    {
        timeflowTimer = new Timer();
        timeflowTimer.Elapsed += new ElapsedEventHandler(UpdateGameTime);
        timeflowTimer.Interval = 10;
        timeflowTimer.Enabled = true;
    }

    static void UpdateGameTime(object source, ElapsedEventArgs e)
    {
        if (game.currentYear > 30)
        {
            timeflowTimer.Stop();
            //Console.Clear();
            //game.DisplayRegion(game.regions[0]);
        }

        else
        {
            bool newEvent = game.UpdateTime();

            if (newEvent)
            {
                timeflowTimer.Stop();
                game.StartNewEvent();
                timeflowTimer.Start();
            }
        }
            
    }
}
