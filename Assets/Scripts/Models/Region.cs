using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEvent
{
    //This class stores the values of the Regions
    public class Region
    {
        public string name { get; private set; }
        public string regionType { get; private set; }
        public Council council { get; private set; }
        public Animal[] animals { get; private set; }
        public Statistics statistics { get; private set; }
        public List<Building> buildings { get; private set; }

        public Region(string name, string regionType, Statistics statistics, Animal[] animals, Council council)
        {
            this.name = name;
            this.regionType = regionType;
            this.council = council;
            this.animals = animals;
            this.statistics = statistics;
            buildings = new List<Building>();
        }

        //adds a building to the list of buildings the region has
        public void CreateBuilding(Building newBuilding)
        {
            buildings.Add(newBuilding);
            ImplementBuildingValues(newBuilding);
        }

        private void ImplementBuildingValues(Building building)
        {
            statistics.ChangeIncome(building.statistics.income);
            statistics.changeHappiness(building.statistics.happiness);
            statistics.ChangePollutionMutation(building.statistics.pollution);
            statistics.ChangeOxygenMutation(building.statistics.oxygen);
        }

        //method to show all the values of the Region
        public void DisplayRegionValues()
        {
            string textDistance = "{0,-15}";
            ShowRegion(textDistance);
            ShowAnimals(textDistance);
            ShowStatistics(textDistance);
            ShowBuildings(textDistance);
        }

        //shows region name, type, council name and local animals (fixed info)
        private void ShowRegion(string textDistance)
        {
            Console.Write(textDistance, "Region:");
            Console.WriteLine("{0} ({1})", name, regionType);
            Console.Write(textDistance, "Council:");
            Console.WriteLine("{0} (Mood: {1})", council.name, council.currentMood);
            Console.WriteLine("===============================================================");
            Console.WriteLine();
        }

        private void ShowAnimals(string textDistance)
        {
            Console.WriteLine("Animals");
            Console.WriteLine("---------------------------------------------------------------");
            foreach (Animal animal in animals)
            {
                Console.Write(textDistance, animal.name);
                Console.WriteLine("[{0}]", animal.currentStatus);
            }
            Console.WriteLine();
        }

        //shows the current statistics of the region (changeable)
        private void ShowStatistics(string textDistance)
        {
            Console.WriteLine("Statistics");
            Console.WriteLine("---------------------------------------------------------------");

            Console.Write(textDistance, "Income: ");
            Console.WriteLine(statistics.income);

            Console.Write(textDistance, "Happiness: ");
            Console.WriteLine(statistics.happiness);

            Console.Write(textDistance, "Pollution: ");
            if (statistics.pollutionIncrease >= 0)
                Console.WriteLine("{0:0.0}% (+{1:0.0}% of pollution per year)", statistics.pollution, statistics.pollutionIncrease);
            else
                Console.WriteLine("{0:0.0}% ({1:0.0}% of pollution per year)", statistics.pollution, statistics.pollutionIncrease);

            Console.Write(textDistance, "Oxygen: ");
            if (statistics.oxygenIncrease >= 0)
                Console.WriteLine("{0:0.0}% (+{1:0.0}% of oxygen per year)", statistics.oxygen, statistics.oxygenIncrease);
            else
                Console.WriteLine("{0:0.0}% ({1:0.0}% of missing oxygen per year)", statistics.oxygen, statistics.oxygenIncrease);

            Console.WriteLine();
        }

        //shows the currently present buildings (changeable)
        private void ShowBuildings(string textDistance)
        {
            string textValue;
            Console.WriteLine("Buildings");
            Console.WriteLine("---------------------------------------------------------------");
            Console.Write(textDistance, "Name");
            Console.Write(textDistance, "Income");
            Console.Write(textDistance, "Happiness");
            Console.Write(textDistance, "Pollution");
            Console.WriteLine(textDistance, "Oxygen");

            foreach (Building building in buildings)
            {
                Console.Write(textDistance, building.name);
                Console.Write(textDistance, building.statistics.income);
                Console.Write(textDistance, building.statistics.happiness);

                textValue = (building.statistics.pollution + "% per year");
                Console.Write(textDistance, textValue);

                textValue = (building.statistics.oxygen + "% per year");
                Console.Write(textDistance, textValue);

                Console.WriteLine();
            }
        }
    }
}
