using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEvent
{
    //this class stores the values of the buildings in a region
    public class Building
    {
        public string name { get; private set; }

        public Statistics statistics { get; private set; }

        public Building(string name)
        {
            this.name = name;
            switch (name)
            {
                case "Coal factory":
                        statistics = new Statistics(2000, 2, 5, -2);
                        break;
                default:
                        statistics = new Statistics(0, 0, 0, 0);
                        break;
            }

        }
    }
}
