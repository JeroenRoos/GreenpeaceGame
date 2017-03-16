using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectGreanLeader
{
    //this class stores the values of the buildings in a region
    public class Building
    {
        public string name { get; private set; }

        public BuildingStatistics statistics { get; private set; }

        public Building(string name)
        {
            this.name = name;
            Pollution pollution;
            switch (name)
            {
                case "Coal factory":
                    pollution = new Pollution(0, 0, 0, 5, 5, 5);
                    statistics = new BuildingStatistics(2000, pollution, 5);
                    break;
                default:
                    pollution = new Pollution(0, 0, 0, 0, 0, 0);
                    statistics = new BuildingStatistics(0, pollution, 0);
                    break;
            }
        }
    }
}
